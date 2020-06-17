using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Hl7.Fhir.Rest;

namespace whatsupdoc
{
    // Class to hold static api logic and configs
    public static class Utils
    {
        public static string FHIR_Resource = "http://hapi.fhir.org/baseR4/";
        public static string ProviderQuery = "&_include=PractitionerRole:organization&_include=PractitionerRole:location";
        public static string PractitionerRoleResource = "PractitionerRole?specialty=";
        //public static string PractitionerResource = "Practitioner/";

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

        public static JObject GetFHIRResource(string code)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(code);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return JObject.Parse(reader.ReadToEnd());
            }
        }
    }
}
