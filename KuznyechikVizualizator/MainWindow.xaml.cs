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
            textBoxPlaintext.Text = "1122334455667700FFEEDDCCBBAA9988";
            textBoxKey.Text = "8899AABBCCDDEEFF0011223344556677FEDCBA98765432100123456789ABCDEF";
            KeyGenBox.GenerateContent(mainWindow, k);
            EncryptBox.GenerateContent(mainWindow, k);
            IntroBox.GenerateContent(mainWindow, k);
        }

        private void textBoxKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = textBoxKey.SelectionStart;
            textBoxKey.Text = textBoxKey.Text.ToUpper();
            textBoxKey.SelectionStart = x;
            if ((textBoxKey != null && textBoxKey.Text.Length == 64) && ((encryptMode == true && textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) || (encryptMode == false && textBoxCiphertext != null && textBoxCiphertext.Text.Length == 32)))
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
                if (Reversed_LBoxVisualization.IsActive() == true)
                {
                    Reversed_LBoxVisualization.DeleteContent(mainWindow);
                }
                if (encryptMode == true)
                {
                    k = new Kuznyechik(textBoxPlaintext.Text, textBoxKey.Text, "");
                    textBoxCiphertext.Text = k.ToString();
                    EncryptBox.DeleteContent(mainWindow);
                    EncryptBox.GenerateContent(mainWindow, k);
                } else //encryptMode == false
                {
                    k = new Kuznyechik("", textBoxKey.Text, textBoxCiphertext.Text);
                    textBoxPlaintext.Text = k.ToString();
                    DecryptBox.DeleteContent(mainWindow);
                    DecryptBox.GenerateContent(mainWindow, k);
                }
                KeyGenBox.DeleteContent(mainWindow);
                KeyGenBox.GenerateContent(mainWindow, k);
                IntroBox.DeleteContent(mainWindow);
                IntroBox.GenerateContent(mainWindow, k);
            }  
        }

        private void textBoxPlaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (encryptMode != true)
            {
                return;
            }
            int x = textBoxPlaintext.SelectionStart;
            textBoxPlaintext.Text = textBoxPlaintext.Text.ToUpper();
            textBoxPlaintext.SelectionStart = x;
            if ((textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
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
                if (Reversed_LBoxVisualization.IsActive() == true)
                {
                    Reversed_LBoxVisualization.DeleteContent(mainWindow);
                }

                k = new Kuznyechik(textBoxPlaintext.Text, textBoxKey.Text, "");
                textBoxCiphertext.Text = k.ToString();
                EncryptBox.DeleteContent(mainWindow);
                EncryptBox.GenerateContent(mainWindow, k);
            }
        }
        private void textBoxCiphertext_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (encryptMode != false)
            {
                return;
            }
            int x = textBoxCiphertext.SelectionStart;
            textBoxCiphertext.Text = textBoxCiphertext.Text.ToUpper();
            textBoxCiphertext.SelectionStart = x;
            if ((textBoxCiphertext != null && textBoxCiphertext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
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
                if (Reversed_LBoxVisualization.IsActive() == true)
                {
                    Reversed_LBoxVisualization.DeleteContent(mainWindow);
                }

                k = new Kuznyechik("", textBoxKey.Text, textBoxCiphertext.Text);
                textBoxPlaintext.Text = k.ToString();
                DecryptBox.DeleteContent(mainWindow);
                DecryptBox.GenerateContent(mainWindow, k);
            }
        }
        public void X_Click(object sender, RoutedEventArgs e)
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
            if (Reversed_LBoxVisualization.IsActive() == true)
            {
                Reversed_LBoxVisualization.DeleteContent(mainWindow);
            }

            Button b = sender as Button;
            int x = KeyGenBox.buttons.IndexOf(b);
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

            x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                XBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.roundKeys[x / 3], k.cryptRounds[x + 1]);
            }

            x = DecryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                XBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.roundKeys[9 - x / 3], k.cryptRounds[x + 1]);
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
            if (Reversed_LBoxVisualization.IsActive() == true)
            {
                Reversed_LBoxVisualization.DeleteContent(mainWindow);
            }

            Button b = sender as Button;
            int x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.keyGenRounds[x / 4 * 3], k.keyGenRounds[x / 4 * 3 + 1], false);
            }

            x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.cryptRounds[x + 1], false);
            } 

            x = DecryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x], k.cryptRounds[x + 1], true);
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
            if (Reversed_LBoxVisualization.IsActive() == true)
            {
                Reversed_LBoxVisualization.DeleteContent(mainWindow);
            }

            Button b = sender as Button;
            int x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                LBoxVisualization.GenerateContent(mainWindow, k.keyGenRounds[x / 4 * 3 + 1]);
            }

            x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                LBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x]);
            }
            x = DecryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                Reversed_LBoxVisualization.GenerateContent(mainWindow, k.cryptRounds[x]);
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
            if (Reversed_LBoxVisualization.IsActive() == true)
            {
                Reversed_LBoxVisualization.DeleteContent(mainWindow);
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
