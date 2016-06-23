using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BurstPaparazzi
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainContent m_mainContent = null;

        public MainWindow()
        {
            InitializeComponent();

            InitInterface();

            BindInterfaceEvent();
        }

        private void BindInterfaceEvent()
        {
            m_mainContent.BindEvent();
        }

        private void InitInterface()
        {
            m_mainContent = new MainContent(mainTabControl);


            //test code
            LogWindow log = new LogWindow();
            log.Show();

            List<string> testlist = new List<string>();
            testlist.Add("1");
            testlist.Add("2");

            log.printLog(testlist);

            LogWindow log2 = new LogWindow();
            log2.Show();
        }

    }
}