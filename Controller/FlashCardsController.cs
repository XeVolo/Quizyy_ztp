using Microsoft.VisualBasic.FileIO;
using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quizyy_wpf.Controller
{
    public class FlashCardsController	
    {
		private MainWindow mainWindow;

		private StackPanel? stackPanel;
		private Button? previousButton;
		private Button? nextButton;
		private Button? contextButton;
		private TextBlock? DisplayTextBlock;
		List<FlashCardsModel> items = BaseController.GetFlashCardsList();
		private int currentIndex = 1;
		private int control = 0;

		public FlashCardsController(MainWindow mainView) 
		{
			mainWindow = mainView;
		}

		public void OpenMode()
		{
			mainWindow.MainGrid.Children.Clear();
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

			previousButton = new Button
			{
				Content = "Poprzedni",
				Margin = new Thickness(100),
				Width = 140,
				Height = 30
			};
			previousButton.Click += PreviousButtonClick;

			nextButton = new Button
			{
				Content = "Następny",
				Margin = new Thickness(100),
				Width = 140,
				Height = 30
			};
			nextButton.Click += NextButtonClick;
			contextButton = new Button
			{
				Content = "Tłumaczenie",
				Margin = new Thickness(0, 150, 0, 0),
				Width = 140,
				Height = 30
			};
			contextButton.Click += ContextButtonClick;

			stackPanel.Children.Add(previousButton);

			stackPanel.Children.Add(contextButton);

			stackPanel.Children.Add(nextButton);

			DisplayTextBlock = new TextBlock
			{
				Margin = new Thickness(0, 10, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30
			};
			
			Grid.SetRow(stackPanel, 0);
			Grid.SetRow(DisplayTextBlock, 1);

			mainWindow.MainGrid.Children.Add(stackPanel);
			mainWindow.MainGrid.Children.Add(DisplayTextBlock);
			DisplayCurrentItem();
		}

		private void PreviousButtonClick(object sender, RoutedEventArgs e)
		{
			if (currentIndex > 0)
			{
				currentIndex--;
				DisplayCurrentItem();
			}else if(currentIndex == 0)
			{
				currentIndex=items.Count-1;
				DisplayCurrentItem();
			}
		}

		private void NextButtonClick(object sender, RoutedEventArgs e)
		{
			if (currentIndex < items.Count - 1)
			{
				currentIndex++;
				DisplayCurrentItem();
			}
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
