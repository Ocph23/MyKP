using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class ManifestView : ContentPage
	{
		public ManifestView ()
		{
			InitializeComponent ();
            BindingContext = new ManifestViewModel();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as manifest;
            Navigation.PushAsync(new Views.ManifestDetailView(item));
        }
    }


    public class ManifestViewModel:BaseViewModel
    {

        public ObservableCollection<manifest> Source { get; set; }
        public ICommand RefreshCommand { get; set; }

        public ManifestViewModel()
        {
            Source = new ObservableCollection<manifest>();
            RefreshCommand = new Command(() => RefreshAction());
            RefreshCommand.Execute(null);
        }

        private async void RefreshAction()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            var result = await ManifestServices.GetItemsAsync();
            if(result!=null)
            {
                foreach(var item in result)
                {
                    Source.Add(item);
                }
            }

            IsBusy = false;
        }
    }
}