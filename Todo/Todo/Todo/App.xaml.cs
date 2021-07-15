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
            AppCenter.Start("android=20b33ffd-82cb-4ae2-9b51-19dbce0371cc;" +
                            "uwp={Your UWP App secret here};" +
                            "ios={Your iOS App secret here}",
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
