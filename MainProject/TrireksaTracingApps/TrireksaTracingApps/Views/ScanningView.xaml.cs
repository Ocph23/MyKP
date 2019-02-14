using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanningView : ContentPage
	{
		public ScanningView ()
		{
			InitializeComponent ();
            Scan();
		}

        public ZXing.Net.Mobile.Forms.ZXingScannerView scanner = new ZXing.Net.Mobile.Forms.ZXingScannerView();


        public async void Scan()
        {
            try
            {
                await Task.Delay(200);
                scanner.Options = new MobileBarcodeScanningOptions()
                {
                    UseFrontCameraIfAvailable = false, //update later to come from settings
                    PossibleFormats = new List<BarcodeFormat>(),
                    TryHarder = true,
                    AutoRotate = false,
                    TryInverted = true,
                    DelayBetweenContinuousScans = 2000
                };

                scanner.IsVisible = true;
                scanner.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
                scanner.Options.PossibleFormats.Add(BarcodeFormat.DATA_MATRIX);
                scanner.Options.PossibleFormats.Add(BarcodeFormat.EAN_13);


                scanner.OnScanResult += (result) => {

                    // Stop scanning
                    scanner.IsAnalyzing = false;
                    if (scanner.IsScanning)
                    {
                        scanner.AutoFocus();
                    }

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(async () => {
                        if (result != null)
                        {
                           // await DisplayAlert("Scan Value", result.Text, "OK");
                        }
                    });
                };

                mainGrid.Children.Add(scanner, 1, 0);

            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
        }
    }
}