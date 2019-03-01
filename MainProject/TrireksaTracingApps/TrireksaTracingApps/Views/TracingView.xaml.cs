using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrireksaTracingApps.Models;
using TrireksaTracingApps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TracingView : ContentPage
	{
		public TracingView ()
		{
			InitializeComponent ();
            TracingViewModel vm;
            BindingContext =vm = new TracingViewModel();
            vm.OnSearch += Vm_OnSearch;

        }

        private async void Vm_OnSearch(object sender, EventArgs e)
        {
            var resultData = sender as stt;
            await Navigation.PushAsync(new TrackingDetail(resultData));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var option = new ZXing.Mobile.MobileBarcodeScanningOptions();
            option.AutoRotate = true;
            option.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE, ZXing.BarcodeFormat.CODABAR , ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.DATA_MATRIX};
            var scanPage = new ZXingScannerPage(option);
            scanPage.HeightRequest = 300;
            scanPage.WidthRequest = 200;
            
            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;
                if (scanPage.IsScanning)
                {
                    scanPage.AutoFocus();
                }

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                   // await DisplayAlert("Scanned Barcode", result.Text, "OK");
                    try
                    {
                        var data = result.Text.Split(';');
                        if (data.Length > 1)
                        {
                            var context = BindingContext as TracingViewModel;
                            var resultData =await context.Find(data[0], data[1]);
                            if (result != null)
                                await Navigation.PushAsync(new TrackingDetail(resultData));
                        }
                        else
                            throw new SystemException("Ulangi Scan QR Code");
                    }
                    catch (Exception ex)
                    {

                        await DisplayAlert("Error", ex.Message, "OK");
                    }
                   


                });
            };
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
        }
    }

    public class TracingViewModel:BaseViewModel
    {


        public event EventHandler OnSearch;
        private string _stt;
        public string STT
        {
            get { return _stt; }
            set { SetProperty(ref _stt ,value);
                SearchCommand = new Command(SearchAction, CanSearch);
            }
        }

        private ICommand searchCommand;

        public ICommand SearchCommand
        {
            get { return searchCommand; }
            set {
                SetProperty(ref searchCommand ,value);
            }
        }

        public ICommand ScanningCommand { get; set; }

        public TracingViewModel()
        {
            SearchCommand = new Command(SearchAction, CanSearch);
            ScanningCommand = new Command(ScanningAction);
        }

        private void ScanningAction(object obj)
        {
           
        }

        private bool CanSearch(object arg)
        {
            if (string.IsNullOrEmpty(STT))
                return false;
            return true;
        }

        private async void SearchAction(object obj)
        {
            var data = await ManifestServices.FindSTT(STT);
            if (data != null && OnSearch != null)
                OnSearch(data, new EventArgs());
            else
            {
                Helper.ShowMessageError("Data Tidak Ditemukan");
            }
        }

        internal async Task<stt> Find(string v1, string v2)
        {
            var agentid = Convert.ToInt32(v1);
            var data = await  ManifestServices.FindSTT(agentid, v2);
            return data;

        }
    }
}