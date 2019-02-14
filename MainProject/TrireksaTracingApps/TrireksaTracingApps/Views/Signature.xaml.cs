using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrireksaTracingApps.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Signature : ContentPage
	{
		public Signature ()
		{
			InitializeComponent ();
            signatureView.CaptionText = "Tanda Tangan Disini";
            tcs = new System.Threading.Tasks.TaskCompletionSource<bool>();
        }

        public Stream ResultValue { get; private set; }
        public ImageSource Sign { get; set; }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //ResultValue = await signatureView.GetImageStreamAsync(SignatureImageFormat.Jpeg);
            //Sign = ImageSource.FromStream(() => { return ResultValue; });
            await Navigation.PopModalAsync();
        }



        public Task PageClosedTask { get { return tcs.Task; } }

        private TaskCompletionSource<bool> tcs { get; set; }

     
        // Either override OnDisappearing 
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            tcs.SetResult(true);
        }

        // Or provide your own PopAsync function so that when you decide to leave the page explicitly the TaskCompletion is triggered
        public async Task PopAwaitableAsync()
        {
            await Navigation.PopAsync();
            tcs.SetResult(true);
        }

    }
}