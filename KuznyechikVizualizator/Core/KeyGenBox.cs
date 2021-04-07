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
    class KeyGenBox
    {
        private static List<TextBlock> textBlocks = new List<TextBlock>();
        public static List<Button> buttons = new List<Button>();

        public static void GenerateContent(MainWindow mainWindow, Kuznyechik k)
        {
            textBlocks.Clear();
            buttons.Clear();
            object wantedNode = mainWindow.FindName("keyTabScroller");
            ScrollViewer keyTabScroller = wantedNode as ScrollViewer;

            ////////////////////////////GRID Gefinition////////////////////////////////////

            Grid keyGrid = new Grid
            {
                Height = 7710,
                Width = 740,
                Margin = new Thickness(20, 20, 20, 20),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            List<ColumnDefinition> columns = new List<ColumnDefinition>();
            for (int i = 0; i < 8; ++i)
            {
                columns.Add(new ColumnDefinition());
            }
            columns[0].Width = new GridLength(60);
            columns[1].Width = new GridLength(140);
            columns[2].Width = new GridLength(60);
            columns[3].Width = new GridLength(140);
            columns[4].Width = new GridLength(60);
            columns[5].Width = new GridLength(140);
            columns[6].Width = new GridLength(60);
            columns[7].Width = new GridLength(80);

            for (int i = 0; i < 8; ++i)
            {
                keyGrid.ColumnDefinitions.Add(columns[i]);
            }

            List<RowDefinition> rows = new List<RowDefinition>();
            for (int i = 0; i < 193; ++i)
            {
                rows.Add(new RowDefinition());
            }
            for (int i = 0; i < 32; ++i)
            {
                rows[i * 6 + 0].Height = new GridLength(30);
                rows[i * 6 + 1].Height = new GridLength(60);
                rows[i * 6 + 2].Height = new GridLength(30);
                rows[i * 6 + 3].Height = new GridLength(30);
                rows[i * 6 + 4].Height = new GridLength(60);
                rows[i * 6 + 5].Height = new GridLength(30);
            }

            for (int i = 0; i < 193; ++i)
            {
                keyGrid.RowDefinitions.Add(rows[i]);
            }

            rows[192].Height = new GridLength(30);
            keyTabScroller.Content = keyGrid;

            List<Line> lines = new List<Line>();
            List<Polygon> polygons = new List<Polygon>();
            List<Label> labels = new List<Label>();

            //////////////////////////LOOP UIELEMENTS DEFINITION////////////////////////////

            for (int i = 0; i < 33; ++i)
            {
                Label lb0 = new Label
                {
                    Content = "k" + i.ToString(),
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(lb0, 0);
                Grid.SetRow(lb0, i * 6 + 0);
                labels.Add(lb0);
                keyGrid.Children.Add(lb0);

                Label lb1 = new Label
                {
                    Content = "k" + (i + 1).ToString(),
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                Grid.SetColumn(lb1, 7);
                Grid.SetRow(lb1, i * 6 + 0);
                labels.Add(lb1);
                keyGrid.Children.Add(lb1);

                TextBlock tb0 = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(127, 127, 127)),
                    FontStyle = FontStyles.Italic
                };
                tb0.Inlines.Add(BitConverter.ToString(k.k[i].ToArray()).Replace("-", ""));
                Grid.SetColumn(tb0, 1);
                Grid.SetRow(tb0, i * 6 + 0);
                Grid.SetColumnSpan(tb0, 3);
                textBlocks.Add(tb0);
                keyGrid.Children.Add(tb0);

                TextBlock tb1 = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Color.FromRgb(127, 127, 127)),
                    FontStyle = FontStyles.Italic
                };
                tb1.Inlines.Add(BitConverter.ToString(k.k[i + 1].ToArray()).Replace("-", ""));
                Grid.SetColumn(tb1, 4);
                Grid.SetRow(tb1, i * 6 + 0);
                Grid.SetColumnSpan(tb1, 3);
                textBlocks.Add(tb1);
                keyGrid.Children.Add(tb1);

                if (i == 32)
                {
                    break;
                }

                Line vertt = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 0,
                    X2 = 30,
                    Y2 = 60
                };
                Grid.SetColumn(vertt, 0);
                Grid.SetRow(vertt, i * 6 + 1);
                RenderOptions.SetEdgeMode(vertt, EdgeMode.Aliased);
                lines.Add(vertt);
                keyGrid.Children.Add(vertt);

                Polygon vertp = new Polygon
                {
                    Fill = Brushes.Black,
                    Points = new PointCollection(new List<Point> { new Point(30, 60), new Point(27, 50), new Point(33, 50) }),

                };
                Grid.SetColumn(vertp, 0);
                Grid.SetRow(vertp, i * 6 + 1);
                polygons.Add(vertp);
                keyGrid.Children.Add(vertp);

                Line vertt1 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 30,
                    X2 = 30,
                    Y2 = 60
                };
                Grid.SetColumn(vertt1, 6);
                Grid.SetRow(vertt1, i * 6 + 1);
                RenderOptions.SetEdgeMode(vertt1, EdgeMode.Aliased);
                lines.Add(vertt1);
                keyGrid.Children.Add(vertt1);

                Polygon vertp1 = new Polygon
                {
                    Fill = Brushes.Black,
                    Points = new PointCollection(new List<Point> { new Point(30, 60), new Point(27, 50), new Point(33, 50) }),

                };
                Grid.SetColumn(vertp1, 6);
                Grid.SetRow(vertp1, i * 6 + 1);
                polygons.Add(vertp1);
                keyGrid.Children.Add(vertp1);

                Line vertt2 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 40,
                    Y1 = 0,
                    X2 = 40,
                    Y2 = 120
                };
                Grid.SetColumn(vertt2, 7);
                Grid.SetRow(vertt2, i * 6 + 1);
                Grid.SetRowSpan(vertt2, 3);
                RenderOptions.SetEdgeMode(vertt2, EdgeMode.Aliased);
                lines.Add(vertt2);
                keyGrid.Children.Add(vertt2);

                Label lb2 = new Label
                {
                    Content = "C" + i.ToString(),
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 16,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 5, 0, 0)
                };
                Grid.SetColumn(lb2, 6);
                Grid.SetRow(lb2, i * 6 + 1);
                labels.Add(lb2);
                keyGrid.Children.Add(lb2);

                

                for (int j = 0; j < 3; ++j)
                {
                    TextBlock tb = new TextBlock
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Foreground = new SolidColorBrush(Color.FromRgb(127, 127, 127)),
                        FontStyle = FontStyles.Italic
                    };
                    tb.Inlines.Add(BitConverter.ToString(k.keyGenRounds[3 * i + j].ToArray()).Replace("-", "").Substring(0, 16) + "\n" + BitConverter.ToString(k.keyGenRounds[3 * i + j].ToArray()).Replace("-", "").Substring(16, 16));
                    Grid.SetColumn(tb, 5 - j * 2);
                    Grid.SetRow(tb, i * 6 + 1);
                    textBlocks.Add(tb);
                    keyGrid.Children.Add(tb);

                    Line t = new Line
                    {
                        Stroke = Brushes.Black,
                        X1 = 0,
                        Y1 = 15,
                        X2 = 140,
                        Y2 = 15

                    };
                    Grid.SetColumn(t, 1 + j * 2);
                    Grid.SetRow(t, i * 6 + 2);
                    RenderOptions.SetEdgeMode(t, EdgeMode.Aliased);
                    lines.Add(t);
                    keyGrid.Children.Add(t);

                    Polygon p = new Polygon
                    {
                        Fill = Brushes.Black,
                        Points = new PointCollection(new List<Point> { new Point(0, 15), new Point(10, 12), new Point(10, 18) }),

                    };
                    Grid.SetColumn(p, 1 + j * 2);
                    Grid.SetRow(p, i * 6 + 2);
                    polygons.Add(p);
                    keyGrid.Children.Add(p);
                }

                Button x0 = new Button
                {
                    Content = "X",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(x0, 0);
                Grid.SetRow(x0, i * 6 + 2);
                buttons.Add(x0);
                keyGrid.Children.Add(x0);
                x0.Click += new RoutedEventHandler(mainWindow.X_Click);

                Button l = new Button
                {
                    Content = "L",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(l, 2);
                Grid.SetRow(l, i * 6 + 2);
                buttons.Add(l);
                keyGrid.Children.Add(l);
                l.Click += new RoutedEventHandler(mainWindow.L_Click);

                Button s = new Button
                {
                    Content = "S",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(s, 4);
                Grid.SetRow(s, i * 6 + 2);
                buttons.Add(s);
                keyGrid.Children.Add(s);
                s.Click += new RoutedEventHandler(mainWindow.S_Click);

                Button x1 = new Button
                {
                    Content = "X",
                    FontFamily = new FontFamily("Times New Roman"),
                    FontSize = 18,
                    Background = Brushes.Transparent

                };
                Grid.SetColumn(x1, 6);
                Grid.SetRow(x1, i * 6 + 2);
                buttons.Add(x1);
                keyGrid.Children.Add(x1);
                x1.Click += new RoutedEventHandler(mainWindow.X_Click);

                Line l2 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 0,
                    Y1 = 15,
                    X2 = 40,
                    Y2 = 15

                };
                Grid.SetColumn(l2, 7);
                Grid.SetRow(l2, i * 6 + 2);
                RenderOptions.SetEdgeMode(l2, EdgeMode.Aliased);
                lines.Add(l2);
                keyGrid.Children.Add(l2);

                Polygon p2 = new Polygon
                {
                    Fill = Brushes.Black,
                    Points = new PointCollection(new List<Point> { new Point(0, 15), new Point(10, 12), new Point(10, 18) }),

                };
                Grid.SetColumn(p2, 7);
                Grid.SetRow(p2, i * 6 + 2);
                polygons.Add(p2);
                keyGrid.Children.Add(p2);

                Line l3 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 0,
                    X2 = 30,
                    Y2 = 30

                };
                Grid.SetColumn(l3, 0);
                Grid.SetRow(l3, i * 6 + 3);
                RenderOptions.SetEdgeMode(l3, EdgeMode.Aliased);
                lines.Add(l3);
                keyGrid.Children.Add(l3);

                Line l4 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 0,
                    X2 = 700,
                    Y2 = 60

                };
                Grid.SetColumn(l4, 0);
                Grid.SetColumnSpan(l4, 8);
                Grid.SetRow(l4, i * 6 + 4);
                RenderOptions.SetEdgeMode(l4, EdgeMode.Aliased);
                lines.Add(l4);
                keyGrid.Children.Add(l4);

                Line l5 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 700,
                    Y1 = 0,
                    X2 = 30,
                    Y2 = 60

                };
                Grid.SetColumn(l5, 0);
                Grid.SetColumnSpan(l5, 8);
                Grid.SetRow(l5, i * 6 + 4);
                RenderOptions.SetEdgeMode(l5, EdgeMode.Aliased);
                lines.Add(l5);
                keyGrid.Children.Add(l5);

                Line l6 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 30,
                    Y1 = 0,
                    X2 = 30,
                    Y2 = 30

                };
                Grid.SetColumn(l6, 0);
                Grid.SetRow(l6, i * 6 + 5);
                RenderOptions.SetEdgeMode(l6, EdgeMode.Aliased);
                lines.Add(l6);
                keyGrid.Children.Add(l6);

                Line l7 = new Line
                {
                    Stroke = Brushes.Black,
                    X1 = 40,
                    Y1 = 0,
                    X2 = 40,
                    Y2 = 30

                };
                Grid.SetColumn(l7, 7);
                Grid.SetRow(l7, i * 6 + 5);
                RenderOptions.SetEdgeMode(l7, EdgeMode.Aliased);
                lines.Add(l7);
                keyGrid.Children.Add(l7);

            }
        }

        public static void DeleteContent(MainWindow mainWindow)
        {
            object wantedNode = mainWindow.FindName("keyTabScroller");
            ScrollViewer keyTabScroller = wantedNode as ScrollViewer;
            keyTabScroller.Content = null;
        }
    }
}
