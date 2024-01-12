using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Windows.Input;

namespace StableDiffusionAPI_Sample
{
    public class MainModel
    {
        MainViewModel _viewmodel;
        Stopwatch sw;

        public MainModel(MainViewModel viewmodel)
        {
            _viewmodel = viewmodel;
            sw = new Stopwatch();
        }

        public async void GenerateImage()
        {
            _viewmodel.CanGenerateImage = false;
            sw.Restart();
            LogWriter.AddLog("画像生成を開始", false);

            int imageWidth = _viewmodel.Width;
            int imageHeight = _viewmodel.Height;

            JsonData jsonData = new JsonData(_viewmodel.Prompt, _viewmodel.NegativePrompt, imageWidth, imageHeight, _viewmodel.BatchSize);

            //LogWriter.AddLog(JsonSerializer.Serialize<JsonData>(jsonData));

            // uri の作成
            Uri uri = new Uri($"http://{_viewmodel.Ip}:{_viewmodel.Port}/sdapi/v1/txt2img");
            //Uri uri = new Uri(@"http://192.168.11.111:7860/sdapi/v1/options");
            //LogWriter.AddLog(uri.ToString());

            // 構造体を作成
            HttpDatas httpDatas = new HttpDatas()
            {
                Uri = uri,
                Data = JsonSerializer.Serialize<JsonData>(jsonData)
            };

            // POST して画像を生成する
            string result = await _viewmodel.HttpCommunications.PostAsync(httpDatas);
            //LogWriter.AddLog(result);

            _viewmodel.CanGenerateImage = true;
            
            // 出力結果をオブジェクト形式に変換
            if (String.IsNullOrWhiteSpace(result))
                return;
            LogWriter.AddLog($"画像生成が終了しました\n\t処理時間： {sw.ElapsedMilliseconds.ToString("n0", CultureInfo.CurrentCulture)} ms", false);
            sw.Stop();
            _viewmodel.jsonResponse = JsonSerializer.Deserialize<JsonResponse>(result);

            // 画像表示用のウインドウを作成・表示
            this.ShowGeneratedImages(imageWidth, imageHeight);
        }



        public void ShowGeneratedImages(int imageWidth, int imageHeight)
        {
            try
            {
                // 生成した画像分ウインドウを作成して表示
                foreach (var (item, index) in _viewmodel.jsonResponse.images.Select((item, index) => (item, index)))
                {
                    // 2枚以上生成した場合はグリッド画像も送られてくるため そのデータはスキップ
                    if (_viewmodel.jsonResponse.images.Count > 1)
                    {
                        if (index == 0)
                            continue;
                    }

                    // BASE64 形式 > byte 変換
                    byte[] imageData = Convert.FromBase64String(item);

                    // ウインドウを作成
                    ImageWindow imageWindow = new ImageWindow(imageData, imageWidth, imageHeight);
                    imageWindow.Show();
                }
            }
            catch (Exception error)
            {
                LogWriter.AddErrorLog(error, "Show Generated Images");
            }
        }
    }



    public class GenerateCommand : ICommand
    {
        MainViewModel _viewModel { get; set; }
        MainModel _mainModel { get; set; }

        public GenerateCommand(MainViewModel viewModel, MainModel mainModel)
        {
            _viewModel = viewModel;
            _mainModel = mainModel;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _viewModel.CanGenerateImage;
        }

        public void Execute(object? parameter)
        {
            _mainModel.GenerateImage();
        }
    }
}
