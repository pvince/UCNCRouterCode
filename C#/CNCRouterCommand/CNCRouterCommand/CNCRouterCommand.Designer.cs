namespace CNCRouterCommand
{
    partial class CNCRouterCommand
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CNCRouterCommand));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPortStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tpCommDebug = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnRefreshPorts = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.lblQueue = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.rtbTraffic = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.cmbMsgs = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.lblDbgOut = new System.Windows.Forms.Label();
            this.btnSndMsg = new System.Windows.Forms.Button();
            this.tcInterface = new System.Windows.Forms.TabControl();
            this.tpAuto = new System.Windows.Forms.TabPage();
            this.btnClearEventOutput = new System.Windows.Forms.Button();
            this.lblStatusBuild = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.rtbRCOutput = new System.Windows.Forms.RichTextBox();
            this.btnAbortBuild = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnStartBuild = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblStatusFile = new System.Windows.Forms.Label();
            this.btnLoadGCode = new System.Windows.Forms.Button();
            this.tpManual = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtZFeedrate = new System.Windows.Forms.TextBox();
            this.txtXYFeedrate = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnZm = new System.Windows.Forms.Button();
            this.btnZp = new System.Windows.Forms.Button();
            this.btnXm = new System.Windows.Forms.Button();
            this.btnYm = new System.Windows.Forms.Button();
            this.btnXp = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMoveDistance = new System.Windows.Forms.ComboBox();
            this.btnYp = new System.Windows.Forms.Button();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.txtArcAccuracy = new System.Windows.Forms.TextBox();
            this.cmbRouterPort = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tpGenDebug = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.ofdGcodeBrowse = new System.Windows.Forms.OpenFileDialog();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnRefreshRtrPorts = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tpCommDebug.SuspendLayout();
            this.tcInterface.SuspendLayout();
            this.tpAuto.SuspendLayout();
            this.tpManual.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.tpGenDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tsPortStatus,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 271);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(541, 22);
            this.statusStrip1.TabIndex = 14;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel1.Text = "Router Status:";
            // 
            // tsPortStatus
            // 
            this.tsPortStatus.Name = "tsPortStatus";
            this.tsPortStatus.Size = new System.Drawing.Size(110, 17);
            this.tsPortStatus.Text = "No router detected.";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(194, 17);
            this.toolStripStatusLabel3.Spring = true;
            this.toolStripStatusLabel3.Text = " ";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabel4.Text = "Router Mode:";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(63, 17);
            this.toolStripStatusLabel5.Text = "Automatic";
            // 
            // tpCommDebug
            // 
            this.tpCommDebug.Controls.Add(this.btnClear);
            this.tpCommDebug.Controls.Add(this.btnRefreshPorts);
            this.tpCommDebug.Controls.Add(this.label7);
            this.tpCommDebug.Controls.Add(this.lblQueue);
            this.tpCommDebug.Controls.Add(this.label5);
            this.tpCommDebug.Controls.Add(this.txtHex);
            this.tpCommDebug.Controls.Add(this.rtbTraffic);
            this.tpCommDebug.Controls.Add(this.label6);
            this.tpCommDebug.Controls.Add(this.label4);
            this.tpCommDebug.Controls.Add(this.btnClosePort);
            this.tpCommDebug.Controls.Add(this.cmbMsgs);
            this.tpCommDebug.Controls.Add(this.label3);
            this.tpCommDebug.Controls.Add(this.label2);
            this.tpCommDebug.Controls.Add(this.cmbPorts);
            this.tpCommDebug.Controls.Add(this.btnOpenPort);
            this.tpCommDebug.Controls.Add(this.lblDbgOut);
            this.tpCommDebug.Controls.Add(this.btnSndMsg);
            this.tpCommDebug.Location = new System.Drawing.Point(4, 22);
            this.tpCommDebug.Name = "tpCommDebug";
            this.tpCommDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpCommDebug.Size = new System.Drawing.Size(509, 230);
            this.tpCommDebug.TabIndex = 0;
            this.tpCommDebug.Text = "Comm Debug";
            this.tpCommDebug.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(257, 201);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 28;
            this.btnClear.Text = "Clear Output";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnRefreshPorts
            // 
            this.btnRefreshPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshPorts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshPorts.BackgroundImage")));
            this.btnRefreshPorts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshPorts.Location = new System.Drawing.Point(468, 49);
            this.btnRefreshPorts.Name = "btnRefreshPorts";
            this.btnRefreshPorts.Size = new System.Drawing.Size(26, 23);
            this.btnRefreshPorts.TabIndex = 27;
            this.btnRefreshPorts.UseVisualStyleBackColor = true;
            this.btnRefreshPorts.Click += new System.EventHandler(this.btnRefreshPorts_Click);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(256, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 26;
            this.label7.Text = "Msg Queue:";
            // 
            // lblQueue
            // 
            this.lblQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQueue.AutoSize = true;
            this.lblQueue.Location = new System.Drawing.Point(324, 22);
            this.lblQueue.Name = "lblQueue";
            this.lblQueue.Size = new System.Drawing.Size(16, 13);
            this.lblQueue.TabIndex = 25;
            this.lblQueue.Text = "---";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Comm Traffic:";
            // 
            // txtHex
            // 
            this.txtHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHex.Location = new System.Drawing.Point(327, 102);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(167, 20);
            this.txtHex.TabIndex = 24;
            this.txtHex.Text = "00";
            // 
            // rtbTraffic
            // 
            this.rtbTraffic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTraffic.Location = new System.Drawing.Point(6, 22);
            this.rtbTraffic.Name = "rtbTraffic";
            this.rtbTraffic.ReadOnly = true;
            this.rtbTraffic.Size = new System.Drawing.Size(247, 202);
            this.rtbTraffic.TabIndex = 15;
            this.rtbTraffic.Text = "";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(292, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Hex:";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(259, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Debug Out:";
            // 
            // btnClosePort
            // 
            this.btnClosePort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePort.Enabled = false;
            this.btnClosePort.Location = new System.Drawing.Point(419, 149);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 21;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // cmbMsgs
            // 
            this.cmbMsgs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMsgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMsgs.FormattingEnabled = true;
            this.cmbMsgs.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbMsgs.Items.AddRange(new object[] {
            "-: Hex",
            "0: Ping",
            "1: CmdAck",
            "2: E-Stop",
            "3: RequestCommands",
            "4: StartQueue",
            "5: SetSpeed",
            "6: Move",
            "7: ToolCmd"});
            this.cmbMsgs.Location = new System.Drawing.Point(327, 75);
            this.cmbMsgs.Name = "cmbMsgs";
            this.cmbMsgs.Size = new System.Drawing.Size(167, 21);
            this.cmbMsgs.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Message:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Port:";
            // 
            // cmbPorts
            // 
            this.cmbPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(327, 51);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(135, 21);
            this.cmbPorts.TabIndex = 17;
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.Location = new System.Drawing.Point(257, 149);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 16;
            this.btnOpenPort.Text = "Open Port";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // lblDbgOut
            // 
            this.lblDbgOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbgOut.AutoSize = true;
            this.lblDbgOut.Location = new System.Drawing.Point(327, 128);
            this.lblDbgOut.Name = "lblDbgOut";
            this.lblDbgOut.Size = new System.Drawing.Size(19, 13);
            this.lblDbgOut.TabIndex = 14;
            this.lblDbgOut.Text = "----";
            // 
            // btnSndMsg
            // 
            this.btnSndMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSndMsg.Enabled = false;
            this.btnSndMsg.Location = new System.Drawing.Point(338, 149);
            this.btnSndMsg.Name = "btnSndMsg";
            this.btnSndMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSndMsg.TabIndex = 13;
            this.btnSndMsg.Text = "Send Msg";
            this.btnSndMsg.UseVisualStyleBackColor = true;
            this.btnSndMsg.Click += new System.EventHandler(this.btnSndMsg_Click);
            // 
            // tcInterface
            // 
            this.tcInterface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcInterface.Controls.Add(this.tpAuto);
            this.tcInterface.Controls.Add(this.tpManual);
            this.tcInterface.Controls.Add(this.tpSettings);
            this.tcInterface.Controls.Add(this.tpCommDebug);
            this.tcInterface.Controls.Add(this.tpGenDebug);
            this.tcInterface.Location = new System.Drawing.Point(12, 12);
            this.tcInterface.Name = "tcInterface";
            this.tcInterface.SelectedIndex = 0;
            this.tcInterface.Size = new System.Drawing.Size(517, 256);
            this.tcInterface.TabIndex = 13;
            this.tcInterface.SelectedIndexChanged += new System.EventHandler(this.tcInterface_SelectedIndexChanged);
            // 
            // tpAuto
            // 
            this.tpAuto.Controls.Add(this.btnClearEventOutput);
            this.tpAuto.Controls.Add(this.lblStatusBuild);
            this.tpAuto.Controls.Add(this.label17);
            this.tpAuto.Controls.Add(this.rtbRCOutput);
            this.tpAuto.Controls.Add(this.btnAbortBuild);
            this.tpAuto.Controls.Add(this.label16);
            this.tpAuto.Controls.Add(this.btnStartBuild);
            this.tpAuto.Controls.Add(this.label11);
            this.tpAuto.Controls.Add(this.label10);
            this.tpAuto.Controls.Add(this.label8);
            this.tpAuto.Controls.Add(this.lblStatusFile);
            this.tpAuto.Controls.Add(this.btnLoadGCode);
            this.tpAuto.Location = new System.Drawing.Point(4, 22);
            this.tpAuto.Name = "tpAuto";
            this.tpAuto.Padding = new System.Windows.Forms.Padding(3);
            this.tpAuto.Size = new System.Drawing.Size(509, 230);
            this.tpAuto.TabIndex = 2;
            this.tpAuto.Text = "Automatic";
            this.tpAuto.UseVisualStyleBackColor = true;
            // 
            // btnClearEventOutput
            // 
            this.btnClearEventOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearEventOutput.Location = new System.Drawing.Point(428, 60);
            this.btnClearEventOutput.Name = "btnClearEventOutput";
            this.btnClearEventOutput.Size = new System.Drawing.Size(75, 23);
            this.btnClearEventOutput.TabIndex = 29;
            this.btnClearEventOutput.Text = "Clear Output";
            this.btnClearEventOutput.UseVisualStyleBackColor = true;
            this.btnClearEventOutput.Click += new System.EventHandler(this.btnClearEventOutput_Click);
            // 
            // lblStatusBuild
            // 
            this.lblStatusBuild.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusBuild.Location = new System.Drawing.Point(285, 40);
            this.lblStatusBuild.Name = "lblStatusBuild";
            this.lblStatusBuild.Size = new System.Drawing.Size(218, 23);
            this.lblStatusBuild.TabIndex = 18;
            this.lblStatusBuild.Text = "Build not started.";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 73);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(127, 13);
            this.label17.TabIndex = 17;
            this.label17.Text = "Router Command Output:";
            // 
            // rtbRCOutput
            // 
            this.rtbRCOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbRCOutput.Location = new System.Drawing.Point(6, 89);
            this.rtbRCOutput.Name = "rtbRCOutput";
            this.rtbRCOutput.ReadOnly = true;
            this.rtbRCOutput.Size = new System.Drawing.Size(497, 135);
            this.rtbRCOutput.TabIndex = 16;
            this.rtbRCOutput.Text = "";
            // 
            // btnAbortBuild
            // 
            this.btnAbortBuild.Location = new System.Drawing.Point(119, 35);
            this.btnAbortBuild.Name = "btnAbortBuild";
            this.btnAbortBuild.Size = new System.Drawing.Size(88, 23);
            this.btnAbortBuild.TabIndex = 15;
            this.btnAbortBuild.Text = "Abort Build";
            this.btnAbortBuild.UseVisualStyleBackColor = true;
            this.btnAbortBuild.Click += new System.EventHandler(this.btnAbortBuild_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(213, 40);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 13);
            this.label16.TabIndex = 14;
            this.label16.Text = "Build Status:";
            // 
            // btnStartBuild
            // 
            this.btnStartBuild.Location = new System.Drawing.Point(25, 35);
            this.btnStartBuild.Name = "btnStartBuild";
            this.btnStartBuild.Size = new System.Drawing.Size(88, 23);
            this.btnStartBuild.TabIndex = 13;
            this.btnStartBuild.Text = "Start Build";
            this.btnStartBuild.UseVisualStyleBackColor = true;
            this.btnStartBuild.Click += new System.EventHandler(this.btnStartBuild_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "2.)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "1.)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(219, 11);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Current file:";
            // 
            // lblStatusFile
            // 
            this.lblStatusFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatusFile.Location = new System.Drawing.Point(285, 11);
            this.lblStatusFile.Name = "lblStatusFile";
            this.lblStatusFile.Size = new System.Drawing.Size(218, 23);
            this.lblStatusFile.TabIndex = 3;
            this.lblStatusFile.Text = "G-code file not loaded.";
            // 
            // btnLoadGCode
            // 
            this.btnLoadGCode.Location = new System.Drawing.Point(25, 6);
            this.btnLoadGCode.Name = "btnLoadGCode";
            this.btnLoadGCode.Size = new System.Drawing.Size(141, 23);
            this.btnLoadGCode.TabIndex = 0;
            this.btnLoadGCode.Text = "Load G-Code File.";
            this.btnLoadGCode.UseVisualStyleBackColor = true;
            this.btnLoadGCode.Click += new System.EventHandler(this.btnLoadGCode_Click);
            // 
            // tpManual
            // 
            this.tpManual.Controls.Add(this.label18);
            this.tpManual.Controls.Add(this.label15);
            this.tpManual.Controls.Add(this.txtZFeedrate);
            this.tpManual.Controls.Add(this.txtXYFeedrate);
            this.tpManual.Controls.Add(this.label14);
            this.tpManual.Controls.Add(this.label13);
            this.tpManual.Controls.Add(this.btnZm);
            this.tpManual.Controls.Add(this.btnZp);
            this.tpManual.Controls.Add(this.btnXm);
            this.tpManual.Controls.Add(this.btnYm);
            this.tpManual.Controls.Add(this.btnXp);
            this.tpManual.Controls.Add(this.label9);
            this.tpManual.Controls.Add(this.cmbMoveDistance);
            this.tpManual.Controls.Add(this.btnYp);
            this.tpManual.Location = new System.Drawing.Point(4, 22);
            this.tpManual.Name = "tpManual";
            this.tpManual.Padding = new System.Windows.Forms.Padding(3);
            this.tpManual.Size = new System.Drawing.Size(509, 230);
            this.tpManual.TabIndex = 3;
            this.tpManual.Text = "Manual";
            this.tpManual.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(384, 37);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(50, 13);
            this.label18.TabIndex = 34;
            this.label18.Text = "mm / min";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(384, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(50, 13);
            this.label15.TabIndex = 33;
            this.label15.Text = "mm / min";
            // 
            // txtZFeedrate
            // 
            this.txtZFeedrate.Location = new System.Drawing.Point(315, 34);
            this.txtZFeedrate.Name = "txtZFeedrate";
            this.txtZFeedrate.Size = new System.Drawing.Size(63, 20);
            this.txtZFeedrate.TabIndex = 32;
            this.txtZFeedrate.Text = "30.0";
            this.txtZFeedrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtXYFeedrate
            // 
            this.txtXYFeedrate.Location = new System.Drawing.Point(315, 8);
            this.txtXYFeedrate.Name = "txtXYFeedrate";
            this.txtXYFeedrate.Size = new System.Drawing.Size(63, 20);
            this.txtXYFeedrate.TabIndex = 31;
            this.txtXYFeedrate.Text = "300.0";
            this.txtXYFeedrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(247, 37);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 13);
            this.label14.TabIndex = 30;
            this.label14.Text = "Z Feedrate:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(240, 11);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "XY Feedrate:";
            // 
            // btnZm
            // 
            this.btnZm.Location = new System.Drawing.Point(202, 125);
            this.btnZm.Name = "btnZm";
            this.btnZm.Size = new System.Drawing.Size(52, 37);
            this.btnZm.TabIndex = 28;
            this.btnZm.Text = "Z-";
            this.btnZm.UseVisualStyleBackColor = true;
            this.btnZm.Click += new System.EventHandler(this.btnZm_Click);
            // 
            // btnZp
            // 
            this.btnZp.Location = new System.Drawing.Point(202, 82);
            this.btnZp.Name = "btnZp";
            this.btnZp.Size = new System.Drawing.Size(52, 37);
            this.btnZp.TabIndex = 27;
            this.btnZp.Text = "Z+";
            this.btnZp.UseVisualStyleBackColor = true;
            this.btnZp.Click += new System.EventHandler(this.btnZp_Click);
            // 
            // btnXm
            // 
            this.btnXm.Location = new System.Drawing.Point(16, 102);
            this.btnXm.Name = "btnXm";
            this.btnXm.Size = new System.Drawing.Size(52, 37);
            this.btnXm.TabIndex = 26;
            this.btnXm.Text = "X-";
            this.btnXm.UseVisualStyleBackColor = true;
            this.btnXm.Click += new System.EventHandler(this.btnXm_Click);
            // 
            // btnYm
            // 
            this.btnYm.Location = new System.Drawing.Point(65, 145);
            this.btnYm.Name = "btnYm";
            this.btnYm.Size = new System.Drawing.Size(52, 37);
            this.btnYm.TabIndex = 25;
            this.btnYm.Text = "Y-";
            this.btnYm.UseVisualStyleBackColor = true;
            this.btnYm.Click += new System.EventHandler(this.btnYm_Click);
            // 
            // btnXp
            // 
            this.btnXp.Location = new System.Drawing.Point(114, 102);
            this.btnXp.Name = "btnXp";
            this.btnXp.Size = new System.Drawing.Size(52, 37);
            this.btnXp.TabIndex = 24;
            this.btnXp.Text = "X+";
            this.btnXp.UseVisualStyleBackColor = true;
            this.btnXp.Click += new System.EventHandler(this.btnXp_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Distance:";
            // 
            // cmbMoveDistance
            // 
            this.cmbMoveDistance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMoveDistance.FormattingEnabled = true;
            this.cmbMoveDistance.Location = new System.Drawing.Point(64, 11);
            this.cmbMoveDistance.Name = "cmbMoveDistance";
            this.cmbMoveDistance.Size = new System.Drawing.Size(95, 21);
            this.cmbMoveDistance.TabIndex = 22;
            // 
            // btnYp
            // 
            this.btnYp.Location = new System.Drawing.Point(65, 59);
            this.btnYp.Name = "btnYp";
            this.btnYp.Size = new System.Drawing.Size(52, 37);
            this.btnYp.TabIndex = 21;
            this.btnYp.Text = "Y+";
            this.btnYp.UseVisualStyleBackColor = true;
            this.btnYp.Click += new System.EventHandler(this.btnYp_Click);
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.btnRefreshRtrPorts);
            this.tpSettings.Controls.Add(this.label20);
            this.tpSettings.Controls.Add(this.label19);
            this.tpSettings.Controls.Add(this.txtArcAccuracy);
            this.tpSettings.Controls.Add(this.cmbRouterPort);
            this.tpSettings.Controls.Add(this.label12);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(509, 230);
            this.tpSettings.TabIndex = 4;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // txtArcAccuracy
            // 
            this.txtArcAccuracy.Location = new System.Drawing.Point(106, 37);
            this.txtArcAccuracy.Name = "txtArcAccuracy";
            this.txtArcAccuracy.Size = new System.Drawing.Size(100, 20);
            this.txtArcAccuracy.TabIndex = 12;
            this.txtArcAccuracy.Text = "0.1";
            // 
            // cmbRouterPort
            // 
            this.cmbRouterPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRouterPort.FormattingEnabled = true;
            this.cmbRouterPort.Location = new System.Drawing.Point(106, 10);
            this.cmbRouterPort.Name = "cmbRouterPort";
            this.cmbRouterPort.Size = new System.Drawing.Size(107, 21);
            this.cmbRouterPort.TabIndex = 11;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 13);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 10;
            this.label12.Text = "Select Router Port:";
            // 
            // tpGenDebug
            // 
            this.tpGenDebug.Controls.Add(this.label1);
            this.tpGenDebug.Controls.Add(this.button1);
            this.tpGenDebug.Location = new System.Drawing.Point(4, 22);
            this.tpGenDebug.Name = "tpGenDebug";
            this.tpGenDebug.Size = new System.Drawing.Size(509, 230);
            this.tpGenDebug.TabIndex = 1;
            this.tpGenDebug.Text = "General Debug";
            this.tpGenDebug.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(20, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ofdGcodeBrowse
            // 
            this.ofdGcodeBrowse.Filter = "G-code files|*.nc|All files|*.*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(26, 40);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 13;
            this.label19.Text = "Arc Accuracy:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(212, 40);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 13);
            this.label20.TabIndex = 14;
            this.label20.Text = "mm";
            // 
            // btnRefreshRtrPorts
            // 
            this.btnRefreshRtrPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshRtrPorts.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefreshRtrPorts.BackgroundImage")));
            this.btnRefreshRtrPorts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefreshRtrPorts.Location = new System.Drawing.Point(219, 10);
            this.btnRefreshRtrPorts.Name = "btnRefreshRtrPorts";
            this.btnRefreshRtrPorts.Size = new System.Drawing.Size(26, 23);
            this.btnRefreshRtrPorts.TabIndex = 28;
            this.btnRefreshRtrPorts.UseVisualStyleBackColor = true;
            this.btnRefreshRtrPorts.Click += new System.EventHandler(this.button2_Click);
            // 
            // CNCRouterCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 293);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tcInterface);
            this.MinimumSize = new System.Drawing.Size(557, 331);
            this.Name = "CNCRouterCommand";
            this.Text = "Router Command";
            this.Load += new System.EventHandler(this.CNCRouterCommand_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tpCommDebug.ResumeLayout(false);
            this.tpCommDebug.PerformLayout();
            this.tcInterface.ResumeLayout(false);
            this.tpAuto.ResumeLayout(false);
            this.tpAuto.PerformLayout();
            this.tpManual.ResumeLayout(false);
            this.tpManual.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.tpGenDebug.ResumeLayout(false);
            this.tpGenDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tsPortStatus;
        private System.Windows.Forms.TabPage tpCommDebug;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.RichTextBox rtbTraffic;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.ComboBox cmbMsgs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.Label lblDbgOut;
        private System.Windows.Forms.Button btnSndMsg;
        private System.Windows.Forms.TabControl tcInterface;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblQueue;
        private System.Windows.Forms.Button btnRefreshPorts;
        private System.Windows.Forms.TabPage tpGenDebug;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tpAuto;
        private System.Windows.Forms.TabPage tpManual;
        private System.Windows.Forms.Label lblStatusFile;
        private System.Windows.Forms.Button btnLoadGCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.RichTextBox rtbRCOutput;
        private System.Windows.Forms.Button btnAbortBuild;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnStartBuild;
        private System.Windows.Forms.Label lblStatusBuild;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.ComboBox cmbRouterPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnZm;
        private System.Windows.Forms.Button btnZp;
        private System.Windows.Forms.Button btnXm;
        private System.Windows.Forms.Button btnYm;
        private System.Windows.Forms.Button btnXp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbMoveDistance;
        private System.Windows.Forms.Button btnYp;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.OpenFileDialog ofdGcodeBrowse;
        private System.Windows.Forms.TextBox txtArcAccuracy;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtZFeedrate;
        private System.Windows.Forms.TextBox txtXYFeedrate;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearEventOutput;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnRefreshRtrPorts;
    }
}

