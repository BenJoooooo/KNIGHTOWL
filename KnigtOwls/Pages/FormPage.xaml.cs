using KnigtOwls.Model;
using System.Windows.Input;

namespace KnigtOwls.Pages;

public partial class FormPage : ContentPage
{

    public FormPage()
	{
        InitializeComponent();
        //BindingContext = new FormModel();
    }

    async void OnBackButton_Clicked(object sender, EventArgs e)
    {
        //await Shell.Current.Navigation.PopAsync();
        await Shell.Current.GoToAsync("//MainPage");
    }


}