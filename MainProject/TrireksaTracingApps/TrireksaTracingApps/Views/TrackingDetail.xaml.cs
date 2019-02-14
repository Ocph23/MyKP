using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;
using TrireksaTracingApps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrackingDetail : ContentPage
	{
		public TrackingDetail (Models.stt resultData)
		{
			InitializeComponent ();
            BindingContext = new TrackingDetailViewModel(resultData, Navigation);
		}
	}


    public class TrackingDetailViewModel:BaseViewModel
    {
        private ImageSource _sign;

        public TrackingDetailViewModel(Models.stt resultData, INavigation navigation)
        {
            Model = resultData;
            Navigation = navigation;
            SignCommand = new Command(SignAction);
            if (resultData.Status != null && resultData.Status.Sign!=null)
            {
                Stream sign = new MemoryStream(resultData.Status.Sign);
                Sign = ImageSource.FromStream(() => { return sign; });
            }
                

        }
        public ImageSource Sign {
            get { return _sign; }
            set
            {
                SetProperty(ref _sign, value);
            }
        }

        private async void SignAction(object obj)
        {
            var page = new Signature();
            await Navigation.PushModalAsync(page);
            await page.PageClosedTask;
        }

        private void Complete(Task arg1, object arg2)
        {
            throw new NotImplementedException();
        }

        public stt Model { get; set; }
        public INavigation Navigation { get; }
        public Command SignCommand { get; }
    }
}