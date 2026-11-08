namespace cs2_things
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.checkbox_tools = new System.Windows.Forms.CheckBox();
            this.checkbox_cs2fixes = new System.Windows.Forms.CheckBox();
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tabpage_cs2fixes = new System.Windows.Forms.TabPage();
            this.button_uninstall = new System.Windows.Forms.Button();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.linkLabel6 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.button_install = new System.Windows.Forms.Button();
            this.button_cs2_root_autodetect = new System.Windows.Forms.Button();
            this.button_gfl_ze_configs = new System.Windows.Forms.Button();
            this.textbox_gfl_ze_configs = new System.Windows.Forms.TextBox();
            this.button_strippercs2 = new System.Windows.Forms.Button();
            this.textbox_strippercs2 = new System.Windows.Forms.TextBox();
            this.button_cs2fixes = new System.Windows.Forms.Button();
            this.textbox_cs2fixes = new System.Windows.Forms.TextBox();
            this.button_metamod_launcher = new System.Windows.Forms.Button();
            this.textbox_metamod_launcher = new System.Windows.Forms.TextBox();
            this.button_metamod = new System.Windows.Forms.Button();
            this.textbox_metamod = new System.Windows.Forms.TextBox();
            this.button_cs2fixescfg = new System.Windows.Forms.Button();
            this.textbox_cs2fixescfg = new System.Windows.Forms.TextBox();
            this.textbox_steamid64 = new System.Windows.Forms.TextBox();
            this.button_cs2_root = new System.Windows.Forms.Button();
            this.label_cs2_root = new System.Windows.Forms.Label();
            this.textbox_cs2_root = new System.Windows.Forms.TextBox();
            this.tabpage_autojoiner = new System.Windows.Forms.TabPage();
            this.group_console = new System.Windows.Forms.GroupBox();
            this.rich_console = new System.Windows.Forms.RichTextBox();
            this.button_launch_cs2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textbox_autojoiner_ip = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textbox_autojoiner_port = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textbox_autojoiner_username = new System.Windows.Forms.TextBox();
            this.button_autojoiner_start = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.tab_main.SuspendLayout();
            this.tabpage_cs2fixes.SuspendLayout();
            this.tabpage_autojoiner.SuspendLayout();
            this.group_console.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // checkbox_tools
            // 
            this.checkbox_tools.AutoSize = true;
            this.checkbox_tools.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkbox_tools.Location = new System.Drawing.Point(12, 12);
            this.checkbox_tools.Name = "checkbox_tools";
            this.checkbox_tools.Size = new System.Drawing.Size(91, 19);
            this.checkbox_tools.TabIndex = 0;
            this.checkbox_tools.Text = "launch tools";
            this.checkbox_tools.UseVisualStyleBackColor = true;
            // 
            // checkbox_cs2fixes
            // 
            this.checkbox_cs2fixes.AutoSize = true;
            this.checkbox_cs2fixes.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkbox_cs2fixes.Location = new System.Drawing.Point(109, 12);
            this.checkbox_cs2fixes.Name = "checkbox_cs2fixes";
            this.checkbox_cs2fixes.Size = new System.Drawing.Size(131, 19);
            this.checkbox_cs2fixes.TabIndex = 1;
            this.checkbox_cs2fixes.Text = "launch with cs2fixes";
            this.checkbox_cs2fixes.UseVisualStyleBackColor = true;
            // 
            // tab_main
            // 
            this.tab_main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tab_main.Controls.Add(this.tabpage_cs2fixes);
            this.tab_main.Controls.Add(this.tabpage_autojoiner);
            this.tab_main.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tab_main.Location = new System.Drawing.Point(12, 38);
            this.tab_main.MaximumSize = new System.Drawing.Size(9999, 9999);
            this.tab_main.MinimumSize = new System.Drawing.Size(480, 273);
            this.tab_main.Name = "tab_main";
            this.tab_main.SelectedIndex = 0;
            this.tab_main.Size = new System.Drawing.Size(480, 273);
            this.tab_main.TabIndex = 3;
            // 
            // tabpage_cs2fixes
            // 
            this.tabpage_cs2fixes.Controls.Add(this.button_uninstall);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel5);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel6);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel3);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel4);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel2);
            this.tabpage_cs2fixes.Controls.Add(this.linkLabel1);
            this.tabpage_cs2fixes.Controls.Add(this.label1);
            this.tabpage_cs2fixes.Controls.Add(this.button_install);
            this.tabpage_cs2fixes.Controls.Add(this.button_cs2_root_autodetect);
            this.tabpage_cs2fixes.Controls.Add(this.button_gfl_ze_configs);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_gfl_ze_configs);
            this.tabpage_cs2fixes.Controls.Add(this.button_strippercs2);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_strippercs2);
            this.tabpage_cs2fixes.Controls.Add(this.button_cs2fixes);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_cs2fixes);
            this.tabpage_cs2fixes.Controls.Add(this.button_metamod_launcher);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_metamod_launcher);
            this.tabpage_cs2fixes.Controls.Add(this.button_metamod);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_metamod);
            this.tabpage_cs2fixes.Controls.Add(this.button_cs2fixescfg);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_cs2fixescfg);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_steamid64);
            this.tabpage_cs2fixes.Controls.Add(this.button_cs2_root);
            this.tabpage_cs2fixes.Controls.Add(this.label_cs2_root);
            this.tabpage_cs2fixes.Controls.Add(this.textbox_cs2_root);
            this.tabpage_cs2fixes.Location = new System.Drawing.Point(4, 24);
            this.tabpage_cs2fixes.Name = "tabpage_cs2fixes";
            this.tabpage_cs2fixes.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_cs2fixes.Size = new System.Drawing.Size(472, 245);
            this.tabpage_cs2fixes.TabIndex = 0;
            this.tabpage_cs2fixes.Text = "cs2fixes";
            this.tabpage_cs2fixes.UseVisualStyleBackColor = true;
            // 
            // button_uninstall
            // 
            this.button_uninstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_uninstall.Location = new System.Drawing.Point(391, 214);
            this.button_uninstall.Name = "button_uninstall";
            this.button_uninstall.Size = new System.Drawing.Size(75, 25);
            this.button_uninstall.TabIndex = 34;
            this.button_uninstall.Text = "uninstall";
            this.button_uninstall.UseVisualStyleBackColor = true;
            // 
            // linkLabel5
            // 
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(6, 188);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(97, 15);
            this.linkLabel5.TabIndex = 33;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Tag = "https://github.com/gflze/CS2-ZE-Configs/";
            this.linkLabel5.Text = "gfl cs2 ze configs";
            this.linkLabel5.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // linkLabel6
            // 
            this.linkLabel6.AutoSize = true;
            this.linkLabel6.Location = new System.Drawing.Point(6, 162);
            this.linkLabel6.Name = "linkLabel6";
            this.linkLabel6.Size = new System.Drawing.Size(64, 15);
            this.linkLabel6.TabIndex = 32;
            this.linkLabel6.TabStop = true;
            this.linkLabel6.Tag = "https://github.com/Source2ZE/StripperCS2/releases";
            this.linkLabel6.Text = "strippercs2";
            this.linkLabel6.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(6, 136);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(47, 15);
            this.linkLabel3.TabIndex = 31;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "https://github.com/Source2ZE/CS2Fixes/actions";
            this.linkLabel3.Text = "cs2fixes";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(6, 110);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(108, 15);
            this.linkLabel4.TabIndex = 30;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Tag = "https://github.com/Poggicek/metamod-launcher/releases";
            this.linkLabel4.Text = "metamod launcher";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(6, 85);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(59, 15);
            this.linkLabel2.TabIndex = 29;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "https://www.metamodsource.net/downloads.php?branch=dev";
            this.linkLabel2.Text = "metamod";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(6, 59);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(67, 15);
            this.linkLabel1.TabIndex = 28;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "https://pastebin.com/NUNWgZAY";
            this.linkLabel1.Text = "cs2fixes.cfg";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.downloadLink_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "admin steamid64";
            // 
            // button_install
            // 
            this.button_install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_install.Location = new System.Drawing.Point(311, 214);
            this.button_install.Name = "button_install";
            this.button_install.Size = new System.Drawing.Size(75, 25);
            this.button_install.TabIndex = 27;
            this.button_install.Text = "install";
            this.button_install.UseVisualStyleBackColor = true;
            // 
            // button_cs2_root_autodetect
            // 
            this.button_cs2_root_autodetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2_root_autodetect.Location = new System.Drawing.Point(392, 7);
            this.button_cs2_root_autodetect.Name = "button_cs2_root_autodetect";
            this.button_cs2_root_autodetect.Size = new System.Drawing.Size(41, 23);
            this.button_cs2_root_autodetect.TabIndex = 19;
            this.button_cs2_root_autodetect.Text = "auto";
            this.button_cs2_root_autodetect.UseVisualStyleBackColor = true;
            // 
            // button_gfl_ze_configs
            // 
            this.button_gfl_ze_configs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_gfl_ze_configs.Location = new System.Drawing.Point(439, 187);
            this.button_gfl_ze_configs.Name = "button_gfl_ze_configs";
            this.button_gfl_ze_configs.Size = new System.Drawing.Size(27, 23);
            this.button_gfl_ze_configs.TabIndex = 18;
            this.button_gfl_ze_configs.Text = "...";
            this.button_gfl_ze_configs.UseVisualStyleBackColor = true;
            // 
            // textbox_gfl_ze_configs
            // 
            this.textbox_gfl_ze_configs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_gfl_ze_configs.Location = new System.Drawing.Point(127, 188);
            this.textbox_gfl_ze_configs.Multiline = true;
            this.textbox_gfl_ze_configs.Name = "textbox_gfl_ze_configs";
            this.textbox_gfl_ze_configs.Size = new System.Drawing.Size(306, 20);
            this.textbox_gfl_ze_configs.TabIndex = 17;
            // 
            // button_strippercs2
            // 
            this.button_strippercs2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_strippercs2.Location = new System.Drawing.Point(439, 161);
            this.button_strippercs2.Name = "button_strippercs2";
            this.button_strippercs2.Size = new System.Drawing.Size(27, 23);
            this.button_strippercs2.TabIndex = 16;
            this.button_strippercs2.Text = "...";
            this.button_strippercs2.UseVisualStyleBackColor = true;
            // 
            // textbox_strippercs2
            // 
            this.textbox_strippercs2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_strippercs2.Location = new System.Drawing.Point(127, 162);
            this.textbox_strippercs2.Multiline = true;
            this.textbox_strippercs2.Name = "textbox_strippercs2";
            this.textbox_strippercs2.Size = new System.Drawing.Size(306, 20);
            this.textbox_strippercs2.TabIndex = 15;
            // 
            // button_cs2fixes
            // 
            this.button_cs2fixes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2fixes.Location = new System.Drawing.Point(439, 135);
            this.button_cs2fixes.Name = "button_cs2fixes";
            this.button_cs2fixes.Size = new System.Drawing.Size(27, 23);
            this.button_cs2fixes.TabIndex = 14;
            this.button_cs2fixes.Text = "...";
            this.button_cs2fixes.UseVisualStyleBackColor = true;
            // 
            // textbox_cs2fixes
            // 
            this.textbox_cs2fixes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_cs2fixes.Location = new System.Drawing.Point(127, 136);
            this.textbox_cs2fixes.Multiline = true;
            this.textbox_cs2fixes.Name = "textbox_cs2fixes";
            this.textbox_cs2fixes.Size = new System.Drawing.Size(306, 20);
            this.textbox_cs2fixes.TabIndex = 13;
            // 
            // button_metamod_launcher
            // 
            this.button_metamod_launcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_metamod_launcher.Location = new System.Drawing.Point(439, 109);
            this.button_metamod_launcher.Name = "button_metamod_launcher";
            this.button_metamod_launcher.Size = new System.Drawing.Size(27, 23);
            this.button_metamod_launcher.TabIndex = 12;
            this.button_metamod_launcher.Text = "...";
            this.button_metamod_launcher.UseVisualStyleBackColor = true;
            // 
            // textbox_metamod_launcher
            // 
            this.textbox_metamod_launcher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_metamod_launcher.Location = new System.Drawing.Point(127, 110);
            this.textbox_metamod_launcher.Multiline = true;
            this.textbox_metamod_launcher.Name = "textbox_metamod_launcher";
            this.textbox_metamod_launcher.Size = new System.Drawing.Size(306, 20);
            this.textbox_metamod_launcher.TabIndex = 11;
            // 
            // button_metamod
            // 
            this.button_metamod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_metamod.Location = new System.Drawing.Point(439, 84);
            this.button_metamod.Name = "button_metamod";
            this.button_metamod.Size = new System.Drawing.Size(27, 23);
            this.button_metamod.TabIndex = 10;
            this.button_metamod.Text = "...";
            this.button_metamod.UseVisualStyleBackColor = true;
            // 
            // textbox_metamod
            // 
            this.textbox_metamod.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_metamod.Location = new System.Drawing.Point(127, 85);
            this.textbox_metamod.Multiline = true;
            this.textbox_metamod.Name = "textbox_metamod";
            this.textbox_metamod.Size = new System.Drawing.Size(306, 20);
            this.textbox_metamod.TabIndex = 9;
            // 
            // button_cs2fixescfg
            // 
            this.button_cs2fixescfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2fixescfg.Location = new System.Drawing.Point(439, 58);
            this.button_cs2fixescfg.Name = "button_cs2fixescfg";
            this.button_cs2fixescfg.Size = new System.Drawing.Size(27, 23);
            this.button_cs2fixescfg.TabIndex = 8;
            this.button_cs2fixescfg.Text = "...";
            this.button_cs2fixescfg.UseVisualStyleBackColor = true;
            // 
            // textbox_cs2fixescfg
            // 
            this.textbox_cs2fixescfg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_cs2fixescfg.Location = new System.Drawing.Point(127, 59);
            this.textbox_cs2fixescfg.Multiline = true;
            this.textbox_cs2fixescfg.Name = "textbox_cs2fixescfg";
            this.textbox_cs2fixescfg.Size = new System.Drawing.Size(306, 20);
            this.textbox_cs2fixescfg.TabIndex = 7;
            // 
            // textbox_steamid64
            // 
            this.textbox_steamid64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_steamid64.Location = new System.Drawing.Point(127, 33);
            this.textbox_steamid64.Multiline = true;
            this.textbox_steamid64.Name = "textbox_steamid64";
            this.textbox_steamid64.Size = new System.Drawing.Size(339, 20);
            this.textbox_steamid64.TabIndex = 4;
            // 
            // button_cs2_root
            // 
            this.button_cs2_root.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2_root.Location = new System.Drawing.Point(439, 7);
            this.button_cs2_root.Name = "button_cs2_root";
            this.button_cs2_root.Size = new System.Drawing.Size(27, 23);
            this.button_cs2_root.TabIndex = 3;
            this.button_cs2_root.Text = "...";
            this.button_cs2_root.UseVisualStyleBackColor = true;
            // 
            // label_cs2_root
            // 
            this.label_cs2_root.AutoSize = true;
            this.label_cs2_root.Location = new System.Drawing.Point(6, 8);
            this.label_cs2_root.Name = "label_cs2_root";
            this.label_cs2_root.Size = new System.Drawing.Size(99, 15);
            this.label_cs2_root.TabIndex = 1;
            this.label_cs2_root.Text = "cs2 root directory";
            // 
            // textbox_cs2_root
            // 
            this.textbox_cs2_root.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_cs2_root.Location = new System.Drawing.Point(127, 8);
            this.textbox_cs2_root.Multiline = true;
            this.textbox_cs2_root.Name = "textbox_cs2_root";
            this.textbox_cs2_root.Size = new System.Drawing.Size(259, 20);
            this.textbox_cs2_root.TabIndex = 0;
            // 
            // tabpage_autojoiner
            // 
            this.tabpage_autojoiner.Controls.Add(this.numericUpDown2);
            this.tabpage_autojoiner.Controls.Add(this.label6);
            this.tabpage_autojoiner.Controls.Add(this.numericUpDown1);
            this.tabpage_autojoiner.Controls.Add(this.label5);
            this.tabpage_autojoiner.Controls.Add(this.button_autojoiner_start);
            this.tabpage_autojoiner.Controls.Add(this.label4);
            this.tabpage_autojoiner.Controls.Add(this.textbox_autojoiner_username);
            this.tabpage_autojoiner.Controls.Add(this.label3);
            this.tabpage_autojoiner.Controls.Add(this.textbox_autojoiner_port);
            this.tabpage_autojoiner.Controls.Add(this.label2);
            this.tabpage_autojoiner.Controls.Add(this.textbox_autojoiner_ip);
            this.tabpage_autojoiner.Location = new System.Drawing.Point(4, 24);
            this.tabpage_autojoiner.Name = "tabpage_autojoiner";
            this.tabpage_autojoiner.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_autojoiner.Size = new System.Drawing.Size(472, 287);
            this.tabpage_autojoiner.TabIndex = 1;
            this.tabpage_autojoiner.Text = "autojoiner";
            this.tabpage_autojoiner.UseVisualStyleBackColor = true;
            // 
            // group_console
            // 
            this.group_console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.group_console.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.group_console.Controls.Add(this.rich_console);
            this.group_console.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.group_console.Location = new System.Drawing.Point(12, 317);
            this.group_console.MaximumSize = new System.Drawing.Size(9999, 9999);
            this.group_console.MinimumSize = new System.Drawing.Size(478, 129);
            this.group_console.Name = "group_console";
            this.group_console.Size = new System.Drawing.Size(478, 183);
            this.group_console.TabIndex = 4;
            this.group_console.TabStop = false;
            this.group_console.Text = "console";
            // 
            // rich_console
            // 
            this.rich_console.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rich_console.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rich_console.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rich_console.Location = new System.Drawing.Point(4, 23);
            this.rich_console.Name = "rich_console";
            this.rich_console.ReadOnly = true;
            this.rich_console.Size = new System.Drawing.Size(468, 154);
            this.rich_console.TabIndex = 0;
            this.rich_console.Text = "";
            // 
            // button_launch_cs2
            // 
            this.button_launch_cs2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_launch_cs2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_launch_cs2.Location = new System.Drawing.Point(414, 9);
            this.button_launch_cs2.Margin = new System.Windows.Forms.Padding(0);
            this.button_launch_cs2.Name = "button_launch_cs2";
            this.button_launch_cs2.Size = new System.Drawing.Size(77, 26);
            this.button_launch_cs2.TabIndex = 2;
            this.button_launch_cs2.Text = "launch cs2";
            this.button_launch_cs2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 15);
            this.label2.TabIndex = 22;
            this.label2.Text = "ip";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // textbox_autojoiner_ip
            // 
            this.textbox_autojoiner_ip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_autojoiner_ip.Location = new System.Drawing.Point(71, 8);
            this.textbox_autojoiner_ip.Multiline = true;
            this.textbox_autojoiner_ip.Name = "textbox_autojoiner_ip";
            this.textbox_autojoiner_ip.Size = new System.Drawing.Size(395, 20);
            this.textbox_autojoiner_ip.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 15);
            this.label3.TabIndex = 24;
            this.label3.Text = "port";
            // 
            // textbox_autojoiner_port
            // 
            this.textbox_autojoiner_port.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_autojoiner_port.Location = new System.Drawing.Point(71, 33);
            this.textbox_autojoiner_port.Multiline = true;
            this.textbox_autojoiner_port.Name = "textbox_autojoiner_port";
            this.textbox_autojoiner_port.Size = new System.Drawing.Size(395, 20);
            this.textbox_autojoiner_port.TabIndex = 23;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 26;
            this.label4.Text = "username";
            // 
            // textbox_autojoiner_username
            // 
            this.textbox_autojoiner_username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_autojoiner_username.Location = new System.Drawing.Point(71, 59);
            this.textbox_autojoiner_username.Multiline = true;
            this.textbox_autojoiner_username.Name = "textbox_autojoiner_username";
            this.textbox_autojoiner_username.Size = new System.Drawing.Size(395, 20);
            this.textbox_autojoiner_username.TabIndex = 25;
            // 
            // button_autojoiner_start
            // 
            this.button_autojoiner_start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_autojoiner_start.Location = new System.Drawing.Point(400, 85);
            this.button_autojoiner_start.Name = "button_autojoiner_start";
            this.button_autojoiner_start.Size = new System.Drawing.Size(67, 23);
            this.button_autojoiner_start.TabIndex = 27;
            this.button_autojoiner_start.Text = "start";
            this.button_autojoiner_start.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 15);
            this.label5.TabIndex = 29;
            this.label5.Text = "threshold";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(71, 85);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(56, 23);
            this.numericUpDown1.TabIndex = 30;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(202, 85);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(56, 23);
            this.numericUpDown2.TabIndex = 32;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(133, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "server max";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(504, 512);
            this.Controls.Add(this.group_console);
            this.Controls.Add(this.tab_main);
            this.Controls.Add(this.button_launch_cs2);
            this.Controls.Add(this.checkbox_cs2fixes);
            this.Controls.Add(this.checkbox_tools);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(520, 547);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cs2 things and such";
            this.tab_main.ResumeLayout(false);
            this.tabpage_cs2fixes.ResumeLayout(false);
            this.tabpage_cs2fixes.PerformLayout();
            this.tabpage_autojoiner.ResumeLayout(false);
            this.tabpage_autojoiner.PerformLayout();
            this.group_console.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkbox_tools;
        private System.Windows.Forms.CheckBox checkbox_cs2fixes;
        private System.Windows.Forms.TabControl tab_main;
        private System.Windows.Forms.TabPage tabpage_cs2fixes;
        private System.Windows.Forms.TabPage tabpage_autojoiner;
        private System.Windows.Forms.Button button_cs2_root;
        private System.Windows.Forms.Label label_cs2_root;
        private System.Windows.Forms.TextBox textbox_cs2_root;
        private System.Windows.Forms.TextBox textbox_steamid64;
        private System.Windows.Forms.Button button_gfl_ze_configs;
        private System.Windows.Forms.TextBox textbox_gfl_ze_configs;
        private System.Windows.Forms.Button button_strippercs2;
        private System.Windows.Forms.TextBox textbox_strippercs2;
        private System.Windows.Forms.Button button_cs2fixes;
        private System.Windows.Forms.TextBox textbox_cs2fixes;
        private System.Windows.Forms.Button button_metamod_launcher;
        private System.Windows.Forms.TextBox textbox_metamod_launcher;
        private System.Windows.Forms.Button button_metamod;
        private System.Windows.Forms.TextBox textbox_metamod;
        private System.Windows.Forms.Button button_cs2fixescfg;
        private System.Windows.Forms.TextBox textbox_cs2fixescfg;
        private System.Windows.Forms.Button button_cs2_root_autodetect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_install;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.GroupBox group_console;
        private System.Windows.Forms.RichTextBox rich_console;
        private System.Windows.Forms.Button button_launch_cs2;
        private System.Windows.Forms.Button button_uninstall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textbox_autojoiner_ip;
        private System.Windows.Forms.Button button_autojoiner_start;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textbox_autojoiner_username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textbox_autojoiner_port;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}
