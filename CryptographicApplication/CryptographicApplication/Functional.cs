using System;
using System.Text;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Input;

namespace CryptographicApplication
{
    class Functional
    {
        public void Clear_tb(TextBox a)
        {
            a.Text = "";
        }

        public void Font_Size(TextBox a, bool b)
        {
            if (b == true)
            {
                if (a.FontSize < 20)
                    a.FontSize++;
            }
            else
            {
                if (a.FontSize > 1)
                    a.FontSize--;
            }
        }

        public void File_Selection(TextBox localFileText) // для ключа
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();

            openFileDlg.DefaultExt = ".txt";
            openFileDlg.Filter = "Текстовый документ (.txt) | * .txt";
            openFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                localFileText.Text = File.ReadAllText(openFileDlg.FileName, Encoding.Default);
            }
        }

        public void File_Selection(TextBox localFileName, TextBox localFileText, TextBox clearFileText) // для исходного
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();

            openFileDlg.DefaultExt = ".txt";
            openFileDlg.Filter = "Текстовый документ (.txt) | * .txt";
            openFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            Nullable<bool> result = openFileDlg.ShowDialog();

            if (result == true)
            {
                localFileName.Text = openFileDlg.FileName;
                localFileText.Text = File.ReadAllText(openFileDlg.FileName, Encoding.Default);
                clearFileText.Text = "";
            }
        }

        public void Save_File(TextBox localFileName, TextBox TextToSave)
        {
            File.WriteAllText(localFileName.Text, TextToSave.Text, Encoding.Default);
        }

        public void Save_File_As(TextBox NewFileName, TextBox TextToSave) // для исходного
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            saveFileDlg.DefaultExt = ".txt";
            saveFileDlg.Filter = "Текстовый документ (.txt) | * .txt";
            saveFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDlg.FileName = "Новый текстовый документ";

            Nullable<bool> result = saveFileDlg.ShowDialog();

            if (result == true)
            {
                NewFileName.Text = saveFileDlg.FileName;
                File.WriteAllText(saveFileDlg.FileName, TextToSave.Text, Encoding.Default);
            }
        }

        public void Save_File_As(ComboBox cb, TextBox TextToSave, RadioButton operation, bool a) // для ключа и зашифрованного
        {
            SaveFileDialog saveFileDlg = new SaveFileDialog();

            saveFileDlg.DefaultExt = ".txt";
            saveFileDlg.Filter = "Текстовый документ (.txt) | * .txt";
            saveFileDlg.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (a == true)
            {
                if (operation.IsChecked == true)
                {
                    saveFileDlg.FileName = "Зашифрованный текст (способ - " + cb.Text + ")";
                }
                else
                {
                    saveFileDlg.FileName = "Дешифрованный текст (способ - " + cb.Text + ")";
                }
            }
            else
            {
                saveFileDlg.FileName = "Ключ (способ - " + cb.Text + ")";
            }

            Nullable<bool> result = saveFileDlg.ShowDialog();

            if (result == true)
            {
                File.WriteAllText(saveFileDlg.FileName, TextToSave.Text, Encoding.Default);
            }
        }

        public void Changed_File_Name_TB(TextBox ChangedTB, Button BtnUsed, MenuItem MIUsed)
        {
            if (ChangedTB.Text == "")
            {
                BtnUsed.IsEnabled = false;
                MIUsed.IsEnabled = false;
            }
            else
            {
                BtnUsed.IsEnabled = true;
                MIUsed.IsEnabled = true;
            }
        }

        public void Changed_CB(ComboBox ChangedCB, Button BtnUsed, MenuItem MIUsed)
        {
            if (ChangedCB.SelectedIndex == -1)
            {
                BtnUsed.IsEnabled = false;
                MIUsed.IsEnabled = false;
            }
            else
            {
                BtnUsed.IsEnabled = true;
                MIUsed.IsEnabled = true;
            }
        }

        public void Сheck_for_Text(TextBox a)
        {
            if (a.Foreground != Brushes.Black)
            {
                a.Text = "";
                a.Foreground = Brushes.Black;
            }
        }

        public void Сheck_the_Сursor(TextBox a, int b)
        {
            if (a.Text == "")
            {
                a.Foreground = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                switch (b)
                {
                    case 0: a.Text = "Введите размер ключа"; break;
                    case 1: a.Text = "Ваш ключ"; break;
                    case 2: a.Text = "Введите P"; break;
                    case 3: a.Text = "Введите Q"; break;
                    case 4: a.Text = "Ваш открытый ключ"; break;
                    case 5: a.Text = "Ваш закрытый ключ"; break;
                }
            }
        }

        public void Only_Number(TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }

        public void Without_a_Space(TextBox a, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        public int Check(int value, TextBox a)
        {
            try
            {
                value = Convert.ToInt32(a.Text);
                return value;
            }
            catch (Exception)
            {
                a.BorderBrush = Brushes.Red;
                return 0;
            }
        }
    }
}