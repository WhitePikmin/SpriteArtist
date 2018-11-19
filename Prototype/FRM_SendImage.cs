using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpriteArtist
{
    public partial class FRM_SendImage : Form
    {
        string Username="", Password="", Title="", Description="", Tags="";
        Image Sprite;
        List<dynamic> ListOfTags = new List<dynamic>(){};
        List<dynamic> ListUsed = new List<dynamic>() { };

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
            System.Diagnostics.Process.Start("http://www.spriteartist/Inscription.php");
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

        private void ResetUsed()
        {
            LBx_Used.DataSource = null;
            LBx_Used.ValueMember = "Id";
            LBx_Used.DisplayMember = "Name";
            LBx_Used.DataSource = ListUsed;
        }
        private void ResetUnUsed()
        {
            LBx_Unused.DataSource = null;
            LBx_Unused.ValueMember = "Id";
            LBx_Unused.DisplayMember = "Name";
            LBx_Unused.DataSource = ListOfTags;
        }

        private void Btn_add_tag_Click(object sender, EventArgs e)
        {
            if (LBx_Unused.SelectedIndex != -1)
            {
                dynamic d =  LBx_Unused.SelectedItem;
                ListOfTags.Remove(LBx_Unused.SelectedItem);
                ListUsed.Add(LBx_Unused.SelectedItem);

                ResetUsed();
                ResetUnUsed();
            }
        }

        private void Btn_remove_tag_Click(object sender, EventArgs e)
        {
            if (LBx_Used.SelectedIndex != -1)
            {
                dynamic d = LBx_Used.SelectedItem;
                ListUsed.Remove(LBx_Used.SelectedItem);
                ListOfTags.Add(LBx_Used.SelectedItem);

                ResetUnUsed();
                ResetUsed();
            }
        }

        private string FormatTags(List<dynamic> tags)
        {
            string output = "";
            for (int i = 0; i < tags.Count; i++)
            {
                output += tags[i];
                if (i < tags.Count - 1) output += ",";
            }
            return output;
        }

        private void FRM_SendImage_Load(object sender, EventArgs e)
        {
            Get_tags();
            LBx_Unused.ValueMember = "Id";
            LBx_Unused.DisplayMember = "Name";
            LBx_Unused.DataSource = ListOfTags;


            LBx_Used.ValueMember = "Id";
            LBx_Used.DisplayMember = "Name";
            LBx_Used.DataSource = ListUsed;
        }

        private void BTN_Send_Click(object sender, EventArgs e)
        {
            Tags = FormatTags(ListUsed);
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
        private void Get_tags()
        {
            HttpWebRequest request = WebRequest.Create("http://www.spriteartist.com/api.php/Tags") as HttpWebRequest;
            string json;
            // Get response  
            try {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream  
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    // Console application output  
                    json = reader.ReadToEnd();
                }
                JToken[] tags;
                JObject jObject = JObject.Parse(json);
                JToken jResults = jObject["results"];
                tags = jResults.ToArray();
                foreach (var tag in tags)
                {
                    var id = (string)tag["idTags"];
                    var name = (string)tag["name"];
                    ListOfTags.Add(new { Id = id, Name = name });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Une erreur est survenue lors de la connection à SpriteArtist.com. Vérifiez que vous êtes bien connecté sur Internet.", "Erreur de connection", MessageBoxButtons.OK);
                this.Close();
            }
            } 
            
    }
}
