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
using KuznyechikVizualizator.Core;

namespace KuznyechikVizualizator
{
    public partial class MainWindow : Window
    {
        static Kuznyechik k;
        static bool encryptMode = true;
      public MainWindow()
        {
            InitializeComponent();
            KeyGenBox.GenerateContent(mainWindow, k);
            EncryptBox.GenerateContent(mainWindow, k);
            IntroBox.GenerateContent(mainWindow, k);
        }

        private void textBoxKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = textBoxKey.SelectionStart;
            textBoxKey.Text = textBoxKey.Text.ToUpper();
            textBoxKey.SelectionStart = x;
            if ((textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
            {
                k = new Kuznyechik(textBoxPlaintext.Text, textBoxKey.Text, "");
                textBoxCiphertext.Text = k.ToString();
            }
            EncryptBox.RefreshContent(mainWindow, k);
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }
        }

        private void textBoxPlaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = textBoxPlaintext.SelectionStart;
            textBoxPlaintext.Text = textBoxPlaintext.Text.ToUpper();
            textBoxPlaintext.SelectionStart = x;
            if ((textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
            {
                k = new Kuznyechik(textBoxPlaintext.Text, textBoxKey.Text, "");
            }
            textBoxCiphertext.Text = k.ToString();
            EncryptBox.RefreshContent(mainWindow, k);
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }
        }
        public void X_Click(object sender, RoutedEventArgs e)
        {
            System.Console.Write("Clicked");
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }
            Button b = sender as Button;
            int x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                XBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.roundKeys[x / 3], k.cryptRounds[x + 1]);
            }
            x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                if (x % 4 == 3)
                {
                    XBoxVisualization.GenerateContent(mainWindow, k.k[x / 4 + 1], k.C[x / 4], k.keyGenRounds[x / 4 * 3]);
                }

                if (x % 4 == 0)
                {
                    XBoxVisualization.GenerateContent(mainWindow, k.k[x / 4], k.keyGenRounds[x / 4 * 3 + 2], k.k[x / 4 + 2]);
                }
            }

        }

        public void S_Click(object sender, RoutedEventArgs e)
        {
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }
            Button b = sender as Button;
            int x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.cryptRounds[x + 1]);
            } 

            x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.keyGenRounds[x / 4 * 3], k.keyGenRounds[x / 4 * 3 + 1]);
            }

        }

        public void L_Click(object sender, RoutedEventArgs e)
        {
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }
            Button b = sender as Button;
            int x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                LBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x]);
            }

            x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                LBoxVisualization.GenerateContent(mainWindow, k.keyGenRounds[x / 4 * 3 + 1]);
            }

        }

        private void changeMode_Click(object sender, RoutedEventArgs e)
        {
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            if (LBoxVisualization.IsActive() == true)
            {
                LBoxVisualization.DeleteContent(mainWindow);
            }

            object wantedNode = mainWindow.FindName("cryptTab");
            TabItem cryptTab = wantedNode as TabItem;
            wantedNode = mainWindow.FindName("encryptArrow");
            Polygon encryptArrow = wantedNode as Polygon;
            wantedNode = mainWindow.FindName("decryptArrow");
            Polygon decryptArrow = wantedNode as Polygon;

            if (encryptMode == true)
            {
                encryptMode = false;
                k = new Kuznyechik("", textBoxKey.Text, textBoxCiphertext.Text);
                cryptTab.Header = "Decrypt";
                encryptArrow.Visibility = Visibility.Hidden;
                decryptArrow.Visibility = Visibility.Visible;
                EncryptBox.DeleteContent(mainWindow);
                DecryptBox.GenerateContent(mainWindow, k);
                textBoxCiphertext.IsReadOnly = false;
                textBoxPlaintext.IsReadOnly = true;
            } else //encryptMode == false
            {
                encryptMode = true;
                k = new Kuznyechik(textBoxPlaintext.Text, textBoxKey.Text, "");
                cryptTab.Header = "Encrypt";
                encryptArrow.Visibility = Visibility.Visible;
                decryptArrow.Visibility = Visibility.Hidden;
                DecryptBox.DeleteContent(mainWindow);
                EncryptBox.GenerateContent(mainWindow, k);
                textBoxCiphertext.IsReadOnly = true;
                textBoxPlaintext.IsReadOnly = false;
            }
        }
    }
}
