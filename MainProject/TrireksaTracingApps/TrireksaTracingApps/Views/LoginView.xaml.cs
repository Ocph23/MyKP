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
            var vm = new LoginViewModel(); ;
            BindingContext = vm;
            vm.HideIP += Vm_HideIP;
            vm.ShowIP += Vm_ShowIP;
            ipAddress.TranslateTo(-500, 0, 0, Easing.SinInOut);
        }

        private async void Vm_ShowIP(object sender, EventArgs e)
        {
            await ipAddress.TranslateTo(0, 0, 1000, Easing.SinInOut);
        }

        private async void Vm_HideIP(object sender, EventArgs e)
        {
           await ipAddress.TranslateTo(-500, 0, 1000, Easing.SinInOut);
        }
             
    }

    public class LoginViewModel :BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand ShowIPCommand { get; }
        public ICommand HideIPCommand { get; }

        public event EventHandler ShowIP;
        public event EventHandler HideIP;
        public LoginViewModel()
        {
            LoginCommand = new Command(() => LoginAction());
            ShowIPCommand = new Command(() => ShowIPAction());
            HideIPCommand = new Command(() => HideIPAction());
        }

        private void HideIPAction()
        {
            if (HideIP != null)
                HideIP(null, new EventArgs());
            Helper.Server = this.Address;
        }

        private void ShowIPAction()
        {

            if (ShowIP != null)
                ShowIP(null, new EventArgs());
        }

        private async void LoginAction()
        {
            try
            {
                //UserName = "ocph23@hotmail.com";
                //Password = "Sony@77";
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

                SetProperty(ref server, value);
                Helper.Server = value;
            }

        }
                             
    }

}