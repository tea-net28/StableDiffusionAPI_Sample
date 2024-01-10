using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Globalization;

namespace StableDiffusionAPI_Sample
{
    public partial class MainWindow : Window
    {
        MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _mainViewModel = new MainViewModel();
            this.DataContext = _mainViewModel;

            this.Loaded += OnLoaded_MainWindow;
        }



        private void OnLoaded_MainWindow(object sender, RoutedEventArgs e)
        {
            // バージョンの取得 ＝＞ ログに出力
            string version = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime.ToString("[yyyy.MM.dd HH:mm:ss]", CultureInfo.CurrentCulture);
            string appVersion = $"Version: {version}";
            _mainViewModel.Version = appVersion;
            LogWriter.AddLog($"===== Start program. Version: {appVersion} =====");

            // 毎フレーム実行するメソッドを設定
            CompositionTarget.Rendering += CompositionTargetRendering;
        }

        private List<string> logList = new List<string>();
        private StringBuilder sbLog = new StringBuilder();
        private int maxLogCount = 20;
        private void CompositionTargetRendering(object? sender, EventArgs e)
        {
            // 現在の時間を表示
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("HH:mm:ss.fff", CultureInfo.CurrentCulture));
            _mainViewModel.Time = sb.ToString();
            sb.Clear();

            // キューに貯まっているログを処理
            string? result = "";
            if (!LogWriter.q_Log.IsEmpty)
            {
                if (LogWriter.q_Log.TryDequeue(out result))
                {
                    logList.Add(result);
                    while (logList.Count > maxLogCount)
                        logList.RemoveAt(0);

                    foreach (var log in logList)
                        sbLog.AppendLine(log.Replace("\r\n", ""));
                    _mainViewModel.Logs = sbLog.ToString();
                    sbLog.Clear();
                }
            }
        }
    }
}