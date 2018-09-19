namespace Prototype
{
    partial class FRM_Intro
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
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_New = new System.Windows.Forms.Button();
            this.BTN_Open = new System.Windows.Forms.Button();
            this.BTN_Quit = new System.Windows.Forms.Button();
            this.DLG_Open = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bienvenu sur Sprite Artist!";
            // 
            // BTN_New
            // 
            this.BTN_New.Location = new System.Drawing.Point(16, 30);
            this.BTN_New.Name = "BTN_New";
            this.BTN_New.Size = new System.Drawing.Size(224, 42);
            this.BTN_New.TabIndex = 1;
            this.BTN_New.Text = "Nouveau Canevas";
            this.BTN_New.UseVisualStyleBackColor = true;
            this.BTN_New.Click += new System.EventHandler(this.BTN_New_Click);
            // 
            // BTN_Open
            // 
            this.BTN_Open.Location = new System.Drawing.Point(16, 76);
            this.BTN_Open.Name = "BTN_Open";
            this.BTN_Open.Size = new System.Drawing.Size(224, 42);
            this.BTN_Open.TabIndex = 2;
            this.BTN_Open.Text = "Ouvrir Fichier";
            this.BTN_Open.UseVisualStyleBackColor = true;
            this.BTN_Open.Click += new System.EventHandler(this.BTN_Open_Click);
            // 
            // BTN_Quit
            // 
            this.BTN_Quit.Location = new System.Drawing.Point(16, 124);
            this.BTN_Quit.Name = "BTN_Quit";
            this.BTN_Quit.Size = new System.Drawing.Size(224, 42);
            this.BTN_Quit.TabIndex = 3;
            this.BTN_Quit.Text = "Quitter";
            this.BTN_Quit.UseVisualStyleBackColor = true;
            this.BTN_Quit.Click += new System.EventHandler(this.BTN_Quit_Click);
            // 
            // DLG_Open
            // 
            this.DLG_Open.FileName = "Sprite";
            this.DLG_Open.Filter = "Image PNG|*.png|Image Bitmap |*.bmp|Image GIF |*.gif|Tout les fichiers |*.*";
            this.DLG_Open.Title = "Ouvrir une image";
            // 
            // FRM_Intro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 178);
            this.Controls.Add(this.BTN_Quit);
            this.Controls.Add(this.BTN_Open);
            this.Controls.Add(this.BTN_New);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FRM_Intro";
            this.Text = "Sprite Artist";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BTN_New;
        private System.Windows.Forms.Button BTN_Open;
        private System.Windows.Forms.Button BTN_Quit;
        private System.Windows.Forms.OpenFileDialog DLG_Open;
    }
}