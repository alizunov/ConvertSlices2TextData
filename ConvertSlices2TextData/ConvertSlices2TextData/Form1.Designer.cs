﻿namespace ConvertSlices2TextData
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
            this.SuspendLayout();
            // 
            // OpenImages_button
            // 
            this.OpenImages_button.Location = new System.Drawing.Point(97, 12);
            this.OpenImages_button.Name = "OpenImages_button";
            this.OpenImages_button.Size = new System.Drawing.Size(89, 23);
            this.OpenImages_button.TabIndex = 0;
            this.OpenImages_button.Text = "Open Images";
            this.OpenImages_button.UseVisualStyleBackColor = true;
            this.OpenImages_button.Click += new System.EventHandler(this.OpenImages_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.OpenImages_button);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OpenImages_button;
    }
}

