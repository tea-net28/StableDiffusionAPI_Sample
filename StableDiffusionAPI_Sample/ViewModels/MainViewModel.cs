using StableDiffusionAPI_Sample.Views;
using System.ComponentModel;
using System.Windows;

namespace StableDiffusionAPI_Sample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainModel MainModel { get; set; }

        public class JsonData
        {
            public string prompt { get; set; } = string.Empty;
            public string negative_prompt { get; set; } = string.Empty;
            public int width { get; set; }
            public int height { get; set; }

            public JsonData(string prompt, string negativePrompt, int width, int height)
            {
                this.prompt = prompt;
                negative_prompt = negativePrompt;
                this.width = width;
                this.height = height;
            }
        }

        public HttpCommunications HttpCommunications { get; private set; }

        public List<ImageWindow> ImageWindows { get; set; }

        #region Commands
        public GenerateCommand GenerateCommand { get; set; }
        #endregion

        public MainViewModel()
        {
            MainModel = new MainModel(this);
            HttpCommunications = new HttpCommunications();

            GenerateCommand = new GenerateCommand(this, MainModel);
        }

        // ============================================================================================
        #region Core variables
        private string _version = "Text_Version";
        public string Version
        {
            get { return _version; }
            set
            {
                _version = value;
                RaisePropertyChanged(nameof(Version));
            }
        }
        private string _time = "Text_Time";
        public string Time
        {
            get { return _time; }
            set
            {
                _time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }
        private string _logs = "text_Logs";
        public string Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                RaisePropertyChanged(nameof(Logs));
            }
        }
        #endregion

        #region UI Variables
        private string _prompt = "\tmasterpiece, best quality, soft lighting, absurdres, looking at viewer, solo, ponytail, kamisato ayaka, serafuku, kamisato ayaka (heytea), official art, official alternate costume, blunt bangs, hair bow, hair ribbon, red ribbon, school uniform, sailor shirt, sailor collar, pleated skirt, 1girl, skirt, black bow, cate, genshin,";
        public string Prompt
        {
            get { return _prompt; }
            set
            {
                _prompt = value;
                RaisePropertyChanged(nameof(Prompt));
            }
        }

        private string _negativePrompt = "nsfw, (worst quality, low quality, extra digits, male:1.4)), bad_prompt,";
        public string NegativePrompt
        {
            get { return _negativePrompt; }
            set
            {
                _negativePrompt = value;
                RaisePropertyChanged(nameof(NegativePrompt));
            }
        }

        private int _width = 512;
        public int Width
        {
            get { return _width; }
            set
            {
                _width = value;
                RaisePropertyChanged(nameof(Width));
            }
        }

        private int _height = 768;
        public int Height
        {
            get { return _height; }
            set
            {
                _height = value;
                RaisePropertyChanged(nameof(Height));
            }
        }

        private string _ip = "127.0.0.1";
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                RaisePropertyChanged(nameof(Ip));
            }
        }

        private int _port = 7860;
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                RaisePropertyChanged(nameof(Port));
            }
        }

        #endregion
        // ============================================================================================
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
