using Quizyy_wpf.Proxy;
using Quizyy_wpf.View;
using Quizyy_wpf.View_ViewModel;
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
				VerticalAlignment = VerticalAlignment.Center,
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
            button2.Click += Update;
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
        private void Update(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = UpdateView.GetInstance(this);
            backButton.Visibility = Visibility.Visible;
        }
    }
}