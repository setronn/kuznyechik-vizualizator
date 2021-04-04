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
    class XBoxVisualization
    {
        private static bool isActive = false;
        private static Canvas boxCanvas;

        public static bool IsActive()
        {
            return isActive;
        }

        public static void GenerateContent(MainWindow mainWindow, List<byte> input1, List<byte> input2, List<byte> output)
        {
            object wantedNode = mainWindow.FindName("mainGrid");
            Grid mainGrid = wantedNode as Grid;

            boxCanvas = new Canvas();
            boxCanvas.Margin = new Thickness(80, 10, 10, 10);
            Grid.SetRow(boxCanvas, 1);
            mainGrid.Children.Add(boxCanvas);

            Grid grid1 = new Grid();

            List<RowDefinition> rows = new List<RowDefinition>();
            for (int i = 0; i < 7; ++i)
            {
                rows.Add(new RowDefinition());
            }
            rows[0].Height = new GridLength(10);
            rows[1].Height = new GridLength(21);
            rows[2].Height = new GridLength(21);
            rows[3].Height = new GridLength(21);
            rows[4].Height = new GridLength(40);
            rows[5].Height = new GridLength(21);
            rows[6].Height = new GridLength(10);
            for (int i = 0; i < 7; ++i)
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
            boxCanvas.Children.Add(grid1);


            Rectangle sBound = new Rectangle
            {
                Fill = Brushes.LightGray,
                Stroke = Brushes.Black
            };
            Grid.SetColumnSpan(sBound, 18);
            Grid.SetRowSpan(sBound, 9);
            grid1.Children.Add(sBound);


            List<TextBox> textBoxes1 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                textBoxes1.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString((new List<byte> { input1[i] }).ToArray()),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(textBoxes1[i], i + 1);
                Grid.SetRow(textBoxes1[i], 1);
                grid1.Children.Add(textBoxes1[i]);
            }

            List<TextBox> textBoxes2 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                textBoxes2.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString((new List<byte> { input2[i] }).ToArray()),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(textBoxes2[i], i + 1);
                Grid.SetRow(textBoxes2[i], 3);
                grid1.Children.Add(textBoxes2[i]);
            }

            List<TextBox> textBoxes3 = new List<TextBox>();
            for (int i = 0; i < 16; ++i)
            {
                textBoxes3.Add(new TextBox
                {
                    IsReadOnly = true,
                    Text = BitConverter.ToString((new List<byte> { output[i] }).ToArray()),
                    TextAlignment = TextAlignment.Center
                });
                Grid.SetColumn(textBoxes3[i], i + 1);
                Grid.SetRow(textBoxes3[i], 5);
                grid1.Children.Add(textBoxes3[i]);
            }

            for (int i = 0; i < 16; ++i)
            {
                Label l = new Label
                {
                    Content = "⊕",
                    FontFamily = new FontFamily("Times New Roman"),
                    Margin = new Thickness(0, -5, 0, 0),
                    FontSize = 18,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                RenderOptions.SetEdgeMode(l, EdgeMode.Aliased);
                Grid.SetColumn(l, 1 + i);
                Grid.SetRow(l, 2);
                grid1.Children.Add(l);

                Line vertt = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 19,
                    Y1 = 5,
                    X2 = 19,
                    Y2 = 35
                };
                Grid.SetColumn(vertt, i + 1);
                Grid.SetRow(vertt, 4);
                RenderOptions.SetEdgeMode(vertt, EdgeMode.Aliased);
                grid1.Children.Add(vertt);

                Polygon vertp = new Polygon
                {
                    Fill = Brushes.Black,
                    Points = new PointCollection(new List<Point> { new Point(19, 35), new Point(16, 28), new Point(22, 28) }),

                };
                Grid.SetColumn(vertp, i + 1);
                Grid.SetRow(vertp, 4);
                RenderOptions.SetEdgeMode(vertp, EdgeMode.Aliased);
                grid1.Children.Add(vertp);
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
