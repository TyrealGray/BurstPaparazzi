using System.Collections.Generic;

namespace BurstPaparazzi.core
{
    class TencentTerminator : ITerminator
    {
        private List<string> m_terminateList = new List<string>();

       public TencentTerminator()
        {
            m_terminateList.Add("tencentdl");
            m_terminateList.Add("tenioDL");
        }

        public List<string> terminate()
        {
            List<string> stubbornList = new List<string>();

            foreach (string name in m_terminateList)
            {
                if (!NormalTerminator.terminateByName(name,false))
                {
                    stubbornList.Add(name);
                }
            }

            return stubbornList;
        }
    }
}
