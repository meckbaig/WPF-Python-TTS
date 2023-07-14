using Microsoft.Win32;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shell;

namespace WPF_Py
{


    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<Variable> Models { get; set; } = new ObservableCollection<Variable>()
        {
            new Variable("tiny", "Крошечная"),
            new Variable("base", "Базовая"),
            new Variable("small", "Малая"),
            new Variable("medium", "Средняя"),
            new Variable("large", "Большая")
        };
        public ObservableCollection<Variable> Languages { get; set; } = new ObservableCollection<Variable>()
        {
            new Variable("", "Автоматически"),
            new Variable("en", "english"),
            new Variable("ru", "russian"),
            new Variable("zh", "chinese"),
            new Variable("de", "german"),
            new Variable("es", "spanish"),
            new Variable("ko", "korean"),
            new Variable("fr", "french"),
            new Variable("ja", "japanese"),
            new Variable("pt", "portuguese"),
            new Variable("tr", "turkish"),
            new Variable("pl", "polish"),
            new Variable("ca", "catalan"),
            new Variable("nl", "dutch"),
            new Variable("ar", "arabic"),
            new Variable("sv", "swedish"),
            new Variable("it", "italian"),
            new Variable("id", "indonesian"),
            new Variable("hi", "hindi"),
            new Variable("fi", "finnish"),
            new Variable("vi", "vietnamese"),
            new Variable("he", "hebrew"),
            new Variable("uk", "ukrainian"),
            new Variable("el", "greek"),
            new Variable("ms", "malay"),
            new Variable("cs", "czech"),
            new Variable("ro", "romanian"),
            new Variable("da", "danish"),
            new Variable("hu", "hungarian"),
            new Variable("ta", "tamil"),
            new Variable("no", "norwegian"),
            new Variable("th", "thai"),
            new Variable("ur", "urdu"),
            new Variable("hr", "croatian"),
            new Variable("bg", "bulgarian"),
            new Variable("lt", "lithuanian"),
            new Variable("la", "latin"),
            new Variable("mi", "maori"),
            new Variable("ml", "malayalam"),
            new Variable("cy", "welsh"),
            new Variable("sk", "slovak"),
            new Variable("te", "telugu"),
            new Variable("fa", "persian"),
            new Variable("lv", "latvian"),
            new Variable("bn", "bengali"),
            new Variable("sr", "serbian"),
            new Variable("az", "azerbaijani"),
            new Variable("sl", "slovenian"),
            new Variable("kn", "kannada"),
            new Variable("et", "estonian"),
            new Variable("mk", "macedonian"),
            new Variable("br", "breton"),
            new Variable("eu", "basque"),
            new Variable("is", "icelandic"),
            new Variable("hy", "armenian"),
            new Variable("ne", "nepali"),
            new Variable("mn", "mongolian"),
            new Variable("bs", "bosnian"),
            new Variable("kk", "kazakh"),
            new Variable("sq", "albanian"),
            new Variable("sw", "swahili"),
            new Variable("gl", "galician"),
            new Variable("mr", "marathi"),
            new Variable("pa", "punjabi"),
            new Variable("si", "sinhala"),
            new Variable("km", "khmer"),
            new Variable("sn", "shona"),
            new Variable("yo", "yoruba"),
            new Variable("so", "somali"),
            new Variable("af", "afrikaans"),
            new Variable("oc", "occitan"),
            new Variable("ka", "georgian"),
            new Variable("be", "belarusian"),
            new Variable("tg", "tajik"),
            new Variable("sd", "sindhi"),
            new Variable("gu", "gujarati"),
            new Variable("am", "amharic"),
            new Variable("yi", "yiddish"),
            new Variable("lo", "lao"),
            new Variable("uz", "uzbek"),
            new Variable("fo", "faroese"),
            new Variable("ht", "haitian creole"),
            new Variable("ps", "pashto"),
            new Variable("tk", "turkmen"),
            new Variable("nn", "nynorsk"),
            new Variable("mt", "maltese"),
            new Variable("sa", "sanskrit"),
            new Variable("lb", "luxembourgish"),
            new Variable("my", "myanmar"),
            new Variable("bo", "tibetan"),
            new Variable("tl", "tagalog"),
            new Variable("mg", "malagasy"),
            new Variable("as", "assamese"),
            new Variable("tt", "tatar"),
            new Variable("haw", "hawaiian"),
            new Variable("ln", "lingala"),
            new Variable("ha", "hausa"),
            new Variable("ba", "bashkir"),
            new Variable("jw", "javanese"),
            new Variable("su", "sundanese")
        };

