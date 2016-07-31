using System;
using System.Windows;
using System.Windows.Forms;

namespace BurstPaparazzi
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainContent m_mainContent = null;

        private NotifyIcon m_notifyIcon;

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

            this.m_notifyIcon = new NotifyIcon();
            this.m_notifyIcon.BalloonTipText = "BurstPaparazzi";
            this.m_notifyIcon.ShowBalloonTip(2000);
            this.m_notifyIcon.Text = "BurstPaparazzi";
            this.m_notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.m_notifyIcon.Visible = true;

            this.m_notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            {
                if (e.Button == MouseButtons.Left) this.onShowing(o, e);
            });
        }

        private void onShowing(object sender, EventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void onHiding(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Hide();
        }

        private void onClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}