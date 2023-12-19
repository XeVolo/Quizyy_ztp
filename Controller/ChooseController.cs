﻿using Quizyy_wpf.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Quizyy_wpf.Controller
{
	class ChooseController
	{
		private MainWindow mainWindow;
		private StackPanel? stackPanel1;
		private StackPanel? stackPanel2;
		private Button? ansButton1;
		private Button? ansButton2;
		private Button? ansButton3;
		private Button? ansButton4;
		private TextBlock? DisplayTextBlock;
		private List<WriteModel> items = BaseController.GetWriteList();
		private int index = 0;

		public ChooseController(MainWindow mainView)
		{
			mainWindow = mainView;
		}
		public void OpenMode()
		{
			mainWindow.MainGrid.Children.Clear();
			NewSet();
		}
		private void NewSet()
		{
			List<string> anslist = new List<string>();
			index = GetRandom();
			anslist.Add(items[index].answer);
			anslist.Add(items[index].incorrectans1);
			anslist.Add(items[index].incorrectans2);
			anslist.Add(items[index].incorrectans3);
			List<int> drawn = GetNumbers();
			stackPanel1 = new StackPanel
			{
				Orientation = Orientation.Vertical,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(0, 100, 0, 0)
			};
			stackPanel2 = new StackPanel
			{
				Orientation = Orientation.Vertical,
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(0, 100, 0, 0)
			};
			ansButton1 = new Button
			{
				Content = anslist[drawn[0]],
				Margin = new Thickness(300, 10, 0, 0),
				Width = 140,
				Height = 30
			};
			ansButton1.Click += AnsButtonClick;
			ansButton2 = new Button
			{
				Content = anslist[drawn[1]],
				Margin = new Thickness(300, 10, 0, 0),
				Width = 140,
				Height = 30
			};
			ansButton2.Click += AnsButtonClick;
			ansButton3 = new Button
			{
				Content = anslist[drawn[2]],
				Margin = new Thickness(0, 10, 300, 0),
				Width = 140,
				Height = 30
			};
			ansButton3.Click += AnsButtonClick;
			ansButton4 = new Button
			{
				Content = anslist[drawn[3]],
				Margin = new Thickness(0, 10, 300, 0),
				Width = 140,
				Height = 30
			};
			ansButton4.Click += AnsButtonClick;
			stackPanel1.Children.Add(ansButton1);
			stackPanel1.Children.Add(ansButton2);
			stackPanel2.Children.Add(ansButton3);
			stackPanel2.Children.Add(ansButton4);
			mainWindow.MainGrid.Children.Add(stackPanel1);
			mainWindow.MainGrid.Children.Add(stackPanel2);
			DisplayTextBlock = new TextBlock
			{

				Margin = new Thickness(0, 10, 0, 10),
				HorizontalAlignment = HorizontalAlignment.Center,
				Height = 30
			};
			mainWindow.MainGrid.Children.Add(DisplayTextBlock);
			DisplayQuestion();
		}
		private List<int> GetNumbers()
		{
			Range xy = new Range(0, 3);
			int count = 4;
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
		private int GetRandom()
		{
			List<WriteModel> list = BaseController.GetWriteList();
			int size = list.Count;
			Random rnd = new Random();
			int result = rnd.Next(size);
			return result;
		}
		private void AnsButtonClick(object sender, EventArgs e)
		{
			if (sender is Button clickedButton)
			{
				string ans = clickedButton.Content.ToString();
				bool correctness = ans.Equals(items[index].answer);
				if (correctness)
				{

					MessageBox.Show("Odpowiedź prawidłowa");
					DisplayTextBlock.Text = "";
					NewSet();
				}
				else
				{
					MessageBox.Show("Odpowiedź błędna");
				}
				
			}
		}
		private void DisplayQuestion()
		{
			DisplayTextBlock.Text = items[index].question;
		}

	}
}
