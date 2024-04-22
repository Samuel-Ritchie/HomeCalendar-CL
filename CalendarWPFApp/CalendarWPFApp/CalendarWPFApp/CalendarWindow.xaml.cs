using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CalendarWPFApp.Pages;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        private bool _usingStandardTheme = true;
        public CalendarWindow()
        {
            InitializeComponent();
            main.Content = new HomePage();
        }

        //==============================================
        //  Event Handlers
        //==============================================
        private void SetPageButtonColor(Button button, bool clicked)
        {
            SolidColorBrush UNPRESSED_BG = new BrushConverter().ConvertFrom("#555555") as SolidColorBrush;
            SolidColorBrush UNPRESSED_FG = new BrushConverter().ConvertFrom("#BBBBBB") as SolidColorBrush;
            SolidColorBrush PRESSED_BG = new BrushConverter().ConvertFrom("#e88833") as SolidColorBrush;
            SolidColorBrush PRESSED_FG = new BrushConverter().ConvertFrom("#FFFFFF") as SolidColorBrush;

            Border border = button.Content as Border;
            border.Background = (clicked) ? PRESSED_BG : UNPRESSED_BG;

            TextBlock textBlock = border.Child as TextBlock;
            textBlock.Foreground = (clicked) ? PRESSED_FG : UNPRESSED_FG;

        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new HomePage();
            PageBG.Source = new BitmapImage(new Uri("../assets/Home.jpg", UriKind.Relative));

            foreach (Button b in PageBar.Children)
            {
                if (b == HomeButton)
                    SetPageButtonColor(b, true);
                else
                    SetPageButtonColor(b, false);
            }
        }
        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new CreateEventPage();
            PageBG.Source = new BitmapImage(new Uri("../assets/Event.jpg", UriKind.Relative));

            foreach (Button b in PageBar.Children)
            {
                if (b == CreateEventButton)
                    SetPageButtonColor(b, true);
                else
                    SetPageButtonColor(b, false);
            }
        }
        private void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            main.Content = new CreateCategoryPage();
            PageBG.Source = new BitmapImage(new Uri("../assets/Category.jpg", UriKind.Relative));

            foreach (Button b in PageBar.Children)
            {
                if (b == CreateCategoryButton)
                    SetPageButtonColor(b, true);
                else
                    SetPageButtonColor(b, false);
            }
        }
        private void Header_Grab(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Swap_Theme_Click(object sender, RoutedEventArgs e)
        {
            _usingStandardTheme = (_usingStandardTheme) ? false : true;

            SolidColorBrush BG_VISIBLE = new BrushConverter().ConvertFrom("#66e88833") as SolidColorBrush;
            SolidColorBrush BG_HIDDEN = new BrushConverter().ConvertFrom("#FFFFFFFF") as SolidColorBrush;

            PageBG.Visibility = (_usingStandardTheme) ? Visibility.Visible : Visibility.Hidden;
            mainBorder.Background = (_usingStandardTheme) ? BG_VISIBLE : BG_HIDDEN;
        }

        //==============================================
        //  Interface Methods
        //==============================================
        public void ChangeDisplayInfo(string databaseName)
        {
            //DatabaseNameDisplay.Text = databaseName;
        }
    }
}
