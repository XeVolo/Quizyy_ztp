using Quizyy_wpf.Model;
using Quizyy_wpf.Proxy;
using System.Windows;
using System.Windows.Controls;

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
		private Button? standardButton;
		private TextBlock? DisplayTextBlock;
		private TextBlock? DifficultyLvlTextBlock;
		private TextBlock? Mode;
		private List<FlashCardsModel> items = new List<FlashCardsModel>();
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
			DatabaseConnectionProxy proxy2 = mainWindow.GetProxy();
			items = proxy2.GetFlashCardsList();
			defaultIterator = new FlashCardsDefaultIterator(items);
			randomIterator = new FlashCardsRandomIterator(items);
			by3Iterator = new FlashCardsBy3Iterator(items);
			InitializeComponent();
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
				Margin = new Thickness(200, 350, 0, 0),
				Width = 180,
				Height = 30,
				Style = (Style)FindResource("CustomButtonStyle")
			};
			chooseRandomButton.Click += ChooseRandomButtonClick;

            standardButton = new Button
            {
                Content = "Po kolei",
                Margin = new Thickness(-900, 350, 0, 0),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            standardButton.Click += StandardButtonClick;

            chooseBy3Button = new Button
			{
				Content = "Co trzecie",
				Margin = new Thickness(0, 350, 0, 0),
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
			stackPanel2.Children.Add(standardButton);

			DisplayTextBlock = new TextBlock
			{
				Margin = new Thickness(0, 10, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30,
				Style = (Style)FindResource("CustomTextStyle")
			};
			DifficultyLvlTextBlock = new TextBlock
			{
				Margin = new Thickness(700, 500, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Height = 30,
				Style = (Style)FindResource("CustomTextStyle")
			};
			Mode = new TextBlock
			{
				Margin = new Thickness(700, 450, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Height = 30,
				Style = (Style)FindResource("CustomTextStyle"),
				Text = "Tryb: Po kolei"
            };

            MainGrid.Children.Add(DifficultyLvlTextBlock);
			MainGrid.Children.Add(stackPanel);
			MainGrid.Children.Add(stackPanel2);
			MainGrid.Children.Add(DisplayTextBlock);
			MainGrid.Children.Add(Mode);
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
			else if (choose == 2)
			{
				by3Iterator.MovePrevious();
				currentIndex = by3Iterator.GetCurrentIndex();
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
			Mode.Text = "Tryb: Losowo";
		}

		private void StandardButtonClick(object sender, RoutedEventArgs e)
		{
			choose = 0;
			Mode.Text = "Tryb: Po kolei";
		}

		private void ChooseBy3ButtonClick(object sender, RoutedEventArgs e)
		{
			choose = 2;
            Mode.Text = "Tryb: Co trzeci";
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
				DifficultyLvlTextBlock.Text = "Poziom trudności: " + items[currentIndex].difficultylvl;
				control = 1;
			}
		}

	}
}

