using System;
using System.IO;
using System.Threading;

namespace DirectoryOperations
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDriveLetter = Path.GetPathRoot(System.Reflection.Assembly.GetEntryAssembly().Location);
            DriveInfo di = new DriveInfo(currentDriveLetter);
            if (di.IsReady) // Checking if drive is ready
            {
                Console.WriteLine($"Available space in {currentDriveLetter} is {di.AvailableFreeSpace.ToString()}");
            }

            Console.WriteLine($"Current location (using Environment class) - {Environment.CurrentDirectory}");
            Console.WriteLine($"Current location (using path class)- {Path.GetFullPath(Environment.CurrentDirectory)}");

            string newDirectoryName = "New Data Directory";
            if (Directory.Exists(newDirectoryName))
            {
                Console.WriteLine($"Directory with name {newDirectoryName} already exists!!");
            }
            else
            {
                Directory.CreateDirectory(newDirectoryName);
                Console.WriteLine($"Directory {newDirectoryName} created!!");
            }

            // Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, newDirectoryName, $"Sub {newDirectoryName}"));
            DirectoryInfo dirInfo = new DirectoryInfo(newDirectoryName);
            dirInfo.CreateSubdirectory($"Sub {newDirectoryName}");

            //Create few files and enumerate
            for (int i = 0; i < 5; i++)
            {
                FileStream fs = File.Create(Path.Combine(Environment.CurrentDirectory, newDirectoryName, $"File {i}"));
                fs.Dispose();
            }

            foreach (FileInfo fi in dirInfo.GetFiles())
            {
                Console.WriteLine($"File {fi.Name} created on - {fi.CreationTime.TimeOfDay}, Size: {fi.Length}");                
            }

            // dirInfo.Delete(true);// clean up, passing true to recursivel delete contents

            // Console.WriteLine("Directory deleted!!");

            Console.ReadKey();
        }
    }
}
