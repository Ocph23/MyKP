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
            var vm = new TrackingDetailViewModel(resultData, Navigation);
            vm.OnClose += Vm_OnClose;
            BindingContext = vm;
		}

        private void Vm_OnClose(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }


    public class TrackingDetailViewModel:BaseViewModel
    {
        private ImageSource _sign;
        public event EventHandler OnClose;
        public TrackingDetailViewModel(Models.stt resultData, INavigation navigation)
        {
            Model = resultData;
            if (Model.Status == null)
                Model.Status = new status();
            Navigation = navigation;
            SignCommand = new Command(SignAction);
            SaveCommand = new Command(SaveAction);
            if (resultData.Status != null && resultData.Status.Sign!=null)
            {
                Stream sign = new MemoryStream(resultData.Status.Sign);
                Sign = ImageSource.FromStream(() => { return sign; });
            }
        }

        private async void SaveAction(object obj)
        {
            if (string.IsNullOrEmpty(Model.Status.RecieverName) || Sign == null)
                Helper.ShowMessageError("Lengkapi Nama Dan Tanda Tangan Penerima !");
            else
            {
                
                var result = await StatusServices.AddItemAsync(Model);
                if (OnClose != null)
                    OnClose(result, new EventArgs());
            }
        }

        public byte[] ReadFully(Stream imagesource)
        {
            byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                imagesource.CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }
            return ImageBytes;
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
            page.OnSign += Page_OnSign;
            await Navigation.PushModalAsync(page);
        
        }

        private void Page_OnSign(object sender, EventArgs e)
        {
            Stream result = sender as Stream;
            if (result != null)
            {
                Model.Status.Sign = ReadFully(result);
                Sign = ImageSource.FromStream(() => { return result; });
            }
        }
            
        public stt Model { get; set; }
        public INavigation Navigation { get; }
        public Command SignCommand { get; }
        public Command SaveCommand { get; }
    }
}