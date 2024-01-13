using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Quizyy_wpf.Model;

namespace Quizyy_wpf.View
{
    /// <summary>
    /// Logika interakcji dla klasy FlashCardsView.xaml
    /// </summary>
    public partial class FlashCardsView : UserControl
    {
        private MainWindow mainWindow;
        private StackPanel? stackPanel;
        private StackPanel? stackPanel2;
        private Button? previousButton;
        private Button? nextButton;
        private Button? contextButton;
        private Button? chooseRandomButton;
        private Button? chooseBy3Button;
        private TextBlock? DisplayTextBlock;
        List<FlashCardsModel> items = BaseController.GetFlashCardsList();
        private int currentIndex = 1;
        private int control = 0;
        private static FlashCardsView instance;
        private int choose = 0;

        private FlashCardsDefaultIterator defaultIterator;
        private FlashCardsRandomIterator randomIterator;
        private FlashCardsBy3Iterator by3Iterator;

        public static FlashCardsView GetInstance(MainWindow mainView)
		{
			if (instance == null)
			{
				instance = new FlashCardsView(mainView);
			}
			return instance;
		}

        private FlashCardsView(MainWindow mainView)
        {
            mainWindow = mainView;
            InitializeComponent();
            items = BaseController.GetFlashCardsList();
            defaultIterator = new FlashCardsDefaultIterator(items);
            randomIterator = new FlashCardsRandomIterator(items);
            by3Iterator = new FlashCardsBy3Iterator(items);
            OpenMode();
        }

        public void OpenMode()
        {
            CreateUI();
        }

        private void CreateUI()
        {
            stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };

            stackPanel2 = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(0, 10, 0, 0)
            };

            previousButton = new Button
            {
                Content = "Poprzedni",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            previousButton.Click += PreviousButtonClick;

            nextButton = new Button
            {
                Content = "Następny",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            nextButton.Click += NextButtonClick;

            contextButton = new Button
            {
                Content = "Tłumaczenie",
                Margin = new Thickness(0, 150, 0, 0),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            contextButton.Click += ContextButtonClick;

            chooseRandomButton = new Button
            {
                Content = "Losowo",
                Margin = new Thickness(-5, 350, 0, 0),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            chooseRandomButton.Click += ChooseRandomButtonClick;

            chooseBy3Button = new Button
            {
                Content = "Co trzecie",
                Margin = new Thickness(5, 350, 0, 0),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            chooseBy3Button.Click += ChooseBy3ButtonClick;

            stackPanel.Children.Add(previousButton);
            stackPanel.Children.Add(contextButton);
            stackPanel.Children.Add(nextButton);
            stackPanel2.Children.Add(chooseRandomButton);
            stackPanel2.Children.Add(chooseBy3Button);

            DisplayTextBlock = new TextBlock
            {
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };

            MainGrid.Children.Add(stackPanel);
            MainGrid.Children.Add(DisplayTextBlock);
            MainGrid.Children.Add(stackPanel2);
            DisplayCurrentItem();
        }

        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            if (choose == 0)
            {
                if (defaultIterator.MovePrevious())
                {
                    currentIndex = defaultIterator.GetCurrentIndex();
                }
                else
                {
                    currentIndex = items.Count - 1;
                }
            }
            else if (choose == 1 && randomIterator.HasPrevious())
            {
                randomIterator.MovePrevious();
                currentIndex = randomIterator.GetCurrentIndex();
            }
            else if(choose == 2)
            {
                by3Iterator.MovePrevious();
                currentIndex= by3Iterator.GetCurrentIndex();
            }
            else
            {
                currentIndex = 0;
            }

            DisplayCurrentItem();
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (choose == 0)
            {
                if (defaultIterator.MoveNext())
                {
                    currentIndex = defaultIterator.GetCurrentIndex();
                }
                else
                {
                    currentIndex = 0;
                }
            }
            else if (choose == 1 && randomIterator.HasNext())
            {
                randomIterator.MoveNext();
                currentIndex = randomIterator.GetCurrentIndex();
            }
            else if (choose == 2)
            {
                by3Iterator.MoveNext();
                currentIndex = by3Iterator.GetCurrentIndex();
            }
            else
            {
                currentIndex = 0;
            }

            DisplayCurrentItem();
        }

        private void ChooseRandomButtonClick(object sender, RoutedEventArgs e)
        {
            choose = 1;
        }

        private void ChooseBy3ButtonClick(object sender, RoutedEventArgs e)
        {
            choose = 2;
        }

        private void ContextButtonClick(object sender, RoutedEventArgs e)
        {
            if (control == 1)
            {
                DisplayTextBlock.Text = items[currentIndex].definition;
                control = 0;
            }
            else
            {
                DisplayTextBlock.Text = items[currentIndex].concept;
                control = 1;
            }
        }

        private void DisplayCurrentItem()
        {
            if (items.Count > 0 && currentIndex >= 0 && currentIndex < items.Count)
            {
                DisplayTextBlock.Text = items[currentIndex].concept;
                control = 1;
            }
        }
    }
}