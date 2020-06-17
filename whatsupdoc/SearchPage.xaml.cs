using System.ComponentModel;
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

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
        }

        async void Search_Button_Clicked(System.Object sender, System.EventArgs e)
        {

            // Go to listview page and change its binding
            await Navigation.PushAsync(new ResultPage
            {
                // Updates the proper context for the next view
                BindingContext = this.BindingContext,
                code = "208D00000X"
            });
            
        }
    }
}
