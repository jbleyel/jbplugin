namespace jbplugin
{
    partial class MainForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm2));
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripCenter = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.manualexportbtn = new System.Windows.Forms.Button();
            this.btncheckconn = new System.Windows.Forms.Button();
            this.tlabelPort = new System.Windows.Forms.Label();
            this.labelIPAdress = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.checkBoxManualIP = new System.Windows.Forms.CheckBox();
            this.btnGDFolder = new System.Windows.Forms.Button();
            this.lblGDFolder = new System.Windows.Forms.Label();
            this.radioButtonGD = new System.Windows.Forms.RadioButton();
            this.btnDBFolder = new System.Windows.Forms.Button();
            this.lblDBFolder = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonWIFI = new System.Windows.Forms.RadioButton();
            this.radioButtonDB = new System.Windows.Forms.RadioButton();
            this.listBoxEAN = new System.Windows.Forms.ListBox();
            this.buttonDELEAN = new System.Windows.Forms.Button();
            this.buttonloadimport = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnlogfiles = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCommon = new System.Windows.Forms.TabPage();
            this.tabEAN = new System.Windows.Forms.TabPage();
            this.btnautoimportean = new System.Windows.Forms.Button();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.checkBoxAutostartExport = new System.Windows.Forms.CheckBox();
            this.labelclb2 = new System.Windows.Forms.Label();
            this.labelclb1 = new System.Windows.Forms.Label();
            this.checkedListBoxProfilerDBs = new System.Windows.Forms.CheckedListBox();
            this.maxretryonnetworkerror = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.maxprofilesperzip = new System.Windows.Forms.NumericUpDown();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabCommon.SuspendLayout();
            this.tabEAN.SuspendLayout();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxretryonnetworkerror)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxprofilesperzip)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripCenter,
            this.toolStripRight});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // toolStripCenter
            // 
            this.toolStripCenter.Name = "toolStripCenter";
            resources.ApplyResources(this.toolStripCenter, "toolStripCenter");
            this.toolStripCenter.Spring = true;
            // 
            // toolStripRight
            // 
            this.toolStripRight.Name = "toolStripRight";
            resources.ApplyResources(this.toolStripRight, "toolStripRight");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.manualexportbtn);
            this.groupBox1.Controls.Add(this.btncheckconn);
            this.groupBox1.Controls.Add(this.tlabelPort);
            this.groupBox1.Controls.Add(this.labelIPAdress);
            this.groupBox1.Controls.Add(this.textBoxPort);
            this.groupBox1.Controls.Add(this.textBoxIP);
            this.groupBox1.Controls.Add(this.checkBoxManualIP);
            this.groupBox1.Controls.Add(this.btnGDFolder);
            this.groupBox1.Controls.Add(this.lblGDFolder);
            this.groupBox1.Controls.Add(this.radioButtonGD);
            this.groupBox1.Controls.Add(this.btnDBFolder);
            this.groupBox1.Controls.Add(this.lblDBFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.radioButtonWIFI);
            this.groupBox1.Controls.Add(this.radioButtonDB);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // manualexportbtn
            // 
            resources.ApplyResources(this.manualexportbtn, "manualexportbtn");
            this.manualexportbtn.Name = "manualexportbtn";
            this.manualexportbtn.UseVisualStyleBackColor = true;
            this.manualexportbtn.Click += new System.EventHandler(this.manualexportbtn_Click);
            // 
            // btncheckconn
            // 
            resources.ApplyResources(this.btncheckconn, "btncheckconn");
            this.btncheckconn.Name = "btncheckconn";
            this.btncheckconn.UseVisualStyleBackColor = true;
            this.btncheckconn.Click += new System.EventHandler(this.btncheckconn_Click);
            // 
            // tlabelPort
            // 
            resources.ApplyResources(this.tlabelPort, "tlabelPort");
            this.tlabelPort.Name = "tlabelPort";
            // 
            // labelIPAdress
            // 
            resources.ApplyResources(this.labelIPAdress, "labelIPAdress");
            this.labelIPAdress.Name = "labelIPAdress";
            // 
            // textBoxPort
            // 
            resources.ApplyResources(this.textBoxPort, "textBoxPort");
            this.textBoxPort.Name = "textBoxPort";
            // 
            // textBoxIP
            // 
            resources.ApplyResources(this.textBoxIP, "textBoxIP");
            this.textBoxIP.Name = "textBoxIP";
            // 
            // checkBoxManualIP
            // 
            resources.ApplyResources(this.checkBoxManualIP, "checkBoxManualIP");
            this.checkBoxManualIP.Name = "checkBoxManualIP";
            this.checkBoxManualIP.UseVisualStyleBackColor = true;
            this.checkBoxManualIP.CheckedChanged += new System.EventHandler(this.checkBoxManualIP_CheckedChanged);
            // 
            // btnGDFolder
            // 
            resources.ApplyResources(this.btnGDFolder, "btnGDFolder");
            this.btnGDFolder.Name = "btnGDFolder";
            this.btnGDFolder.UseVisualStyleBackColor = true;
            this.btnGDFolder.Click += new System.EventHandler(this.btnGDFolder_Click);
            // 
            // lblGDFolder
            // 
            resources.ApplyResources(this.lblGDFolder, "lblGDFolder");
            this.lblGDFolder.Name = "lblGDFolder";
            // 
            // radioButtonGD
            // 
            resources.ApplyResources(this.radioButtonGD, "radioButtonGD");
            this.radioButtonGD.Name = "radioButtonGD";
            this.radioButtonGD.TabStop = true;
            this.radioButtonGD.UseVisualStyleBackColor = true;
            this.radioButtonGD.CheckedChanged += new System.EventHandler(this.radioButtonGD_CheckedChanged);
            // 
            // btnDBFolder
            // 
            resources.ApplyResources(this.btnDBFolder, "btnDBFolder");
            this.btnDBFolder.Name = "btnDBFolder";
            this.btnDBFolder.UseVisualStyleBackColor = true;
            this.btnDBFolder.Click += new System.EventHandler(this.btnDBFolder_Click);
            // 
            // lblDBFolder
            // 
            resources.ApplyResources(this.lblDBFolder, "lblDBFolder");
            this.lblDBFolder.Name = "lblDBFolder";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // radioButtonWIFI
            // 
            resources.ApplyResources(this.radioButtonWIFI, "radioButtonWIFI");
            this.radioButtonWIFI.Name = "radioButtonWIFI";
            this.radioButtonWIFI.TabStop = true;
            this.radioButtonWIFI.UseVisualStyleBackColor = true;
            this.radioButtonWIFI.CheckedChanged += new System.EventHandler(this.radioButtonWIFI_CheckedChanged);
            // 
            // radioButtonDB
            // 
            resources.ApplyResources(this.radioButtonDB, "radioButtonDB");
            this.radioButtonDB.Name = "radioButtonDB";
            this.radioButtonDB.TabStop = true;
            this.radioButtonDB.UseVisualStyleBackColor = true;
            this.radioButtonDB.CheckedChanged += new System.EventHandler(this.radioButtonDB_CheckedChanged);
            // 
            // listBoxEAN
            // 
            this.listBoxEAN.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxEAN, "listBoxEAN");
            this.listBoxEAN.Name = "listBoxEAN";
            // 
            // buttonDELEAN
            // 
            resources.ApplyResources(this.buttonDELEAN, "buttonDELEAN");
            this.buttonDELEAN.Name = "buttonDELEAN";
            this.buttonDELEAN.UseVisualStyleBackColor = true;
            // 
            // buttonloadimport
            // 
            resources.ApplyResources(this.buttonloadimport, "buttonloadimport");
            this.buttonloadimport.Name = "buttonloadimport";
            this.buttonloadimport.UseVisualStyleBackColor = true;
            this.buttonloadimport.Click += new System.EventHandler(this.buttonloadimport_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::jbplugin.Properties.Resources.Header1;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // btnlogfiles
            // 
            resources.ApplyResources(this.btnlogfiles, "btnlogfiles");
            this.btnlogfiles.Name = "btnlogfiles";
            this.btnlogfiles.UseVisualStyleBackColor = true;
            this.btnlogfiles.Click += new System.EventHandler(this.btnlogfiles_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCommon);
            this.tabControl1.Controls.Add(this.tabEAN);
            this.tabControl1.Controls.Add(this.tabSettings);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tabCommon
            // 
            this.tabCommon.Controls.Add(this.groupBox1);
            this.tabCommon.Controls.Add(this.label1);
            resources.ApplyResources(this.tabCommon, "tabCommon");
            this.tabCommon.Name = "tabCommon";
            this.tabCommon.UseVisualStyleBackColor = true;
            // 
            // tabEAN
            // 
            this.tabEAN.Controls.Add(this.btnautoimportean);
            this.tabEAN.Controls.Add(this.listBoxEAN);
            this.tabEAN.Controls.Add(this.buttonloadimport);
            this.tabEAN.Controls.Add(this.buttonDELEAN);
            resources.ApplyResources(this.tabEAN, "tabEAN");
            this.tabEAN.Name = "tabEAN";
            this.tabEAN.UseVisualStyleBackColor = true;
            // 
            // btnautoimportean
            // 
            resources.ApplyResources(this.btnautoimportean, "btnautoimportean");
            this.btnautoimportean.Name = "btnautoimportean";
            this.btnautoimportean.UseVisualStyleBackColor = true;
            this.btnautoimportean.Click += new System.EventHandler(this.btnautoimportean_Click);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.checkBoxAutostartExport);
            this.tabSettings.Controls.Add(this.labelclb2);
            this.tabSettings.Controls.Add(this.labelclb1);
            this.tabSettings.Controls.Add(this.checkedListBoxProfilerDBs);
            this.tabSettings.Controls.Add(this.maxretryonnetworkerror);
            this.tabSettings.Controls.Add(this.label4);
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.maxprofilesperzip);
            this.tabSettings.Controls.Add(this.btnlogfiles);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutostartExport
            // 
            resources.ApplyResources(this.checkBoxAutostartExport, "checkBoxAutostartExport");
            this.checkBoxAutostartExport.Name = "checkBoxAutostartExport";
            this.checkBoxAutostartExport.UseVisualStyleBackColor = true;
            this.checkBoxAutostartExport.CheckedChanged += new System.EventHandler(this.checkBoxAutostartExport_CheckedChanged);
            // 
            // labelclb2
            // 
            resources.ApplyResources(this.labelclb2, "labelclb2");
            this.labelclb2.Name = "labelclb2";
            // 
            // labelclb1
            // 
            resources.ApplyResources(this.labelclb1, "labelclb1");
            this.labelclb1.Name = "labelclb1";
            // 
            // checkedListBoxProfilerDBs
            // 
            this.checkedListBoxProfilerDBs.FormattingEnabled = true;
            resources.ApplyResources(this.checkedListBoxProfilerDBs, "checkedListBoxProfilerDBs");
            this.checkedListBoxProfilerDBs.Name = "checkedListBoxProfilerDBs";
            this.checkedListBoxProfilerDBs.SelectedIndexChanged += new System.EventHandler(this.checkedListBoxProfilerDBs_SelectedIndexChanged);
            this.checkedListBoxProfilerDBs.SelectedValueChanged += new System.EventHandler(this.checkedListBoxProfilerDBs_SelectedValueChanged);
            // 
            // maxretryonnetworkerror
            // 
            resources.ApplyResources(this.maxretryonnetworkerror, "maxretryonnetworkerror");
            this.maxretryonnetworkerror.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.maxretryonnetworkerror.Name = "maxretryonnetworkerror";
            this.maxretryonnetworkerror.ValueChanged += new System.EventHandler(this.maxretryonnetworkerror_ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // maxprofilesperzip
            // 
            resources.ApplyResources(this.maxprofilesperzip, "maxprofilesperzip");
            this.maxprofilesperzip.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxprofilesperzip.Name = "maxprofilesperzip";
            this.maxprofilesperzip.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.maxprofilesperzip.ValueChanged += new System.EventHandler(this.maxprofilesperzip_ValueChanged);
            // 
            // MainForm2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm1_FormClosed);
            this.Shown += new System.EventHandler(this.MainForm2_Shown);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabCommon.ResumeLayout(false);
            this.tabEAN.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxretryonnetworkerror)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxprofilesperzip)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonWIFI;
        private System.Windows.Forms.RadioButton radioButtonDB;
        private System.Windows.Forms.ListBox listBoxEAN;
        private System.Windows.Forms.Button buttonDELEAN;
        private System.Windows.Forms.Button buttonloadimport;
        private System.Windows.Forms.Label lblDBFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDBFolder;
        private System.Windows.Forms.Button btnGDFolder;
        private System.Windows.Forms.RadioButton radioButtonGD;
        private System.Windows.Forms.CheckBox checkBoxManualIP;
        private System.Windows.Forms.Label tlabelPort;
        private System.Windows.Forms.Label labelIPAdress;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label lblGDFolder;
        private System.Windows.Forms.Button btncheckconn;
        private System.Windows.Forms.Button manualexportbtn;
        private System.Windows.Forms.Button btnlogfiles;
        private System.Windows.Forms.ToolStripStatusLabel toolStripCenter;
        private System.Windows.Forms.ToolStripStatusLabel toolStripRight;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCommon;
        private System.Windows.Forms.TabPage tabEAN;
        private System.Windows.Forms.Button btnautoimportean;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.NumericUpDown maxretryonnetworkerror;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown maxprofilesperzip;
        private System.Windows.Forms.Label labelclb2;
        private System.Windows.Forms.Label labelclb1;
        private System.Windows.Forms.CheckedListBox checkedListBoxProfilerDBs;
        private System.Windows.Forms.CheckBox checkBoxAutostartExport;
    }
}