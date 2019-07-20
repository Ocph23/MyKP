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
        }

        public Stream ResultValue { get; private set; }
      

        private async void Button_Clicked(object sender, EventArgs e)
        {
            ResultValue = await signatureView.GetImageStreamAsync(SignatureImageFormat.Png);
          
            
            if (OnSign != null)
                OnSign(ResultValue, new EventArgs());
            await Navigation.PopModalAsync();
        }


        public event EventHandler OnSign;
     
    }
}