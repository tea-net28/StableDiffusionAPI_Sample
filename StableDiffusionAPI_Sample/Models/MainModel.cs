using System.Text.Json;
using System.Windows.Input;
using static StableDiffusionAPI_Sample.MainViewModel;

namespace StableDiffusionAPI_Sample
{
    public class MainModel
    {
        MainViewModel _viewmodel;


        public MainModel(MainViewModel viewmodel)
        {
            _viewmodel = viewmodel;
        }

        public async void GenerateImage()
        {
            JsonData jsonData = new JsonData(_viewmodel.Prompt, _viewmodel.NegativePrompt, _viewmodel.Width, _viewmodel.Height);

            LogWriter.AddLog(JsonSerializer.Serialize<JsonData>(jsonData));

            // uri の作成
            //Uri uri = new Uri($"http://{_viewmodel.Ip}:{_viewmodel.Port}/sdapi/v1/txt2img");
            Uri uri = new Uri($"http://{_viewmodel.Ip}:{_viewmodel.Port}/sdapi/v1/txt2img");
            //Uri uri = new Uri(@"http://192.168.11.111:7860/sdapi/v1/options");
        
            LogWriter.AddLog(uri.ToString());
            // 構造体を作成
            HttpDatas httpDatas = new HttpDatas()
            {
                Uri = uri,
                Data = JsonSerializer.Serialize<JsonData>(jsonData)
            };

            // POST して画像を生成する
            string result = await _viewmodel.HttpCommunications.PostAsync(httpDatas);
            //string result = await _viewmodel.HttpCommunications.GetAsync(httpDatas);
            LogWriter.AddLog(result);
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
            return true;
        }

        public void Execute(object? parameter)
        {
            _mainModel.GenerateImage();
        }
    }
}
