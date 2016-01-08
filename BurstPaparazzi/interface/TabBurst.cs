using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace BurstPaparazzi
{
    class MainTabControl
    {
        private TabControl m_control = null;

        public MainTabControl(TabControl control)
        {
            m_control = control;
        }

        public void BindEvent()
        {
            Button burstItButton = (Button)m_control.FindName("burstItButton");

            burstItButton.Click += OnBurstItButtonClick;
        }

        private void OnBurstItButtonClick(object sender, RoutedEventArgs e)
        {
            TextBox burstNameTextBox = (TextBox)m_control.FindName("burstNameTextBox");

            string burstName = burstNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(burstName))
            {
                return;
            }

            Process[] processes = Process.GetProcessesByName(burstName);

            if (0 == processes.Length)
            {
                MessageBox.Show("There is no paparazzo named " + burstName + " :(");
                return;
            }

            try
            {
                foreach (Process p in processes)
                {
                    p.Kill();
                    p.Close();
                }

                MessageBox.Show("Paparazzi now fly in sky :)");
            }
            catch
            {
                MessageBox.Show("Oh no,those paparazzi are still looking at your ass :(");
            }

        }
    }
}
