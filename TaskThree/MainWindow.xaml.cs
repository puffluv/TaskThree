using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Threading;

namespace TaskThree
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public ObservableCollection<TimerItem> timers { get; private set; }
        private System.Timers.Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            timers = new ObservableCollection<TimerItem>();
            timerListBox.ItemsSource = timers;

            timer = new System.Timers.Timer(1000); // 1000 миллисекунд = 1 секунда
            timer.Elapsed += Timer_Tick;
            timer.AutoReset = true;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateRemainingTime();
            Debug.WriteLine("Timer Tick");
        }

        public void UpdateRemainingTime()
        {
            foreach (var timer in timers)
            {
                timer.UpdateRemainingTimeFormatted();
            }
        }
        private void AddTimerButton_Click(object sender, RoutedEventArgs e)
        {
            TimerDialog dialog = new TimerDialog();
            if (dialog.ShowDialog() == true)
            {
                timers.Add(dialog.Timer);
                UpdateRemainingTime();
            }
        }

        private void RemoveTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (timerListBox.SelectedIndex != -1)
            {
                timers.RemoveAt(timerListBox.SelectedIndex);
            }
        }

        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    foreach (var timer in timers)
                    {
                        writer.WriteLine($"{timer.Name};{timer.TargetDate}");
                    }
                }
            }
        }


        private void LoadFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                timers.Clear();

                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] parts = line.Split(';');
                        if (parts.Length == 2 && DateTime.TryParse(parts[1], out DateTime targetDate))
                        {
                            timers.Add(new TimerItem { Name = parts[0], TargetDate = targetDate });
                        }
                    }
                }
            }
        }

    }
}