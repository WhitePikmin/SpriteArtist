namespace SpriteArtist
{
    partial class FRM_SendImage
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
            this.PBX_Preview = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TBX_Password = new System.Windows.Forms.TextBox();
            this.TBX_Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LBL_Create_Account = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_remove_tag = new System.Windows.Forms.Button();
            this.Btn_add_tag = new System.Windows.Forms.Button();
            this.LBx_Unused = new System.Windows.Forms.ListBox();
            this.LBx_Used = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TBX_Description = new System.Windows.Forms.RichTextBox();
            this.TBX_Title = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BTN_Send = new System.Windows.Forms.Button();
            this.BTN_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Preview)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // PBX_Preview
            // 
            this.PBX_Preview.BackgroundImage = global::SpriteArtist.Properties.Resources.checkerboard;
            this.PBX_Preview.Location = new System.Drawing.Point(276, 44);
            this.PBX_Preview.Name = "PBX_Preview";
            this.PBX_Preview.Size = new System.Drawing.Size(369, 349);
            this.PBX_Preview.TabIndex = 0;
            this.PBX_Preview.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Envoyer votre image sur SpriteArtist.com!";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TBX_Password);
            this.groupBox1.Controls.Add(this.TBX_Username);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.LBL_Create_Account);
            this.groupBox1.Location = new System.Drawing.Point(12, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(258, 145);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informations de compte";
            // 
            // TBX_Password
            // 
            this.TBX_Password.Location = new System.Drawing.Point(10, 72);
            this.TBX_Password.Name = "TBX_Password";
            this.TBX_Password.PasswordChar = '*';
            this.TBX_Password.Size = new System.Drawing.Size(156, 20);
            this.TBX_Password.TabIndex = 4;
            this.TBX_Password.TextChanged += new System.EventHandler(this.TBX_Password_TextChanged);
            // 
            // TBX_Username
            // 
            this.TBX_Username.Location = new System.Drawing.Point(10, 33);
            this.TBX_Username.Name = "TBX_Username";
            this.TBX_Username.Size = new System.Drawing.Size(156, 20);
            this.TBX_Username.TabIndex = 3;
            this.TBX_Username.TextChanged += new System.EventHandler(this.TBX_Username_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mot de passe:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Pseudonyme:";
            // 
            // LBL_Create_Account
            // 
            this.LBL_Create_Account.AutoSize = true;
            this.LBL_Create_Account.Location = new System.Drawing.Point(7, 116);
            this.LBL_Create_Account.Name = "LBL_Create_Account";
            this.LBL_Create_Account.Size = new System.Drawing.Size(160, 13);
            this.LBL_Create_Account.TabIndex = 0;
            this.LBL_Create_Account.TabStop = true;
            this.LBL_Create_Account.Text = "Pas de compte? Créez en un ici!";
            this.LBL_Create_Account.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LBL_Create_Account_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_remove_tag);
            this.groupBox2.Controls.Add(this.Btn_add_tag);
            this.groupBox2.Controls.Add(this.LBx_Unused);
            this.groupBox2.Controls.Add(this.LBx_Used);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TBX_Description);
            this.groupBox2.Controls.Add(this.TBX_Title);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(13, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 242);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Information de l\'image";
            // 
            // Btn_remove_tag
            // 
            this.Btn_remove_tag.Location = new System.Drawing.Point(117, 190);
            this.Btn_remove_tag.Name = "Btn_remove_tag";
            this.Btn_remove_tag.Size = new System.Drawing.Size(29, 23);
            this.Btn_remove_tag.TabIndex = 11;
            this.Btn_remove_tag.Text = ">>";
            this.Btn_remove_tag.UseVisualStyleBackColor = true;
            this.Btn_remove_tag.Click += new System.EventHandler(this.Btn_remove_tag_Click);
            // 
            // Btn_add_tag
            // 
            this.Btn_add_tag.Location = new System.Drawing.Point(117, 161);
            this.Btn_add_tag.Name = "Btn_add_tag";
            this.Btn_add_tag.Size = new System.Drawing.Size(29, 23);
            this.Btn_add_tag.TabIndex = 11;
            this.Btn_add_tag.Text = "<<";
            this.Btn_add_tag.UseVisualStyleBackColor = true;
            this.Btn_add_tag.Click += new System.EventHandler(this.Btn_add_tag_Click);
            // 
            // LBx_Unused
            // 
            this.LBx_Unused.FormattingEnabled = true;
            this.LBx_Unused.Location = new System.Drawing.Point(160, 162);
            this.LBx_Unused.Name = "LBx_Unused";
            this.LBx_Unused.Size = new System.Drawing.Size(91, 69);
            this.LBx_Unused.TabIndex = 10;
            // 
            // LBx_Used
            // 
            this.LBx_Used.FormattingEnabled = true;
            this.LBx_Used.Location = new System.Drawing.Point(12, 162);
            this.LBx_Used.Name = "LBx_Used";
            this.LBx_Used.Size = new System.Drawing.Size(91, 69);
            this.LBx_Used.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(139, 146);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Étiquettes disponibles:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Étiquettes utilisées:";
            // 
            // TBX_Description
            // 
            this.TBX_Description.Location = new System.Drawing.Point(9, 79);
            this.TBX_Description.Name = "TBX_Description";
            this.TBX_Description.Size = new System.Drawing.Size(242, 64);
            this.TBX_Description.TabIndex = 8;
            this.TBX_Description.Text = "";
            this.TBX_Description.TextChanged += new System.EventHandler(this.TBX_Description_TextChanged);
            // 
            // TBX_Title
            // 
            this.TBX_Title.Location = new System.Drawing.Point(9, 39);
            this.TBX_Title.Name = "TBX_Title";
            this.TBX_Title.Size = new System.Drawing.Size(242, 20);
            this.TBX_Title.TabIndex = 7;
            this.TBX_Title.TextChanged += new System.EventHandler(this.TBX_Title_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Titre:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Description";
            // 
            // BTN_Send
            // 
            this.BTN_Send.Enabled = false;
            this.BTN_Send.Location = new System.Drawing.Point(449, 399);
            this.BTN_Send.Name = "BTN_Send";
            this.BTN_Send.Size = new System.Drawing.Size(196, 39);
            this.BTN_Send.TabIndex = 4;
            this.BTN_Send.Text = "Envoyer!";
            this.BTN_Send.UseVisualStyleBackColor = true;
            this.BTN_Send.Click += new System.EventHandler(this.BTN_Send_Click);
            // 
            // BTN_Cancel
            // 
            this.BTN_Cancel.Location = new System.Drawing.Point(276, 399);
            this.BTN_Cancel.Name = "BTN_Cancel";
            this.BTN_Cancel.Size = new System.Drawing.Size(167, 39);
            this.BTN_Cancel.TabIndex = 5;
            this.BTN_Cancel.Text = "Annuler";
            this.BTN_Cancel.UseVisualStyleBackColor = true;
            this.BTN_Cancel.Click += new System.EventHandler(this.BTN_Cancel_Click);
            // 
            // FRM_SendImage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 450);
            this.Controls.Add(this.BTN_Cancel);
            this.Controls.Add(this.BTN_Send);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PBX_Preview);
            this.Name = "FRM_SendImage";
            this.Text = "Envoie d\'un Sprite";
            this.Load += new System.EventHandler(this.FRM_SendImage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PBX_Preview)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox PBX_Preview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel LBL_Create_Account;
        private System.Windows.Forms.TextBox TBX_Password;
        private System.Windows.Forms.TextBox TBX_Username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox TBX_Description;
        private System.Windows.Forms.TextBox TBX_Title;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button BTN_Send;
        private System.Windows.Forms.Button BTN_Cancel;
        private System.Windows.Forms.ListBox LBx_Used;
        private System.Windows.Forms.ListBox LBx_Unused;
        private System.Windows.Forms.Button Btn_remove_tag;
        private System.Windows.Forms.Button Btn_add_tag;
        private System.Windows.Forms.Label label7;
    }
}