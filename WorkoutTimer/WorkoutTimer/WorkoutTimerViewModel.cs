﻿using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WorkoutTimer
{
    public class WorkoutTimerViewModel : INotifyPropertyChanged
    {
        private int _sets;
        private int _elapsedTime;
        private Color _bgColor;
        private int _previousTime;
        public int Sets 
        {
            get => _sets;
            private set
            {
                _sets = value;
                OnPropertyChanged(nameof(Sets));
            }
        }

        private bool _isTimerRunning;

        public Boolean IsTimerRunning
        {
            get => _isTimerRunning;
            private set
            {
                _isTimerRunning = value;
                OnPropertyChanged(nameof(IsTimerRunning));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private int ElapsedTime 
        {
            get => _elapsedTime;
            set
            {
                if (value < 0)
                {
                    _elapsedTime = 0;
                }
                else
                {
                    _elapsedTime = value;
                }
                OnPropertyChanged(nameof(ElapsedTimeString));
            }
        }

        public string ElapsedTimeString
        {
            get => ConvertTime(_elapsedTime);
        }

        private string ConvertTime(int elapsed)
        {
            var ts = TimeSpan.FromSeconds(elapsed);
            return ts.Minutes.ToString().PadLeft(2, '0') + ":" + ts.Seconds.ToString().PadLeft(2, '0');
        }

        public Command<int> SetRestTimeCommand { get; private set; }

        public Command IncreaseSetsCommand { get; private set; }

        public Command DecreaseSetsCommand { get; private set; }

        public Command ResetSetsCommand { get; private set; }

        public Command StartTimerCommand { get; private set; }

        public Command StopTimerCommand { get; private set; }

        public Color BackgroundColor 
        {
            get => _bgColor;
            private set
            {
                _bgColor = value;
                OnPropertyChanged(nameof(BackgroundColor));
            }
        }

        public WorkoutTimerViewModel() 
        {
            BackgroundColor = Color.Green;
            Sets = 0;
            _isTimerRunning = false;
            ElapsedTime = 0;
            _previousTime = 0;

            //SetRestTimeCommand = new Command(SetRest);
            StartTimerCommand = new Command(StartTimer);
            IncreaseSetsCommand = new Command(IncreaseSets);
            DecreaseSetsCommand = new Command(DecreaseSets);
            ResetSetsCommand = new Command(ResetSets);
            StopTimerCommand = new Command(StopTimer);
        }

        public void OnNotificationActionTapped(NotificationEventArgs e)
        {

        }

        private void ResetSets()
        {
            Sets = 0;
            Vibration.Vibrate(TimeSpan.FromMilliseconds(100));
        }

        private void IncreaseSets()
        {
            Sets++;
        }

        private void DecreaseSets()
        {
            if (Sets > 0)
            {
                Sets--;
            }
        }

        public void SetRest(int rest)
        {
            ElapsedTime = rest;
        }

        private void StartTimer()
        {
            if (_isTimerRunning || ElapsedTime == 0)
            {
                return;
            }
            _isTimerRunning = true;
            BackgroundColor = Color.Red;
            _previousTime = ElapsedTime;
            LocalNotificationCenter.Current.CancelAll();
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                ElapsedTime--;
                if (ElapsedTime <= 0)
                {
                    _isTimerRunning = false;
                    BackgroundColor = Color.Green;
                    if (!App.IsForeground)
                    {
                        SendEndTimerNotification();
                    }
                    Vibration.Vibrate(TimeSpan.FromSeconds(1));
                    Sets++;
                    ElapsedTime = _previousTime;
                    return false;
                }
                return true;
            });
        }

        private void StopTimer()
        {
            ElapsedTime = 0;
            _isTimerRunning = false;
        }

        private async void SendEndTimerNotification()
        {
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Timer is up!",
                Description = "",
                ReturningData = "",
                Schedule =
                {
                    NotifyTime = null
                }
            };
            await LocalNotificationCenter.Current.Show(notification);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
