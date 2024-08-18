using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TM.Desktop
{
    //Read Parse File
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        public int Level { get; set; }
        public List<Course> Courses { get; set; }
    }
    public class Course
    {
        public string Name { get; set; } = string.Empty;
        public int CreditUnits { get; set; }
        public int NumberOfStudents { get; set; }
    }

    // ReadAndParseJsonFileWithNewtonsoftJson
    public class ReadParseJSONNewtonsoft
    {
        private readonly string _jsonFilePath;

        public ReadParseJSONNewtonsoft(string jsonFilePath)
        {
            _jsonFilePath = jsonFilePath;
        }
        public List<T> UseUserDefinedObjectWithNewtonsoftJson<T>()
        {
            using StreamReader reader = new(_jsonFilePath);
            var json = reader.ReadToEnd();
            var rs = JsonConvert.DeserializeObject<List<T>>(json);
            return rs;
        }
        public List<KeyValuePair<string, T>> UseUserDefinedObjectWithNewtonsoftJsonKeyValue<T>()
        {
            using StreamReader reader = new(_jsonFilePath);
            var json = reader.ReadToEnd();
            var rs = JsonConvert.DeserializeObject<List<KeyValuePair<string, T>>>(json);
            return rs;
        }

        //UseJsonTextReaderInNewtonsoftJson
        public List<T> UseJsonTextReaderInNewtonsoftJson<T>()
        {
            var serializer = new JsonSerializer();
            List<T> rs = new();
            using (var streamReader = new StreamReader(_jsonFilePath))
            using (var textReader = new JsonTextReader(streamReader))
            {
                rs = serializer.Deserialize<List<T>>(textReader);
            }

            return rs;
        }

        //UseJArrayParseInNewtonsoftJson
        public List<T> UseJArrayParseInNewtonsoftJson<T>()
        {
            using StreamReader reader = new(_jsonFilePath);
            var json = reader.ReadToEnd();
            var jarray = JArray.Parse(json);
            List<T> rs = new();

            foreach (var item in jarray)
            {
                var obj = item.ToObject<T>();
                rs.Add(obj);
            }

            return rs;
        }
    }
    public class ReadParseJsonFileSystemTextJson
    {
        private readonly string _sampleJsonFilePath;

        public ReadParseJsonFileSystemTextJson(string sampleJsonFilePath)
        {
            _sampleJsonFilePath = sampleJsonFilePath;
        }
        private readonly System.Text.Json.JsonSerializerOptions _options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        public List<T> FileReadAllTextWithSystemTextJson<T>()
        {
            var json = File.ReadAllText(_sampleJsonFilePath);
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _options);
            return rs;
        }
        public List<T> ListFileOpenReadTextWithSystemTextJson<T>()
        {
            using FileStream json = File.OpenRead(_sampleJsonFilePath);
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _options);
            return rs;
        }
        public List<KeyValuePair<string, T>> FileOpenReadTextWithSystemTextJsonKeyValuePair<T>()
        {
            using FileStream json = File.OpenRead(_sampleJsonFilePath);
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<KeyValuePair<string, T>>>(json, _options);
            return rs;
        }
        public List<T> StreamReaderWithSystemTextJson<T>()
        {
            using StreamReader streamReader = new(_sampleJsonFilePath);
            var json = streamReader.ReadToEnd();
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<T>>(json, _options);
            return rs;
        }
        public T FileOpenReadTextWithSystemTextJson<T>()
        {
            using FileStream json = File.OpenRead(_sampleJsonFilePath);
            var rs = System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
            return rs;
        }
    }

    //Json Serialization Write To File
    public static class JsonFileUtils
    {
        private static readonly JsonSerializerSettings _options = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void Write(object obj, string fileName)
        {
            var jsonString = JsonConvert.SerializeObject(obj, _options);

            File.WriteAllText(fileName, jsonString);
        }
        public static void Write(string jsonString, string fileName)
        {
            File.WriteAllText(fileName, jsonString);
        }

        public static void WriteFormat(object obj, string fileName) // Pretty Write
        {
            var jsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, _options);

            File.WriteAllText(fileName, jsonString);
        }

        static byte[] SerializeToUtf8Bytes(object obj, JsonSerializerSettings options)
        {
            using var stream = new MemoryStream();
            using var streamWriter = new StreamWriter(stream);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            JsonSerializer.CreateDefault(options).Serialize(jsonWriter, obj);

            jsonWriter.Flush();
            stream.Position = 0;
            return stream.ToArray();
        }

        public static void Utf8BytesWrite(object obj, string fileName)
        {
            var utf8Bytes = SerializeToUtf8Bytes(obj, _options);
            File.WriteAllBytes(fileName, utf8Bytes);
        }

        public static void StreamWrite(object obj, string fileName)
        {
            using var streamWriter = File.CreateText(fileName);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            JsonSerializer.CreateDefault(_options).Serialize(jsonWriter, obj);
        }

        public static async Task StreamWriteAsync(object obj, string fileName)
        {
            await Task.Run(() => StreamWrite(obj, fileName));
        }

        public static void WriteDynamicJsonObject(JObject jsonObj, string fileName)
        {
            using var streamWriter = File.CreateText(fileName);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            jsonObj.WriteTo(jsonWriter);
        }
    }

    //TM Json
    public static class Json
    {
        private static readonly System.Text.Json.JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
        public static List<T> ReadTextWithSystemTextJsonList<T>(this string serialize)
        {
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<T>>(serialize, _options);
            return rs;
        }
        public static T ReadTextWithSystemTextJson<T>(this string serialize)
        {
            var rs = System.Text.Json.JsonSerializer.Deserialize<T>(serialize, _options);
            return rs;
        }
        public static List<KeyValuePair<string, T>> ReadTextWithSystemTextJsonKeyValuePair<T>(this string serialize)
        {
            var rs = System.Text.Json.JsonSerializer.Deserialize<List<KeyValuePair<string, T>>>(serialize, _options);
            return rs;
        }
    }
}