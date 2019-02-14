using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TrireksaTracingApps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
		public LoginView ()
		{
			InitializeComponent ();
            BindingContext = new LoginViewModel();
		}

        private async void ClickGestureRecognizer_Clicked(object sender, EventArgs e)
        {
            await ipAddress.TranslateTo(0, 0, 2000, Easing.BounceIn);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await ipAddress.TranslateTo(-300, 0, 2000, Easing.BounceIn);
        }
    }

    public class LoginViewModel :BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(() => LoginAction());
        }

       

        private async void LoginAction()
        {
            try
            {
                UserName = "ocph23@gmail.com";
                Password = "Sony@77";
                var result = await AuthServices.Login(UserName, Password);
                if (result != null)
                {
                    var main = await Helper.GetBaseApp();
                    main.ChangeScreen(new MenuView());
                }
            }
            catch (Exception ex)
            {
                Helper.ShowMessageError(ex.Message);
            }
        }


        private string  username;

        public string UserName
        {
            get { return username; }
            set {SetProperty(ref username ,value); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        private string server;
        public string Address
        {
            get { return Helper.Server; }
            set {
                Helper.Server = server;
                SetProperty(ref server, value); }
        }

    }

}