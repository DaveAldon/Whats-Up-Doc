using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace whatsupdoc
{
    // Class to hold static api logic and configs
    public static class Utils
    {
        public static string FHIR_Resource = "http://hapi.fhir.org/baseR4/";
        public static string Token = "";
        public static string ProviderQuery = "&_include=PractitionerRole:organization&_include=PractitionerRole:location";
        public static string PractitionerRoleResource = "PractitionerRole?specialty=";
        public static string ConferenceLink = "https://www.devdays.com/us/wp-content/uploads/sites/5/2020/06/PROGRAM_US_VIRTUAL_EDITION_2020_3.pdf";

        public static JObject GetPractitionerRolesAPI(string code)
        {
            var uri = FHIR_Resource + PractitionerRoleResource + code + ProviderQuery;
            return GetFHIRResource(uri);
        }

        public static JObject GetCustomAPI(string ID)
        {
            var uri = FHIR_Resource + ID;
            return GetFHIRResource(uri);
        }

        // Get json data from api
        public static JObject GetFHIRResource(string code)
        {
            JObject returnData = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(code);

            // Token parameter if the request needs authorization
            request.Headers.Add("Authorization", "Bearer " + Token);

            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())

                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    returnData = JObject.Parse(reader.ReadToEnd());
                }
            }
            catch {
                return null;
            }
            return returnData;
        }

        // Returns a sorted list of diseases
        public static List<string> GetDiseasePickers()
        {
            var diseaseList = new List<string>();

            foreach (KeyValuePair<string, string> i in DiseaseMapping)
            {
                diseaseList.Add(i.Key);
            }
            diseaseList.Sort();
            return diseaseList;
        }

        // Mapping definitions with codes as value pairs
        public static Dictionary<string, string> DiseaseMapping = new Dictionary<string, string>()
        {
            { "COVID-19", "208D00000X" },
            { "Coronary Artery Disease", "207RC0000X" },
            { "Hypertension", "207RC0000X" },
            { "Lyme Disease", "208D00000X" },
            { "Measles", "208D00000X" },
            { "Bronchitis", "208D00000X" }
        };

        // Returns the code based on the disease
        public static string GetDiseaseMapping(string disease)
        {
            return DiseaseMapping[disease];
        }
    }
}
