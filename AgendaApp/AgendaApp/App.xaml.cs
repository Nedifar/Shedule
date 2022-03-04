using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;


namespace AgendaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.TabbedPag());
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=e7aa7329-25a0-490d-8287-7f1cc3f22b2c; ios=c5f3ae9a-c1f7-4da9-9ea6-c611de5b91f8",
                  typeof(Analytics), typeof(Distribute), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
