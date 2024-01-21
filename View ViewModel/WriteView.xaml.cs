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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Quizyy_wpf.Model;
using Quizyy_wpf.Observer;
using Quizyy_wpf.Proxy;

namespace Quizyy_wpf.View
{
    /// <summary>
    /// Logika interakcji dla klasy WriteView.xaml
    /// </summary>
    public partial class WriteView : UserControl
    {
        private MainWindow mainWindow1;
        private StackPanel? stackPanel;
        private Button? previousButton;
        private Button? nextButton;
        private Button? contextButton;
        private TextBlock? DisplayTextBlock;
		private TextBlock? DifficultyLvlTextBlock;
        private TextBlock? RecordDisplayer;
        private TextBlock? CurrentSessionDisplayer;
        private TextBlock? CurrentResult;
        private TextBlock? Record;
        private TextBox? TextBox;
        private List<WriteModel> items = new List<WriteModel>();
        private int index = 0;
        private static WriteView instance;
        public PointsManager pointsManager = new PointsManager();

        public static WriteView GetInstance(MainWindow mainView)
		{
			if (instance == null)
			{
				instance = new WriteView(mainView);
			}
			return instance;
		}
		private WriteView(MainWindow mainView)
        {
            mainWindow1 = mainView;
			DatabaseConnectionProxy proxy2 = mainWindow1.GetProxy();
			items = proxy2.GetWriteList();
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

            previousButton = new Button
            {
                Content = "Poprzednie",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            previousButton.Click += PreviousButtonClick;

            nextButton = new Button
            {
                Content = "Następne",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            nextButton.Click += NextButtonClick;
            contextButton = new Button
            {
                Content = "Sprawdź poprawność odpowiedzi",
                Margin = new Thickness(0, 150, 0, 0),
                Width = 250,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            contextButton.Click += ContextButtonClick;

            stackPanel.Children.Add(previousButton);

            stackPanel.Children.Add(contextButton);

            stackPanel.Children.Add(nextButton);

            DisplayTextBlock = new TextBlock
            {

                Margin = new Thickness(0, 10, 0, 100),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            CurrentResult = new TextBlock
            {

                Margin = new Thickness(190, 380, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            Record = new TextBlock
            {

                Margin = new Thickness(-80, 380, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };
            TextBox = new TextBox
            {
                Margin = new Thickness(0, 20, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Width = 200
            };

            RecordDisplayer = new TextBlock
            {
                Margin = new Thickness(-170, 380, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };

            CurrentSessionDisplayer = new TextBlock
            {
                Margin = new Thickness(100, 380, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
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

			MainGrid.Children.Add(DifficultyLvlTextBlock);
            MainGrid.Children.Add(RecordDisplayer);
            MainGrid.Children.Add(CurrentSessionDisplayer);

            MainGrid.Children.Add(stackPanel);
            MainGrid.Children.Add(DisplayTextBlock);
            MainGrid.Children.Add(CurrentResult);
            MainGrid.Children.Add(Record);
            MainGrid.Children.Add(TextBox);
            DisplayQuestion();
            ObserverDisplayer();
        }
        private void PreviousButtonClick(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                DisplayQuestion();
            }
            else if (index == 0)
            {
                index = items.Count - 1;
                DisplayQuestion();
            }
        }

        private void NextButtonClick(object sender, RoutedEventArgs e)
        {
            if (index < items.Count - 1)
            {
                index++;
                DisplayQuestion();
            }
            else if (index >= items.Count - 1)
            {
                index = 0;
                DisplayQuestion();
            }
        }

        private void ContextButtonClick(object sender, RoutedEventArgs e)
        {
            string ans = TextBox.Text.ToLower();
            bool correctness = ans.Equals(items[index].answer.ToLower());
            if (correctness)
            {
                pointsManager.IncreasePoints();
                MessageBox.Show("Odpowiedź prawidłowa");
                index++;
                if (index >= items.Count - 1) index = 0;
                TextBox.Text = "";
                DisplayQuestion();
                DisplayCurrentResult();
                DisplayRecord();
            }
            else
            {
                pointsManager.ResetPoints();
                DisplayCurrentResult();
                TextBox.Text = "";
                MessageBox.Show("Odpowiedź błędna");
            }
        }
        private void DisplayQuestion()
        {
            DisplayTextBlock.Text = items[index].question;
            DifficultyLvlTextBlock.Text = "";
			DifficultyLvlTextBlock.Text = "Poziom trudności: " + items[index].difficultylvl;
		}
        private void ObserverDisplayer()
        {
            RecordDisplayer.Text = "Rekord:";
            CurrentSessionDisplayer.Text = "Wynik:";
        }
        private void DisplayCurrentResult()
        {
            CurrentResult.Text = pointsManager.Show(1).ToString();
        }
        private void DisplayRecord()
        {
            Record.Text = pointsManager.Show(0).ToString();
        }
    }
}
