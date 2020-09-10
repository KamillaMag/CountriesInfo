using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace CountriesInfo
{
    internal class Searcher
    {
        private static Country countryResponse;
        private static WebResponse response;
        internal static Country Search(string country)
        {
            if (!string.IsNullOrEmpty(country))
            {
                try
                {
                    response = WebRequest.Create("https://restcountries.eu/rest/v2/name/" + country).GetResponse();
                }
                catch (WebException we)
                {
                    HttpWebResponse errorResponse = we.Response as HttpWebResponse;
                    if (errorResponse.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Country with the given name was not found", we);
                    }
                    else { throw; }
                }

                countryResponse = new Country();
                ParseResponse();
                return countryResponse;
            }
            else
            {
                throw new ArgumentException("Country name is empty or null");
            }
        }

        private static void ParseResponse()
        {
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string line = reader.ReadLine();
                JArray jArray = JArray.Parse(line);
                foreach (JProperty parsedProperty in jArray.Children<JObject>().Properties())
                {
                    if (parsedProperty.Name.Equals("name"))
                    {
                        countryResponse.Name = (string)parsedProperty.Value;
                    }
                    if (parsedProperty.Name.Equals("alpha2Code"))
                    {
                        countryResponse.Code = (string)parsedProperty.Value;
                    }
                    if (parsedProperty.Name.Equals("capital"))
                    {
                        countryResponse.Capital = (string)parsedProperty.Value;
                    }
                    if (parsedProperty.Name.Equals("area"))
                    {
                        countryResponse.Area = (decimal)parsedProperty.Value;
                    }
                    if (parsedProperty.Name.Equals("population"))
                    {
                        countryResponse.Population = (int)parsedProperty.Value;
                    }
                    if (parsedProperty.Name.Equals("region"))
                    {
                        countryResponse.Region = (string)parsedProperty.Value;
                    }
                }
            }
        }
    }
}