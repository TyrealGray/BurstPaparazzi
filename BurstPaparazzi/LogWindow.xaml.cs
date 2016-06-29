using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace BurstPaparazzi
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        public void printBurstError(List<string> logInfo)
        {
            string logPrefix = "Cannot burst ";

            foreach (string info in logInfo)
            {
                logBlock.Inlines.Add(new Run(logPrefix + info));
                logBlock.Inlines.Add(new LineBreak());
            }
        }

        public void printLog(string logInfo)
        {
            string logPrefix = "[info]: ";
            logBlock.Inlines.Add(new Run(logPrefix + logInfo));
        }
    }
}
