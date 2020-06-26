using Xamarin.Essentials;
using Xamarin.Forms;

namespace whatsupdoc
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        // Save the settings to the utils
        async void Save_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            Utils.FHIR_Resource = FHIRResource.Text;
            Utils.Token = FHIRToken.Text;
            await DisplayAlert("Done!", "Settings updated successfully", "OK");
        }

        // Reset settings fields to default
        async void Default_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            FHIRResource.Text = "http://hapi.fhir.org/baseR4/";
            FHIRToken.Text = "";
        }

        // Shows conference info in an embedded browser
        async void Conference_Link(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(Utils.ConferenceLink, BrowserLaunchMode.SystemPreferred);
        }
    }
}
