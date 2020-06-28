using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;
using Newtonsoft.Json.Linq;

namespace whatsupdoc
{
    public partial class ResultPage : ContentPage
    {
        // List of provider objects for the listview
        IList<Provider> Providers = new List<Provider>();
        // Specialty code populated from search page PushAsync
        public string code = "";

        public ResultPage()
        {
            InitializeComponent();

            // Pull to refresh list command
            listView.RefreshCommand = new Command(() => {   
                SearchForProviders();
                listView.IsRefreshing = false;
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // If the providers list already has data, we don't want to run the query again without user input
            if (!Providers.Any())
            {
                SearchForProviders();
            }
        }

        async void SearchForProviders()
        {
            listView.ItemsSource = null;
            Providers = new List<Provider>();

            var results = Utils.GetPractitionerRolesAPI(code);

            JToken entries = String.Empty;

            // Parent structure
            try
            {
                entries = results["entry"];
            }
            catch
            {
                await DisplayAlert("Alert", "Error searching registry. Is your token or URL valid in the settings?", "OK");
                await Navigation.PopAsync();
            }

            // Iter through the providers
            foreach (var entry in entries)
            {
                // TODO: Put json parse handling in a method to make exceptions cleaner and less redundant
                try
                {
                    string pName, pOrganization, pOrganizationID = "", pID = "", pRole = "", pSpecialty = "";
                    // Parsing out certain fields
                    try { pName = entry["resource"]["practitioner"]["display"].ToString(); } catch { pName = "Unknown Name"; }
                    // Names have numbers in the sample data. This makes them prettier
                    pName = Regex.Replace(pName, @"[\d-]", string.Empty);

                    // Parse out "header" information for the list
                    try { pOrganization = entry["resource"]["organization"]["display"].ToString(); } catch { pOrganization = "Unknown Organization"; }
                    try { pOrganizationID = entry["resource"]["organization"]["reference"].ToString(); } catch { }
                    try { pID = entry["resource"]["practitioner"]["reference"].ToString(); } catch { }

                    // Special data that is only in PractitionerRole
                    try { pRole = entry["resource"]["code"][0]["coding"][0]["code"].ToString(); } catch { }
                    try { pSpecialty = entry["resource"]["specialty"][0]["coding"][0]["display"].ToString(); } catch { }

                    // Add them to a new provider object
                    Providers.Add(new Provider
                    {
                        ProviderName = pName,
                        ProviderOrganization = pOrganization,
                        ProviderOrganizationID = pOrganizationID,
                        ProviderID = pID,
                        ProviderRole = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pRole.ToLower()),
                        ProviderSpecialty = pSpecialty
                    });
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }

            // Populate the listview
            listView.ItemsSource = Providers
                .OrderBy(d => d.ProviderName)
                .ToList();
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
                var providerContext = e.SelectedItem as Provider;
                
                try
                {
                    await Navigation.PushAsync(new ProviderPage
                    {
                        ProviderContext = providerContext,
                        ProviderResult = Utils.GetCustomAPI(providerContext.ProviderID),
                        OrganizationResult = Utils.GetCustomAPI(providerContext.ProviderOrganizationID)
                    });
                }
                catch
                {
                    await DisplayAlert("Alert", "Registry is missing information for this Provider", "OK");
                }
            }
        }
    }
}
