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
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}