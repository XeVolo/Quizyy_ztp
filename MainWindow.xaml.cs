using Quizyy_wpf.Controller;
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

		private void GenerateButtons()
		{
			string[] buttonLabels = { "Fiszki", "Dopasowanie pojęć", "Podanie odpowiedzi", "Wybór odpowiedzi" };
			FlashCardsController flashCardsController = new FlashCardsController(this);
			FitController matchingController = new FitController(this);
			WriteController answeringController = new WriteController(this);
			ChooseController choiceController = new ChooseController(this);
			EasterEggController secretobject = new EasterEggController(this);

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
					Margin = new Thickness(30),
					Width = 140,
					Height = 30
				};

				switch (i)
				{
					case 0:
						button.Click += (sender, e) => flashCardsController.OpenMode();
						break;
					case 1:
						button.Click += (sender, e) => matchingController.OpenMode();
						break;
					case 2:
						button.Click += (sender, e) => answeringController.OpenMode();
						break;
					case 3:
						button.Click += (sender, e) => choiceController.OpenMode();
						break;
				}

				 buttonPanel.Children.Add(button);
			}
			MainGrid.Children.Add(buttonPanel);
		}


	}
}