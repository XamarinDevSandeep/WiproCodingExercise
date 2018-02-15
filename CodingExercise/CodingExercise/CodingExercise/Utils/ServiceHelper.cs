using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise.Utils
{
    public static class ServiceHelper
    {
        public static async Task<string> GetFactsFromAPI()
        {
            string result = string.Empty;
            try
            {
                var oHttpClient = new HttpClient();
                oHttpClient.Timeout = new TimeSpan(0, 0, 60);
                var uri = new Uri("https://dl.dropboxusercontent.com/s/2iodh4vg0eortkl/facts.json");
                var response = await oHttpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }


        public static string GetFactsFromFileSystem()
        {
            var assembly = typeof(ServiceHelper).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("CodingExercise.Utils.facts.json");

            string result = string.Empty;
            using (var reader = new System.IO.StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
    }
}
