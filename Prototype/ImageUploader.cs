using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Drawing;

namespace SpriteArtist
{
    class ImageUploader
    {
        private static string Username, Password, Title, Description, Tags;
        private static byte[] ImageRaw;

        public static string SendImage(string uname_,string psswd_,string title_,string desc_,string tags_,Image img_)
        {
            Username = uname_;
            Password = psswd_;
            Title = title_;
            Description = desc_;
            Tags = tags_;
            ImageRaw = (byte[])(new ImageConverter().ConvertTo(img_, typeof(byte[])));
            Task<string> Message = Task.Run(async () => await Upload());
            return Message.Result;
        }

        private static async Task <string> Upload()
        {
            string FinalMessage = "";
            try
            {
                Uri Link = new Uri(@"http://localhost/InsertionPixelArt.php");

                HttpClient hc = new HttpClient();

                MultipartFormDataContent data = new MultipartFormDataContent("Upload----" + DateTime.Now.ToString());

                data.Add(new StringContent(Username), "User");
                data.Add(new StringContent(Password), "Password");

                data.Add(new StringContent(Title), "Title");
                data.Add(new StringContent(Description), "Description");
                data.Add(new StringContent(Tags), "Tags");
                data.Add(new StreamContent(new MemoryStream(ImageRaw)), "file", "image.png");
            
                var response = await hc.PostAsync(Link, data);
                var contents = await response.Content.ReadAsStringAsync();

                string ErrorMessage = GetErrorMessage(contents);
                if (ErrorMessage != "")
                    FinalMessage = ErrorMessage;
                else
                    FinalMessage = "File sent";
                hc.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Upload failed: " + e.Message);
                Console.ReadKey();
            }
            return FinalMessage;
        }

        private static string GetErrorMessage(string HTMLPage)
        {
            string NewMessage = "";
            const string ErrorElementStart = "<div class=\"error\">";
            const string ErrorElementEnd = "</div>";

            int StartIndex = HTMLPage.IndexOf(ErrorElementStart) + ErrorElementStart.Length;
            NewMessage = HTMLPage.Substring(StartIndex, HTMLPage.Length - StartIndex);

            int EndIndex = NewMessage.IndexOf(ErrorElementEnd);
            NewMessage = NewMessage.Substring(0,EndIndex);

            if (NewMessage.Length != 1)
                return NewMessage.Trim();
            else
                return "";
        }
    }
}
