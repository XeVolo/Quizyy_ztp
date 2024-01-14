﻿
using Quizyy_wpf.Model;
using Quizyy_wpf.Proxy;
using Quizyy_wpf.View;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Quizyy_wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private static DatabaseConnection realconnection = new DatabaseConnection();
		public static DatabaseConnectionProxy proxy1 = new DatabaseConnectionProxy(realconnection);
		public MainWindow()
		{
			//UpdateDatabase();
			InitializeComponent();
			GenerateButtons();
		}
		public DatabaseConnectionProxy GetProxy()
		{
			return proxy1;
		}

        public void GenerateButtons()
        {
            backButton.Visibility = Visibility.Collapsed;
            string[] buttonLabels = { "Fiszki", "Dopasowanie pojęć", "Podanie odpowiedzi", "Wybór odpowiedzi" };

            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 200, 0, 0)
            };

            for (int i = 0; i < buttonLabels.Length; i++)
            {
                Button button = new Button
                {
                    Content = buttonLabels[i],
                    Margin = new Thickness(10),
                    Width = 180,
                    Height = 30,
                    Style = (Style)FindResource("CustomButtonStyle"),



                };
                switch (i)
                {
                    case 0:
                        button.Click += FlashCards;
                        break;
                    case 1:
                        button.Click += Fit;
                        break;
                    case 2:
                        button.Click += Write;
                        break;
                    case 3:
                        button.Click += Choose;
                        break;
                }

                buttonPanel.Children.Add(button);
            }
            Button button2 = new Button
            {
                Content = "Dodaj nowe",
                Margin = new Thickness(750, 500, 0, 0),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle"),
            };
            button2.Click += NewResource;
            MainGrid.Children.Add(button2);
            MainGrid.Children.Add(buttonPanel);
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = null;
            MainGrid.Children.Clear();
            GenerateButtons();

        }
		
		private void FlashCards(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = FlashCardsView.GetInstance(this);
			backButton.Visibility = Visibility.Visible;
		}
        private void Choose(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
			contentControl.Content = ChooseView.GetInstance(this);
			backButton.Visibility = Visibility.Visible;
		}
        private void Fit(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = FitView.GetInstance(this);
			backButton.Visibility = Visibility.Visible;
		}
        private void Write(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = WriteView.GetInstance(this);
			backButton.Visibility = Visibility.Visible;
		}
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                contentControl.Content = null;
                MainGrid.Children.Clear();
                GenerateButtons();
            }

        }

        private void NewResource(object sender, RoutedEventArgs e)
        {
            Window window2 = new Window();
            window2.Show();
        }
        /*private void UpdateDatabase()
        {				
				string path = "E:\\Studia\\ztp\\Projekt\\Quizyy wpf\\danefiszki2.txt";
				string[] lines = File.ReadAllLines(path);
				foreach (var line in lines)
				{
					string[] elements = line.Split(';');
					if (elements.Length == 3)
					{
				
						string concept = elements[0];
						string definition = elements[1];
						string difficultylvl = elements[2];

						FlashCardsProxy flashCardsSaving = new FlashCardsProxy(concept, definition, difficultylvl);
                        flashCardsSaving.Save();
					}
				}
				
							
				string path2 = "E:\\Studia\\ztp\\Projekt\\Quizyy wpf\\danepytanie1.txt";
				string[] lines2 = File.ReadAllLines(path2);
				foreach (var line in lines2)
				{
					string[] elements = line.Split(';');
					if (elements.Length == 6)
					{						
						string question = elements[0];
						string answer = elements[1];
						string incorrectans1 = elements[2];
						string incorrectans2 = elements[3];
						string incorrectans3 = elements[4];
						string difficultylvl = elements[5];

                    QuestionProxy questionSaving = new QuestionProxy(question, answer, incorrectans1, incorrectans2, incorrectans3, difficultylvl);
                    questionSaving.Save();
					}
				}
					
			}*/
		}


}
