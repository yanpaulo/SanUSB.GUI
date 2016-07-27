using System;
using System.Collections.Generic;
using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;
using System.ComponentModel;
using static SanUSB.GUI.InjectionPOG;

namespace SanUSB.GUI
{
    public class ViewModel : INotifyPropertyChanged
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set
            {
                _path = value;
                HasFile = !string.IsNullOrEmpty(_path);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path"));
            }
        }

        private bool _hasFile;

        public bool HasFile
        {
            get { return _hasFile; }
            set { _hasFile = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HasFile"));
            }
        }

        private bool _topmost = true;

        public bool Topmost
        {
            get { return _topmost; }
            set { _topmost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Topmost"));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class MainForm : Form
    {
        private TextArea textArea;
        private ViewModel viewModel;
        public MainForm()
        {
            if (ToolPath == null)
            {
                MessageBox.Show("Não foi possível localizar a ferramenta SanUSB");
                Application.Instance.Quit();
            }

            XamlReader.Load(this);
            //Loads image from path
            FindChild<ImageView>("logoImage").Image =
                (Image)new ImageConverter().ConvertFromString("Images/logo-full.png");
            
            //Set properties and DataContext
            textArea = FindChild<TextArea>("txtLog");
            DataContext = viewModel = new ViewModel();
        }
       
        public void Browse_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog(this) == DialogResult.Ok)
            {
                textArea.Append($"[{DateTime.Now}] Arquivo selecionado:  {dialog.FileName}");
                viewModel.Path = dialog.FileName;
            }
        }

        public void Reset_Click(object sender, EventArgs e) =>
            AppendStatus(sender, $"[{DateTime.Now}] {StartProcess(ToolPath, $"-r")}\n");

        public void Write_Click(object sender, EventArgs e) =>
            AppendStatus(sender, $"[{DateTime.Now}] {StartProcess(ToolPath, $"-w \"{viewModel.Path}\"")}\n");

        public void WriteReset_Click(object sender, EventArgs e) =>
            AppendStatus(sender, $"[{DateTime.Now}] {StartProcess(ToolPath, $"-w \"{viewModel.Path}\" -r")}\n");

        public void OpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFile(viewModel.Path);
            }
            catch (Exception)
            {
                MessageBox.Show("Erro.");
            }
        }

        private void AppendStatus(object sender, string status)
        {
            textArea.Focus();
            textArea.Append(status);
            textArea.CaretIndex = textArea.Text.Length;
            //(sender as Control).Focus();
        }

    }
}
