using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Windows.Shapes;
using Microsoft.Win32;

namespace Reader
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string content = await ReadFileAsync(filePath);
                DisplayFileContent(filePath, content);
            }
        }

        private async Task<string> ReadFileAsync(string filePath)
        {
            try
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, buffer.Length);
                    return Encoding.UTF8.GetString(buffer);
                }
            }
            catch (Exception ex)
            {
                return $"Error reading file: {ex.Message}";
            }
        }

        private void DisplayFileContent(string filePath, string content)
        {
            FileContentTextBox.AppendText($"============= {filePath} =============\n");
            FileContentTextBox.AppendText(content + "\n");
        }
    }
}
