using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace cs2_things
{
    internal sealed class SourceServerInfo
    {
        public string Name { get; set; }
        public string Map { get; set; }
        public int PlayerCount { get; set; }
        public int MaxPlayers { get; set; }
    }

    internal static class SourceServerQuery
    {
        private const byte InfoRequestType = 0x54;
        private const byte InfoResponseType = 0x49;
        private const byte PlayerRequestType = 0x55;
        private const byte PlayerResponseType = 0x44;
        private const byte ChallengeResponseType = 0x41;

        public static SourceServerInfo QueryInfo(string host, int port, int timeoutMilliseconds)
        {
            byte[] queryText = Encoding.ASCII.GetBytes("Source Engine Query\0");
            byte[] request = BuildRequest(InfoRequestType, queryText, null);

            using (var client = CreateClient(host, port, timeoutMilliseconds))
            {
                byte[] response = SendWithChallenge(client, request, InfoRequestType);
                int offset = 0;
                RequireResponseType(response, ref offset, InfoResponseType, "server information");
                ReadByte(response, ref offset); // Protocol version.

                return new SourceServerInfo
                {
                    Name = ReadNullTerminatedString(response, ref offset),
                    Map = ReadNullTerminatedString(response, ref offset),
                    PlayerCount = ReadInfoPlayerCounts(response, ref offset, true),
                    MaxPlayers = ReadInfoPlayerCounts(response, ref offset, false)
                };
            }
        }

        public static IList<string> QueryPlayers(string host, int port, int timeoutMilliseconds)
        {
            using (var client = CreateClient(host, port, timeoutMilliseconds))
            {
                byte[] initialRequest = BuildRequest(PlayerRequestType, null, -1);
                Send(client, initialRequest);
                byte[] response = ReceivePayload(client);
                int offset = 0;
                RequireResponseType(response, ref offset, ChallengeResponseType, "player challenge");
                int challenge = ReadInt32(response, ref offset);

                byte[] request = BuildRequest(PlayerRequestType, null, challenge);
                Send(client, request);
                response = ReceivePayload(client);
                offset = 0;
                RequireResponseType(response, ref offset, PlayerResponseType, "player list");

                int count = ReadByte(response, ref offset);
                var players = new List<string>(count);
                for (int index = 0; index < count; index++)
                {
                    ReadByte(response, ref offset); // Player index.
                    players.Add(ReadNullTerminatedString(response, ref offset));
                    ReadInt32(response, ref offset); // Score.
                    ReadInt32(response, ref offset); // Connection duration as IEEE 754 bytes.
                }
                return players;
            }
        }

        private static UdpClient CreateClient(string host, int port, int timeoutMilliseconds)
        {
            var client = new UdpClient();
            client.Client.SendTimeout = timeoutMilliseconds;
            client.Client.ReceiveTimeout = timeoutMilliseconds;
            client.Connect(host, port);
            return client;
        }

        private static byte[] SendWithChallenge(UdpClient client, byte[] request, byte requestType)
        {
            Send(client, request);
            byte[] response = ReceivePayload(client);
            if (response.Length == 0 || response[0] != ChallengeResponseType)
            {
                return response;
            }

            int offset = 1;
            int challenge = ReadInt32(response, ref offset);
            byte[] challengedRequest = BuildRequest(
                requestType,
                request.Skip(5).ToArray(),
                challenge);
            Send(client, challengedRequest);
            return ReceivePayload(client);
        }

        private static byte[] BuildRequest(byte requestType, byte[] content, int? challenge)
        {
            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(-1);
                writer.Write(requestType);
                if (content != null)
                {
                    writer.Write(content);
                }
                if (challenge.HasValue)
                {
                    writer.Write(challenge.Value);
                }
                return stream.ToArray();
            }
        }

        private static void Send(UdpClient client, byte[] request)
        {
            client.Send(request, request.Length);
        }

        private static byte[] ReceivePayload(UdpClient client)
        {
            System.Net.IPEndPoint remoteEndpoint = null;
            byte[] first = client.Receive(ref remoteEndpoint);
            int header = ReadPacketHeader(first);
            if (header == -1)
            {
                return Slice(first, 4);
            }
            if (header != -2)
            {
                throw new InvalidDataException("The server returned an invalid A2S packet header.");
            }

            int requestId = BitConverter.ToInt32(first, 4);
            if ((requestId & unchecked((int)0x80000000)) != 0)
            {
                throw new InvalidDataException("The server returned a compressed split A2S packet.");
            }

            int total = first[8];
            var fragments = new byte[total][];
            StoreFragment(first, requestId, fragments);
            while (fragments.Any(fragment => fragment == null))
            {
                byte[] packet = client.Receive(ref remoteEndpoint);
                StoreFragment(packet, requestId, fragments);
            }

            byte[] combined = Combine(fragments);
            return combined.Length >= 4 && BitConverter.ToInt32(combined, 0) == -1
                ? Slice(combined, 4)
                : combined;
        }

        private static void StoreFragment(byte[] packet, int requestId, byte[][] fragments)
        {
            if (packet.Length < 12 || ReadPacketHeader(packet) != -2 ||
                BitConverter.ToInt32(packet, 4) != requestId)
            {
                throw new InvalidDataException("The server returned an invalid split A2S packet.");
            }

            int total = packet[8];
            int number = packet[9];
            if (total != fragments.Length || number < 0 || number >= fragments.Length)
            {
                throw new InvalidDataException("The server returned inconsistent A2S fragments.");
            }
            fragments[number] = Slice(packet, 12);
        }

        private static byte[] Combine(IEnumerable<byte[]> fragments)
        {
            using (var stream = new MemoryStream())
            {
                foreach (byte[] fragment in fragments)
                {
                    stream.Write(fragment, 0, fragment.Length);
                }
                return stream.ToArray();
            }
        }

        private static int ReadPacketHeader(byte[] packet)
        {
            if (packet == null || packet.Length < 4)
            {
                throw new InvalidDataException("The server returned a truncated A2S packet.");
            }
            return BitConverter.ToInt32(packet, 0);
        }

        private static void RequireResponseType(
            byte[] data,
            ref int offset,
            byte expected,
            string responseName)
        {
            byte actual = ReadByte(data, ref offset);
            if (actual != expected)
            {
                throw new InvalidDataException(
                    "The server returned an unexpected A2S " + responseName + " response.");
            }
        }

        private static int ReadInfoPlayerCounts(byte[] data, ref int offset, bool players)
        {
            if (players)
            {
                ReadNullTerminatedString(data, ref offset); // Game directory.
                ReadNullTerminatedString(data, ref offset); // Game description.
                ReadUInt16(data, ref offset); // App ID.
            }
            return ReadByte(data, ref offset);
        }

        private static string ReadNullTerminatedString(byte[] data, ref int offset)
        {
            int start = offset;
            while (offset < data.Length && data[offset] != 0)
            {
                offset++;
            }
            if (offset >= data.Length)
            {
                throw new InvalidDataException("The server returned a truncated A2S string.");
            }
            string value = Encoding.UTF8.GetString(data, start, offset - start);
            offset++;
            return value;
        }

        private static byte ReadByte(byte[] data, ref int offset)
        {
            EnsureAvailable(data, offset, 1);
            return data[offset++];
        }

        private static ushort ReadUInt16(byte[] data, ref int offset)
        {
            EnsureAvailable(data, offset, 2);
            ushort value = BitConverter.ToUInt16(data, offset);
            offset += 2;
            return value;
        }

        private static int ReadInt32(byte[] data, ref int offset)
        {
            EnsureAvailable(data, offset, 4);
            int value = BitConverter.ToInt32(data, offset);
            offset += 4;
            return value;
        }

        private static void EnsureAvailable(byte[] data, int offset, int count)
        {
            if (data == null || offset < 0 || count < 0 || offset + count > data.Length)
            {
                throw new InvalidDataException("The server returned a truncated A2S response.");
            }
        }

        private static byte[] Slice(byte[] source, int offset)
        {
            var result = new byte[source.Length - offset];
            Buffer.BlockCopy(source, offset, result, 0, result.Length);
            return result;
        }

    }
}
