using System;
using System.Collections.Generic;
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
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;

namespace CryptographicApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Переменные

        Functional func_obj;
        RandomKeyGeneration rndkey_obj;
        Transposition trans_obj;
        Monoalphabetic mono_obj;
        Polyalphabetic poly_obj;
        XOR xor_obj;
        Vernam vernam_obj;
        RSA rsa_obj;

        public bool transition = false;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            rb_Encryption.IsChecked = true;
            func_obj = new Functional();
            trans_obj = new Transposition();
            mono_obj = new Monoalphabetic();
            poly_obj = new Polyalphabetic();
            xor_obj = new XOR();
            vernam_obj = new Vernam();
            rsa_obj = new RSA();
            rndkey_obj = new RandomKeyGeneration();
        }

        #region Алгоритмы

        public void Transposition_Cipher()
        {
            trans_obj.SetKey(tb_Key.Text);

            if (rb_Encryption.IsChecked == true)
            {
                tb_EncryptedData.Text = trans_obj.Encrypt(tb_SourceData.Text);
            }
            else
            {
                tb_EncryptedData.Text = trans_obj.Decrypt(tb_SourceData.Text);
            }
        }

        public void Monoalphabetic_Cipher()
        {
            if (rb_Encryption.IsChecked == true)
            {
                tb_EncryptedData.Text = mono_obj.Encrypt(tb_SourceData.Text, Convert.ToInt32(tb_Key.Text));
            }
            else
            {
                tb_EncryptedData.Text = mono_obj.Decrypt(tb_SourceData.Text, Convert.ToInt32(tb_Key.Text));
            }
        }

        public void Polyalphabetic_Cipher()
        {
            if (rb_Encryption.IsChecked == true)
            {
                tb_EncryptedData.Text = poly_obj.Encrypt(tb_SourceData.Text, tb_Key.Text);
            }
            else
            {
                tb_EncryptedData.Text = poly_obj.Decrypt(tb_SourceData.Text, tb_Key.Text);
            }
        }

        public void Vernam_Cipher()
        {
            tb_EncryptedData.Text = vernam_obj.Encrypt_and_Decrypt(tb_SourceData.Text, tb_Key.Text);
        }

        public void XOR_Cipher()
        {
            tb_EncryptedData.Text = xor_obj.Encrypt_and_Decrypt(tb_SourceData.Text, tb_Key.Text);
        }

        public void RSA_Cipher()
        {
            rsa_obj.SetKey(tb_Key.Text);

            if (rb_Encryption.IsChecked == true)
            {
                tb_EncryptedData.Text = rsa_obj.Encrypt(tb_SourceData.Text);
            }
            else
            {
                String[] s = tb_SourceData.Text.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                List<string> sourcetext = new List<string>(s);

                tb_EncryptedData.Text = rsa_obj.Decrypt(sourcetext);
            }
        }

        #endregion

        #region Элементы формы

        #region Меню

        private void Menu_btn_ExecuteOperation_Click(object sender, RoutedEventArgs e)
        {
            List_of_Operations_to_Perform();
        }

        private void Menu_btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Menu_btn_Key_Generation_Click(object sender, RoutedEventArgs e)
        {
            List_of_Key_Generation_Methods();
        }

        private void Menu_btn_FileSelection_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.File_Selection(tb_FileName_Source, tb_SourceData, tb_EncryptedData);
        }

        private void Menu_btn_FileSelection_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.File_Selection(tb_Key);
        }

        private void Menu_btn_SaveFile_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File(tb_FileName_Source, tb_SourceData);
        }

        private void Menu_btn_SaveFileAs_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(tb_FileName_Source, tb_SourceData);
        }

        private void Menu_btn_SaveFileAs_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(cb_Algorithms, tb_EncryptedData, rb_Encryption, true);
        }

        private void Menu_btn_SaveFileAs_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(cb_Algorithms, tb_Key, rb_Encryption, false);
        }

        private void Menu_btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Menu_btn_About_the_Program_Click(object sender, RoutedEventArgs e)
        {
            Calling_Help();
        }

        #endregion

        #region Исходные данные

        private void Btn_FileSelection_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.File_Selection(tb_FileName_Source, tb_SourceData, tb_EncryptedData);
        }

        private void Btn_SaveFile_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File(tb_FileName_Source, tb_SourceData);
        }

        private void Btn_SaveFileAs_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(tb_FileName_Source, tb_SourceData);
        }

        private void Tb_FileName_Source_TextChanged(object sender, TextChangedEventArgs e)
        {
            func_obj.Changed_File_Name_TB(tb_FileName_Source, btn_SaveFile_Source, menu_btn_SaveFile);
        }

        private void Btn_Clear_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Clear_tb(tb_SourceData);
        }

        private void Btn_Increase_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_SourceData, true);
        }

        private void Btn_Reduce_Source_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_SourceData, false);
        }

        #endregion

        #region Результат

        private void Btn_SaveFileAs_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(cb_Algorithms, tb_EncryptedData, rb_Encryption, true);
        }

        private void Btn_Clear_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Clear_tb(tb_EncryptedData);
        }

        private void Btn_Increase_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_EncryptedData, true);
        }

        private void Btn_Reduce_Encrypted_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_EncryptedData, false);
        }

        #endregion

        #region Ключ

        #region Основное меню

        private void Btn_FileSelection_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.File_Selection(tb_Key);
        }

        private void Btn_SaveFileAs_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Save_File_As(cb_Algorithms, tb_Key, rb_Encryption, false);
        }

        private void Btn_Clear_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Clear_tb(tb_Key);
        }

        private void Btn_Increase_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_Key, true);
        }

        private void Btn_Reduce_Key_Click(object sender, RoutedEventArgs e)
        {
            func_obj.Font_Size(tb_Key, false);
        }

        private void Tb_Key_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Key.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        #endregion

        #region Меню для генерации ключей с выбором размера

        private void Btn_Key_Generation_1_Click(object sender, RoutedEventArgs e)
        {
            Key_Generation_Selection();
        }

        private void Btn_Backward_Click(object sender, RoutedEventArgs e) //назад
        {
            transition = false;

            Grid_Main_Key_Menu.Visibility = Visibility.Visible;
            menu_btn_SaveFileAs_Key.IsEnabled = true;

            tb_Key_Size.Text = "";
            tb_Key_1.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_Size, 0);
            func_obj.Сheck_the_Сursor(tb_Key_1, 1);

            Grid_Generation_Key_Menu_1.Visibility = Visibility.Collapsed;
        }

        private void Btn_OK_Click(object sender, RoutedEventArgs e) //ок
        {
            transition = false;

            Grid_Main_Key_Menu.Visibility = Visibility.Visible;
            menu_btn_SaveFileAs_Key.IsEnabled = true;

            tb_Key.Text = tb_Key_1.Text;
            tb_Key_Size.Text = "";
            tb_Key_1.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_Size, 0);
            func_obj.Сheck_the_Сursor(tb_Key_1, 1);

            Grid_Generation_Key_Menu_1.Visibility = Visibility.Collapsed;
        }

        private void Tb_Key_Size_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Key_Size);
        }

        private void Tb_Key_Size_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Key_Size, 0);
        }

        private void Tb_Key_Size_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Key_Size, e);
        }

        private void Tb_Key_Size_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            func_obj.Only_Number(e);
        }

        private void Tb_Key_Size_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Key_Size.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void Tb_Key_1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Key_1);
        }

        private void Tb_Key_1_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Key_1, 1);
        }

        private void Tb_Key_1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Key_1, e);
        }

        private void Tb_Key_1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (tb_Key_1.Text == "Ваш ключ" || tb_Key_1.Text == "")
                {
                    btn_OK.IsEnabled = false;
                }
                else
                {
                    btn_OK.IsEnabled = true;
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Меню для генерации ключей RSA

        private void Btn_Key_Generation_2_Click(object sender, RoutedEventArgs e)
        {
            Key_Generation_Selection();
        }

        private void Btn_Backward_1_Click(object sender, RoutedEventArgs e)
        {
            transition = false;

            Grid_Main_Key_Menu.Visibility = Visibility.Visible;
            menu_btn_SaveFileAs_Key.IsEnabled = true;

            tb_Key_P.Text = "";
            tb_Key_Q.Text = "";
            tb_Public_Key.Text = "";
            tb_Privat_Key.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_P, 2);
            func_obj.Сheck_the_Сursor(tb_Key_Q, 3);
            func_obj.Сheck_the_Сursor(tb_Public_Key, 4);
            func_obj.Сheck_the_Сursor(tb_Privat_Key, 5);

            Grid_Generation_Key_Menu_2.Visibility = Visibility.Collapsed;
        }

        private void Btn_OK_1_Click(object sender, RoutedEventArgs e)
        {
            transition = false;

            Grid_Main_Key_Menu.Visibility = Visibility.Visible;
            menu_btn_SaveFileAs_Key.IsEnabled = true;

            if (rb_Encryption.IsChecked == true)
            {
                tb_Key.Text = tb_Public_Key.Text;
            }
            else
            {
                tb_Key.Text = tb_Privat_Key.Text;
            }

            tb_Key_P.Text = "";
            tb_Key_Q.Text = "";
            tb_Public_Key.Text = "";
            tb_Privat_Key.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_P, 2);
            func_obj.Сheck_the_Сursor(tb_Key_Q, 3);
            func_obj.Сheck_the_Сursor(tb_Public_Key, 4);
            func_obj.Сheck_the_Сursor(tb_Privat_Key, 5);

            Grid_Generation_Key_Menu_2.Visibility = Visibility.Collapsed;
        }

        private void Tb_Key_P_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            func_obj.Only_Number(e);
        }

        private void Tb_Key_P_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Key_P);
        }

        private void Tb_Key_P_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Key_P, e);
        }

        private void Tb_Key_P_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Key_P, 2);
        }

        private void Tb_Key_P_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Key_P.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void Tb_Key_Q_TextChanged(object sender, TextChangedEventArgs e)
        {
            tb_Key_Q.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
        }

        private void Tb_Key_Q_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Key_Q, 3);
        }

        private void Tb_Key_Q_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Key_Q, e);
        }

        private void Tb_Key_Q_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Key_Q);
        }

        private void Tb_Key_Q_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            func_obj.Only_Number(e);
        }

        //открытый

        private void Tb_Public_Key_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Public_Key, 4);
        }

        private void Tb_Public_Key_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Public_Key, e);
        }

        private void Tb_Public_Key_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Public_Key);
        }

        private void Tb_Public_Key_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unlock_Btn();
        }

        //закрытый

        private void Tb_Privat_Key_LostFocus(object sender, RoutedEventArgs e)
        {
            func_obj.Сheck_the_Сursor(tb_Privat_Key, 5);
        }

        private void Tb_Privat_Key_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            func_obj.Without_a_Space(tb_Privat_Key, e);
        }

        private void Tb_Privat_Key_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            func_obj.Сheck_for_Text(tb_Privat_Key);
        }

        private void Tb_Privat_Key_TextChanged(object sender, TextChangedEventArgs e)
        {
            Unlock_Btn();
        }

        #endregion

        #endregion

        #region Прочее

        public void List_of_Operations_to_Perform() //меню, определяющее какой метод выполнять
        {
            try
            {
                tb_Key.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                switch (cb_Algorithms.SelectedIndex)
                {
                    case -1: cb_Algorithms_Border.Background = Brushes.Red; break;
                    case 0: Transposition_Cipher(); break;
                    case 1: Monoalphabetic_Cipher(); break;
                    case 2: Polyalphabetic_Cipher(); break;
                    case 3: XOR_Cipher(); break;
                    case 4: Vernam_Cipher(); break;
                    case 5: RSA_Cipher(); break;
                }
            }
            catch (Exception)
            {
                tb_Key.BorderBrush = Brushes.Red;
            }
        }

        public void Displaying_the_Key_Generation_Menu(bool a) //переходы
        {
            if (a == true)
            {
                transition = true;
                menu_btn_SaveFileAs_Key.IsEnabled = false;
                Grid_Main_Key_Menu.Visibility = Visibility.Collapsed;
                Grid_Generation_Key_Menu_1.Visibility = Visibility.Visible;
                Grid_Generation_Key_Menu_2.Visibility = Visibility.Collapsed;
            }
            else
            {
                transition = true;
                menu_btn_SaveFileAs_Key.IsEnabled = false;
                Grid_Main_Key_Menu.Visibility = Visibility.Collapsed;
                Grid_Generation_Key_Menu_1.Visibility = Visibility.Collapsed;
                Grid_Generation_Key_Menu_2.Visibility = Visibility.Visible;
            }
        }

        public void List_of_Key_Generation_Methods() //меню, определяющее для какого способа генерировать ключ
        {
            switch (cb_Algorithms.SelectedIndex)
            {
                case 0:
                    if (transition == false)
                    {
                        Displaying_the_Key_Generation_Menu(true);
                    }
                    else
                    {
                        Key_Generation_Selection();
                    }  
                    break;

                case 1: Key_Generation_Selection(); break;

                case 2:
                    if (transition == false)
                    {
                        Displaying_the_Key_Generation_Menu(true);
                    }
                    else
                    {
                        Key_Generation_Selection();
                    }
                    break;

                case 3:
                    if (transition == false)
                    {
                        Displaying_the_Key_Generation_Menu(true);
                    }
                    else
                    {
                        Key_Generation_Selection();
                    }
                    break;

                case 4: Key_Generation_Selection(); break;

                case 5:
                    if (transition == false)
                    {
                        Displaying_the_Key_Generation_Menu(false);
                    }
                    else
                    {
                        Key_Generation_Selection();
                    }
                    break;
            }
        }

        public void Key_Generation_Selection() //меню, для выбора генерации ключей
        {
            try
            {
                switch (cb_Algorithms.SelectedIndex)
                {
                    case 0:
                        tb_Key_1.Text = rndkey_obj.Rand_Key_Generation_Transposition(Convert.ToInt32(tb_Key_Size.Text));
                        tb_Key_1.Foreground = Brushes.Black;
                        break;

                    case 1: tb_Key.Text = rndkey_obj.Rand_Key_Generation(); break;
                    case 2:
                        tb_Key_1.Text = rndkey_obj.Rand_Key_Generation(Convert.ToInt32(tb_Key_Size.Text));
                        tb_Key_1.Foreground = Brushes.Black;
                        break;

                    case 3:
                        tb_Key_1.Text = rndkey_obj.Rand_Key_Generation(Convert.ToInt32(tb_Key_Size.Text));
                        tb_Key_1.Foreground = Brushes.Black;
                        break;

                    case 4: tb_Key.Text = rndkey_obj.Rand_Key_Generation(tb_SourceData.Text.Length); break;
                    case 5:
                        int p = 0, q = 0, i = 0;

                        p = func_obj.Check(p, tb_Key_P);
                        q = func_obj.Check(q, tb_Key_Q);

                        if (p > 10 && p != q && rsa_obj.IsTheNumberSimple(p))
                        {
                            i++;
                        }
                        else
                        {
                            tb_Key_P.BorderBrush = Brushes.Red;
                        }

                        if (q > 10 && q != p && rsa_obj.IsTheNumberSimple(q))
                        {
                            i++;
                        }
                        else
                        {
                            tb_Key_Q.BorderBrush = Brushes.Red;
                        }

                        if (i == 2)
                        {
                            tb_Key_P.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
                            tb_Key_Q.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));

                            long n = p * q;
                            long fi = (p - 1) * (q - 1);
                            long e = rsa_obj.Calculate_e(fi);
                            long d = rsa_obj.Calculate_d(e, fi);

                            tb_Public_Key.Foreground = Brushes.Black;
                            tb_Privat_Key.Foreground = Brushes.Black;

                            tb_Public_Key.Text = Convert.ToString(e) + " " + Convert.ToString(n);
                            tb_Privat_Key.Text = Convert.ToString(d) + " " + Convert.ToString(n);
                        }
                        
                        break;
                }
            }
            catch (Exception)
            {
                if (cb_Algorithms.SelectedIndex == 0 || cb_Algorithms.SelectedIndex == 2 || cb_Algorithms.SelectedIndex == 3)
                {
                    tb_Key_Size.BorderBrush = Brushes.Red;
                }
            }
        }

        public void Calling_Help()
        {
            try
            {
                string commandText = "Help.chm";
                var proc = new Process();
                proc.StartInfo.FileName = commandText;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
            catch
            {
                MessageBox.Show("Не найден файл справки!");
            }
        }

        public void Refresh() //обновить всё
        {
            transition = false;

            //операция
            rb_Encryption.IsChecked = true;

            //способ шифрования
            cb_Algorithms.SelectedIndex = -1;
            cb_Algorithms_Border.Background = this.Background;

            //исходные
            tb_FileName_Source.Text = "";
            tb_SourceData.Text = "";
            tb_SourceData.FontSize = 12;

            //результат
            tb_EncryptedData.Text = "";
            tb_EncryptedData.FontSize = 12;

            //ключ
            menu_btn_SaveFileAs_Key.IsEnabled = true;
            tb_Key.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
            tb_Key.Text = "";
            tb_Key.FontSize = 12;

            //генарация ключа с вводом размера
            tb_Key_Size.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
            tb_Key_Size.Text = "";
            tb_Key_1.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_Size, 0);
            func_obj.Сheck_the_Сursor(tb_Key_1, 1);

            //генарация ключа RSA
            tb_Key_P.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
            tb_Key_Q.BorderBrush = new SolidColorBrush(Color.FromRgb(171, 173, 179));
            tb_Key_P.Text = "";
            tb_Key_Q.Text = "";
            tb_Public_Key.Text = "";
            tb_Privat_Key.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_P, 2);
            func_obj.Сheck_the_Сursor(tb_Key_Q, 3);
            func_obj.Сheck_the_Сursor(tb_Public_Key, 4);
            func_obj.Сheck_the_Сursor(tb_Privat_Key, 5);

            Grid_Main_Key_Menu.Visibility = Visibility.Visible;
            Grid_Generation_Key_Menu_1.Visibility = Visibility.Collapsed;
            Grid_Generation_Key_Menu_2.Visibility = Visibility.Collapsed;
        }

        public void Unlock_Btn()
        {
            try
            {
                if (tb_Public_Key.Text == "Ваш открытый ключ" || tb_Public_Key.Text == ""
                    || tb_Privat_Key.Text == "Ваш закрытый ключ" || tb_Privat_Key.Text == "")
                {
                    btn_OK_1.IsEnabled = false;
                }
                else
                {
                    btn_OK_1.IsEnabled = true;
                }
            }
            catch (Exception)
            {

            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) //горячие клавиши
        {
            switch (e.Key)
            {
                case Key.F1: List_of_Operations_to_Perform(); break; //выполнить
                case Key.F2: Refresh(); break; //обновить
                case Key.F3: if (cb_Algorithms.SelectedIndex != -1) List_of_Key_Generation_Methods(); break; //сгенерировать ключ
                case Key.F4: func_obj.File_Selection(tb_FileName_Source, tb_SourceData, tb_EncryptedData); break; //открыть файл с исходным текстом
                case Key.F5: func_obj.File_Selection(tb_Key); break; //открыть файл с ключом
                case Key.F6: if (tb_FileName_Source.Text != "") func_obj.Save_File(tb_FileName_Source, tb_SourceData); break; //сохранить
                case Key.F7: func_obj.Save_File_As(tb_FileName_Source, tb_SourceData); break;  //сохранить исходный текст
                case Key.F8: func_obj.Save_File_As(cb_Algorithms, tb_EncryptedData, rb_Encryption, true); break;  //сохранить зашифрованный/дешифрованный текст
                case Key.F9: if (Grid_Main_Key_Menu.Visibility == Visibility.Visible) func_obj.Save_File_As(cb_Algorithms, tb_Key, rb_Encryption, false); break;  //сохранить ключ
                case Key.F11: Calling_Help(); break;  //о программе
                case Key.F12: this.Close(); break; //выход
            }
        }

        private void Cb_Algorithms_SelectionChanged(object sender, SelectionChangedEventArgs e) //изменения комбобокса
        {
            transition = false;
            Grid_Main_Key_Menu.Visibility = Visibility.Visible;

            tb_Key_Size.Text = "";
            tb_Key_1.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_Size, 0);
            func_obj.Сheck_the_Сursor(tb_Key_1, 1);
            Grid_Generation_Key_Menu_1.Visibility = Visibility.Collapsed;

            tb_Key_P.Text = "";
            tb_Key_Q.Text = "";
            tb_Public_Key.Text = "";
            tb_Privat_Key.Text = "";
            func_obj.Сheck_the_Сursor(tb_Key_P, 2);
            func_obj.Сheck_the_Сursor(tb_Key_Q, 3);
            func_obj.Сheck_the_Сursor(tb_Public_Key, 4);
            func_obj.Сheck_the_Сursor(tb_Privat_Key, 5);
            Grid_Generation_Key_Menu_2.Visibility = Visibility.Collapsed;

            cb_Algorithms_Border.Background = this.Background;
            menu_btn_SaveFileAs_Key.IsEnabled = true; //разблокировать кнопку сохранение ключа в меню
            func_obj.Changed_CB(cb_Algorithms, btn_Key_Generation, menu_btn_Key_Generation); //разблокирует или блокирует кнопку "Сгенерировать ключ"
        }

        private void Rb_Encryption_Checked(object sender, RoutedEventArgs e)
        {
            menu_btn_SaveFileAs_Encrypted.Header = "Сохранить зашифрованный текст";
        }

        private void Rb_Decryption_Checked(object sender, RoutedEventArgs e)
        {
            menu_btn_SaveFileAs_Encrypted.Header = "Сохранить дешифрованный текст";
        }

        private void Btn_ExecuteOperation_Click(object sender, RoutedEventArgs e)
        {
            List_of_Operations_to_Perform();
        }

        private void Btn_Key_Generation_Click(object sender, RoutedEventArgs e)
        {
            List_of_Key_Generation_Methods();
        }

        #endregion

        #endregion
    }
}