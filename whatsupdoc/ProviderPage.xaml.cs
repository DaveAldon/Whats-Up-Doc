﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace whatsupdoc
{
    public partial class ProviderPage : ContentPage
    {
        public JObject ProviderResult;
        public JObject OrganizationResult;
        public Provider ProviderContext = new Provider();

        public ProviderPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // TODO: Add each field to a single exception handling method to avoid the try catch repitition

            try { ProviderContext.ProviderNPI =  ProviderResult["identifier"][0]["value"].ToString(); } catch { };
            try { ProviderContext.ProviderRole = ProviderResult["identifier"][0]["value"].ToString(); } catch { };
            try { ProviderContext.ProviderSpecialty = ProviderResult["identifier"][0]["value"].ToString(); } catch { };
            try { ProviderContext.ProviderPhone = ProviderResult["telecom"][0]["value"].ToString(); } catch { };
            try { ProviderContext.ProviderEmail = ProviderResult["telecom"][1]["value"].ToString(); } catch { };
            try { ProviderContext.OrganizationName = OrganizationResult["name"].ToString(); } catch { };
            try { ProviderContext.OrganizationAddress =
                OrganizationResult["address"][0]["line"][0].ToString() + " " +
                OrganizationResult["address"][0]["city"].ToString() + ", " +
                OrganizationResult["address"][0]["state"].ToString();
            }
            catch { };

            //await DisplayAlert("Alert", ProviderContext.OrganizationAddress.ToString(), "OK");

            BindingContext = ProviderContext;
        }
    }
}