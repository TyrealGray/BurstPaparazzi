using System.Windows;
using System.Windows.Controls;
using BurstPaparazzi.core;
using System.Collections.Generic;
using System.IO;

namespace BurstPaparazzi
{
    class MainContent
    {
        private TabControl m_control = null;

        private NormalTerminator m_normalTerminator = new NormalTerminator();
        private TencentTerminator m_tencentTerminator = new TencentTerminator();

        public MainContent(TabControl control)
        {
            m_control = control;
            refreshIsolateList();
        }

        public void BindEvent()
        {
            ((Button)m_control.FindName("burstItButton")).Click += OnBurstItButtonClick;

            ((Button)m_control.FindName("autoBurstButton")).Click += OnClickAutoBurstButton;

            ((Button)m_control.FindName("recoverButton")).Click += OnClickRecoverButton;

        }

        private void OnClickRecoverButton(object sender, RoutedEventArgs e)
        {
            ListBox isolateList = (ListBox)m_control.FindName("isolateList");

            m_normalTerminator.recover(isolateList.SelectedItem.ToString());

            refreshIsolateList();
        }

        private void OnClickAutoBurstButton(object sender, RoutedEventArgs e)
        {
            List<string> stubbornPaparazzi = new List<string>();

            stubbornPaparazzi.AddRange(m_normalTerminator.terminate());

            stubbornPaparazzi.AddRange(m_tencentTerminator.terminate());

            LogWindow log = new LogWindow();
            
            log.printLog(stubbornPaparazzi);
            log.Show();

            refreshIsolateList();
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
            List<string> result = new List<string>();

            if (!NormalTerminator.terminateByName(burstName, isBurstForever))
            {
                result.Add(burstName + ".exe");
            }
            else
            {
                MessageBox.Show("Paparazzi now fly in sky :)");
            }

            LogWindow log = new LogWindow();

            log.printLog(result);
            log.Show();

            refreshIsolateList();
        }

        private void refreshIsolateList()
        {
            ListBox isolateList = (ListBox)m_control.FindName("isolateList");

            isolateList.Items.Clear();

            string isolatePath = Directory.GetCurrentDirectory() + "\\isolate";

            DirectoryInfo folder = new DirectoryInfo(isolatePath);

            foreach (FileInfo file in folder.GetFiles("*.exe"))
            {
                isolateList.Items.Add(file.Name);
            }
        }
    }
}
