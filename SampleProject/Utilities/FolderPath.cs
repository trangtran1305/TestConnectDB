using System.IO;

namespace Assignment06.reporter
{
    class FolderPath
    {          
        public static string CreateFolder(string folderPath)
        {           
            DirectoryInfo directory = new DirectoryInfo(folderPath);
            if (!directory.Exists)
            {
                directory.Create();              
            }
            return folderPath;

        }
        public static void DeleteFolder(string folderPath)
        {
            DirectoryInfo directory = new DirectoryInfo(folderPath);
            if (directory.Exists)
            {
                directory.Delete();
            }

        }
}
}
