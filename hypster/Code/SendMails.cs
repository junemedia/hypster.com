using hypster.Models;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace hypster.Code
{
    public class SendMails
    {
        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.maropost.com/accounts/694/emails/deliver.json?auth_token=bc1bb40f4beaf7e65b6b0a13d73977674380eb2f");
        HttpWebResponse httpResponse = null;
        
        public void SendPasswordRecoveryEMail(SendEMail email)
        {
            Response response = new Response();
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";                
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = new JavaScriptSerializer().Serialize(new
                {
                    email
                });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Newtonsoft.Json.Linq.JObject msg = Newtonsoft.Json.Linq.JObject.Parse(result);
                response.status_code = ((int)httpResponse.StatusCode).ToString() + " " + httpResponse.StatusDescription;
                response.status_description = msg["message"].ToString();
                email.response = response;
            }
        }
    }
}