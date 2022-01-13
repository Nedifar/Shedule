﻿using AgendaApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CabinetPage : ContentPage
    {
        HttpClient http = new HttpClient();
        Date dateSchedule = new Date();
        public CabinetPage()
        {
            InitializeComponent();
            GetGroupList();
            dpDateSchedule.Date = DateSave.date.SelectedDate;
            dpDateSchedule_DateSelected(null, null);
            tryingNewSchedule();
            this.BindingContext = this;
        }

        private async void dpDateSchedule_DateSelected(object sender, DateChangedEventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    loading.IsVisible = true;
                    loading.IsAnimationPlaying = true;
                    cvSchedule.IsVisible = false;
                    cvSchedule.ItemsSource = null;
                    dateSchedule.GetDate(dpDateSchedule.Date);
                    lbFirstDay.Text = dateSchedule.downDay.ToString();
                    lbSecondDay.Text = dateSchedule.upDay.ToString();
                    lbFirstMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateSchedule.DupDay.Month);
                    lbSecondMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateSchedule.DdownDay.Month);                    
                    var resDate = await http.GetAsync(new Uri($"https://bsite.net/Greorgi/api/lastdance/getdate/{dpDateSchedule.Date}"));
                    pCabinet_SelectedIndexChanged(null, null);
                    resDate.EnsureSuccessStatusCode();
                    cvSchedule.IsVisible = true;
                    loading.IsAnimationPlaying = false;
                    loading.IsVisible = false;
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                        continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }

        private async void pCabinet_SelectedIndexChanged(object sender, EventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    if (pCabinet.SelectedIndex != -1)
                    {
                        loading.IsVisible = true;
                        loading.IsAnimationPlaying = true;
                        cvSchedule.IsVisible = false;
                        var resGroup = await http.GetAsync(new Uri($"https://bsite.net/Greorgi/api/lastdance/getcabinet/{pCabinet.SelectedItem.ToString()}"));
                        resGroup.EnsureSuccessStatusCode();
                        var groupShedule = resGroup.Content.ReadAsAsync<List<DayWeek>>();
                        List<DayWeek> list = await groupShedule;
                        Agenda agenda = new Agenda();
                        cvSchedule.ItemsSource = agenda.MyAgenda(DateSave.date.DdownDay, list);
                        cvSchedule.IsVisible = true;
                        loading.IsAnimationPlaying = false;
                        loading.IsVisible = false;

                    }
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                        continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }

        private async void GetGroupList()
        {
            while (5 > 3)
            {
                try
                {
                    var resGroupList = await http.GetAsync(new Uri($"https://bsite.net/Greorgi/api/lastdance/getcabinetslist/"));
                    resGroupList.EnsureSuccessStatusCode();
                    pCabinet.ItemsSource = await resGroupList.Content.ReadAsAsync<List<string>>();
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                        continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    if (NewSheduleBt.Source.ToString() == "gg.png")
                    {
                        string s = "Новое расписание!!!";
                        var resultNewWeek = await http.GetAsync(new Uri($"http://lastDance.com/api/lastdance/getnewWeek/{s}"));
                        resultNewWeek.EnsureSuccessStatusCode();
                        pCabinet.SelectedIndex = -1;
                        dpDateSchedule.Date = DateTime.Today.AddDays(5);
                        NewSheduleBt.Source = "old.png";
                    }
                    else
                    {
                        var resNew = await http.GetAsync(new Uri($"https://bsite.net/Greorgi/api/lastdance/getdate/{DateTime.Now.ToShortDateString()}"));
                        resNew.EnsureSuccessStatusCode();
                        NewSheduleBt.Source = "gg.png";
                        pCabinet.SelectedIndex = -1;
                        dpDateSchedule.Date = DateTime.Today;
                    }
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                        continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }
        private async void tryingNewSchedule()
        {
            while (5 > 3)
            {
                try
                {
                    var resnew = await http.GetAsync(new Uri($"https://bsite.net/Greorgi/api/lastdance/getnew"));
                    resnew.EnsureSuccessStatusCode();
                    var res = resnew.Content.ReadAsStringAsync();
                    if (res.ToString() == "есть новое расписание")
                    {
                        NewSheduleBt.IsVisible = true;
                    }
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                        continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }
    }
}