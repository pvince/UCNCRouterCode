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
            this.btnSndMsg = new System.Windows.Forms.Button();
            this.lblDbgOut = new System.Windows.Forms.Label();
            this.rtbTraffic = new System.Windows.Forms.RichTextBox();
            this.btnOpenPort = new System.Windows.Forms.Button();
            this.cmbPorts = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbMsgs = new System.Windows.Forms.ComboBox();
            this.btnClosePort = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSndMsg
            // 
            this.btnSndMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSndMsg.Enabled = false;
            this.btnSndMsg.Location = new System.Drawing.Point(404, 122);
            this.btnSndMsg.Name = "btnSndMsg";
            this.btnSndMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSndMsg.TabIndex = 0;
            this.btnSndMsg.Text = "Send Msg";
            this.btnSndMsg.UseVisualStyleBackColor = true;
            this.btnSndMsg.Click += new System.EventHandler(this.btnSndMsg_Click);
            // 
            // lblDbgOut
            // 
            this.lblDbgOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDbgOut.AutoSize = true;
            this.lblDbgOut.Location = new System.Drawing.Point(381, 97);
            this.lblDbgOut.Name = "lblDbgOut";
            this.lblDbgOut.Size = new System.Drawing.Size(19, 13);
            this.lblDbgOut.TabIndex = 1;
            this.lblDbgOut.Text = "----";
            // 
            // rtbTraffic
            // 
            this.rtbTraffic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbTraffic.Location = new System.Drawing.Point(2, 16);
            this.rtbTraffic.Name = "rtbTraffic";
            this.rtbTraffic.Size = new System.Drawing.Size(312, 129);
            this.rtbTraffic.TabIndex = 2;
            this.rtbTraffic.Text = "";
            // 
            // btnOpenPort
            // 
            this.btnOpenPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenPort.Location = new System.Drawing.Point(323, 122);
            this.btnOpenPort.Name = "btnOpenPort";
            this.btnOpenPort.Size = new System.Drawing.Size(75, 23);
            this.btnOpenPort.TabIndex = 3;
            this.btnOpenPort.Text = "Open Port";
            this.btnOpenPort.UseVisualStyleBackColor = true;
            this.btnOpenPort.Click += new System.EventHandler(this.btnOpenPort_Click);
            // 
            // cmbPorts
            // 
            this.cmbPorts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPorts.FormattingEnabled = true;
            this.cmbPorts.Location = new System.Drawing.Point(381, 20);
            this.cmbPorts.Name = "cmbPorts";
            this.cmbPorts.Size = new System.Drawing.Size(179, 21);
            this.cmbPorts.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(346, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(322, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Message:";
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
            this.cmbMsgs.Location = new System.Drawing.Point(381, 44);
            this.cmbMsgs.Name = "cmbMsgs";
            this.cmbMsgs.Size = new System.Drawing.Size(179, 21);
            this.cmbMsgs.TabIndex = 7;
            // 
            // btnClosePort
            // 
            this.btnClosePort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePort.Enabled = false;
            this.btnClosePort.Location = new System.Drawing.Point(485, 122);
            this.btnClosePort.Name = "btnClosePort";
            this.btnClosePort.Size = new System.Drawing.Size(75, 23);
            this.btnClosePort.TabIndex = 8;
            this.btnClosePort.Text = "Close Port";
            this.btnClosePort.UseVisualStyleBackColor = true;
            this.btnClosePort.Click += new System.EventHandler(this.btnClosePort_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(313, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Debug Out:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Comm Traffic:";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(346, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Hex:";
            // 
            // txtHex
            // 
            this.txtHex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHex.Location = new System.Drawing.Point(381, 71);
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(179, 20);
            this.txtHex.TabIndex = 12;
            this.txtHex.Text = "00";
            // 
            // CNCRouterCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 157);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClosePort);
            this.Controls.Add(this.cmbMsgs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbPorts);
            this.Controls.Add(this.btnOpenPort);
            this.Controls.Add(this.rtbTraffic);
            this.Controls.Add(this.lblDbgOut);
            this.Controls.Add(this.btnSndMsg);
            this.Name = "CNCRouterCommand";
            this.Text = "Comm Debug";
            this.Load += new System.EventHandler(this.CNCRouterCommand_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSndMsg;
        private System.Windows.Forms.Label lblDbgOut;
        private System.Windows.Forms.RichTextBox rtbTraffic;
        private System.Windows.Forms.Button btnOpenPort;
        private System.Windows.Forms.ComboBox cmbPorts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMsgs;
        private System.Windows.Forms.Button btnClosePort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtHex;
    }
}

