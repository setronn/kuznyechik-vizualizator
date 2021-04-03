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

namespace KuznyechikVizualizator
{
    class SBoxVisualization
    {
        private static Canvas boxCanvas;
        private static bool isActive = false;

        public static bool IsActive()
        {
            return isActive;
        }

        public static void GenerateContent(MainWindow mainWindow, List<byte> input, List<byte> output)
        {
            object wantedNode = mainWindow.FindName("mainGrid");
            Grid mainGrid = wantedNode as Grid;

            boxCanvas = new Canvas();
            boxCanvas.Margin = new Thickness(80, 10, 10, 10);
            Grid.SetRow(boxCanvas, 1);

            Grid grid1 = new Grid();

            List<RowDefinition> rows = new List<RowDefinition>();
            for (int i = 0; i < 9; ++i)
            {
                rows.Add(new RowDefinition());
            }
            rows[0].Height = new GridLength(10);
            rows[1].Height = new GridLength(21);
            rows[2].Height = new GridLength(21);
            rows[3].Height = new GridLength(40);
            rows[4].Height = new GridLength(21);
            rows[5].Height = new GridLength(40);
            rows[6].Height = new GridLength(21);
            rows[7].Height = new GridLength(21);
            rows[8].Height = new GridLength(10);
            for (int i = 0; i < 9; ++i)
            {
                grid1.RowDefinitions.Add(rows[i]);
            }

            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            for (int i = 0; i < 18; ++i)
            {
                columns.Add(new ColumnDefinition());
            }
            columns[0].Width = new GridLength(14);
            columns[1].Width = new GridLength(38);
            columns[2].Width = new GridLength(38);
            columns[3].Width = new GridLength(38);
            columns[4].Width = new GridLength(38);
            columns[5].Width = new GridLength(38);
            columns[6].Width = new GridLength(38);
            columns[7].Width = new GridLength(38);
            columns[8].Width = new GridLength(38);
            columns[9].Width = new GridLength(38);
            columns[10].Width = new GridLength(38);
            columns[11].Width = new GridLength(38);
            columns[12].Width = new GridLength(38);
            columns[13].Width = new GridLength(38);
            columns[14].Width = new GridLength(38);
            columns[15].Width = new GridLength(38);
            columns[16].Width = new GridLength(38);
            columns[17].Width = new GridLength(14);
            for (int i = 0; i < 18; ++i)
            {
                grid1.ColumnDefinitions.Add(columns[i]);
            }


            Rectangle sBound = new Rectangle
            {
                Fill = Brushes.LightGray,
                Stroke = Brushes.Black
            };
            Grid.SetColumnSpan(sBound, 18);
            Grid.SetRowSpan(sBound, 9);

            mainGrid.Children.Add(boxCanvas);
            boxCanvas.Children.Add(grid1);
            grid1.Children.Add(sBound);

            List<TextBox> input1 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                input1.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString((new List<byte> { input[i] }).ToArray()),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(input1[i], i + 1);
                Grid.SetRow(input1[i], 1);
                grid1.Children.Add(input1[i]);
            }

            List<TextBox> input2 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                input2.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = input[i].ToString(),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(input2[i], i + 1);
                Grid.SetRow(input2[i], 2);
                grid1.Children.Add(input2[i]);
            }

            Rectangle permBoxBound = new Rectangle
            {
                Stroke = Brushes.Black,
                Fill = Brushes.White
            };
            Grid.SetColumn(permBoxBound, 1);
            Grid.SetRow(permBoxBound, 4);
            Grid.SetColumnSpan(permBoxBound, 16);
            grid1.Children.Add(permBoxBound);


            Label permBoxLabel = new Label
            {
                Content = "Permutation box",
                Margin = new Thickness(0, -4, 0, -2),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Top

            };
            Grid.SetColumn(permBoxLabel, 1);
            Grid.SetRow(permBoxLabel, 4);
            Grid.SetColumnSpan(permBoxLabel, 16);
            grid1.Children.Add(permBoxLabel);

            List<TextBox> input3 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                input3.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = output[i].ToString(),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(input3[i], i + 1);
                Grid.SetRow(input3[i], 6);
                grid1.Children.Add(input3[i]);
            }

            List<TextBox> input4 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                input4.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString((new List<byte> { output[i] }).ToArray()),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(input4[i], i + 1);
                Grid.SetRow(input4[i], 7);
                grid1.Children.Add(input4[i]);
            }

            for (int i = 0; i < 16; ++i)
            {
                for (int j = 0; j < 2; ++j)
                {
                    Line vertt = new Line
                    {
                        Stroke = Brushes.Black,
                        X1 = 19,
                        Y1 = 5,
                        X2 = 19,
                        Y2 = 35
                    };
                    Grid.SetColumn(vertt, i + 1);
                    Grid.SetRow(vertt, 3 + j * 2);
                    RenderOptions.SetEdgeMode(vertt, EdgeMode.Aliased);
                    grid1.Children.Add(vertt);

                    Polygon vertp = new Polygon
                    {
                        Fill = Brushes.Black,
                        Points = new PointCollection(new List<Point> { new Point(19, 35), new Point(16, 28), new Point(22, 28) }),

                    };
                    Grid.SetColumn(vertp, i + 1);
                    Grid.SetRow(vertp, 3 + j * 2);
                    RenderOptions.SetEdgeMode(vertp, EdgeMode.Aliased);
                    grid1.Children.Add(vertp);
                }
            }

            isActive = true;
        }

        public static void DeleteContent(MainWindow mainWindow)
        {
            boxCanvas.Children.Clear();
            isActive = false;
        }
    }
}
