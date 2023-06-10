using Plugin.LocalNotification;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutTimer
{
    public partial class App : Application
    {
        public static bool IsForeground { get; private set; } = false;
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override async void OnStart()
        {
            IsForeground = true;
        }

        protected override void OnSleep()
        {
            IsForeground = false;
        }

        protected override void OnResume()
        {
            IsForeground = true;
        }
    }
}
