using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace whatsupdoc
{
    [DesignTimeVisible(false)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            // Refresh simple data points
            DiseasePicker.ItemsSource = Utils.GetDiseasePickers();
            // Hide search button
            Search_Button.IsVisible = false;
        }

        // Search init
        async void Search_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            // Go to listview page and change its binding
            await Navigation.PushAsync(new ResultPage
            {
                // Updates the proper context for the next view
                BindingContext = this.BindingContext,
                code = Utils.GetDiseaseMapping(DiseasePicker.SelectedItem.ToString())
            });
            
        }

        // If the user selects something, let them search
        void Changed_Disease_Selection(System.Object sender, System.EventArgs e)
        {
            Search_Button.IsVisible = true;
        }

        // Shows conference info in an embedded browser
        async void Conference_Link(System.Object sender, System.EventArgs e)
        {
            await Browser.OpenAsync(Utils.ConferenceLink, BrowserLaunchMode.SystemPreferred);
        }
    }
}
