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
    class EncryptBox
    {
        private static List<TextBlock> textBlocks = new List<TextBlock>();
        public static List<Button> buttons = new List<Button>();

        public static void GenerateContent(MainWindow mainWindow, Kuznyechik k)
        {
            object wantedNode = mainWindow.FindName("cryptTabScroller");
            ScrollViewer cryptTabScroller = wantedNode as ScrollViewer;

            Grid encryptGrid = new Grid
            {
                Height = 1440,
                Width = 640,
                Margin = new Thickness(20, 20, 20, 20),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            for (int i = 0; i < 8; ++i)
            {
                columns.Add(new ColumnDefinition());
            }
            columns[0].Width = new GridLength(20);
            columns[1].Width = new GridLength(140);
            columns[2].Width = new GridLength(60);
            columns[3].Width = new GridLength(140);
            columns[4].Width = new GridLength(60);
            columns[5].Width = new GridLength(140);
            columns[6].Width = new GridLength(60);
            columns[7].Width = new GridLength(20);

            for (int i = 0; i < 8; ++i)
            {
                encryptGrid.ColumnDefinitions.Add(columns[i]);
            }

            List<RowDefinition> rows = new List<RowDefinition>();
            for (int i = 0; i < 29; ++i)
            {
                rows.Add(new RowDefinition());
            }
            rows[0].Height = new GridLength(60);
            rows[1].Height = new GridLength(30);
            rows[2].Height = new GridLength(60);
            rows[3].Height = new GridLength(60);
            rows[4].Height = new GridLength(30);
            rows[5].Height = new GridLength(60);
            rows[6].Height = new GridLength(60);
            rows[7].Height = new GridLength(30);
            rows[8].Height = new GridLength(60);
            rows[9].Height = new GridLength(60);
            rows[10].Height = new GridLength(30);
            rows[11].Height = new GridLength(60);
            rows[12].Height = new GridLength(60);
            rows[13].Height = new GridLength(30);
            rows[14].Height = new GridLength(60);
            rows[15].Height = new GridLength(60);
            rows[16].Height = new GridLength(30);
            rows[17].Height = new GridLength(60);
            rows[18].Height = new GridLength(60);
            rows[19].Height = new GridLength(30);
            rows[20].Height = new GridLength(60);
            rows[21].Height = new GridLength(60);
            rows[22].Height = new GridLength(30);
            rows[23].Height = new GridLength(60);
            rows[24].Height = new GridLength(60);
            rows[25].Height = new GridLength(30);
            rows[26].Height = new GridLength(60);
            rows[27].Height = new GridLength(60);
            rows[28].Height = new GridLength(30);

            for (int i = 0; i < 29; ++i)
            {
                encryptGrid.RowDefinitions.Add(rows[i]);
            }

            cryptTabScroller.Content = encryptGrid;


            List <Line> lines = new List<Line>();
            List<Polygon> polygons = new List<Polygon>();
            List<Label> labels = new List<Label>();

            for (int i = 0; i <= 9; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    TextBlock tb0 = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Foreground = new SolidColorBrush(Color.FromRgb(127, 127, 127)),
                        FontStyle = FontStyles.Italic
                    };
                    tb0.Inlines.Add(BitConverter.ToString(k.encryptRounds[3 * i + j].ToArray()).Replace("-", "").Substring(0, 16) + "\n" + BitConverter.ToString(k.encryptRounds[3 * i + j].ToArray()).Replace("-", "").Substring(16, 16));
                    Grid.SetColumn(tb0, 1 + j * 2);
                    Grid.SetRow(tb0, i * 3);
                    textBlocks.Add(tb0);
                    encryptGrid.Children.Add(tb0);


                    if (i == 9)
                    {
                        break;
                    }

                    Line t1 = new Line
                    {
                        Stroke = Brushes.Black,
                        X1 = 0,
                        Y1 = 15,
                        X2 = 140,
                        Y2 = 15

                    };
                    Grid.SetColumn(t1, 1 + j * 2);
                    Grid.SetRow(t1, i * 3 + 1);
                    RenderOptions.SetEdgeMode(t1, EdgeMode.Aliased);
                    lines.Add(t1);
                    encryptGrid.Children.Add(t1);

                    Polygon p1 = new Polygon
                    {
                        Fill = Brushes.Black,
                        Points = new PointCollection(new List<Point> { new Point(140, 15), new Point(130, 12), new Point(130, 18) }),

                    };
                    Grid.SetColumn(p1, 1 + j * 2);
                    Grid.SetRow(p1, i * 3 + 1);
                    polygons.Add(p1);
                    encryptGrid.Children.Add(p1);
                }

                Label l0 = new Label
                {
                    Content = "K" + i.ToString(),
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(l0, 2);
                Grid.SetRow(l0, i * 3);
                labels.Add(l0);
                encryptGrid.Children.Add(l0);

                Line vertt = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 25,
                    X2 = 30,
                    Y2 = 60
                };
                Grid.SetColumn(vertt, 2);
                Grid.SetRow(vertt, i * 3);
                RenderOptions.SetEdgeMode(vertt, EdgeMode.Aliased);
                lines.Add(vertt);
                encryptGrid.Children.Add(vertt);

                Polygon vertp = new Polygon
                {
                    Fill = Brushes.Black,
                    Points = new PointCollection(new List<Point> { new Point(30, 60), new Point(27, 50), new Point(33, 50) }),

                };
                Grid.SetColumn(vertp, 2);
                Grid.SetRow(vertp, i * 3);
                polygons.Add(vertp);
                encryptGrid.Children.Add(vertp);

                Line t0 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 19,
                    Y1 = 15,
                    X2 = 40,
                    Y2 = 15

                };
                Grid.SetColumn(t0, 0);
                Grid.SetRow(t0, i * 3 + 1);
                RenderOptions.SetEdgeMode(t0, EdgeMode.Aliased);
                lines.Add(t0);
                encryptGrid.Children.Add(t0);

                Button x = new Button
                {
                    Content = "X",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(x, 2);
                Grid.SetRow(x, i * 3 + 1);
                buttons.Add(x);
                encryptGrid.Children.Add(x);

                if (i == 9)
                {
                    break;
                }

                Button s = new Button
                {
                    Content = "S",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent,

                };
                Grid.SetColumn(s, 4);
                Grid.SetRow(s, i * 3 + 1);
                buttons.Add(s);
                encryptGrid.Children.Add(s);
                s.Click += new RoutedEventHandler(mainWindow.S_Click);

                Button l = new Button
                {
                    Content = "L",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(l, 6);
                Grid.SetRow(l, i * 3 + 1);
                buttons.Add(l);
                encryptGrid.Children.Add(l);

                Line t3 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 0,
                    Y1 = 15,
                    X2 = 20,
                    Y2 = 15

                };
                Grid.SetColumn(t3, 7);
                Grid.SetRow(t3, i * 3 + 1);
                RenderOptions.SetEdgeMode(t3, EdgeMode.Aliased);
                lines.Add(t3);
                encryptGrid.Children.Add(t3);

                Line t4 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 20,
                    Y1 = 15,
                    X2 = 20,
                    Y2 = 70

                };
                Grid.SetColumn(t4, 7);
                Grid.SetRow(t4, i * 3 + 1);
                Grid.SetRowSpan(t4, 2);
                RenderOptions.SetEdgeMode(t4, EdgeMode.Aliased);
                lines.Add(t4);
                encryptGrid.Children.Add(t4);

                Line t5 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 20,
                    Y1 = 40,
                    X2 = 640,
                    Y2 = 40

                };
                Grid.SetColumn(t5, 0);
                Grid.SetColumnSpan(t5, 8);
                Grid.SetRow(t5, i * 3 + 2);
                RenderOptions.SetEdgeMode(t5, EdgeMode.Aliased);
                lines.Add(t5);
                encryptGrid.Children.Add(t5);

                Line t6 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 20,
                    Y1 = 40,
                    X2 = 20,
                    Y2 = 135

                };
                Grid.SetColumn(t6, 0);
                Grid.SetRowSpan(t6, 3);
                Grid.SetRow(t6, i * 3 + 2);
                RenderOptions.SetEdgeMode(t6, EdgeMode.Aliased);
                lines.Add(t6);
                encryptGrid.Children.Add(t6);

            }

            Line tt0 = new Line
            {
                Stroke = Brushes.Black,
                X1 = 19,
                Y1 = 15,
                X2 = 160,
                Y2 = 15

            };
            Grid.SetColumn(tt0, 0);
            Grid.SetColumnSpan(tt0, 2);
            Grid.SetRow(tt0, 28);
            RenderOptions.SetEdgeMode(tt0, EdgeMode.Aliased);
            lines.Add(tt0);
            encryptGrid.Children.Add(tt0);


            Polygon tp0 = new Polygon
            {
                Fill = Brushes.Black,
                Points = new PointCollection(new List<Point> { new Point(140, 15), new Point(130, 12), new Point(130, 18) }),

            };
            Grid.SetColumn(tp0, 1);
            Grid.SetRow(tp0, 28);
            polygons.Add(tp0);
            encryptGrid.Children.Add(tp0);

            TextBlock lasttb = new TextBlock
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                Foreground = new SolidColorBrush(Color.FromRgb(127, 127, 127)),
                FontStyle = FontStyles.Italic
            };
            lasttb.Inlines.Add(BitConverter.ToString(k.ciphertext.ToArray()).Replace("-", "").Substring(0, 16) + "\n" + BitConverter.ToString(k.encryptRounds[0].ToArray()).Replace("-", "").Substring(16, 16));
            Grid.SetColumn(lasttb, 3);
            Grid.SetRow(lasttb, 27);
            textBlocks.Add(lasttb);
            encryptGrid.Children.Add(lasttb);

            Line tt1 = new Line
            {
                Stroke = Brushes.Black,
                X1 = 0,
                Y1 = 15,
                X2 = 440,
                Y2 = 15

            };
            Grid.SetColumn(tt1, 3);
            Grid.SetColumnSpan(tt1, 5);
            Grid.SetRow(tt1, 28);
            RenderOptions.SetEdgeMode(tt1, EdgeMode.Aliased);
            lines.Add(tt1);
            encryptGrid.Children.Add(tt1);

            Polygon tp1 = new Polygon
            {
                Fill = Brushes.Black,
                Points = new PointCollection(new List<Point> { new Point(20, 15), new Point(10, 12), new Point(10, 18) }),

            };
            Grid.SetColumn(tp1, 7);
            Grid.SetRow(tp1, 28);
            polygons.Add(tp1);
            encryptGrid.Children.Add(tp1);
        }

        public static void RefreshContent(MainWindow mainWindow, Kuznyechik k)
        {
            if (textBlocks.Count != 0)
            {
                for (int i = 0; i < 27; i++)
                {
                    textBlocks[i].Inlines.Clear();
                    textBlocks[i].Inlines.Add(BitConverter.ToString(k.encryptRounds[i].ToArray()).Replace("-", "").Substring(0, 16) + "\n" + BitConverter.ToString(k.encryptRounds[0].ToArray()).Replace("-", "").Substring(16, 16));
                }
                textBlocks[27].Inlines.Clear();
                textBlocks[27].Inlines.Add(BitConverter.ToString(k.ciphertext.ToArray()).Replace("-", "").Substring(0, 16) + "\n" + BitConverter.ToString(k.encryptRounds[0].ToArray()).Replace("-", "").Substring(16, 16));
            }
            
        }

        public static void DeleteContent(MainWindow mainWindow, Kuznyechik k)
        {
            object wantedNode = mainWindow.FindName("cryptTabScroller");
            ScrollViewer cryptTabScroller = wantedNode as ScrollViewer;
            cryptTabScroller.Content = null;
        }

    }
}
