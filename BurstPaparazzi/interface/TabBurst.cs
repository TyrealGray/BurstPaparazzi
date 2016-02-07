using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BurstPaparazzi.core;
using System.Collections.Generic;

namespace BurstPaparazzi
{
    class MainTabControl
    {
        private TabControl m_control = null;

        private NormalTerminator m_normalTerminator = new NormalTerminator();
        private TencentTerminator m_tencentTerminator = new TencentTerminator();

        public MainTabControl(TabControl control)
        {
            m_control = control;
        }

        public void BindEvent()
        {
            ((Button)m_control.FindName("burstItButton")).Click += OnBurstItButtonClick;

            ((Button)m_control.FindName("autoBurstButton")).Click += OnClickAutoBurstButton;
        }

        private void OnClickAutoBurstButton(object sender, RoutedEventArgs e)
        {
            List<string> stubbornPaparazzi = new List<string>();

            stubbornPaparazzi = m_normalTerminator.terminate();

            stubbornPaparazzi = m_tencentTerminator.terminate();

        }

        private void OnBurstItButtonClick(object sender, RoutedEventArgs e)
        {
            TextBox burstNameTextBox = (TextBox)m_control.FindName("burstNameTextBox");

            string burstName = burstNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(burstName))
            {
                return;
            }

            if (!NormalTerminator.terminateByName(burstName))
            {
                MessageBox.Show("Oh no,those paparazzi are still looking at your ass :(");
                return;
            }

            MessageBox.Show("Paparazzi now fly in sky :)");
        }
    }
}
