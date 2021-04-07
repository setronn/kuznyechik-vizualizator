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

namespace KuznyechikVizualizator.Core
{
    class IntroBox
    {
       public static void GenerateContent(MainWindow mainWindow, Kuznyechik k)
        {
            object wantedNode = mainWindow.FindName("introGrid");
            Grid introGrid = wantedNode as Grid;

            List<RowDefinition> rows = new List<RowDefinition>();
            for (int i = 0; i < 32; ++i)
            {
                rows.Add(new RowDefinition());
                rows[i].Height = new GridLength(22);
                introGrid.RowDefinitions.Add(rows[i]);
            }
            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            for (int i = 0; i < 5; ++i)
            {
                columns.Add(new ColumnDefinition());
                introGrid.ColumnDefinitions.Add(columns[i]);
            }
            columns[0].Width = new GridLength(50);
            columns[1].Width = new GridLength(250);
            columns[2].Width = new GridLength(40);
            columns[3].Width = new GridLength(80);
            columns[4].Width = new GridLength(250);

            for (int i = 0; i < 10; ++i)
            {
                Label introL1 = new Label()
                {
                    Margin = new Thickness(0, -3, 0, -3),
                    Content = "K[" + Convert.ToString(i) + "] = k[" + Convert.ToString((i / 2) * 8 - (i % 2) + 1) + "] = "
                };
                Grid.SetRow(introL1, i);
                Grid.SetColumn(introL1, 3);
                introGrid.Children.Add(introL1);

                TextBox introTb1 = new TextBox()
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString(k.roundKeys[i].ToArray()).Replace("-", "").ToUpper(),
                    BorderBrush = Brushes.Transparent,
                };
                Grid.SetRow(introTb1, i);
                Grid.SetColumn(introTb1, 4);
                introGrid.Children.Add(introTb1);
            }

            for (int i = 0; i < 32; ++i)
            {
                Label introL0 = new Label()
                {
                    Margin = new Thickness(0, -3, 0, -3),
                    Content = "C[" + Convert.ToString(i) + "] = ",
                };
                Grid.SetRow(introL0, i);
                Grid.SetColumn(introL0, 0);
                introGrid.Children.Add(introL0);

                TextBox introTb0 = new TextBox()
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString(k.C[i].ToArray()).Replace("-", "").ToUpper(),
                    BorderBrush = Brushes.Transparent,
                };
                Grid.SetRow(introTb0, i);
                Grid.SetColumn(introTb0, 1);
                introGrid.Children.Add(introTb0);
            }   
        }
    }
}