        bool modelLoaded = false;
        bool ModelLoaded 
        {
            get => modelLoaded;
            set
            {
                modelLoaded = value;
                ClearModel.IsEnabled = value;
                Model.IsEnabled = !value;
            }
                
        }

        dynamic script;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            PythonEngine.Initialize();
            script = Py.Import("whisper_py2");
            ButtonsHover.AllButtonsHover(ThisGrid);
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "Аудио|*.mp3;*.flac;*.wav|Все файлы (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() == true)
            {
                using (Py.GIL())
                {
                    string size = (Model.SelectedItem as Variable).Name;
                    string language = (Language.SelectedItem as Variable).Name;
                    if (!ModelLoaded)
                    {
                        try
                        {
                            script.load_model(size);
                        }
                        catch (Exception)
                        {
                            DownloadModel(size);
                            script.load_model(size);
                        }
                        ModelLoaded = true;
                    }
                    script.load_audio(fileDialog.FileName, language);
                    Output.Text = File.ReadAllText("text.txt");
                    //File.Delete("lang.txt");
                    File.Delete("text.txt");
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog()
            {
                Title = "Сохранение текста",
                FileName = $"Текст",
                Filter = "Текстовые файлы|*.txt|Все файлы (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() == true)
            {
                File.WriteAllText(fileDialog.FileName, Output.Text);
                Process.Start("notepad.exe", fileDialog.FileName);
            }

        }

        private void ClearModel_Click(object sender, RoutedEventArgs e)
        {
            script = Py.Import("whisper_py2");
            GC.Collect();
            GC.SuppressFinalize(this);
            ModelLoaded = false;
        }

        private void CloseWindow(object sender, MouseButtonEventArgs e)
        {
            script = null;
            Process.GetCurrentProcess().Kill();
        }

        private void Drag(object sender, MouseButtonEventArgs e) => DragMove();


        private string GetPy()
        {
            var command = "/c where python";
            var start = new ProcessStartInfo("cmd.exe");
            start.Arguments = command;
            start.RedirectStandardOutput = true;
            start.UseShellExecute = false;
            var cmd = Process.Start(start);
            var output = cmd.StandardOutput.ReadToEnd();

            cmd.WaitForExit();

            return output.Remove(output.IndexOf("\r\n"));
        }

        void DownloadModel(string size)
        {
            string whisperFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.cache\\whisper";
            string command = $"python \"{Environment.CurrentDirectory}\\download_model.py\" {size} \"{whisperFolder}";
            Process p = new Process();

            p.StartInfo = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };
            p.Start();
            StreamWriter myStreamWriter = p.StandardInput;
            myStreamWriter.WriteLine(command);
            myStreamWriter.Close();
            p.WaitForExit();
        }

        void LoadModel(string size)
        {
            string whisperFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\.cache\\whisper";

            Process cmd = new Process();

            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Verb = "runas";
            cmd.Start();

            using (StreamWriter sw = cmd.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("python");
                    sw.WriteLine("from whisper import _download, _MODELS");
                    //sw.WriteLine($"_download(_MODELS[\"{size}\"], \"__pycache__\", False)");
                    sw.WriteLine($"_download(_MODELS[\"{size}\"], \"{whisperFolder}\", False)");
                }
            }

            cmd.WaitForExit();
            //File.Move(Path.Combine(Environment.CurrentDirectory, "__pycache__", size +".pt"), whisperFolder);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        { 
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
