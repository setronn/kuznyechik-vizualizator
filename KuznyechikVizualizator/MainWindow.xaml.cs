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
using static KuznyechikVizualizator.Kuznyechik;

namespace KuznyechikVizualizator
{
    public partial class MainWindow : Window
    {
        static Kuznyechik k;
        public MainWindow()
        {
            InitializeComponent();
            KeyGenBox.GenerateContent(mainWindow, k);
            EncryptBox.GenerateContent(mainWindow, k);
        }

        private void textBoxKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = textBoxPlaintext.SelectionStart;
            textBoxKey.Text = textBoxKey.Text.ToUpper();
            textBoxPlaintext.SelectionStart = x;
            if ((textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
            {
                k = new Kuznyechik(textBoxKey.Text, textBoxPlaintext.Text);
                textBoxCiphertext.Text = k.ToString();
            }
            EncryptBox.RefreshContent(mainWindow, k);
        }

        private void textBoxPlaintext_TextChanged(object sender, TextChangedEventArgs e)
        {
            int x = textBoxPlaintext.SelectionStart;
            textBoxPlaintext.Text = textBoxPlaintext.Text.ToUpper();
            textBoxPlaintext.SelectionStart = x;
            if ((textBoxPlaintext != null && textBoxPlaintext.Text.Length == 32) && (textBoxKey != null && textBoxKey.Text.Length == 64))
            {
                k = new Kuznyechik(textBoxKey.Text, textBoxPlaintext.Text);
                textBoxCiphertext.Text = k.ToString();
            }
            EncryptBox.RefreshContent(mainWindow, k);
        }

        public void S_Click(object sender, RoutedEventArgs e)
        {
            if (XBoxVisualization.IsActive() == true)
            {
                XBoxVisualization.DeleteContent(mainWindow);
            }
            Button b = sender as Button;
            int x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.encryptRounds[x], k.encryptRounds[x + 1]);
            } 

            x = KeyGenBox.buttons.IndexOf(b);
            if (x != -1)
            {
                SBoxVisualization.GenerateContent(mainWindow, k.keyGenRounds[x / 4 * 3], k.keyGenRounds[x / 4 * 3 + 1]);
            }

        }

        public void X_Click(object sender, RoutedEventArgs e)
        {
            if (SBoxVisualization.IsActive() == true)
            {
                SBoxVisualization.DeleteContent(mainWindow);
            }
            Button b = sender as Button;
            int x = EncryptBox.buttons.IndexOf(b);
            if (x != -1)
            {
                XBoxVisualization.GenerateContent(mainWindow, k.encryptRounds[x], k.roundKeys[x / 3], k.encryptRounds[x + 1]);
            }
        }
    }
}
