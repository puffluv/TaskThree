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
        public TimerItem Timer { get; set; }
        public TimerItem SelectedTimer { get; }

        public TimerDialog(TimerItem timer)
        {
            InitializeComponent();
            Timer = timer;
            DataContext = this;

            // Сброс значений полей при создании нового таймера
            ResetFields();

            nameTextBox.Text = Timer.Name;
            datePicker.SelectedDate = Timer.TargetDate; // Устанавливаем дату и время
            hoursTextBox.Text = Timer.Hours.ToString();
            minutesTextBox.Text = Timer.Minutes.ToString();
            secondsTextBox.Text = Timer.Seconds.ToString();
        }

        private void ResetFields()
        {
            nameTextBox.Text = string.Empty;
            datePicker.SelectedDate = DateTime.Now;
            hoursTextBox.Text = string.Empty;
            minutesTextBox.Text = string.Empty;
            secondsTextBox.Text = string.Empty;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Timer.Name = nameTextBox.Text;
            Timer.Hours = int.Parse(hoursTextBox.Text);
            Timer.Minutes = int.Parse(minutesTextBox.Text);
            Timer.Seconds = int.Parse(secondsTextBox.Text);
            Timer.TargetDate = datePicker.SelectedDate ?? DateTime.Now;

            int hours, minutes, seconds;
            if (int.TryParse(hoursTextBox.Text, out hours) &&
                int.TryParse(minutesTextBox.Text, out minutes) &&
                int.TryParse(secondsTextBox.Text, out seconds))
            {
                Timer.TargetDate = Timer.TargetDate.AddHours(hours).AddMinutes(minutes).AddSeconds(seconds);
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
        private void UpdateRemainingTimeFormatted()
        {
            Timer.UpdateRemainingTimeFormatted();
        }

    }
}