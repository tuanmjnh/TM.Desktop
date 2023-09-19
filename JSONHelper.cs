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
    public class ReadParseJSONNewtonsoft
    {
        public class Configs
        {
            public static void Initialize()
            {
                TM.Desktop.IO.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "Resources/Config"));
                TM.Desktop.IO.CreateFileExist(Path.Combine(Environment.CurrentDirectory, "Resources/Config", "config.json"));
            }

            // ReadAndParseJsonFileWithNewtonsoftJson
            public class ReadAndParseJsonFileWithNewtonsoftJson
            {
                private readonly string _sampleJsonFilePath;

                public ReadAndParseJsonFileWithNewtonsoftJson(string sampleJsonFilePath)
                {
                    _sampleJsonFilePath = sampleJsonFilePath;
                }
            }
            public List<Teacher> UseUserDefinedObjectWithNewtonsoftJson(string configFilePath)
            {
                using StreamReader reader = new(configFilePath);
                var json = reader.ReadToEnd();
                List<Teacher> teachers = JsonConvert.DeserializeObject<List<Teacher>>(json);

                return teachers;
            }

            //UseJsonTextReaderInNewtonsoftJson
            public List<Teacher> UseJsonTextReaderInNewtonsoftJson(string configFilePath)
            {
                var serializer = new JsonSerializer();
                List<Teacher> teachers = new();
                using (var streamReader = new StreamReader(configFilePath))
                using (var textReader = new JsonTextReader(streamReader))
                {
                    teachers = serializer.Deserialize<List<Teacher>>(textReader);
                }

                return teachers;
            }

            //UseJArrayParseInNewtonsoftJson
            public List<Teacher> UseJArrayParseInNewtonsoftJson(string configFilePath)
            {
                using StreamReader reader = new(configFilePath);
                var json = reader.ReadToEnd();
                var jarray = JArray.Parse(json);
                List<Teacher> teachers = new();

                foreach (var item in jarray)
                {
                    Teacher teacher = item.ToObject<Teacher>();
                    teachers.Add(teacher);
                }

                return teachers;
            }
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
        public List<Teacher> UseFileReadAllTextWithSystemTextJson()
        {
            var json = File.ReadAllText(_sampleJsonFilePath);
            List<Teacher> teachers = System.Text.Json.JsonSerializer.Deserialize<List<Teacher>>(json, _options);

            return teachers;
        }
        public List<Teacher> UseFileOpenReadTextWithSystemTextJson()
        {
            using FileStream json = File.OpenRead(_sampleJsonFilePath);
            List<Teacher> teachers = System.Text.Json.JsonSerializer.Deserialize<List<Teacher>>(json, _options);

            return teachers;
        }
        public List<Teacher> UseStreamReaderWithSystemTextJson()
        {
            using StreamReader streamReader = new(_sampleJsonFilePath);
            var json = streamReader.ReadToEnd();
            List<Teacher> teachers = System.Text.Json.JsonSerializer.Deserialize<List<Teacher>>(json, _options);

            return teachers;
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
    public class Json
    {
        string _path = "";
        public Json(string path)
        {
            _path = path;
        }
        public T LoadJson<T>()
        {
            try
            {
                using (var r = new StreamReader(_path))
                {
                    var json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<T>(json);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public dynamic LoadJson()
        {
            try
            {
                using (var r = new StreamReader(_path))
                {
                    var json = r.ReadToEnd();
                    var items = Newtonsoft.Json.Linq.JObject.Parse(json);
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}