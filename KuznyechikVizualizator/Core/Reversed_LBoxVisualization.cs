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
    class Reversed_LBoxVisualization
    {
        private static bool isActive = false;
        private static ScrollViewer mainSV;
        private static Canvas scrollerCanvas;
        private static Rectangle sBound;
        private static Canvas canvas1;
        private static Grid grid1;
        private static List<Expander> expanders;
        private static List<RowDefinition> rows;
        private static List<List<TextBox>> textBoxes;
        private static List<List<byte>> vectors;
        private static int expandedHeight = 474;
        private static TextBox expTb;
        private static List<ColumnDefinition> columns;
        public static bool IsActive()
        {
            return isActive;
        }

        public static void GenerateContent(MainWindow mainWindow, List<byte> input)
        {
            object wantedNode = mainWindow.FindName("mainGrid");
            Grid mainGrid = wantedNode as Grid;

            mainSV = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Visible,
                Height = 630,
                Width = 653,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 20, 0, 0)

            };
            Grid.SetRow(mainSV, 1);
            mainGrid.Children.Add(mainSV);

            canvas1 = new Canvas()
            {
                Height = 812,
                Width = 636
            };
            mainSV.Content = canvas1;

            sBound = new Rectangle
            {
                Height = 812,
                Width = 636,
                Fill = Brushes.LightGray,
                Stroke = Brushes.Black
            };
            canvas1.Children.Add(sBound);


            grid1 = new Grid()
            {
                Margin = new Thickness(10, 10, 10, 10)
            };

            rows = new List<RowDefinition>();
            for (int i = 0; i < 33; ++i)
            {
                rows.Add(new RowDefinition());
                rows[i].Height = new GridLength(24);
                grid1.RowDefinitions.Add(rows[i]);
            }

            columns = new List<ColumnDefinition>();
            for (int i = 0; i < 16; ++i)
            {
                columns.Add(new ColumnDefinition());
                columns[i].Width = new GridLength(38);
                grid1.ColumnDefinitions.Add(columns[i]);
            }
            canvas1.Children.Add(grid1);

            vectors = new List<List<byte>>();
            vectors.Add(new List<byte>(input));
            textBoxes = new List<List<TextBox>>();
            expanders = new List<Expander>();
            for (int i = 0; i < 17; ++i)
            {
                textBoxes.Add(new List<TextBox>());
                for (int j = 0; j < 16; ++j)
                {
                    textBoxes[i].Add(new TextBox
                    {
                        IsReadOnly = true,
                        Text = BitConverter.ToString((new List<byte> { vectors[i][j] }).ToArray()),
                        TextAlignment = TextAlignment.Center,
                        Margin = new Thickness(0, 2, 0, 2)
                    });
                    Grid.SetColumn(textBoxes[i][j], j);
                    Grid.SetRow(textBoxes[i][j], i * 2);
                    grid1.Children.Add(textBoxes[i][j]);
                }
                vectors.Add(Kuznyechik.reversed_R(vectors[i]));
                if (i == 16)
                {
                    break;
                }
                byte t = vectors[i][0];
                for (int k = 0; k < 15; ++k)
                {
                    vectors[i][k] = vectors[i][k + 1];
                }
                vectors[i][15] = t;

                Label ans = new Label
                {
                    Content = "l(" + BitConverter.ToString(vectors[i].ToArray()) + ") = " + BitConverter.ToString((new List<byte> { vectors[i + 1][15] }).ToArray()),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    FontStyle = FontStyles.Italic
                };
                Grid.SetColumn(ans, 0);
                Grid.SetColumnSpan(ans, 16);
                Grid.SetRow(ans, i * 2 + 1);
                grid1.Children.Add(ans);
                ans.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));

                Expander lexp = new Expander
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(Convert.ToInt32(ans.DesiredSize.Width) + 5, 0, 0, 0)
                };
                Grid.SetColumn(lexp, 0);
                Grid.SetColumnSpan(lexp, 16);
                Grid.SetRow(lexp, i * 2 + 1);
                grid1.Children.Add(lexp);
                expanders.Add(lexp);
                lexp.Expanded += Lexp_Expanded;
                lexp.Collapsed += Lexp_Collapsed;
            }

            isActive = true;
        }

        private static void Lexp_Collapsed(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;
            int x = expanders.IndexOf(exp);
            rows[x * 2 + 1].Height = new GridLength(24);
            canvas1.Height = canvas1.Height - expandedHeight;
            grid1.Height = grid1.Height - expandedHeight;
            sBound.Height = sBound.Height - expandedHeight;
            expTb = null;
        }

        private static void Lexp_Expanded(object sender, RoutedEventArgs e)
        {
            Expander exp = sender as Expander;
            int x = expanders.IndexOf(exp);
            rows[x * 2 + 1].Height = new GridLength(24 + expandedHeight);
            canvas1.Height = canvas1.Height + expandedHeight;
            grid1.Height = grid1.Height + expandedHeight;
            sBound.Height = sBound.Height + expandedHeight;

            expTb = new TextBox
            {
                IsReadOnly = true,
                Margin = new Thickness(0, 24, 0, 0),
                Padding = new Thickness(0, 5, 0, 5),
                Width = 608,
                Text = "",
                FontFamily = new FontFamily("Courier New")
            };

            Grid.SetColumn(expTb, 0);
            Grid.SetColumnSpan(expTb, 16);
            Grid.SetRow(expTb, x * 2 + 1);
            grid1.Children.Add(expTb);
            List<byte> coefficients = new List<byte> { 148, 32, 133, 16, 194, 192, 1, 251, 1, 192, 194, 16, 133, 32, 148, 1 };

            UInt16 ans = 0;
            for (int i = 0; i < 16; ++i)
            {
                expTb.Text += "a" + Convert.ToString(15 - i, 10).PadLeft(2, '0') + ") " + textBoxes[x][i].Text.PadLeft(2, ' ') + " = " + Convert.ToString(vectors[x][i], 2).PadLeft(8, '0') + ",    " +
                           "c" + Convert.ToString(15 - i, 10).PadLeft(2, '0') + ") " + Convert.ToString(coefficients[i], 10).PadLeft(3, ' ') + " = " + Convert.ToString(coefficients[i], 2).PadLeft(8, '0') + ". " + "\n";

                ans ^= Kuznyechik.mul(vectors[x][i], coefficients[i]);
            }
            for (int i = 0; i < 16; ++i)
            {
                expTb.Text += "a" + Convert.ToString(15 - i, 10).PadLeft(2, '0') + " * " + "c" + Convert.ToString(15 - i, 10).PadLeft(2, '0') + " = " +
                           Convert.ToString(vectors[x][i], 2).PadLeft(8, '0') + " * " + Convert.ToString(coefficients[i], 2).PadLeft(8, '0') + " = " +
                           Convert.ToString(Kuznyechik.mul(vectors[x][i], coefficients[i]), 2).PadLeft(16, '0') + "\n";
            }
            expTb.Text += "Σ(ai * ci) = " + Convert.ToString(ans, 2).PadLeft(16, '0') + "\n";
            expTb.Text += "Σ(ai * ci) mod x8 + x7 + x6 + x + 1 = " + Convert.ToString(Kuznyechik.norm(ans), 2).PadLeft(8, '0') + " = " + Convert.ToString(Kuznyechik.norm(ans), 16).PadLeft(2, '0').ToUpper();
            coefficients.Clear();
        }

        public static void DeleteContent(MainWindow mainWindow)
        {
            grid1.Children.Clear();
            mainSV.Height = 0;
            isActive = false;
        }
    }
}
