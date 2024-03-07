namespace KnigtOwls.Pages;

public partial class FormPage : ContentPage
{
	public FormPage()
	{
		InitializeComponent();
	}

    async void OnBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PopAsync();
    }
}