using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace StableDiffusionAPI_Sample
{
    public partial class ImageWindow : Window
    {
        public ImageWindow(byte[] imageData)
        {
            InitializeComponent();

            // BitmapImage 型に変換
            var generatedImage = Byte2BitmapImage(imageData);

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
