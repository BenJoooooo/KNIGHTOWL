using KnigtOwls.Model;
using System.Windows.Input;

namespace KnigtOwls.Pages;

public partial class FormPage : ContentPage
{

    FormModel fm;

    public FormPage()
	{
        InitializeComponent();

        fm = new FormModel();
        BindingContext = fm;
    }

    async void OnBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    public void ClearForm_Clicked(object sender, EventArgs e)
    {
        fm.FirstName = string.Empty;
        fm.LastName = string.Empty;
        fm.Email = string.Empty;
        fm.Contact = string.Empty;
    }


}