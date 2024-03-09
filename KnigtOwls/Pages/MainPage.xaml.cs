using KnigtOwls.Model;
using SkiaSharp;

namespace KnigtOwls.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void SettingsButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//FormPage");
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
    }
}