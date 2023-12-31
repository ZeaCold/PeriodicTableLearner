﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using ZC.PeriodicTableLearner.Resources.Models;
using ZC.PeriodicTableLearner.Resources.Extensions;
using System.Windows.Media.Animation;

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

        public Color colorCache = Color.FromArgb(160, 255, 255, 255);

        public static MainWindow thisMainWindow = null;

        public (ChemicalGroup c, Element e) lastFilter = default;
        public static bool hasClicked;

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

            CreateColumns();
            CreateLegend();

            thisMainWindow = this;
        }

        /// <summary>
        /// On left click, apply the cache over the elements that are not in the specified ChemicalGroup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Label_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            ChemicalGroup chemicalGroup = ChemicalGroup.GetChemicalGroupFromName(label.Content.ToString());
            if (thisMainWindow.lastFilter.c != default && thisMainWindow.lastFilter.c.Equals(chemicalGroup))
                hasClicked = !hasClicked;

            thisMainWindow.ToggleFilter(chemicalGroup);
        }

        /// <summary>
        /// On mouse enter, apply the cache over the elements that are not in the specified ChemicalGroup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Label_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!hasClicked)
            {
                Label label = sender as Label;
                thisMainWindow.ToggleFilter(ChemicalGroup.GetChemicalGroupFromName(label.Content.ToString()));
            }
        }

        /// <summary>
        /// On mouse leave, remove the cache over the elements that are not in the specified ChemicalGroup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Label_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!hasClicked)
            {
                thisMainWindow.ToggleFilter();
            }
        }

        /// <summary>
        /// Toggle the visibility of the cache
        /// </summary>
        /// <param name="chemicalGroup">The chemical group to filter</param>
        /// <param name="element">The element to filter</param>
        public void ToggleFilter(ChemicalGroup chemicalGroup = default, Element element = default)
        {
            foreach (Rectangle rectangle in mainGrid.Children.OfType<Rectangle>())
            {
                if (chemicalGroup == default && element == default)
                {
                    AnimateVisibility(rectangle, false, rectangle.IsVisible ? 1 : 0);
                    continue;
                }

                if (rectangle.Tag == default) continue;
                (ChemicalGroup c, Element e) tagOfO = ((ChemicalGroup c, Element e))rectangle.Tag;
                AnimateVisibility(rectangle, !(tagOfO.c.Equals(chemicalGroup) || tagOfO.e.Equals(element)), rectangle.IsVisible ? 1 : 0);
            }

            lastFilter = lastFilter.Equals((chemicalGroup, element)) && !hasClicked ? default : (chemicalGroup, element);
        }

        /// <summary>
        /// Animate the visibility toggle
        /// </summary>
        /// <param name="element">The element to change visibility</param>
        /// <param name="isVisible">If the element finishes visible or not</param>
        /// <param name="currentState">The current state of the element, 1 for visible, 0 otherwise</param>
        private void AnimateVisibility(UIElement element, bool isVisible, int currentState)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                Duration = TimeSpan.FromSeconds(0.2),
                From = currentState,
                To = isVisible ? 1 : 0,
                FillBehavior = FillBehavior.HoldEnd
            };

            element.BeginAnimation(OpacityProperty, animation);

            if (!isVisible)
                animation.Completed += (s, e) => element.Visibility = Visibility.Hidden;
            else element.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Create the legends of chemical groups
        /// </summary>
        public void CreateLegend()
        {
            int height = 40;
            int nbrColumn = 8;
            double width = (mainWindow.Width - 20 - (nbrColumn * 10) - (nbrColumn * 20)) / nbrColumn;

            double marginLeft = 10 + (1 * width);
            for (int i = 0; i < ChemicalGroup.ChemicalGroups.Count; i++)
            {
                if (i == 6) marginLeft = 10 + (1.5 * width);

                int rtl, rtr, rbl, rbr;
                rtl = rtr = rbl = rbr = 3;

                Label lblName = new Label()
                {
                    Content = ChemicalGroup.ChemicalGroups[i].Name,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Width = width,
                    Height = 30,
                    FontSize = 12,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Arial"),
                    Padding = new Thickness(0, 0, 0, 0)
                };

                lblName.MouseLeftButtonUp += Label_MouseLeftButtonUp;
                lblName.MouseEnter += Label_MouseEnter;
                lblName.MouseLeave += Label_MouseLeave;

                Border border = new Border()
                {
                    Width = width,
                    Height = 30,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(marginLeft, 750 + ((i >= 6 ? 1 : 0) * height), 0, 0),
                    CornerRadius = new CornerRadius(rtl, rtr, rbr, rbl),
                    Child = lblName,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black,
                    Background = ChemicalGroup.ChemicalGroups[i].GroupColor
                };

                mainGrid.Children.Add(border);

                marginLeft += width + 10 + 20;
            }
        }

        /// <summary>
        /// Create columns index
        /// </summary>
        public void CreateColumns()
        {
            int height;
            string letter;
            int number = 0;

            for (int i = 0; i < 18; i++)
            {
                letter = i >= 2 && i <= 11 ? "B" : "A";
                height = i >= 1 && i < 17 ? (i >= 2 && i <= 11 ? 3 : 1) : 0;
                number++;
                if (i == 10) number = 1;
                if (i >= 7 && i <= 9) number = 8;

                Label lblColumnTitle = new Label()
                {
                    Content = number.ToRoman() + letter,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + i * CASE_WIDTH + i * 2, 10 + height * CASE_HEIGHT + height * 2, 0, 0),
                    Width = CASE_WIDTH,
                    Height = 24,
                    FontSize = FONT_SIZE * 2 / 3,
                    FontWeight = FontWeights.Bold,
                    FontFamily = new FontFamily("Arial"),
                    Padding = new Thickness(2, 1, 0, 0)
                };

                mainGrid.Children.Add(lblColumnTitle);
            }
        }

        /// <summary>
        /// Create the table case of the series of element
        /// </summary>
        /// <param name="first">The first element of the series</param>
        /// <param name="width">The width in the table</param>
        /// <param name="height">The height in the table</param>
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
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
                    CornerRadius = new CornerRadius(rtl, rtr, rbr, rbl),
                    Child = rect,
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black
                };

                Rectangle cache = new Rectangle()
                {
                    Tag = (allElements[first].ChemicalGroup, allElements[first]),
                    Visibility = Visibility.Hidden,
                    Width = CASE_WIDTH,
                    Height = CASE_HEIGHT,
                    Fill = new SolidColorBrush(colorCache),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
                };

                Label lblNoAtom = new Label()
                {
                    Content = allElements[first].AtomicNumber.ToString() + "-" + (allElements[first].AtomicNumber + 14).ToString(),
                    HorizontalContentAlignment = HorizontalAlignment.Left,
                    VerticalContentAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
                    Width = CASE_WIDTH,
                    Height = CASE_HEIGHT / 3,
                    FontSize = FONT_SIZE * 1 / 2,
                    FontFamily = new FontFamily("Arial"),
                    Padding = new Thickness(3, 2, 0, 0)
                };

                _ = mainGrid.Children.Add(border);
                _ = mainGrid.Children.Add(lblNoAtom);
                _ = mainGrid.Children.Add(cache);
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
                    Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
                    Width = CASE_WIDTH + 5,
                    Height = CASE_HEIGHT,
                    FontSize = FONT_SIZE * 1 / 2,
                    FontFamily = new FontFamily("Arial Black"),
                    Padding = new Thickness(2, 1, 0, 0)
                };

                _ = mainGrid.Children.Add(lblSerieTitle);
            }
        }

        /// <summary>
        /// Create the case for an element
        /// </summary>
        /// <param name="element">The element to create the case for</param>
        /// <param name="width">The width in the table</param>
        /// <param name="height">The height in the table</param>
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
                Tag = (element.ChemicalGroup, element),
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
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
                CornerRadius = new CornerRadius(rtl, rtr, rbr, rbl),
                Child = rect,
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.Black
            };

            Rectangle cache = new Rectangle()
            {
                Tag = (element.ChemicalGroup, element),
                Visibility = Visibility.Hidden,
                Width = CASE_WIDTH,
                Height = CASE_HEIGHT,
                Fill = new SolidColorBrush(colorCache),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
            };

            Label lblSymbol = new Label()
            {
                Content = element.Symbol,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 29 + height * CASE_HEIGHT + height * 2, 0, 0),
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
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 74 + height * CASE_HEIGHT + height * 2, 0, 0),
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
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 86 + height * CASE_HEIGHT + height * 2, 0, 0),
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
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
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
                Margin = new Thickness(10 + width * CASE_WIDTH + width * 2, 32 + height * CASE_HEIGHT + height * 2, 0, 0),
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
            _ = mainGrid.Children.Add(cache);
        }    
    }
}
