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
            this.button_launch_cs2 = new System.Windows.Forms.Button();
            this.tab_main = new System.Windows.Forms.TabControl();
            this.tabpage_cs2fixes = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.tab_main.SuspendLayout();
            this.tabpage_cs2fixes.SuspendLayout();
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
            // button_launch_cs2
            // 
            this.button_launch_cs2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_launch_cs2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_launch_cs2.Location = new System.Drawing.Point(461, 9);
            this.button_launch_cs2.Margin = new System.Windows.Forms.Padding(0);
            this.button_launch_cs2.Name = "button_launch_cs2";
            this.button_launch_cs2.Size = new System.Drawing.Size(77, 23);
            this.button_launch_cs2.TabIndex = 2;
            this.button_launch_cs2.Text = "launch cs2";
            this.button_launch_cs2.UseVisualStyleBackColor = true;
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
            this.tab_main.Name = "tab_main";
            this.tab_main.SelectedIndex = 0;
            this.tab_main.Size = new System.Drawing.Size(523, 249);
            this.tab_main.TabIndex = 3;
            // 
            // tabpage_cs2fixes
            // 
            this.tabpage_cs2fixes.Controls.Add(this.label7);
            this.tabpage_cs2fixes.Controls.Add(this.label6);
            this.tabpage_cs2fixes.Controls.Add(this.label5);
            this.tabpage_cs2fixes.Controls.Add(this.label4);
            this.tabpage_cs2fixes.Controls.Add(this.label3);
            this.tabpage_cs2fixes.Controls.Add(this.label2);
            this.tabpage_cs2fixes.Controls.Add(this.label1);
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
            this.tabpage_cs2fixes.Size = new System.Drawing.Size(515, 221);
            this.tabpage_cs2fixes.TabIndex = 0;
            this.tabpage_cs2fixes.Text = "cs2fixes";
            this.tabpage_cs2fixes.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 15);
            this.label7.TabIndex = 26;
            this.label7.Text = "gfl ze configs";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 15);
            this.label6.TabIndex = 25;
            this.label6.Text = "strippercs2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "cs2fixes";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "metamod launcher";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "metamod";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 21;
            this.label2.Text = "cs2fixes.cfg";
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
            // button_cs2_root_autodetect
            // 
            this.button_cs2_root_autodetect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2_root_autodetect.Location = new System.Drawing.Point(435, 7);
            this.button_cs2_root_autodetect.Name = "button_cs2_root_autodetect";
            this.button_cs2_root_autodetect.Size = new System.Drawing.Size(41, 23);
            this.button_cs2_root_autodetect.TabIndex = 19;
            this.button_cs2_root_autodetect.Text = "auto";
            this.button_cs2_root_autodetect.UseVisualStyleBackColor = true;
            // 
            // button_gfl_ze_configs
            // 
            this.button_gfl_ze_configs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_gfl_ze_configs.Location = new System.Drawing.Point(482, 187);
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
            this.textbox_gfl_ze_configs.Size = new System.Drawing.Size(349, 20);
            this.textbox_gfl_ze_configs.TabIndex = 17;
            // 
            // button_strippercs2
            // 
            this.button_strippercs2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_strippercs2.Location = new System.Drawing.Point(482, 161);
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
            this.textbox_strippercs2.Size = new System.Drawing.Size(349, 20);
            this.textbox_strippercs2.TabIndex = 15;
            // 
            // button_cs2fixes
            // 
            this.button_cs2fixes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2fixes.Location = new System.Drawing.Point(482, 135);
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
            this.textbox_cs2fixes.Size = new System.Drawing.Size(349, 20);
            this.textbox_cs2fixes.TabIndex = 13;
            // 
            // button_metamod_launcher
            // 
            this.button_metamod_launcher.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_metamod_launcher.Location = new System.Drawing.Point(482, 109);
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
            this.textbox_metamod_launcher.Size = new System.Drawing.Size(349, 20);
            this.textbox_metamod_launcher.TabIndex = 11;
            // 
            // button_metamod
            // 
            this.button_metamod.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_metamod.Location = new System.Drawing.Point(482, 84);
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
            this.textbox_metamod.Size = new System.Drawing.Size(349, 20);
            this.textbox_metamod.TabIndex = 9;
            // 
            // button_cs2fixescfg
            // 
            this.button_cs2fixescfg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2fixescfg.Location = new System.Drawing.Point(482, 58);
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
            this.textbox_cs2fixescfg.Size = new System.Drawing.Size(349, 20);
            this.textbox_cs2fixescfg.TabIndex = 7;
            // 
            // textbox_steamid64
            // 
            this.textbox_steamid64.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textbox_steamid64.Location = new System.Drawing.Point(127, 33);
            this.textbox_steamid64.Multiline = true;
            this.textbox_steamid64.Name = "textbox_steamid64";
            this.textbox_steamid64.Size = new System.Drawing.Size(382, 20);
            this.textbox_steamid64.TabIndex = 4;
            // 
            // button_cs2_root
            // 
            this.button_cs2_root.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_cs2_root.Location = new System.Drawing.Point(482, 7);
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
            this.textbox_cs2_root.Size = new System.Drawing.Size(302, 20);
            this.textbox_cs2_root.TabIndex = 0;
            // 
            // tabpage_autojoiner
            // 
            this.tabpage_autojoiner.Location = new System.Drawing.Point(4, 24);
            this.tabpage_autojoiner.Name = "tabpage_autojoiner";
            this.tabpage_autojoiner.Padding = new System.Windows.Forms.Padding(3);
            this.tabpage_autojoiner.Size = new System.Drawing.Size(515, 249);
            this.tabpage_autojoiner.TabIndex = 1;
            this.tabpage_autojoiner.Text = "autojoiner";
            this.tabpage_autojoiner.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(547, 299);
            this.Controls.Add(this.tab_main);
            this.Controls.Add(this.button_launch_cs2);
            this.Controls.Add(this.checkbox_cs2fixes);
            this.Controls.Add(this.checkbox_tools);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.MinimumSize = new System.Drawing.Size(520, 320);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "cs2 things and such";
            this.tab_main.ResumeLayout(false);
            this.tabpage_cs2fixes.ResumeLayout(false);
            this.tabpage_cs2fixes.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkbox_tools;
        private System.Windows.Forms.CheckBox checkbox_cs2fixes;
        private System.Windows.Forms.Button button_launch_cs2;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
