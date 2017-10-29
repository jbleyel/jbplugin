namespace Exporter
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.lblModus = new System.Windows.Forms.Label();
            this.backgroundWorkerWiFi = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerDB = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorkerGD = new System.ComponentModel.BackgroundWorker();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblclosetimer = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.FindWorker = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // lblStatus
            // 
            resources.ApplyResources(this.lblStatus, "lblStatus");
            this.lblStatus.Name = "lblStatus";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblModus
            // 
            resources.ApplyResources(this.lblModus, "lblModus");
            this.lblModus.Name = "lblModus";
            // 
            // backgroundWorkerWiFi
            // 
            this.backgroundWorkerWiFi.WorkerSupportsCancellation = true;
            this.backgroundWorkerWiFi.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerWiFi_DoWork);
            this.backgroundWorkerWiFi.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerWiFi_RunWorkerCompleted);
            // 
            // backgroundWorkerDB
            // 
            this.backgroundWorkerDB.WorkerSupportsCancellation = true;
            this.backgroundWorkerDB.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerDB_DoWork);
            this.backgroundWorkerDB.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerDB_RunWorkerCompleted);
            // 
            // backgroundWorkerGD
            // 
            this.backgroundWorkerGD.WorkerSupportsCancellation = true;
            this.backgroundWorkerGD.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerGD_DoWork);
            this.backgroundWorkerGD.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerGD_RunWorkerCompleted);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblclosetimer
            // 
            resources.ApplyResources(this.lblclosetimer, "lblclosetimer");
            this.lblclosetimer.Name = "lblclosetimer";
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FindWorker
            // 
            this.FindWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.FindWorker_DoWork);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblclosetimer);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblModus);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblModus;
        private System.ComponentModel.BackgroundWorker backgroundWorkerWiFi;
        private System.ComponentModel.BackgroundWorker backgroundWorkerDB;
        private System.ComponentModel.BackgroundWorker backgroundWorkerGD;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblclosetimer;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker FindWorker;
    }
}

