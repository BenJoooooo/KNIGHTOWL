using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui;

namespace KnigtOwls.Pages;

public partial class VideoViewPage : ContentPage
{
	public VideoViewPage()
	{
		InitializeComponent();
        this.Appearing += OnPageReapearing;
	}

    async void OnBackButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }

    private async void OnPageReapearing(object sender, EventArgs e)
    {
        await InitializeCamera();
    }

    private async Task InitializeCamera()
    {
        await cameraView.StopRecordingAsync();

        nextButton.IsVisible = false;

        cameraView.Camera = cameraView.Cameras.FirstOrDefault();

        capture_Button.IsVisible = true;

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filename = "recordKnightOwl.mp4";
        string filePath = Path.Combine(documentsPath, filename);

        await cameraView.StartRecordingAsync(filePath, new Size(1920, 1080) );

        string message = "Camera is now recording";

        var snackbar = Snackbar.Make(message, null, "Okay",
            TimeSpan.FromSeconds(10), new CommunityToolkit.Maui.Core.SnackbarOptions
            {
                BackgroundColor = Colors.DarkGrey,
                TextColor = Colors.White,
                CornerRadius = 10,

            }, popup);

        await snackbar.Show();
    }

    private void CameraView_CamerasLoaded(object sender, EventArgs e)
    {

        cameraView.Camera = cameraView.Cameras.First();

        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string filename = "recordKnightOwl.mp4";
        string filePath = Path.Combine(documentsPath, filename);

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await cameraView.StartRecordingAsync(filePath, new Size(1920, 1080));
        });
    }

    private void OnCapture_Clicked(object sender, EventArgs e)
    {
        var image = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);

        if (image != null)
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
        await DisplayAlert("Notification", "Video has been sent", "OK");
        await Shell.Current.GoToAsync("//MainPage");
    }
}