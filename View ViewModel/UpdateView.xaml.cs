using Microsoft.Win32;
using Quizyy_wpf.Model;
using Quizyy_wpf.Observer;
using Quizyy_wpf.Proxy;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Quizyy_wpf.View_ViewModel
{
    /// <summary>
    /// Interaction logic for UpdateView.xaml
    /// </summary>
    public partial class UpdateView : UserControl
    {
        public UpdateView()
        {
            InitializeComponent();
        }
        private MainWindow mainWindow1;
        private StackPanel? stackPanel;
        private Button? updateFlashCardsButton;
        private Button? updateWritesButton;
        private TextBlock? DisplayTextBlock;
        private TextBox? TextBox;
        private static UpdateView instance;

        public static UpdateView GetInstance(MainWindow mainView)
        {
            if (instance == null)
            {
                instance = new UpdateView(mainView);
            }
            return instance;
        }
        private UpdateView(MainWindow mainView)
        {
            mainWindow1 = mainView;            
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

            updateFlashCardsButton = new Button
            {
                Content = "Dodaj nowe fiszki",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            updateFlashCardsButton.Click += UpdateFlashCardsButtonClick;

            updateWritesButton = new Button
            {
                Content = "Dodaj nowe pytania",
                Margin = new Thickness(100),
                Width = 180,
                Height = 30,
                Style = (Style)FindResource("CustomButtonStyle")
            };
            updateWritesButton.Click += UpdateWritesButtonClick;

            stackPanel.Children.Add(updateWritesButton);

            stackPanel.Children.Add(updateFlashCardsButton);

            DisplayTextBlock = new TextBlock
            {

                Margin = new Thickness(0, 10, 0, 100),
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 30,
                Style = (Style)FindResource("CustomTextStyle")
            };


            MainGrid.Children.Add(stackPanel);
            MainGrid.Children.Add(DisplayTextBlock);
        }

        private void UpdateFlashCardsButtonClick(object sender, RoutedEventArgs e)
        {
            Window update = new Window
            {
                Title = "Dodaj nowe fiszki",
                Width = 450,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.5));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
            update.Background = gradientBrush;

            TextBlock textBlock = new TextBlock
            {
                Text = "Z jakiego pliku chcesz dodać fiszki?",
                FontSize = 20,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Style = (Style)FindResource("CustomTextStyle")
            };

            Button selectFileButton = new Button
            {
                Content = "Wybierz plik",
                Width = 100,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Style = (Style)FindResource("CustomButtonStyle")
            };

            TextBlock selectedFileTextBlock = new TextBlock
            {
                Text = "Wybrany plik: ",
                Visibility = Visibility.Hidden, 
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("CustomTextStyle")
            };

            Button okButton = new Button
            {
                Content = "Zatwierdź",
                Width = 80,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("CustomButtonStyle")
            };

            Grid grid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);

            Grid.SetRow(textBlock, 0);
            Grid.SetColumnSpan(textBlock, 2);

            Grid.SetRow(selectFileButton, 1);
            Grid.SetColumn(selectFileButton, 0);
            Grid.SetColumnSpan(selectFileButton, 2);

            Grid.SetRow(selectedFileTextBlock, 2);
            Grid.SetColumn(selectedFileTextBlock, 0);
            Grid.SetColumnSpan(selectedFileTextBlock, 2);

            Grid.SetRow(okButton, 3);
            Grid.SetColumn(okButton, 0);
            Grid.SetColumnSpan(okButton, 2);

            grid.Children.Add(textBlock);
            grid.Children.Add(selectFileButton);
            grid.Children.Add(selectedFileTextBlock);
            grid.Children.Add(okButton);
            update.Content = grid;

            string selectedFilePath = null;

            selectFileButton.Click += (sender, e) =>
            {
                OpenFileDialog filePath = new OpenFileDialog();
                filePath.Filter = "Text Files (*.txt)|*.txt";
                filePath.Multiselect = false;

                var success = filePath.ShowDialog();
                if (success == true)
                {
                    selectedFilePath = filePath.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedFilePath);
                    selectedFileTextBlock.Text = "Wybrany plik: " + fileName;
                    selectedFileTextBlock.Visibility = Visibility.Visible;
                }
            };

            okButton.Click += (sender, e) =>
            {
                if (selectedFilePath != null)
                {
                    SaveFlashCards(selectedFilePath);
                    MessageBox.Show("Pomyślnie dodano nowe fiszki");
                }           
                update.Close();
            };

            update.Show();
        }
        private void UpdateWritesButtonClick(object sender, RoutedEventArgs e)
        {
            Window update = new Window
            {
                Title = "Dodaj nowe pytania",
                Width = 450,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            LinearGradientBrush gradientBrush = new LinearGradientBrush();
            gradientBrush.StartPoint = new Point(0, 0);
            gradientBrush.EndPoint = new Point(1, 1);
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 0.0));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.5));
            gradientBrush.GradientStops.Add(new GradientStop(Colors.Red, 1.0));
            update.Background = gradientBrush;

            TextBlock textBlock = new TextBlock
            {
                Text = "Z jakiego pliku chcesz dodać pytania?",
                FontSize = 20,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Style = (Style)FindResource("CustomTextStyle")
            };

            Button selectFileButton = new Button
            {
                Content = "Wybierz plik",
                Width = 100,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Style = (Style)FindResource("CustomButtonStyle")
            };

            TextBlock selectedFileTextBlock = new TextBlock
            {
                Text = "Wybrany plik: ",
                Visibility = Visibility.Hidden,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("CustomTextStyle")
            };

            Button okButton = new Button
            {
                Content = "Zatwierdź",
                Width = 80,
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Style = (Style)FindResource("CustomButtonStyle")
            };

            Grid grid = new Grid();

            ColumnDefinition column1 = new ColumnDefinition();
            ColumnDefinition column2 = new ColumnDefinition();
            grid.ColumnDefinitions.Add(column1);
            grid.ColumnDefinitions.Add(column2);

            RowDefinition row1 = new RowDefinition();
            RowDefinition row2 = new RowDefinition();
            RowDefinition row3 = new RowDefinition();
            RowDefinition row4 = new RowDefinition();
            grid.RowDefinitions.Add(row1);
            grid.RowDefinitions.Add(row2);
            grid.RowDefinitions.Add(row3);
            grid.RowDefinitions.Add(row4);

            Grid.SetRow(textBlock, 0);
            Grid.SetColumnSpan(textBlock, 2);

            Grid.SetRow(selectFileButton, 1);
            Grid.SetColumn(selectFileButton, 0);
            Grid.SetColumnSpan(selectFileButton, 2);

            Grid.SetRow(selectedFileTextBlock, 2);
            Grid.SetColumn(selectedFileTextBlock, 0);
            Grid.SetColumnSpan(selectedFileTextBlock, 2);

            Grid.SetRow(okButton, 3);
            Grid.SetColumn(okButton, 0);
            Grid.SetColumnSpan(okButton, 2);

            grid.Children.Add(textBlock);
            grid.Children.Add(selectFileButton);
            grid.Children.Add(selectedFileTextBlock);
            grid.Children.Add(okButton);
            update.Content = grid;

            string selectedFilePath = null;

            selectFileButton.Click += (sender, e) =>
            {
                OpenFileDialog filePath = new OpenFileDialog();
                filePath.Filter = "Text Files (*.txt)|*.txt";
                filePath.Multiselect = false;

                var success = filePath.ShowDialog();
                if (success == true)
                {
                    selectedFilePath = filePath.FileName;
                    string fileName = System.IO.Path.GetFileName(selectedFilePath);
                    selectedFileTextBlock.Text = "Wybrany plik: " + fileName;
                    selectedFileTextBlock.Visibility = Visibility.Visible;
                }
            };

            okButton.Click += (sender, e) =>
            {
                if (selectedFilePath != null)
                {
                    SaveWrites(selectedFilePath);
                    MessageBox.Show("Pomyślnie dodano nowe pytania");
                }
                update.Close();
            };

            update.Show();
        }
        private void SaveFlashCards(string filePath)
        {
            DatabaseConnectionProxy proxy2 = mainWindow1.GetProxy();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                string[] elements = line.Split(';');
                if (elements.Length == 3)
                {
                    string concept = elements[0];
                    string definition = elements[1];
                    string difficultylvl = elements[2];
                   
                    proxy2.SaveFlashCards(concept, definition, difficultylvl);
                }
            }
        }
        private void SaveWrites(string filePath)
        {
            DatabaseConnectionProxy proxy2 = mainWindow1.GetProxy();
            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
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
                   
                    proxy2.SaveWriteList(question, answer, incorrectans1, incorrectans2, incorrectans3, difficultylvl);
                }
            }
        }
    }
}
