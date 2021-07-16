using System;

namespace Assignment06.reporter
{
    class Common
    {
        public static string REPORTS_FOLDER;
        public static string REPORT_DATETIME_FOLDER;
        public static string REPORT_HTML_FILE;
        public static string SCREENSHOT_FOLDER;
        public static string SCREENSHOT_PNG_FILE;

        //To obtain the current solution path/project path
        public static string GetProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            return projectPath;
        }
    
        public static void InitReportDirection()
        {
            REPORTS_FOLDER = FolderPath.CreateFolder(GetProjectPath() + "\\Reports");
            REPORT_DATETIME_FOLDER = FolderPath.CreateFolder(REPORTS_FOLDER + "\\Report_" + GetToday());
            REPORT_HTML_FILE = REPORT_DATETIME_FOLDER + "\\index.html";
            SCREENSHOT_FOLDER = FolderPath.CreateFolder(REPORT_DATETIME_FOLDER + "\\Screenshot");          
            SCREENSHOT_PNG_FILE = SCREENSHOT_FOLDER + "\\Errorscreenshot" + GetToday() + ".png";

        }
		public static String GetToday()
		{
            return DateTime.Now.ToString("dd-MM-yyyy");			
		}

    }
}
