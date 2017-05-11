using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Yahoo.Data.Interfaces;
using Yahoo.Data.Model;

namespace Yahoo.Data
{
    public class Service
    {
        private object Deserialize(string xml, Type toType)
        {
            using (Stream stream = new MemoryStream())
            {
                var data = Encoding.UTF8.GetBytes(xml);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var deserializer = new XmlSerializer(toType);
                return deserializer.Deserialize(stream);
            }
        }

        public async Task<IDtoObject> RetrieveCurrenciesAsync(int i)
        {
            using (var client = new HttpClient())
            using (var response = client.GetAsync("http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote"))
            using (var content = response.Result.Content)
            {
                var result = await content.ReadAsStringAsync();
                var dataObjects = Deserialize(result, typeof (CurrenciesList));
                return (CurrenciesList) dataObjects;
            }
        }

        public IDtoObject RetrieveCurrencies(int i)
        {
            using (var client = new HttpClient())
            using (var response = client.GetAsync("http://finance.yahoo.com/webservice/v1/symbols/allcurrencies/quote"))
            using (var content = response.Result.Content)
            {
                var result = content.ReadAsStringAsync();
                var dataObjects = Deserialize(result.GetAwaiter().GetResult(), typeof (CurrenciesList));
                return (CurrenciesList) dataObjects;
            }
        }
    }
}