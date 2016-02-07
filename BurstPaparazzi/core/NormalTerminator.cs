using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace BurstPaparazzi.core
{
    class NormalTerminator : ITerminator
    {
        private List<string> m_terminateList = new List<string>();

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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
