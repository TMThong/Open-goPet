using NativeClient.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace NativeClient.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}