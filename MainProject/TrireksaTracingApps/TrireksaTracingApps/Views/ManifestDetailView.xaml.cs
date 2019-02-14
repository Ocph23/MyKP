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

namespace TrireksaTracingApps.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ManifestDetailView : ContentPage
	{
		public ManifestDetailView (manifest item)
		{
			InitializeComponent ();
            BindingContext = new ManifestDetailViewModel(item);
		}

       
    }



    public class ManifestDetailViewModel:BaseViewModel
    {
        private manifest manifestItem;
        public ICommand RefreshCommand { get; set; }
        public ICommand TerimaCommand { get; set; }
        public manifest Manifest
        {
            get { return manifestItem; }
            set
            {
                SetProperty(ref manifestItem, value);
            }
        }

        public ManifestDetailViewModel(manifest item)
        {
            Manifest = item;
            Manifest.Items = new System.Collections.ObjectModel.ObservableCollection<stt>();
            RefreshCommand = new Command(() => RefreshAction());
            TerimaCommand = new Command(Terima, CanTerima);


            RefreshCommand.Execute(null);

        }

        private async void Terima(object obj)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var Saved = await ManifestServices.UpdateItemAsync(Manifest);
            if (!Saved)
            {
                Manifest.RecieveOnPort = null;
            }
            IsBusy = false;
        }

        private bool CanTerima(object arg)
        {
            if (Manifest.RecieveOnPort == null)
                return true;
            return false;
        }

        private async void RefreshAction()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var result = await ManifestServices.GetItemAsync(Manifest.Id.ToString());
            if (result != null)
            {
                foreach (var item in result.Items)
                {

                    Manifest.Items.Add(item);
                }
            }

            IsBusy = false;
        }

      
    }
}