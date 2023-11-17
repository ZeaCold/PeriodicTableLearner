using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;
using ZC.PeriodicTableLearner.Resources.Models;

namespace ZC.PeriodicTableLearner
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int CASE_WIDTH = 75;
        public const int CASE_HEIGHT = 75;
        public const int FONT_SIZE = 24;

        public static List<Element> allElements = new List<Element>();
        public static Element[,] elements = new Element[9, 18];

        public MainWindow()
        {
            InitializeComponent();

            ChemicalGroup.CreateAll();
            
            foreach (ChemicalGroup chemicalGroup in ChemicalGroup.ChemicalGroups)
            {
                foreach (Element element in chemicalGroup.Elements)
                    allElements.Add(element);
            }

            allElements = allElements.OrderBy(element => element.AtomicNumber).ToList();

            int idx = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (i >= 5 && i <= 6 && j == 2)
                    {
                        for (int k = 0; k < 15; k++)
                        {
                            elements[i + 2, j + k] = allElements[idx];
                            idx++;
                        }
                    }
                    else if ((i == j && j == 0) || (i == 0 && j == 17) || (i >= 1 && i <= 2 && j <= 1) || (i >= 1 && i <= 2 && j >= 12) || (i >= 3 && i <= 4) ||
                        (i >= 5 && i <= 6 && j != 2))
                    {
                        elements[i, j] = allElements[idx];
                        idx++;
                    }
                }
            }

            idx = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 18; j++)
                {
                    if (i >= 5 && i <= 6 && j == 2)
                    {
                        CreateSerie(idx, j, i);
                        idx += 15;
                    }
                    else if (elements[i, j] == default)
                    {
                        continue; 
                    }
                    else
                    {
                        CreateCase(elements[i, j], j + (i >= 7 && i <= 8 ? 0.5 : 0), i + (i >= 7 && i <= 8 ? 0.1 : 0));
                        idx++;
                    }
                }
            }
        }

        public void CreateSerie(int first, double width, double height)
        {
            CreateTableCase();
            CreateSerieTitle();
            void CreateTableCase()
            {
                int rtl, rtr, rbl, rbr;
                rtl = rtr = rbl = rbr = 5;
                Rectangle rect = new Rectangle()
                {
                    Width = CASE_WIDTH,
                    Height = CASE_HEIGHT,
                    Fill = allElements[first].ChemicalGroup.GroupColor,
                };
                Border border = new Border()
                {
                    Width = CASE_WIDTH,
                    Height = CASE_HEIGHT,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                    CornerRadius = new CornerRadius(rtl, rtr, rbr, rbl),
                    Child = rect,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black
                };

                Label lblNoAtom = new Label()
                {
                    Content = allElements[first].AtomicNumber.ToString() + "-" + (allElements[first].AtomicNumber + 14).ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                    Width = CASE_WIDTH,
                    Height = CASE_HEIGHT / 3,
                    FontSize = FONT_SIZE * 1 / 2,
                    FontFamily = new FontFamily("Arial"),
                    Padding = new Thickness(3, 2, 0, 0)
                };

                _ = mainGrid.Children.Add(border);
                _ = mainGrid.Children.Add(lblNoAtom);
            }

            void CreateSerieTitle()
            {
                width -= 0.6;
                height += 2.1;

                Label lblSerieTitle = new Label()
                {
                    Content = allElements[first].AtomicNumber == 57 ? "Lanthanide" : "Actinide",
                    HorizontalContentAlignment = HorizontalAlignment.Right,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                    Width = CASE_WIDTH + 5,
                    Height = CASE_HEIGHT,
                    FontSize = FONT_SIZE * 1 / 2,
                    FontFamily = new FontFamily("Arial Black"),
                    Padding = new Thickness(2, 1, 0, 0)
                };

                _ = mainGrid.Children.Add(lblSerieTitle);
            }
        }

        public void CreateCase(Element element, double width, double height)
        {
            int rtl, rtr, rbl, rbr;
            rtl = rtr = rbl = rbr = 6;

            int i = (int)Math.Floor(width);
            int j = (int)Math.Floor(height);
            if (i - 1 >= 0 && elements[j, i - 1] != default && elements[j, i - 1].ChemicalGroup.Equals(elements[j, i].ChemicalGroup)) rtl = rbl = 1;
            if (i + 1 <= 17 && elements[j, i + 1] != default && elements[j, i + 1].ChemicalGroup.Equals(elements[j, i].ChemicalGroup)) rtr = rbr = 1;
            if (j - 1 >= 0 && elements[j - 1, i] != default && elements[j - 1, i].ChemicalGroup.Equals(elements[j, i].ChemicalGroup)) rtl = rtr = 1;
            if (j + 1 <= 6 && elements[j + 1, i] != default && elements[j + 1, i].ChemicalGroup.Equals(elements[j, i].ChemicalGroup)) rbl = rbr = 1;

            Rectangle rect = new Rectangle()
            {
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT,
                Fill = element.ChemicalGroup.GroupColor,
            };
            Border border = new Border()
            {
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                CornerRadius = new CornerRadius(rtl, rtr, rbr, rbl),
                Child = rect,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black
            };

            Label lblSymbol = new Label()
            {
                Content = element.Symbol,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 7 + height * CASE_HEIGHT + height * 2, 0, 0),
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT,
                FontSize = FONT_SIZE,
                FontFamily = new FontFamily("Arial Black"),
                Padding = new Thickness(0, 0, 0, 0)
            };

            Label lblName = new Label()
            {
                Content = element.Name,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, (CASE_HEIGHT * 2 / 3) + height * CASE_HEIGHT + height * 2, 0, 0),
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT / 3,
                FontSize = FONT_SIZE * 1 / 2,
                FontFamily = new FontFamily("Arial"),
                Padding = new Thickness(0, 0, 0, 0)
            };

            Label lblAtomicWeight = new Label()
            {
                Content = element.AtomicWeight,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, (CASE_HEIGHT * 5 / 6) + height * CASE_HEIGHT + height * 2, 0, 0),
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT / 3,
                FontSize = (FONT_SIZE * 1 / 2) - 2,
                FontFamily = new FontFamily("Arial"),
                Padding = new Thickness(0, 0, 0, 0)
            };

            Label lblNoAtom = new Label()
            {
                Content = element.AtomicNumber.ToString(),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT / 3,
                FontSize = (FONT_SIZE * 1 / 2) + 4,
                FontFamily = new FontFamily("Arial"),
                Padding = new Thickness(3, 2, 0, 0)
            };

            Label lblElecNeg = new Label()
            {
                Content = element.ElectroNegativity.ToString(),
                HorizontalContentAlignment = HorizontalAlignment.Right,
                VerticalContentAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT / 3,
                FontSize = FONT_SIZE * 1 / 2,
                FontFamily = new FontFamily("Arial"),
                Padding = new Thickness(0, 2, 3, 0)
            };

            _ = mainGrid.Children.Add(border);
            _ = mainGrid.Children.Add(lblNoAtom);
            _ = mainGrid.Children.Add(lblSymbol);
            _ = mainGrid.Children.Add(lblElecNeg);
            _ = mainGrid.Children.Add(lblName);
            _ = mainGrid.Children.Add(lblAtomicWeight);
        }    
    }
}
