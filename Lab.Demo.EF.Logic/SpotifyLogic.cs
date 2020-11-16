using Lab.Demo.EF.APISpotify;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Demo.EF.Logic
{
    public class SpotifyLogic
    {
        private string GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();
            string url5 = "https://accounts.spotify.com/api/token";

            var client_id = "275cc342045b49698790f0bf5380c91c";
            var client_secret = "75181ee3873441a7abaf8266466ead6e";
            var clientid_clientsecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", client_id, client_secret)));


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url5);


            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            webRequest.Headers.Add("Authorization: Basic " + clientid_clientsecret);


            var request = ("grant_type=client_credentials");
            byte[] req_bytes = Encoding.ASCII.GetBytes(request);
            webRequest.ContentLength = req_bytes.Length;


            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();


            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String json = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    json = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            token = JsonConvert.DeserializeObject<SpotifyToken>(json);
            return token.Access_token;
        }

        private string GetUrl(string album, string artist)
        {
            const string sep = "%20";

            var album_final = album.Replace(" ", sep);
            var artist_final = artist.Replace(" ", sep);
            
            return $"https://api.spotify.com/v1/search?q=album%3A{album_final}{sep}artist%3A{artist_final}&type=track&market=ES&limit=20";
        }

        public async Task<Root> GetAlbum(string album, string artist)
        {
            var webResponse = string.Empty;
            Root Data = new Root();

            try
            {
                HttpClient hc = new HttpClient();
                var token = GetAccessToken();
                var url = GetUrl(album, artist);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get
                };
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Authorization", "Bearer " + token);

                HttpResponseMessage response = await hc.SendAsync(request);
                
                if (response.IsSuccessStatusCode)
                {
                    Data = await response.Content.ReadAsAsync<Root>();
                }
            }
            catch (WebException ex)
            {
                throw new Exception("Track Request Error: " + ex.Status);
            }
            catch (TaskCanceledException tex)
            {
                throw new Exception("Request Error: " + tex.Message);
            }
            return Data;
        }
    }
}
