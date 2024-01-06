using Quizyy_wpf.View;
using System.IO;
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
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Escape)
            {
                contentControl.Content = null;
                MainGrid.Children.Clear();
                GenerateButtons();
            }

        }

        private void UpdateDatabase()
        {
            string path = "E:\\Studia\\kck\\danefiszki2.txt";
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] elements = line.Split(';');
                if (elements.Length == 3)
                {

                    int id = Convert.ToInt32(elements[0]);
                    string concept = elements[1];
                    string definition = elements[2];
                    string level = elements[2];

                    var flashCardsSaving = new FlashCardsProxy(concept, definition, level);
                    flashCardsSaving.Save();
                }
            }

            /*
            string path = "E:\\Studia\\kck\\danepytanie2.txt";
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                string[] elements = line.Split(';');
                if (elements.Length == 6)
                {

                    int id = Convert.ToInt32(elements[0]);
                    string question = elements[1];
                    string answer = elements[2];
                    string incorrectans1 = elements[3];
                    string incorrectans2 = elements[4];
                    string incorrectans3 = elements[5];


                    var neww = new WriteModel
                    {
                        id = id,
                        question = question,
                        answer = answer,
                        incorrectans1=incorrectans1,
                        incorrectans2=incorrectans2,
                        incorrectans3=incorrectans3
                    };
                    context.Writes.Add(neww);
                }
            }
            context.SaveChanges();
            */
            context.Dispose();
        }
    }


}
}