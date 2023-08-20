namespace ControlDemo
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboOptions = new System.Windows.Forms.ComboBox();
            this.btnDisplay = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.lstLayer = new System.Windows.Forms.ListBox();
            this.lstLineType = new System.Windows.Forms.ListBox();
            this.lstTextStyle = new System.Windows.Forms.ListBox();
            this.lblLayerCount = new System.Windows.Forms.Label();
            this.lblLinetypeCount = new System.Windows.Forms.Label();
            this.lblTextStyleCount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClearAll);
            this.groupBox1.Controls.Add(this.btnDisplay);
            this.groupBox1.Controls.Add(this.cboOptions);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(53, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 399);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Options Container";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTextStyleCount);
            this.groupBox2.Controls.Add(this.lblLinetypeCount);
            this.groupBox2.Controls.Add(this.lblLayerCount);
            this.groupBox2.Controls.Add(this.lstTextStyle);
            this.groupBox2.Controls.Add(this.lstLineType);
            this.groupBox2.Controls.Add(this.lstLayer);
            this.groupBox2.Location = new System.Drawing.Point(344, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(384, 399);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display Container";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select item to display:";
            // 
            // cboOptions
            // 
            this.cboOptions.FormattingEnabled = true;
            this.cboOptions.Items.AddRange(new object[] {
            "All",
            "Layer",
            "Linetype",
            "TextStyle"});
            this.cboOptions.Location = new System.Drawing.Point(145, 30);
            this.cboOptions.Name = "cboOptions";
            this.cboOptions.Size = new System.Drawing.Size(121, 21);
            this.cboOptions.TabIndex = 1;
            // 
            // btnDisplay
            // 
            this.btnDisplay.Location = new System.Drawing.Point(32, 362);
            this.btnDisplay.Name = "btnDisplay";
            this.btnDisplay.Size = new System.Drawing.Size(75, 23);
            this.btnDisplay.TabIndex = 2;
            this.btnDisplay.Text = "Show Itens";
            this.btnDisplay.UseVisualStyleBackColor = true;
            this.btnDisplay.Click += new System.EventHandler(this.btnDisplay_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(178, 362);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 3;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            // 
            // lstLayer
            // 
            this.lstLayer.FormattingEnabled = true;
            this.lstLayer.Location = new System.Drawing.Point(6, 30);
            this.lstLayer.Name = "lstLayer";
            this.lstLayer.Size = new System.Drawing.Size(120, 329);
            this.lstLayer.TabIndex = 0;
            // 
            // lstLineType
            // 
            this.lstLineType.FormattingEnabled = true;
            this.lstLineType.Location = new System.Drawing.Point(132, 30);
            this.lstLineType.Name = "lstLineType";
            this.lstLineType.Size = new System.Drawing.Size(120, 329);
            this.lstLineType.TabIndex = 1;
            // 
            // lstTextStyle
            // 
            this.lstTextStyle.FormattingEnabled = true;
            this.lstTextStyle.Location = new System.Drawing.Point(258, 30);
            this.lstTextStyle.Name = "lstTextStyle";
            this.lstTextStyle.Size = new System.Drawing.Size(120, 329);
            this.lstTextStyle.TabIndex = 2;
            // 
            // lblLayerCount
            // 
            this.lblLayerCount.AutoSize = true;
            this.lblLayerCount.Location = new System.Drawing.Point(6, 362);
            this.lblLayerCount.Name = "lblLayerCount";
            this.lblLayerCount.Size = new System.Drawing.Size(16, 13);
            this.lblLayerCount.TabIndex = 2;
            this.lblLayerCount.Text = "...";
            // 
            // lblLinetypeCount
            // 
            this.lblLinetypeCount.AutoSize = true;
            this.lblLinetypeCount.Location = new System.Drawing.Point(132, 366);
            this.lblLinetypeCount.Name = "lblLinetypeCount";
            this.lblLinetypeCount.Size = new System.Drawing.Size(16, 13);
            this.lblLinetypeCount.TabIndex = 3;
            this.lblLinetypeCount.Text = "...";
            // 
            // lblTextStyleCount
            // 
            this.lblTextStyleCount.AutoSize = true;
            this.lblTextStyleCount.Location = new System.Drawing.Point(258, 362);
            this.lblTextStyleCount.Name = "lblTextStyleCount";
            this.lblTextStyleCount.Size = new System.Drawing.Size(16, 13);
            this.lblTextStyleCount.TabIndex = 4;
            this.lblTextStyleCount.Text = "...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 478);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainForm";
            this.Text = "Main Form";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboOptions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.Button btnDisplay;
        private System.Windows.Forms.Label lblLayerCount;
        private System.Windows.Forms.ListBox lstTextStyle;
        private System.Windows.Forms.ListBox lstLineType;
        private System.Windows.Forms.ListBox lstLayer;
        private System.Windows.Forms.Label lblTextStyleCount;
        private System.Windows.Forms.Label lblLinetypeCount;
    }
}