using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskThree
{
    public class TimerItem : INotifyPropertyChanged
    {
        private int hours;
        public int Hours
        {
            get { return hours; }
            set
            {
                if (value != hours)
                {
                    hours = value;
                    NotifyPropertyChanged(nameof(Hours));
                    UpdateRemainingTimeFormatted();
                }
            }
        }

        private int minutes;
        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value != minutes)
                {
                    minutes = value;
                    NotifyPropertyChanged(nameof(Minutes));
                    UpdateRemainingTimeFormatted();
                }
            }
        }

        private int seconds;
        public int Seconds
        {
            get { return seconds; }
            set
            {
                if (value != seconds)
                {
                    seconds = value;
                    NotifyPropertyChanged(nameof(Seconds));
                    UpdateRemainingTimeFormatted();
                }
            }
        }
        private int days;
        public int Days
        {
            get { return days; }
            set
            {
                if (value != days)
                {
                    days = value;
                    NotifyPropertyChanged(nameof(Days));
                    UpdateRemainingTimeFormatted();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value != name)
                {
                    name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
        }

        private DateTime targetDate;
        public DateTime TargetDate
        {
            get { return targetDate; }
            set
            {
                if (value != targetDate)
                {
                    targetDate = value;
                    NotifyPropertyChanged(nameof(TargetDate));
                    UpdateRemainingTimeFormatted();
                }
            }
        }

        private string remainingTimeFormatted;
        public string RemainingTimeFormatted
        {
            get { return remainingTimeFormatted; }
            set
            {
                if (value != remainingTimeFormatted)
                {
                    remainingTimeFormatted = value;
                    NotifyPropertyChanged(nameof(RemainingTimeFormatted));
                }
            }
        }
        public string DisplayString => $"{Name} - {RemainingTimeFormatted}";

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void UpdateRemainingTimeFormatted()
        {
            TimeSpan remainingTime = TargetDate - DateTime.Now;

            if (remainingTime.TotalSeconds <= 0)
            {
                RemainingTimeFormatted = "Время истекло";
            }
            else
            {
                RemainingTimeFormatted = $"{remainingTime.Days} дней, {remainingTime.Hours} часов, {remainingTime.Minutes} минут, {remainingTime.Seconds} секунд";
            }
        }
    }
}
