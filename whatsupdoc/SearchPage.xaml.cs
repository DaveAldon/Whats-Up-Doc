using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            await Navigation.PushAsync(new ResultPage
            {
                // Updates the proper context for the next view
                BindingContext = this.BindingContext
            });
        }
    }
}
