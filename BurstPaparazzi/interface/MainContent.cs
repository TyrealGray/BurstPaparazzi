using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using BurstPaparazzi.core;
using System.Collections.Generic;
using System.IO;

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
            InitData();
        }

        public void BindEvent()
        {
            ((Button)m_control.FindName("burstItButton")).Click += OnBurstItButtonClick;

            ((Button)m_control.FindName("autoBurstButton")).Click += OnClickAutoBurstButton;
        }

        private void OnClickRecoverButton(object sender, RoutedEventArgs e)
        {
            ListBox isolateList = (ListBox)m_control.FindName("isolateList");

            //TODO recover a exe
        }

        private void OnClickAutoBurstButton(object sender, RoutedEventArgs e)
        {
            List<string> stubbornPaparazzi = new List<string>();

            stubbornPaparazzi.AddRange(m_normalTerminator.terminate());

            stubbornPaparazzi.AddRange(m_tencentTerminator.terminate());

        }

        private void OnBurstItButtonClick(object sender, RoutedEventArgs e)
        {
            TextBox burstNameTextBox = (TextBox)m_control.FindName("burstNameTextBox");

            string burstName = burstNameTextBox.Text;

            if (string.IsNullOrWhiteSpace(burstName))
            {
                return;
            }

            bool isBurstForever = ((CheckBox)m_control.FindName("isolateCheckBox")).IsChecked.Value;

            if (!NormalTerminator.terminateByName(burstName, isBurstForever))
            {
                MessageBox.Show("Oh no,those paparazzi are still looking at your ass :(");
                return;
            }

            MessageBox.Show("Paparazzi now fly in sky :)");
        }

        private void InitData()
        {
            ListBox isolateList = (ListBox)m_control.FindName("isolateList");

            string isolatePath = Directory.GetCurrentDirectory() + "\\isolate";

            DirectoryInfo folder = new DirectoryInfo(isolatePath);

            foreach (FileInfo file in folder.GetFiles("*.exe"))
            {
                isolateList.Items.Add(file.Name);
            }
        }
    }
}
