using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace whatsupdoc
{
    [DesignTimeVisible(false)]
    public partial class SearchPage : ContentPage
    {
        // List of provider objects for the listview
        IList<Provider> Providers = new List<Provider>();

        public SearchPage()
        {
            InitializeComponent();
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void Search_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var o = Utils.GetFHIRResource();
            JObject results = JObject.Parse(o);

            // Parent structure
            var entries = results["entry"];

            // Iter through the providers
            foreach(var entry in entries)
            {
                try
                {
                    // Parsing out certain fields
                    var pName = entry["resource"]["practitioner"]["display"].ToString();
                    var pOrganization = entry["resource"]["organization"]["display"].ToString();
                    //var pSpecialty = entry["resource"]["specialty"][1]["coding"][0]["display"].ToString();

                    // Add them to a new provider object
                    Providers.Add(new Provider
                    {
                        ProviderName =  pName,
                        ProviderOrganization = pOrganization,
                        //ProviderSpecialty = pSpecialty
                    });
                }
                catch(Exception exc)
                {
                    Console.WriteLine(exc);
                }
            }

            // Troubleshooting that displays all of the provider objects
            foreach(var x in Providers)
            {
                await DisplayAlert("Alert", x.ProviderName + " " + x.ProviderOrganization + " " + x.ProviderSpecialty, "OK");
            }
            
            /*
            // Go to listview page and change its binding
            await Navigation.PushAsync(new ResultPage
            {
                // Updates the proper context for the next view
                BindingContext = this.BindingContext
            });
            */
        }
    }
}
