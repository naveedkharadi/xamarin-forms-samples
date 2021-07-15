using Xamarin.Forms;
using Todo.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Todo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new TodoListPage())
            {
                BarTextColor = Color.White,
                BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"]
            };
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=6ac016b6-b0bb-4f4d-93fe-6bea8651d72c;" +
                            "uwp=8ee797d3-efb8-4f05-987d-4bf9bec7de54;" +
                            "ios=c85b49f1-c665-47ab-89a9-a8c424f68439",
                            typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
