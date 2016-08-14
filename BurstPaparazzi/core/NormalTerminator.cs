using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BurstPaparazzi.core
{
    class NormalTerminator : ITerminator
    {
        private List<string> m_terminateList = new List<string>();

        static private string m_guardExePath = Directory.GetCurrentDirectory() + "\\guard\\NoWindowGuard.exe";

        public NormalTerminator()
        {
            m_terminateList.Add("ppap_startup");
            m_terminateList.Add("PPAP");
        }

        public List<string> terminate()
        {
            List<string> stubbornList = new List<string>();

            foreach (string name in m_terminateList)
            {
                if (!terminateByName(name,false))
                {
                    stubbornList.Add(name);
                }
            }

            return stubbornList;
        }

        static public bool terminateByName(string name,bool isIsolate)
        {
            Process[] processes = Process.GetProcessesByName(name);

            try
            {
                foreach (Process p in processes)
                {
                    p.Kill();
                    p.Close();
                }

                if (isIsolate)
                {
                    isolate(name);
                }
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public void isolate(string name)
        {
            string exeName = name + ".exe";

            if (isIgnored(exeName))
            {
                return;
            }

            string isolatePath = Directory.GetCurrentDirectory() + "\\isolate";

            DirectoryInfo folder = new DirectoryInfo(isolatePath);

            foreach (FileInfo file in folder.GetFiles("*.exe"))
            {
                if(exeName == file.Name)
                {
                    return;
                }
            }

            List<string> paparazziLocation = findPaparazziLocation(name);

            string newFilePath, targetPath;

            bool hasIsolate = false;

            foreach (string location in paparazziLocation)
            {

                if (!hasIsolate)
                {
                    targetPath = Directory.GetCurrentDirectory() + "\\isolate";
                    if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
                    newFilePath = targetPath + "\\" + exeName;
                    File.Copy(location, newFilePath, true);

                    hasIsolate = true;
                }

                File.Delete(location);

                File.Copy(m_guardExePath, location, true);

            }
        }

        public void recover(string name)
        {
            string orignFilePath = Directory.GetCurrentDirectory() + "\\isolate\\"+ name;

            List<string> paparazziLocation = findPaparazziLocation(name);

            foreach(string location in paparazziLocation)
            {
                if (location.Contains(Directory.GetCurrentDirectory()))
                {
                    continue;
                }

                File.Copy(orignFilePath, location, true);
            }

            File.Delete(orignFilePath);

        }

        static private List<string> findPaparazziLocation(string name)
        {
            List<string> location = new List<string>();

            const int bufsize = 260;
            StringBuilder buf = new StringBuilder(bufsize);

            string exeName = name + ".exe";

            // set the search
            Everything.Everything_SetSearchW(exeName);

            // execute the query
            Everything.Everything_QueryW(true);

            string orignFilePath;

            for (int index = 0; index < Everything.Everything_GetNumResults(); index++)
            {
                // get the result's full path and file name.
                Everything.Everything_GetResultFullPathNameW(index, buf, bufsize);

                orignFilePath = buf.ToString();

                location.Add(orignFilePath);

            }

            return location;
        }

        static private bool isIgnored(string exeName)
        {
            bool shouldBeIgnore = false;

            //TODO should make a list array
            switch (exeName)
            {
                case "svchost.exe":
                case "smss.exe":
                case "conhost.exe":
                case "chrome.exe":
                case "csrss.exe":
                case "devenv.exe":
                case "lantern.exe":
                    shouldBeIgnore = true;
                    break;
                default:
                    shouldBeIgnore = false;
                    break;
            }

            return shouldBeIgnore;
        }
    }
}
