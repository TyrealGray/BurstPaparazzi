using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace BurstPaparazzi.core
{
    class NormalTerminator : ITerminator
    {
        private List<string> m_terminateList = new List<string>();

        static private string m_guardExePath = Directory.GetCurrentDirectory() + "\\guard\\BurstPaparazziGuard.exe";

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

            List<string> paparazziLocation = findPaparazziLocation(name);

            string orignFilePath, newFilePath, targetPath;

            for (int index = 0; index < paparazziLocation.Capacity; index++)
            {

                orignFilePath = paparazziLocation[index];

                if (0 == index)
                {
                    targetPath = Directory.GetCurrentDirectory() + "\\isolate";
                    if (!Directory.Exists(targetPath)) Directory.CreateDirectory(targetPath);
                    newFilePath = targetPath + "\\" + exeName;
                    File.Copy(orignFilePath, newFilePath, true);
                }

                File.Delete(orignFilePath);

                File.Copy(m_guardExePath, orignFilePath, true);

            }
        }

        static public void recover(string name)
        {
            string orignFilePath = Directory.GetCurrentDirectory() + "\\isolate\\"+ name+".exe";

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
    }
}
