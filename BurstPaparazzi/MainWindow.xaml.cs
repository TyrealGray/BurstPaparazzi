using System;
using System.Windows;

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