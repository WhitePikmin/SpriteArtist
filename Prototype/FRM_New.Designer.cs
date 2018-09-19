namespace Prototype
{
    partial class FRM_New
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
            this.label3 = new System.Windows.Forms.Label();
            this.pixels = new System.Windows.Forms.Label();
            this.NUM_Height = new System.Windows.Forms.NumericUpDown();
            this.NUM_Width = new System.Windows.Forms.NumericUpDown();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            this.BTN_Create = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Width)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.pixels);
            this.groupBox1.Controls.Add(this.NUM_Height);
            this.groupBox1.Controls.Add(this.NUM_Width);
            this.groupBox1.Controls.Add(this.BTN_Cancel);
            this.groupBox1.Controls.Add(this.BTN_Create);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nouveau Canevas";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "pixels";
            // 
            // pixels
            // 
            this.pixels.AutoSize = true;
            this.pixels.Location = new System.Drawing.Point(139, 31);
            this.pixels.Name = "pixels";
            this.pixels.Size = new System.Drawing.Size(33, 13);
            this.pixels.TabIndex = 8;
            this.pixels.Text = "pixels";
            // 
            // NUM_Height
            // 
            this.NUM_Height.Location = new System.Drawing.Point(68, 51);
            this.NUM_Height.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUM_Height.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUM_Height.Name = "NUM_Height";
            this.NUM_Height.Size = new System.Drawing.Size(64, 20);
            this.NUM_Height.TabIndex = 7;
            this.NUM_Height.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.NUM_Height.ValueChanged += new System.EventHandler(this.NUM_Height_ValueChanged);
            // 
            // NUM_Width
            // 
            this.NUM_Width.Location = new System.Drawing.Point(68, 25);
            this.NUM_Width.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.NUM_Width.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NUM_Width.Name = "NUM_Width";
            this.NUM_Width.Size = new System.Drawing.Size(64, 20);
            this.NUM_Width.TabIndex = 6;
            this.NUM_Width.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.NUM_Width.ValueChanged += new System.EventHandler(this.NUM_Width_ValueChanged);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(93, 96);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BTN_Cancel.TabIndex = 5;
            this.BTN_Cancel.Text = "Annuler";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // BTN_Create
            // 
            this.BTN_Create.Location = new System.Drawing.Point(6, 96);
            this.BTN_Create.Name = "BTN_Create";
            this.BTN_Create.Size = new System.Drawing.Size(75, 23);
            this.BTN_Create.TabIndex = 4;
            this.BTN_Create.Text = "Créer";
            this.BTN_Create.UseVisualStyleBackColor = true;
            this.BTN_Create.Click += new System.EventHandler(this.BTN_Create_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hauteur:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Largeur:";
            // 
            // FRM_New
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(220, 150);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FRM_New";
            this.Text = "Nouveau Canevas";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NUM_Width)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.Button BTN_Create;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pixels;
        private System.Windows.Forms.NumericUpDown NUM_Height;
        private System.Windows.Forms.NumericUpDown NUM_Width;
    }
}