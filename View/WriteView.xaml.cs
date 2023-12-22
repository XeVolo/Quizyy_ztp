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
using Quizyy_wpf.Controller;
using Quizyy_wpf.Model;

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
        private TextBox? TextBox;
        private List<WriteModel> items = BaseController.GetWriteList();
        private int index = 0;
        public WriteView(MainWindow mainView)
        {
            mainWindow1 = mainView;
            InitializeComponent();
            OpenMode();
        }
        public void OpenMode()
        {
            mainWindow1.backButton.Visibility = Visibility.Visible;
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
                Height = 30
            };
            TextBox = new TextBox
            {
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Width = 200
            };

            Grid.SetRow(stackPanel, 0);
            Grid.SetRow(DisplayTextBlock, 1);

            MainGrid.Children.Add(stackPanel);
            MainGrid.Children.Add(DisplayTextBlock);
            MainGrid.Children.Add(TextBox);
            DisplayQuestion();
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

                MessageBox.Show("Odpowiedź prawidłowa");
                index++;
                if (index >= items.Count - 1) index = 0;
                TextBox.Text = "";
                DisplayQuestion();
            }
            else
            {
                TextBox.Text = "";
                MessageBox.Show("Odpowiedź błędna");
            }
        }
        private void DisplayQuestion()
        {
            DisplayTextBlock.Text = items[index].question;
        }
    }
}
