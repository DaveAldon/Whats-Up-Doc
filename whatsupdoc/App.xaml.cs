using Xamarin.Forms;
using Xamarin.Essentials;

namespace whatsupdoc
{
    public partial class App : Application
    {
        public App()
        {
            Preferences.Set("FHIR_Resource_Default", "http://hapi.fhir.org/baseR4/");
            Preferences.Set("ProviderQuery", "&_include=PractitionerRole:organization&_include=PractitionerRole:location");
            Preferences.Set("PractitionerRoleResource", "PractitionerRole?specialty=");
            Preferences.Set("ConferenceLink", "https://www.devdays.com/us/wp-content/uploads/sites/5/2020/06/PROGRAM_US_VIRTUAL_EDITION_2020_3.pdf");

            InitializeComponent();

            MainPage = new NavigationPage(new SearchPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
