﻿using System;
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
using System.Windows.Shapes;

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

        public void printLog(List<string> logInfo)
        {
            string logPrefix = "Cannot burst ";

            foreach (string info in logInfo)
            {
                logBlock.Inlines.Add(new Run(logPrefix + info));
                logBlock.Inlines.Add(new LineBreak());
            }
        }
    }
}
