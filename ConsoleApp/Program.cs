using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        private const string rest_url = @"https://localhost:44310/Rest/APIServerAsync/";
        static void Main(string[] args)
        {
            File.Delete(@"..\..\config.json");

            var animeFigure1 = new AnimeFigure("04", "https://img.joomcdn.net/0e0b1a6ce254b87a050829f8d208bbc4f6818ee9_original.jpeg");
            var animeFigure2 = new AnimeFigure("05", "https://img.joomcdn.net/0e0b1a6ce254b87a050829f8d208bbc4f6818ee9_original.jpeg");

            
            Set_server(animeFigure2);
            Set_server(animeFigure1);
            var animeFigures  = Get_serverAsync().Result;
            

            Console.WriteLine(animeFigures);





            //WebRequest reqGET = WebRequest.Create(@"https://jsonplaceholder.typicode.com/posts");
            //WebResponse resp = reqGET.GetResponse();
            //Stream stream = resp.GetResponseStream();
            //StreamReader sr = new StreamReader(stream);
            //string s = sr.ReadToEnd();
            //Console.WriteLine(s);
            //var request = new HttpRequestMessage(
            //HttpMethod.Get, "https://www.nbrb.by/api/bic");
            //var responce = 
            //var csv_str = responce.Content.ReadAsStringAsync().Result;
            //var client = new HttpClient();
            //var responce = client.GetAsync(data_url).Result;
            //var csv_str = responce.Content.ReadAsStringAsync().Result;
            Console.ReadLine();
        }

        public static async Task<List<AnimeFigure>> Get_serverAsync()
        {
            var dataString = string.Empty;
            using (StreamReader sourceStream = new StreamReader(@"..\..\config.json"))
            {
                dataString = await sourceStream.ReadToEndAsync();
            }
            var x = "||".ToCharArray();
            var splitStrings = dataString.Split(x, StringSplitOptions.RemoveEmptyEntries).ToList();

            for (int i = 0; i < splitStrings.Count; i++)
            {
                if (splitStrings[i].Contains("\""))
                {
                    splitStrings.Remove(splitStrings[i]);
                }
            }
            var Figures = new List<AnimeFigure>();
            for (int i = 0; i < splitStrings.Count; i += 3)
            {
                var Figure = new AnimeFigure(splitStrings[i + 1], splitStrings[i + 2], splitStrings[i]);
                Figures.Add(Figure);
            }
            return Figures;
        }
        public static void Set_server(AnimeFigure animeFigure)
        {
            string stringForRecord = $"||{animeFigure.id}||{animeFigure.animeFiguresName}||{animeFigure.animeFiguresPictureUrl}||";
            var json = JsonConvert.SerializeObject(stringForRecord);
            File.AppendAllText(@"..\..\config.json", json);
        }
    }
    public class AnimeFigure
    {
        public Guid id { get; }
        public string animeFiguresName { get; }
        public string animeFiguresPictureUrl { get; }

        public AnimeFigure(string animeFiguresName, string animeFiguresPictureUrl)
        {
            this.animeFiguresName = animeFiguresName;
            this.animeFiguresPictureUrl = animeFiguresPictureUrl;
            id = Guid.NewGuid();
        }
        public AnimeFigure(string animeFiguresName, string animeFiguresPictureUrl, string animeFiguresId)
        {
            this.animeFiguresName = animeFiguresName;
            this.animeFiguresPictureUrl = animeFiguresPictureUrl;
            id = StringToGuid(animeFiguresId);
        }
        private Guid StringToGuid(string id)
        {
            var reGuid = new Regex(
            @"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$",
            RegexOptions.Compiled);

            if (id == null || id.Length != 36) return Guid.Empty;
            if (reGuid.IsMatch(id))
                return new Guid(id);
            else
                return Guid.Empty;
        }
    }
}
