using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quizyy_wpf.Command;
using Quizyy_wpf.Model;
using Quizyy_wpf.Proxy;
using Quizyy_wpf.State;

namespace Quizyy_wpf.View
{
    /// <summary>
    /// Logika interakcji dla klasy FitView.xaml
    /// </summary>
    public partial class FitView : UserControl
    {
        private MainWindow mainWindow1;
        private StackPanel? stackPanel1;
        private StackPanel? stackPanel2;
		public TextBlock? DisplayTextBlock1;
		public TextBlock? DisplayTextBlock2;
		public TextBlock? DisplayTextBlock3;
		public TextBlock? DifficultyLvlTextBlock;
		public Button? undoButton;
        public Button? redoButton;
        private static FitView instance;

		public string? concept;
		public string? definition;
		public int? conceptid;
		public int? definitionid;
		public Button? chosen1;
		public Button? chosen2;
		public int resoult = 0;
		public ConnectionHistory undoHistory = new ConnectionHistory();
		public ConnectionHistory redoHistory = new ConnectionHistory();

		private State1 state = null;


        private List<FlashCardsModel> items = new List<FlashCardsModel>();


		public static FitView GetInstance(MainWindow mainView)
        {
            if (instance == null)
            {
                instance = new FitView(mainView);
            }
            return instance;
        }
        private FitView(MainWindow mainView)
        {
            TransitionTo(new NoneChoosenState());
            mainWindow1 = mainView;
			DatabaseConnectionProxy proxy2 = mainWindow1.GetProxy();
			items = proxy2.GetFlashCardsList();
			InitializeComponent();
            OpenMode();
        }
        public void OpenMode()
        {
            NewSet();
        }
        
        private int GetRandom()
        {
            List<FlashCardsModel> lista = items;
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
            List<FlashCardsModel> list = items;
            int first = GetRandom();
            Range xy = new Range(first, first + 6);
            List<int> drawn = GetNumbers(xy);
            stackPanel1 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 110, 0, 0)
            };
            stackPanel2 = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Margin = new Thickness(0, 110, 0, 0)
            };
            foreach (int i in drawn)
            {
                Button leftButtons = new Button
                {
                    Content = list[i].concept,
                    Tag = list[i].id,
                    Margin = new Thickness(10, 5, 300, 5),
                    Width = 250,
                    Height = 30,
                    Style = (Style)FindResource("CustomButtonStyle")
                };
                leftButtons.Click += ChooseButtonClick;

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
                    Margin = new Thickness(300, 5, 0, 5),
                    Width = 250,
                    Height = 30,
                    Style = (Style)FindResource("CustomButtonStyle")
                };
                rightButtons.Click += ChooseButtonClick;

                stackPanel2.Children.Add(rightButtons);
            }
            MainGrid.Children.Add(stackPanel1);
            MainGrid.Children.Add(stackPanel2);
            DisplayTextBlock1 = new TextBlock
            {
                Margin = new Thickness(10, 400, 300, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            DisplayTextBlock2 = new TextBlock
            {
                Margin = new Thickness(300, 400, 0, 5),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            DisplayTextBlock3 = new TextBlock
            {
                Margin = new Thickness(0, 450, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
		    undoButton = new Button
			{
				Content = "Cofnij",
				Margin = new Thickness(0, 0, 800, 200),
				Width = 100,
				Height = 30,
				Style = (Style)FindResource("CustomButtonStyle"),
                IsEnabled= false
			};
			undoButton.Click += UndoButtonClick;
			redoButton = new Button
			{
				Content = "Przywróć",
				Margin = new Thickness(800, 5, 0, 200),
				Width = 100,
				Height = 30,
				Style = (Style)FindResource("CustomButtonStyle"),
				IsEnabled = false
			};
			redoButton.Click += RedoButtonClick;
			DifficultyLvlTextBlock = new TextBlock
			{
				Margin = new Thickness(700, 500, 0, 0),
				HorizontalAlignment = HorizontalAlignment.Center,
				VerticalAlignment = VerticalAlignment.Center,
				Height = 30,
				Style = (Style)FindResource("CustomTextStyle")
			};			
			DifficultyLvlTextBlock.Text = "Poziom trudności: " + items[drawn[0]].difficultylvl;
			MainGrid.Children.Add(DifficultyLvlTextBlock);

			MainGrid.Children.Add(undoButton);
            MainGrid.Children.Add(redoButton);
			MainGrid.Children.Add(DisplayTextBlock1);
            MainGrid.Children.Add(DisplayTextBlock2);
            MainGrid.Children.Add(DisplayTextBlock3);
        }
        public void TransitionTo(State1 state)
        {
            this.state= state;
            this.state.SetContext(this);
            
        }

        private void ChooseButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton)
            {
                state.ChooseOption(clickedButton);
                state.ShowChosen();
                state.ShowResult();
				
			}
        }
        
        private void UndoButtonClick(object sender, RoutedEventArgs e)
        {
            FitCommand var1 = undoHistory.Pop();
            if (undoHistory.IsEmpty())
            {
                undoButton.IsEnabled = false;
            }
            resoult--;
            var1.undo();
            redoHistory.Push(var1);
            redoButton.IsEnabled= true;
        }
		private void RedoButtonClick(object sender, RoutedEventArgs e)
		{
			FitCommand var1 = redoHistory.Pop();
			if (redoHistory.IsEmpty())
			{
				redoButton.IsEnabled = false;
			}
            resoult++;
			var1.redo();
			undoHistory.Push(var1);
			undoButton.IsEnabled = true;
		}
	}
}
