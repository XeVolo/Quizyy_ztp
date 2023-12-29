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
using Quizyy_wpf.Controller;
using Quizyy_wpf.Model;

namespace Quizyy_wpf.View
{
    /// <summary>
    /// Logika interakcji dla klasy FitView.xaml
    /// </summary>
    public partial class FitView : UserControl
    {
        private MainWindow mainWindow1;
        private StackPanel? stackPanel1;
        private StackPanel? stackPanel2;
        private TextBlock? DisplayTextBlock1;
        private TextBlock? DisplayTextBlock2;
        private TextBlock? DisplayTextBlock3;
        private string? concept;
        private string? definition;
        private int? conceptid;
        private int? definitionid;
        private Button? chosen1;
        private Button? chosen2;
        private int resoult = 0;
        public FitView(MainWindow mainView)
        {
            mainWindow1 = mainView;
            InitializeComponent();
            OpenMode();
        }
        public void OpenMode()
        {
            mainWindow1.backButton.Visibility = Visibility.Visible;
            
            NewSet();
        }
        private int GetRandom()
        {
            List<FlashCardsModel> lista = BaseController.GetFlashCardsList();
            int size = lista.Count - 7;
            Random rnd = new Random();
            int result = rnd.Next(size);
            return result;
        }
        private List<int> GetNumbers(Range xy)
        {
            int count = 7;
            Random rnd = new Random();
            List<int> numbers = new List<int>();

            while (numbers.Count < count)
            {
                int liczba = rnd.Next(xy.Start.Value, xy.End.Value + 1);
                if (!numbers.Contains(liczba))
                {
                    numbers.Add(liczba);
                }
            }

            return numbers;
        }
        private void NewSet()
        {
            List<FlashCardsModel> list = BaseController.GetFlashCardsList();
            int first = GetRandom();
            Range xy = new Range(first, first + 6);
            List<int> drawn = GetNumbers(xy);
            stackPanel1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 110, 0, 0)
            };
            stackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 110, 0, 0)
            };
            foreach (int i in drawn)
            {
                Button leftButtons = new Button
                {
                    Content = list[i].concept,
                    Tag = list[i].id,
                    Margin = new Thickness(10, 5, 300, 5),
                    Width = 250,
                    Height = 30,
                    Style = (Style)FindResource("CustomButtonStyle")
                };
                leftButtons.Click += LeftButtonClick;

                stackPanel1.Children.Add(leftButtons);
            }
            drawn.Clear();
            drawn = GetNumbers(xy);
            foreach (int i in drawn)
            {
                Button rightButtons = new Button
                {
                    Content = list[i].definition,
                    Tag = list[i].id,
                    Margin = new Thickness(300, 5, 0, 5),
                    Width = 250,
                    Height = 30,
                    Style = (Style)FindResource("CustomButtonStyle")
                };
                rightButtons.Click += RightButtonClick;

                stackPanel2.Children.Add(rightButtons);
            }
            MainGrid.Children.Add(stackPanel1);
            MainGrid.Children.Add(stackPanel2);
            DisplayTextBlock1 = new TextBlock
            {
                Margin = new Thickness(10, 400, 300, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            DisplayTextBlock2 = new TextBlock
            {
                Margin = new Thickness(300, 400, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            DisplayTextBlock3 = new TextBlock
            {
                Margin = new Thickness(0, 450, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            MainGrid.Children.Add(DisplayTextBlock1);
            MainGrid.Children.Add(DisplayTextBlock2);
            MainGrid.Children.Add(DisplayTextBlock3);
        }
        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {

            if (sender is Button clickedButton)
            {
                concept = clickedButton.Content.ToString();
                conceptid = Convert.ToInt32(clickedButton.Tag);
                DisplayTextBlock1.Text = "Wybrano: " + concept;
                chosen1 = clickedButton;
            }
            if (concept != null && definition != null)
            {
                CheckCorrectness();
            }


        }
        private void RightButtonClick(object sender, RoutedEventArgs e)
        {

            if (sender is Button clickedButton)
            {
                definition = clickedButton.Content.ToString();
                definitionid = Convert.ToInt32(clickedButton.Tag);
                DisplayTextBlock2.Text = "Wybrano: " + definition;
                chosen2 = clickedButton;
            }
            if (concept != null && definition != null)
            {
                CheckCorrectness();
            }
        }
        private void CheckCorrectness()
        {
            if (conceptid == definitionid)
            {
                DisplayTextBlock3.Text = "Połączenie poprawne";
                chosen1.Visibility = Visibility.Collapsed;
                chosen2.Visibility = Visibility.Collapsed;
                resoult++;
            }
            else
            {
                DisplayTextBlock3.Text = "Połączenie błędne";
            }
            DisplayTextBlock1.Text = "";
            DisplayTextBlock2.Text = "";
            concept = null;
            definition = null;
            conceptid = null;
            definitionid = null;
            chosen1 = null;
            chosen2 = null;
            if (resoult == 7)
            {
                resoult = 0;
                OpenMode();
            }

        }
    }
}
