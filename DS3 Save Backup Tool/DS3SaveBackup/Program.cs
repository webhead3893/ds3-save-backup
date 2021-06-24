using System;
using System.IO;

namespace DS3SaveBackup
{
    class Program
    {
        private static void DirectoryCopy(string source, string target)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo directory = new DirectoryInfo(source);

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Save location does not exist or could not be found: "
                    + source);
            }

            DirectoryInfo[] directories = directory.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(target);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(target, file.Name);
                if(file.Name != "GraphicsConfig.xml")
                {
                    file.CopyTo(tempPath, true);
                }
            }

            foreach (DirectoryInfo subdir in directories)
            {
                string tempPath = Path.Combine(target, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath);
            }
        }
        static void Main(string[] args)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);  // appdata
            string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);    // documents
            string saveLoc = Path.Combine(appdata, "DarkSoulsIII"); // save location
            string backupLoc = Path.Combine(documents, "DS3SaveBackup");    // backup location
            DirectoryCopy(saveLoc, backupLoc);
            Console.WriteLine("Backup complete. Backup saves can be found in " + backupLoc + ".\nPress enter to close this window...");
            Console.ReadKey();
        }
    }
}
