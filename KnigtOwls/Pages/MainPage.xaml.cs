using BitMiracle.LibTiff.Classic;
using CommunityToolkit.Maui.Alerts;
using KnigtOwls.Model;
using Plugin.LocalNotification;
using SkiaSharp;
using UraniumUI.Dialogs;
using UraniumUI.Dialogs.CommunityToolkit;

namespace KnigtOwls.Pages
{
    public partial class MainPage : ContentPage
    {

        //public IDialogService DialogService { get; set; }

        public MainPage(/*IDialogService dialogService*/)
        {
            InitializeComponent();
            //DialogService = dialogService;
        }

        async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FormPage");
        }

        private void OpenPhoto_Clicked (object sender, EventArgs e)
        {
            MainPage.OpenPhotoCamera();
        }

        private void OpenCamera_Clicked(object sender, EventArgs e)
        {
            MainPage.OpenCamera();
        }

        public static async void OpenCamera()
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();

            if (MediaPicker.Default.IsCaptureSupported && status == PermissionStatus.Granted)
            {
                FileResult photo = await MediaPicker.Default.CaptureVideoAsync();

                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    //Reduce the size of the image. 
                    using Stream sourceStream = await photo.OpenReadAsync();
                    using SKBitmap sourceBitmap = SKBitmap.Decode(sourceStream);
                    int height = Math.Min(794, sourceBitmap.Height);
                    int width = Math.Min(794, sourceBitmap.Width);

                    using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);
                    using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);

                    using (SKData data = scaledImage.Encode())
                    {
                        File.WriteAllBytes(localFilePath, data.ToArray());
                    }

                    ImageModel model = new ImageModel() { ImagePath = localFilePath, Title = "sample", Description = "Cool" };
                    //viewModel.Items.Add(model);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Device is not supported", "Okay");
            }
        }

        public static async void OpenPhotoCamera()
        {
            var status = await Permissions.RequestAsync<Permissions.Camera>();

            if (MediaPicker.Default.IsCaptureSupported && status == PermissionStatus.Granted)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    //Reduce the size of the image. 
                    using Stream sourceStream = await photo.OpenReadAsync();
                    using SKBitmap sourceBitmap = SKBitmap.Decode(sourceStream);
                    int height = Math.Min(794, sourceBitmap.Height);
                    int width = Math.Min(794, sourceBitmap.Width);

                    using SKBitmap scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.Medium);
                    using SKImage scaledImage = SKImage.FromBitmap(scaledBitmap);

                    using (SKData data = scaledImage.Encode())
                    {
                        File.WriteAllBytes(localFilePath, data.ToArray());
                    }

                    ImageModel model = new ImageModel() { ImagePath = localFilePath, Title = "sample", Description = "Cool" };
                    //viewModel.Items.Add(model);
                }

            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Device is not supported", "Okay");
            }
        }

        //private async void OpenCamera_Clicked(object sender, EventArgs e)
        //{
        //    var result = await this.DisplayTextPromptAsync("Your Name", "What is your name?", placeholder: "Uvuvwevwevwe...Osas");

        //    await DisplayAlert("Result:", result, "OK");
        //}

        //public static async void CallNotif()
        //{

        //    var request = new NotificationRequest
        //    {
        //        NotificationId = 1353,
        //        Title = "Notification",
        //        Subtitle = "Camera Opened",
        //        Description = "Capture photo",
        //        BadgeNumber = 32,
        //        Schedule = new NotificationRequestSchedule
        //        {
        //            NotifyTime = DateTime.Now.AddSeconds(5)
        //        }
        //    };

        //    await LocalNotificationCenter.Current.Show(request);
        //}
    }
}