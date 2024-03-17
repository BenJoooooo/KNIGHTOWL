namespace KnigtOwls.Pages;

public partial class CameraViewPage : ContentPage
{
	public CameraViewPage()
	{
		InitializeComponent();
		this.Appearing += OnPageReapearing;
	}

	private async void OnPageReapearing(object sender, EventArgs e)
	{
		await InitializeCamera();
	}

	private async Task InitializeCamera()
	{
		await cameraView.StopCameraAsync();
		nextButton.IsVisible = false;
		cameraView.Camera = cameraView.Cameras.FirstOrDefault();
		capture_Button.IsVisible = true;
		await cameraView.StartCameraAsync();
	}

    async void OnBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private void cameraView_CamerasLoaded(object sender, EventArgs e)
    {
		nextButton.IsVisible = false;

		cameraView.Camera = cameraView.Cameras.First();

		MainThread.BeginInvokeOnMainThread( async () =>
		{
			await cameraView.StartCameraAsync();
		});
    }

	private void OnCapture_Clicked(object sender, EventArgs e)
	{
		var image = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);

		if(image !=  null)
		{
			var capturedImage = new Image
			{
				Source = image,
				WidthRequest = 300,
				HeightRequest = 500
			};

			cameraFrame.Content = capturedImage;

			capture_Button.IsVisible = false;
			nextButton.IsVisible = true;
		}
	}

	private async void OnNext_Clicked(object sender, EventArgs e)
	{
		await DisplayAlert("Notification", "Photo has been sent", "OK");
		await Shell.Current.GoToAsync("//MainPage");
	}
}