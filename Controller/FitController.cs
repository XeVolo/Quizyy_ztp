using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Reflection.Metadata;

namespace Quizyy_wpf.Controller
{
    class FitController
    {
		private MainWindow mainWindow;
		private StackPanel stackPanel1;
		private StackPanel stackPanel2;
		private TextBlock DisplayTextBlock1;
		private TextBlock DisplayTextBlock2;
		private TextBlock DisplayTextBlock3;
		private string? concept;
		private string? definition;
		private int? conceptid;
		private int? definitionid;
		public FitController(MainWindow mainView)
		{
			mainWindow = mainView;
		}
		public void OpenMode()
		{
			mainWindow.MainGrid.Children.Clear();
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
				Margin = new Thickness(0, 10, 0, 0)
			};
			stackPanel2 = new StackPanel
			{
				Orientation = Orientation.Vertical,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(0, 10, 0, 0)
			};
			foreach (int i in drawn)
			{
				Button leftButtons = new Button
				{
					Content = list[i].concept,
					Tag = list[i].id,
					Margin = new Thickness(10,5,300,5),
					Width = 200,
					Height = 30
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
					Margin = new Thickness(300,5,0,5),
					Width = 200,
					Height = 30
				};
				rightButtons.Click += RightButtonClick;

				stackPanel2.Children.Add(rightButtons);
			}
			mainWindow.MainGrid.Children.Add(stackPanel1);
			mainWindow.MainGrid.Children.Add(stackPanel2);
			DisplayTextBlock1 = new TextBlock
			{
				Margin = new Thickness(10, 300, 300, 5),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30
			};
			DisplayTextBlock2 = new TextBlock
			{
				Margin = new Thickness(300, 300, 0, 5),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30
			};
			DisplayTextBlock3 = new TextBlock
			{
				Margin = new Thickness(0, 350, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30
			};
			mainWindow.MainGrid.Children.Add(DisplayTextBlock1);
			mainWindow.MainGrid.Children.Add(DisplayTextBlock2);
			mainWindow.MainGrid.Children.Add(DisplayTextBlock3);
		}
		private void LeftButtonClick(object sender, RoutedEventArgs e)
		{
			
			if (sender is Button clickedButton)
			{
				concept = clickedButton.Content.ToString();
				conceptid = Convert.ToInt32(clickedButton.Tag);
				DisplayTextBlock1.Text = "Wybrano: "+concept;
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
			}
			if (concept != null && definition != null)
			{
				CheckCorrectness();
			}
		}
		private void CheckCorrectness()
		{
			if(conceptid== definitionid)
			{
				DisplayTextBlock3.Text = "Połączenie poprawne";
			}
			else
			{
				DisplayTextBlock3.Text = "Połączenie błędne";
			}
			concept = null;
			definition = null;
			conceptid = null;
			definitionid = null;
		}
	}
}
