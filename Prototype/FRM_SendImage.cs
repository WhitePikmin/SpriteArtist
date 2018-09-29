using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype
{
    public partial class FRM_SendImage : Form
    {
        string Username="", Password="", Title="", Description="", Tags="";
        Image Sprite;

        public FRM_SendImage() => InitializeComponent();

        public FRM_SendImage(Bitmap spr_)
        {
            InitializeComponent();
            Sprite = (Image)spr_;
            PBX_Preview.Image = Sprite;
            PBX_Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
        }

        private void LBL_Create_Account_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost/Inscription.php");
        }

        private void TBX_Username_TextChanged(object sender, EventArgs e)
        {
            Username = TBX_Username.Text;
            UpdateStatus();
        }

        private void TBX_Password_TextChanged(object sender, EventArgs e)
        {
            Password = TBX_Password.Text;
            UpdateStatus();
        }

        private void TBX_Title_TextChanged(object sender, EventArgs e)
        {
            Title = TBX_Title.Text;
            UpdateStatus();
        }

        private void TBX_Description_TextChanged(object sender, EventArgs e)
        {
            Description = TBX_Description.Text;
            UpdateStatus();
        }

        private void TBX_Tags_TextChanged(object sender, EventArgs e)
        {
            Tags = TBX_Tags.Text;
            UpdateStatus();
        }

        private void BTN_Send_Click(object sender, EventArgs e)
        {
            string Message = ImageUploader.SendImage(Username,Password,Title,Description,Tags,Sprite);
            if (Message == "File sent")
            {
                MessageBox.Show("Le fichier à été envoyé avec succès!", "Envoie", MessageBoxButtons.OK);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Erreur: "+Message, "Erreur", MessageBoxButtons.OK);
            }
        }

        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void UpdateStatus()
        {
            if (Username.Length == 0 || Password.Length == 0 || Title.Length == 0)
                BTN_Send.Enabled = false;
            else
                BTN_Send.Enabled = true;
        }
    }
}
