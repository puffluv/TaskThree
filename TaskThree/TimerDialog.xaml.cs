using System;
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

namespace TaskThree
{
    /// <summary>
    /// Логика взаимодействия для TimerDialog.xaml
    /// </summary>
    public partial class TimerDialog : Window
    {
        public TimerItem Timer { get; private set; }

        public TimerDialog()
        {
            InitializeComponent();
            Timer = new TimerItem();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Name = nameTextBox.Text;
            Timer.TargetDate = datePicker.SelectedDate ?? DateTime.Now;

            int hours, minutes, seconds;
            if (int.TryParse(hoursTextBox.Text, out hours) &&
                int.TryParse(minutesTextBox.Text, out minutes) &&
                int.TryParse(secondsTextBox.Text, out seconds))
            {
                Timer.TargetDate = Timer.TargetDate.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            }

            Timer.NotifyPropertyChanged(nameof(Timer.TargetDate)); // Добавляем это для обновления RemainingTimeFormatted

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}