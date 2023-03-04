using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit;

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
    }
}
