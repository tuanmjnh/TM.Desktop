using System.IO.Compression;

namespace TM.Desktop
{
    public static class Zip
    {
      public  static async Task ExtractAppliction(string zipPath, string extractPath)
        {
            //string startPath = @"D:\applications\ffmpeg\TMFFmpegApp";
            //string zipPath = @"D:\applications\ffmpeg\TMFFmpegApp.zip";
            //string extractPath = @"D:\applications\ffmpeg\extract";

            //await Task.Run(() => System.IO.Compression.ZipFile.CreateFromDirectory(startPath, zipPath));
            //Console.WriteLine($"Created Zip");
            //await Task.Run(() => System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath));
            //Console.WriteLine($"Extracted Zip");
            Console.WriteLine($"Unpacking...");
            await Task.Run(() => System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath, true));
            Console.WriteLine($"Unpacked");
        }
        public static async Task ExtractToDirectory(ZipArchive archive, string destinationDirectoryName, bool overwrite = true)
        {
            Console.WriteLine($"Unpacking...");
            if (!overwrite)
            {
                await Task.Run(() => archive.ExtractToDirectory(destinationDirectoryName));
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;
            await Task.Run(() =>
            {
                foreach (ZipArchiveEntry file in archive.Entries)
                {
                    string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                    if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                    }

                    if (file.Name == "")
                    {// Assuming Empty for Directory
                        Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                        continue;
                    }
                    file.ExtractToFile(completeFileName, true);
                }
            });
            Console.WriteLine($"Unpacked");
        }
        public static async Task ExtractToDirectory(string zipPath, string destinationDirectoryName, bool overwrite = true)
        {
            await Task.Run(() =>
            {
                Console.WriteLine($"Unpacking...");
                //var fs = new FileStream(zipPath, System.IO.FileMode.Open);
                using (var archive = ZipFile.OpenRead(zipPath))
                {
                    if (!overwrite)
                    {
                        archive.ExtractToDirectory(destinationDirectoryName);
                        return;
                    }

                    DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
                    string destinationDirectoryFullPath = di.FullName;

                    foreach (ZipArchiveEntry file in archive.Entries)
                    {
                        //
                        if (file.Name == "TMUpdate.exe") continue;
                        if (file.Name == "TMUpdate.pdb") continue;
                        string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                        if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                        {
                            throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                        }

                        if (file.Name == "")
                        {// Assuming Empty for Directory
                            Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                            continue;
                        }
                        file.ExtractToFile(completeFileName, true);
                    }
                    Console.WriteLine($"Unpacked");
                }
            });
        }
    }
}
