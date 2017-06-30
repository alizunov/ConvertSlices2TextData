namespace ConvertSlices2TextData
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
            this.OpenImages_button = new System.Windows.Forms.Button();
            this.FOVX_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.FOVX_label = new System.Windows.Forms.Label();
            this.FOVY_label = new System.Windows.Forms.Label();
            this.FOVY_numericUpDown = new System.Windows.Forms.NumericUpDown();
            this.SubROIY_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FOVX_numericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVY_numericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenImages_button
            // 
            this.OpenImages_button.ForeColor = System.Drawing.SystemColors.Highlight;
            this.OpenImages_button.Location = new System.Drawing.Point(129, 15);
            this.OpenImages_button.Margin = new System.Windows.Forms.Padding(4);
            this.OpenImages_button.Name = "OpenImages_button";
            this.OpenImages_button.Size = new System.Drawing.Size(155, 28);
            this.OpenImages_button.TabIndex = 0;
            this.OpenImages_button.Text = "Open Image Headers";
            this.OpenImages_button.UseVisualStyleBackColor = true;
            this.OpenImages_button.Click += new System.EventHandler(this.OpenImages_button_Click);
            // 
            // FOVX_numericUpDown
            // 
            this.FOVX_numericUpDown.Location = new System.Drawing.Point(12, 83);
            this.FOVX_numericUpDown.Name = "FOVX_numericUpDown";
            this.FOVX_numericUpDown.Size = new System.Drawing.Size(97, 22);
            this.FOVX_numericUpDown.TabIndex = 1;
            this.FOVX_numericUpDown.ValueChanged += new System.EventHandler(this.FOVX_numericUpDown_ValueChanged);
            // 
            // FOVX_label
            // 
            this.FOVX_label.AutoSize = true;
            this.FOVX_label.Location = new System.Drawing.Point(13, 60);
            this.FOVX_label.Name = "FOVX_label";
            this.FOVX_label.Size = new System.Drawing.Size(50, 17);
            this.FOVX_label.TabIndex = 2;
            this.FOVX_label.Text = "FOV-X";
            this.FOVX_label.Click += new System.EventHandler(this.FOVX_label_Click);
            // 
            // FOVY_label
            // 
            this.FOVY_label.AutoSize = true;
            this.FOVY_label.Location = new System.Drawing.Point(12, 112);
            this.FOVY_label.Name = "FOVY_label";
            this.FOVY_label.Size = new System.Drawing.Size(50, 17);
            this.FOVY_label.TabIndex = 3;
            this.FOVY_label.Text = "FOV-Y";
            this.FOVY_label.Click += new System.EventHandler(this.FOVY_label_Click);
            // 
            // FOVY_numericUpDown
            // 
            this.FOVY_numericUpDown.Location = new System.Drawing.Point(12, 133);
            this.FOVY_numericUpDown.Name = "FOVY_numericUpDown";
            this.FOVY_numericUpDown.Size = new System.Drawing.Size(97, 22);
            this.FOVY_numericUpDown.TabIndex = 4;
            this.FOVY_numericUpDown.ValueChanged += new System.EventHandler(this.FOVY_numericUpDown_ValueChanged);
            // 
            // SubROIY_comboBox
            // 
            this.SubROIY_comboBox.FormattingEnabled = true;
            this.SubROIY_comboBox.Location = new System.Drawing.Point(129, 131);
            this.SubROIY_comboBox.Name = "SubROIY_comboBox";
            this.SubROIY_comboBox.Size = new System.Drawing.Size(96, 24);
            this.SubROIY_comboBox.TabIndex = 5;
            this.SubROIY_comboBox.Text = "Y-size (px)";
            this.SubROIY_comboBox.SelectedIndexChanged += new System.EventHandler(this.SubROIY_comboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(129, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Y-size of sub-ROI";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 322);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SubROIY_comboBox);
            this.Controls.Add(this.FOVY_numericUpDown);
            this.Controls.Add(this.FOVY_label);
            this.Controls.Add(this.FOVX_label);
            this.Controls.Add(this.FOVX_numericUpDown);
            this.Controls.Add(this.OpenImages_button);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.FOVX_numericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FOVY_numericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenImages_button;
        private System.Windows.Forms.NumericUpDown FOVX_numericUpDown;
        private System.Windows.Forms.Label FOVX_label;
        private System.Windows.Forms.Label FOVY_label;
        private System.Windows.Forms.NumericUpDown FOVY_numericUpDown;
        private System.Windows.Forms.ComboBox SubROIY_comboBox;
        private System.Windows.Forms.Label label1;
    }
}

