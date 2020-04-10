namespace restClient_0
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
            this.cmdGO = new System.Windows.Forms.Button();
            this.txtRequestURI = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comBoxAction = new System.Windows.Forms.ComboBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.CBMirConnection = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbGUI = new System.Windows.Forms.RadioButton();
            this.rbTwinCat = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdGO
            // 
            this.cmdGO.Location = new System.Drawing.Point(515, 15);
            this.cmdGO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmdGO.Name = "cmdGO";
            this.cmdGO.Size = new System.Drawing.Size(100, 28);
            this.cmdGO.TabIndex = 0;
            this.cmdGO.Text = "GO!";
            this.cmdGO.UseVisualStyleBackColor = true;
            this.cmdGO.Click += new System.EventHandler(this.cmdGO_Click);
            // 
            // txtRequestURI
            // 
            this.txtRequestURI.Location = new System.Drawing.Point(141, 15);
            this.txtRequestURI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRequestURI.Name = "txtRequestURI";
            this.txtRequestURI.Size = new System.Drawing.Size(341, 22);
            this.txtRequestURI.TabIndex = 1;
            this.txtRequestURI.Text = "https://jsonplaceholder.typicode.com/posts";
            this.txtRequestURI.TextChanged += new System.EventHandler(this.txtRequestURI_TextChanged);
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(141, 224);
            this.txtResponse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtResponse.Multiline = true;
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResponse.Size = new System.Drawing.Size(472, 197);
            this.txtResponse.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Request URI:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 228);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Response:";
            // 
            // comBoxAction
            // 
            this.comBoxAction.FormattingEnabled = true;
            this.comBoxAction.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE"});
            this.comBoxAction.Location = new System.Drawing.Point(25, 57);
            this.comBoxAction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comBoxAction.Name = "comBoxAction";
            this.comBoxAction.Size = new System.Drawing.Size(95, 24);
            this.comBoxAction.TabIndex = 5;
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(151, 57);
            this.tbID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(83, 22);
            this.tbID.TabIndex = 6;
            this.tbID.Text = "ID";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(259, 57);
            this.tbValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(83, 22);
            this.tbValue.TabIndex = 7;
            this.tbValue.Text = "Value";
            // 
            // CBMirConnection
            // 
            this.CBMirConnection.AutoSize = true;
            this.CBMirConnection.Location = new System.Drawing.Point(141, 47);
            this.CBMirConnection.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CBMirConnection.Name = "CBMirConnection";
            this.CBMirConnection.Size = new System.Drawing.Size(138, 21);
            this.CBMirConnection.TabIndex = 8;
            this.CBMirConnection.Text = "MiR - Connection";
            this.CBMirConnection.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbGUI);
            this.groupBox1.Controls.Add(this.rbTwinCat);
            this.groupBox1.Controls.Add(this.tbValue);
            this.groupBox1.Controls.Add(this.tbID);
            this.groupBox1.Controls.Add(this.comBoxAction);
            this.groupBox1.Location = new System.Drawing.Point(141, 75);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(473, 105);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MiR - Connection";
            // 
            // rbGUI
            // 
            this.rbGUI.AutoSize = true;
            this.rbGUI.Location = new System.Drawing.Point(151, 23);
            this.rbGUI.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbGUI.Name = "rbGUI";
            this.rbGUI.Size = new System.Drawing.Size(53, 21);
            this.rbGUI.TabIndex = 9;
            this.rbGUI.Text = "GUI";
            this.rbGUI.UseVisualStyleBackColor = true;
            // 
            // rbTwinCat
            // 
            this.rbTwinCat.AutoSize = true;
            this.rbTwinCat.Checked = true;
            this.rbTwinCat.Location = new System.Drawing.Point(25, 23);
            this.rbTwinCat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbTwinCat.Name = "rbTwinCat";
            this.rbTwinCat.Size = new System.Drawing.Size(79, 21);
            this.rbTwinCat.TabIndex = 8;
            this.rbTwinCat.TabStop = true;
            this.rbTwinCat.Text = "TwinCat";
            this.rbTwinCat.UseVisualStyleBackColor = true;
            this.rbTwinCat.CheckedChanged += new System.EventHandler(this.RbTwinCat_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(141, 448);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(183, 28);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 496);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CBMirConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtResponse);
            this.Controls.Add(this.txtRequestURI);
            this.Controls.Add(this.cmdGO);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximumSize = new System.Drawing.Size(661, 543);
            this.MinimumSize = new System.Drawing.Size(661, 543);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MiRInterface";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdGO;
        private System.Windows.Forms.TextBox txtRequestURI;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comBoxAction;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.CheckBox CBMirConnection;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbGUI;
        private System.Windows.Forms.RadioButton rbTwinCat;
        private System.Windows.Forms.Button btnStart;
    }
}

