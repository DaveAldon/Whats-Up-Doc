using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;

namespace whatsupdoc
{
    public partial class ResultPage : ContentPage
    {
        // List of provider objects for the listview
        IList<Provider> Providers = new List<Provider>();

        public ResultPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Providers = new List<Provider>();

            var o = Utils.GetFHIRResource();
            JObject results = JObject.Parse(o);

            // Parent structure
            var entries = results["entry"];

            // Iter through the providers
            foreach (var entry in entries)
            {
                try
                {
                    // Parsing out certain fields
                    var pName = entry["resource"]["practitioner"]["display"].ToString();
                    // Names have numbers in the sample data. This makes them prettier
                    pName = Regex.Replace(pName, @"[\d-]", string.Empty);

                    var pOrganization = entry["resource"]["organization"]["display"].ToString();
                    //var pSpecialty = entry["resource"]["specialty"][1]["coding"][0]["display"].ToString();

                    // Add them to a new provider object
                    Providers.Add(new Provider
                    {
                        ProviderName = pName,
                        ProviderOrganization = pOrganization,
                        //ProviderSpecialty = pSpecialty
                    });
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }

            listView.ItemsSource = Providers
                .OrderBy(d => d.ProviderName)
                .ToList();

            // Troubleshooting that displays all of the provider objects
            /*
            foreach(var x in Providers)
            {
                await DisplayAlert("Alert", x.ProviderName + " " + x.ProviderOrganization + " " + x.ProviderSpecialty, "OK");
            }
            */
        }

        // Search bar filtering
        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            listView.BeginRefresh();
            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                listView.ItemsSource = Providers.OrderBy(d => d.ProviderName).ToList();
            else
                listView.ItemsSource = Providers.OrderBy(d => d.ProviderName).ToList()
                    .Where(i => i.ProviderName.IndexOf(e.NewTextValue, StringComparison.OrdinalIgnoreCase) != -1);

            listView.EndRefresh();
        }

        // User selects a provider
        async void Clicked_Selected_Provider(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new ProviderPage
                {
                    BindingContext = this.BindingContext
                });
            }
        }
    }
}
