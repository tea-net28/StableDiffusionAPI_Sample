using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace StableDiffusionAPI_Sample
{
    public partial class ImageWindow : Window
    {
        byte[] _imageData;
        int _width, _height;


        public ImageWindow(byte[] imageData, int width, int height)
        {
            InitializeComponent();

            _imageData = imageData;
            _width = width;
            _height = height;

            this.Loaded += OnLoaded_ImageWindow;
        }

        private void OnLoaded_ImageWindow(object sender, RoutedEventArgs e)
        {
            // ウインドウのサイズを変更
            this.Width = _width;
            this.Height = _height;

            // BitmapImage 型に変換
            var generatedImage = Byte2BitmapImage(_imageData);

            // 画像を表示
            Image_Generated.Source = generatedImage;
        }

        public static BitmapImage Byte2BitmapImage(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                BitmapImage outputImage = new BitmapImage();
                outputImage.BeginInit();
                // メモリストリームをソースとして設定
                outputImage.StreamSource = ms;
                outputImage.CacheOption = BitmapCacheOption.OnLoad;
                outputImage.EndInit();

                return outputImage;
            }
        }
    }
}
