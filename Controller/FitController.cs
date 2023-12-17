using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Quizyy_wpf.Controller
{
    class FitController
    {
		private MainWindow mainWindow;
		private StackPanel stackPanel1;
		private StackPanel stackPanel2;
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
					Margin = new Thickness(300,5,0,5),
					Width = 200,
					Height = 30
				};
				rightButtons.Click += LeftButtonClick;

				stackPanel2.Children.Add(rightButtons);
			}
			Grid.SetColumn(stackPanel1, 0);
			Grid.SetColumn(stackPanel2, 1);
			Grid.SetRow(stackPanel2, 0);
			mainWindow.MainGrid.Children.Add(stackPanel1);
			mainWindow.MainGrid.Children.Add(stackPanel2);
		}
		private void LeftButtonClick(object sender, RoutedEventArgs e)
		{

		}
		private void RightButtonClick(object sender, RoutedEventArgs e)
		{

		}
	}
}
