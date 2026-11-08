using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace cs2_things
{
    internal static class Cs2Autojoiner
    {
        private const int QueryTimeoutMilliseconds = 3000;
        private const int ConnectAttemptMilliseconds = 10000;

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool NativeBeep(uint frequency, uint duration);

        public static async Task RunAsync(
            string serverHost,
            int serverPort,
            string playerName,
            int playerThreshold,
            int intervalMilliseconds,
            bool playSound,
            Action<SourceServerInfo> serverInfoReceived,
            Action<string> log,
            CancellationToken cancellationToken)
        {
            bool connectAttempted = false;
            DateTime playerCheckUntil = DateTime.MinValue;

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                try
                {
                    SourceServerInfo info = await Task.Run(
                        () => SourceServerQuery.QueryInfo(
                            serverHost,
                            serverPort,
                            QueryTimeoutMilliseconds),
                        cancellationToken);

                    if (serverInfoReceived != null)
                    {
                        serverInfoReceived(info);
                    }
                    Report(
                        log,
                        "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + info.Map +
                        " - player count: " + info.PlayerCount + "/" + info.MaxPlayers);

                    if (connectAttempted)
                    {
                        Report(log, "Checking for '" + playerName + "'.");
                        try
                        {
                            IList<string> players = await Task.Run(
                                () => SourceServerQuery.QueryPlayers(
                                    serverHost,
                                    serverPort,
                                    QueryTimeoutMilliseconds),
                                cancellationToken);
                            if (players.Any(name => string.Equals(
                                name,
                                playerName,
                                StringComparison.OrdinalIgnoreCase)))
                            {
                                Report(
                                    log,
                                    "Detected player '" + playerName +
                                    "' in server; stopping autojoiner.");
                                if (playSound)
                                {
                                    await Task.Run(() => PlayBeeps(
                                        new[] { 600, 800, 1000 },
                                        new[] { 200, 200, 300 }));
                                }
                                return;
                            }
                        }
                        catch (OperationCanceledException)
                        {
                            throw;
                        }
                        catch (Exception error)
                        {
                            Report(log, "Player query failed: " + error.Message);
                        }
                    }

                    if (connectAttempted && DateTime.UtcNow >= playerCheckUntil)
                    {
                        connectAttempted = false;
                    }

                    if (info.PlayerCount < playerThreshold && !connectAttempted)
                    {
                        Report(
                            log,
                            "Player count is below " + playerThreshold +
                            "; attempting to connect.");
                        LaunchCS2.ConnectToServer(serverHost, serverPort);
                        if (playSound)
                        {
                            await Task.Run(() => PlayBeeps(
                                new[] { 600, 600 },
                                new[] { 200, 200 }));
                        }
                        connectAttempted = true;
                        playerCheckUntil = DateTime.UtcNow.AddMilliseconds(
                            ConnectAttemptMilliseconds);
                    }
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception error)
                {
                    Report(log, "Server query failed: " + error.Message);
                }

                await Task.Delay(intervalMilliseconds, cancellationToken);
            }
        }

        private static void PlayBeeps(int[] frequencies, int[] durations)
        {
            try
            {
                for (int index = 0; index < frequencies.Length && index < durations.Length; index++)
                {
                    NativeBeep((uint)frequencies[index], (uint)durations[index]);
                }
            }
            catch (Exception)
            {
            }
        }

        private static void Report(Action<string> log, string message)
        {
            if (log != null)
            {
                log(message);
            }
        }
    }
}
