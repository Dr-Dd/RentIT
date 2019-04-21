﻿using RentIT.Data;
using RentIT.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RentIT
{
    public partial class App : Application
    {
        public static CredentialsManager CredManager { get; private set; }

        public App()
        {
            InitializeComponent();
            CredManager = new CredentialsManager(new RestService());
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}