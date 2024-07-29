using System;
using System.Collections.Generic;

namespace TM.Desktop
{
    public class IO
    {
        public static string Rename(string sourceFile, string newNameFile,  bool isTrim = true)
        {
            try
            {
                var filePath = Path.GetDirectoryName(sourceFile);
                if (filePath != null)
                {
                    var fileName = Path.GetFileNameWithoutExtension(sourceFile);
                    var ext = Path.GetExtension(sourceFile);
                    var newPath = Path.Combine(isTrim ? filePath.Trim() : filePath, (isTrim ? newNameFile.Trim() : newNameFile) + ext);
                    if (sourceFile != newPath)
                    {
                        File.Move(sourceFile, newPath);
                        //if (delSource) IO.Delete(sourceFile);
                    }
                    return newPath;
                }
                return null;
            }
            catch (Exception) { return null; }
        }
        public static FileInfo ReExtension(string sourceFile, string extension)
        {
            try
            {
                //sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                var file = new FileInfo(sourceFile);
                var DestFile = sourceFile.Replace(file.Extension, extension);
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return null; }
        }
        public static FileInfo ReExtensionToLower(string sourceFile)
        {
            //sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
            var file = new FileInfo(sourceFile);
            try
            {
                var DestFile = sourceFile.Replace(file.Extension, file.Extension.ToLower());
                File.Move(sourceFile, DestFile);
                return new FileInfo(DestFile);
            }
            catch (Exception) { return file; }
        }
        public static bool Copy(string sourceFile, string DestFile)
        {
            try
            {
                //sourceFile = IsMapPath ? MapPath(sourceFile) : sourceFile;
                //DestFile = IsMapPath ? MapPath(DestFile) : DestFile;
                File.Copy(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { return false; }
        }
        public static bool Copy(string sourceFile)
        {
            return Copy(sourceFile, CreateFileExist(sourceFile));
        }
        public static bool Delete(string path)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else return true;
            }
            catch (Exception) { return false; }
        }
        public static bool Delete(List<string> files)
        {
            try
            {
                if (files.Count < 1) return false;
                //path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(item))
                        File.Delete(item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string[] files, bool IsMapPath = true)
        {
            try
            {
                if (files.Length < 1) return false;
                //path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(item))
                        File.Delete(item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, string[] files, bool IsMapPath = true)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item))
                        File.Delete(path + item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, FileInfo[] files, bool IsMapPath = true)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                foreach (var item in files)
                    if (File.Exists(path + item.Name))
                        File.Delete(path + item.Name);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool DeleteDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                if (Directory.Exists(path))
                {
                    foreach (var item in Files(path, false))
                        File.Delete(item.FullName);
                    Directory.Delete(path);
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool CreateDirectory(string path, bool IsMapPath = true)
        {
            try
            {
                //var securityRules = new DirectorySecurity();
                //securityRules.AddAccessRule(new FileSystemAccessRule(@"Domain\account1", FileSystemRights.Read, AccessControlType.Allow));
                //securityRules.AddAccessRule(new FileSystemAccessRule(@"Domain\account2", FileSystemRights.FullControl, AccessControlType.Allow));

                //path = IsMapPath ? MapPath(path) : path;
                path = path.Trim('/', '\\');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    var directory = new DirectoryInfo(path);
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }
        public static string CreateFileExist(string file, bool IsMapPath = true)
        {
            try
            {
                int countFile = 0;
                //file = IsMapPath ? MapPath(file) : file;
                string extension = Path.GetExtension(file); //MapPath(file)
                while (File.Exists(file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension))
                    countFile++;
                file = file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension;
                return file;
            }
            catch (Exception) { throw; }
        }
        public static byte[] ReturnByteFile(string path, bool IsMapPath = true)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                byte[] fileBytes = File.ReadAllBytes(path);
                return fileBytes;
            }
            catch (Exception) { throw; }
        }
        public static DirectoryInfo[] Directories(string path, bool IsMapPath = true)
        {
            try
            {
                //path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                return Dir.GetDirectories();
            }
            catch (Exception) { throw; }
        }
        public static List<string> DirectoriesToList(string path, bool IsMapPath = true)
        {
            try
            {
                var list = new List<string>();
                var subDir = Directories(path, IsMapPath);
                foreach (var item in subDir)
                    list.Add(item.Name);
                return list;
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, string[] extension = null, bool IsMapPath = true)
        {
            try
            {
                //var files = System.IO.Directory.GetDirectories(path);
                //string[] ext = new[] { ".dbf" };
                //path = IsMapPath ? MapPath(path) : path;
                var Dir = new DirectoryInfo(path);
                if (extension != null)
                    return Dir.GetFiles().Where(f => extension.Contains(f.Extension.ToLower())).ToArray();
                else
                    return Dir.GetFiles();
                //var subFiles = di.GetFiles("*.dbf").Concat(di.GetFiles("*.dbf2"));
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, bool IsMapPath = true)
        {
            return Files(path, null, IsMapPath);
        }
        public static List<string> FilesToList(string path, string[] extension, bool IsMapPath = true)
        {
            try
            {
                var list = new List<string>();
                var subFiles = Files(path, extension, IsMapPath);
                foreach (var item in subFiles)
                    list.Add(item.Name.Replace(item.Extension, item.Extension.ToLower()));
                return list;
            }
            catch (Exception) { throw; }
        }
        public static List<string> FilesToList(string path, bool IsMapPath = true)
        {
            return FilesToList(path, null, IsMapPath);
        }

        public static string[] ReadFile(string filename, bool IsMapPath = true)
        {
            //filename = IsMapPath ? MapPath(filename) : filename;
            var list = System.IO.File.ReadAllLines(filename);
            return list;
        }
        public static List<string[]> ReadFile(string filename, char split, bool IsMapPath = true)
        {
            var rs = new List<string[]>();
            foreach (var item in ReadFile(filename, IsMapPath))
            {
                var tmp = item.Trim().Split(split);
                rs.Add(tmp);
            }
            return rs;
        }
        public static List<string[]> ReadFile(string filename, string split, bool IsMapPath = true)
        {
            var rs = new List<string[]>();
            foreach (var item in ReadFile(filename, IsMapPath))
            {
                var tmp = item.Trim().Replace(split, "\t").Split('\t');
                rs.Add(tmp);
            }
            return rs;
        }
        public static List<string> GetFiles(string sourceFolder, string filters, System.IO.SearchOption searchOption)
        {
            return filters.Split('|').SelectMany(filter => System.IO.Directory.GetFiles(sourceFolder, filter, searchOption)).ToList();
        }
    }
    public static class IOS
    {
        public static string ToExtension(this string file)
        {
            try
            {
                return Path.GetExtension(file);
            }
            catch (Exception) { throw; }
        }
        public static string ToExtensionNone(this string file)
        {
            return ToExtension(file).Trim('.');
        }
        public static bool IsExtension(this string file, string Extension = null)
        {
            try
            {
                if (string.IsNullOrEmpty(Extension))
                    return false;
                var tmp = Extension.Trim().Trim(',').Split(',').ToLower();
                if (Array.IndexOf(tmp, Path.GetExtension(file).ToLower()) > -1)
                    return true;
                return false;
            }
            catch (Exception) { throw; }
        }
        public static bool IsExtension(this string file, string[] Extension)
        {
            if (Extension.Length > 0 && Array.IndexOf(Extension.ToLower(), Path.GetExtension(file).ToLower()) > -1)
                return true;
            return false;
        }
        public static bool IsExtension(this string file, List<string> Extension)
        {
            if (Extension.Count > 0 && Extension.ToLower().Contains(Path.GetExtension(file).ToLower()))
                return true;
            return false;
        }
        public static List<string> UploadFileSource(this Dictionary<string, object> Upload)
        {
            try
            {
                return (List<string>)Upload["UploadFileSource"];
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static string UploadFileSourceString(this Dictionary<string, object> Upload)
        {
            try
            {
                return (string)Upload["UploadFileSourceString"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<string> UploadFile(this Dictionary<string, object> Upload)
        {
            try
            {
                return (List<string>)Upload["UploadFile"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static string UploadFileString(this Dictionary<string, object> Upload)
        {
            try
            {
                return (string)Upload["UploadFileString"];
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<string> UploadError(this Dictionary<string, object> Upload)
        {
            try
            {
                return (List<string>)Upload["UploadError"];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}