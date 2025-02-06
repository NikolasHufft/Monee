using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace ClientLibrary.Helpers
{
    public static class Serializations
    {
        public static string SerializeObj<T>(T modelObject) 
        {
            var options1 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            var a = JsonSerializer.Serialize(modelObject, options1);
            //var a = JsonSerializer.Serialize(modelObject, 
            //    new JsonSerializerOptions 
            //    { 
            //        WriteIndented = true, 
            //        Encoder = JavaScriptEncoder.Create(new TextEncoderSettings(System.Text.Unicode.UnicodeRanges.All)) });
            return a;

        } //=> JsonSerializer.Serialize(modelObject);
        public static T DeserializeJsonString<T>(string jsonString)
        {
            var options1 = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),                
                WriteIndented = true
            };
            var a = JsonSerializer.Deserialize<T>(jsonString, options1);
            return a;
        }
        
        public static IList<T> DeserializeJsonStringList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
