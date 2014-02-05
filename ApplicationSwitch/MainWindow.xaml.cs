using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;


namespace ApplicationSwitch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr SetFocus(HandleRef hWnd);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //Process current;
        int counter=0;
        List<Process> activeDesktopProcesses=new List<Process>();
        public MainWindow()
        {
            InitializeComponent();
            Process[] processes = Process.GetProcesses();
            foreach (var proc in processes)
            {
                if (!string.IsNullOrEmpty(proc.MainWindowTitle))
                {
                    activeDesktopProcesses.Add(proc);
                }
            }
        }

        public void myTimer_Elapsed(object source, EventArgs e)
        {
            if (counter < activeDesktopProcesses.Count)
            {
                SetForegroundWindow(activeDesktopProcesses[counter].MainWindowHandle);
            }
            else
            {
                counter = 0;
            }
            counter++;
            //Console.WriteLine("0");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //SetFocus(new HandleRef(null, current.MainWindowHandle));
            //SetForegroundWindow(current.MainWindowHandle);
            if (Convert.ToInt32(TimerValue.Text) >= 5)
            {
                Timer myTimer = new Timer(Convert.ToInt32(TimerValue.Text) * 1000);
                string buttonContent = switchButton.Content.ToString();
                if (buttonContent.Equals("Start Switch"))
                {
                    myTimer.Enabled = true;
                    myTimer.Elapsed += new ElapsedEventHandler(myTimer_Elapsed);
                    switchButton.Content = "Stop Switch";
                }
                else
                {
                    myTimer.Enabled = false;
                    switchButton.Content = "Start Switch";
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //private void activeProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    foreach (var proc in processes)
        //    {
        //        if (proc.MainWindowTitle == activeProgram.SelectedItem.ToString())
        //        {
        //            current = proc;
        //        }
        //    }
        //}

    }
}
