using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;
using Plugin.LocalNotification;

namespace WorkoutTimer
{
    public partial class MainPage : ContentPage
    {
        private WorkoutTimerViewModel _viewModel;
        public MainPage()
        {
            InitializeComponent();
            _viewModel = new WorkoutTimerViewModel();
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
            {
                await LocalNotificationCenter.Current.RequestNotificationPermission();
            }
            LocalNotificationCenter.Current.NotificationActionTapped += _viewModel.OnNotificationActionTapped;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            LocalNotificationCenter.Current.NotificationActionTapped -= _viewModel.OnNotificationActionTapped;

            base.OnDisappearing();
        }
    }
}
