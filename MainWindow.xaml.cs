using Quizyy_wpf.Controller;
using Quizyy_wpf.View;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Quizyy_wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			GenerateButtons();
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
            contentControl.Content = new FlashCardsView(this); 
        }
        private void Choose(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = new ChooseView(this);
        }
        private void Fit(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = new FitView(this);
        }
        private void Write(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            contentControl.Content = new WriteView(this);
        }
        

    }
}