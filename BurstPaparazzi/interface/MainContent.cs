using System.Windows;
using System.Windows.Controls;
using BurstPaparazzi.core;
using System.Collections.Generic;
using System.IO;
using System;

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
            m_normalTerminator.watchBegin();
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

            LogWindow log = new LogWindow();

            log.printLog(isolateList.SelectedItem.ToString() + " now is recovered");

            log.Show();

            refreshIsolateList();
        }

        private void OnClickAutoBurstButton(object sender, RoutedEventArgs e)
        {
            List<string> stubbornPaparazzi = new List<string>();

            stubbornPaparazzi.AddRange(m_normalTerminator.terminate());

            stubbornPaparazzi.AddRange(m_tencentTerminator.terminate());

            LogWindow log = new LogWindow();

            if (0 == stubbornPaparazzi.Count)
            {
                log.printLog("succeed.");
            }
            else
            {
                log.printBurstError(stubbornPaparazzi);
            }

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
            LogWindow log = new LogWindow();

            if (!NormalTerminator.terminateByName(burstName, isBurstForever))
            {
                List<string> result = new List<string>();
                result.Add(burstName + ".exe");

                log.printBurstError(result);
            }
            else
            {
                log.printLog(burstName + ".exe now fly in sky :)");
            }

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
