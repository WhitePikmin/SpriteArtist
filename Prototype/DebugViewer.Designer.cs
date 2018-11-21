namespace SpriteArtist
{
    partial class DebugViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DebugViewer));
            this.LBL_Values = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LBL_Values
            // 
            this.LBL_Values.AutoSize = true;
            this.LBL_Values.Location = new System.Drawing.Point(13, 13);
            this.LBL_Values.Name = "LBL_Values";
            this.LBL_Values.Size = new System.Drawing.Size(64, 13);
            this.LBL_Values.TabIndex = 0;
            this.LBL_Values.Text = "LBL_Values";
            // 
            // DebugViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 193);
            this.Controls.Add(this.LBL_Values);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DebugViewer";
            this.Text = "DebugViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LBL_Values;
    }
}