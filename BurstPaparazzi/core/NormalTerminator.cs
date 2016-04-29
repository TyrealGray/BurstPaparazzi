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
        //TODO need guard exe program
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
                if (!terminateByName(name))
                {
                    stubbornList.Add(name);
                }
            }

            return stubbornList;
        }

        static public bool terminateByName(string name)
        {
            Process[] processes = Process.GetProcessesByName(name);

            try
            {
                foreach (Process p in processes)
                {
                    p.Kill();
                    p.Close();
                }

                isolate(name);

                return true;
            }
            catch
            {
                return false;
            }
        }

        static public void isolate(string name)
        {
            const int bufsize = 260;
            StringBuilder buf = new StringBuilder(bufsize);

            string exeName = name + ".exe";

            // set the search
            Everything.Everything_SetSearchW(exeName);

            // execute the query
            Everything.Everything_QueryW(true);

            string orignFilePath, newFilePath, targetPath;

            for (int index = 0; index < Everything.Everything_GetNumResults(); index++)
            {
                // get the result's full path and file name.
                Everything.Everything_GetResultFullPathNameW(index, buf, bufsize);

                orignFilePath = buf.ToString();

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
    }
}
