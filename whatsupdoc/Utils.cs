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
        public static string ProviderQuery = "PractitionerRole?specialty=208D00000X&_include=PractitionerRole:organization&_include=PractitionerRole:location";

        public static string GetProvidersAPI()
        {
            return FHIR_Resource + ProviderQuery;
        }

        public static string GetFHIRResource()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(GetProvidersAPI());
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
