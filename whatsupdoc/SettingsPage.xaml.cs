using Xamarin.Essentials;
using Xamarin.Forms;

namespace whatsupdoc
{
    public partial class SettingsPage : ContentPage
    {
        private string FHIRResourceValue { get; set; } = Preferences.Get("FHIR_Resource", Preferences.Get("FHIR_Resource_Default", ""));
        private string FHIRTokenValue { get; set; } = Preferences.Get("Token", "");

        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        // Save the settings to the utils
        async void Save_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Preferences.Set("FHIR_Resource", FHIRResource.Text);
            Preferences.Set("Token", FHIRToken.Text);
            await DisplayAlert("Done!", "Settings updated successfully", "OK");
        }

        // Reset settings fields to default
        void Default_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var FHIRDefault = Preferences.Get("FHIR_Resource_Default", "");
            Preferences.Set("FHIR_Resource", FHIRDefault);
            Preferences.Set("Token", "");
            FHIRResourceValue = FHIRDefault;
            FHIRTokenValue = "";
            FHIRToken.Text = FHIRTokenValue;
            FHIRResource.Text = FHIRResourceValue;
        }

        // Shows conference info in an embedded browser
        async void Conference_Link(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(Preferences.Get("ConferenceLink", ""), BrowserLaunchMode.SystemPreferred);
        }
    }
}
