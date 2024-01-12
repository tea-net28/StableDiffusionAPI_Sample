using System.ComponentModel;

namespace StableDiffusionAPI_Sample
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainModel MainModel { get; set; }

        public JsonResponse? jsonResponse { get; set; }

        public HttpCommunications HttpCommunications { get; private set; }

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
        private string _prompt = "masterpiece, best quality, soft lighting, absurdres, looking at viewer, solo, ponytail, kamisato ayaka, serafuku, kamisato ayaka (heytea), official art, official alternate costume, blunt bangs, hair bow, hair ribbon, red ribbon, school uniform, sailor shirt, sailor collar, pleated skirt, 1girl, skirt, black bow, cate, genshin,";
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

        private int _batchSize = 1;
        public int BatchSize
        {
            get { return _batchSize; }
            set
            {
                _batchSize = value;
                RaisePropertyChanged(nameof(BatchSize));
            }
        }

        public bool CanGenerateImage { get; set; } = true;


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
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var d = PropertyChanged;
            if (d != null)
                d(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    // ============================================================================================
    #region JsonClass
    public class JsonData
    {
        public string prompt { get; set; } = string.Empty;
        public string negative_prompt { get; set; } = string.Empty;
        public int width { get; set; }
        public int height { get; set; }
        public int steps { get; set; } = 20;
        public int seed { get; set; } = -1;
        public int batch_size { get; set; } = 1;
        public bool save_images { get; set; } = true;

        public JsonData(string prompt, string negativePrompt, int width, int height, int batchSize = 1)
        {
            this.prompt = prompt;
            this.negative_prompt = negativePrompt;
            this.width = width;
            this.height = height;
            this.batch_size = batchSize;
        }
    }

    public class Parameters
    {
        public string prompt { get; set; }
        public string negative_prompt { get; set; }
        public object styles { get; set; }
        public int seed { get; set; }
        public int subseed { get; set; }
        public int subseed_strength { get; set; }
        public int seed_resize_from_h { get; set; }
        public int seed_resize_from_w { get; set; }
        public object sampler_name { get; set; }
        public int batch_size { get; set; }
        public int n_iter { get; set; }
        public int steps { get; set; }
        public double cfg_scale { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public object restore_faces { get; set; }
        public object tiling { get; set; }
        public bool do_not_save_samples { get; set; }
        public bool do_not_save_grid { get; set; }
        public object eta { get; set; }
        public object denoising_strength { get; set; }
        public object s_min_uncond { get; set; }
        public object s_churn { get; set; }
        public object s_tmax { get; set; }
        public object s_tmin { get; set; }
        public object s_noise { get; set; }
        public object override_settings { get; set; }
        public bool override_settings_restore_afterwards { get; set; }
        public object refiner_checkpoint { get; set; }
        public object refiner_switch_at { get; set; }
        public bool disable_extra_networks { get; set; }
        public object comments { get; set; }
        public bool enable_hr { get; set; }
        public int firstphase_width { get; set; }
        public int firstphase_height { get; set; }
        public double hr_scale { get; set; }
        public object hr_upscaler { get; set; }
        public int hr_second_pass_steps { get; set; }
        public int hr_resize_x { get; set; }
        public int hr_resize_y { get; set; }
        public object hr_checkpoint_name { get; set; }
        public object hr_sampler_name { get; set; }
        public string hr_prompt { get; set; }
        public string hr_negative_prompt { get; set; }
        public string sampler_index { get; set; }
        public object script_name { get; set; }
        public List<object> script_args { get; set; }
        public bool send_images { get; set; }
        public bool save_images { get; set; }
        public Dictionary<string, object> alwayson_scripts { get; set; }
    }

    public class JsonResponse
    {
        public List<string> images { get; set; }
        public Parameters parameters { get; set; }
        public string info { get; set; }
    }
    #endregion
}
