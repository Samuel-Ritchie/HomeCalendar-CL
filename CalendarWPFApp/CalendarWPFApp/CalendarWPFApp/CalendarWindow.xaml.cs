using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CalendarWPFApp.Pages;
using PresenterInterfaceClasses;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window, IPromptCreationWindow
    {
        private Presenter _presenter;

        // To keep track of which screen the user is being faced with.
        public enum Interfaces
        {
            ChooseToCreate = 1,
            CreateEvent = 2,
            CreateCategory = 3,
            CalendarView = 4,
        }

        private bool _usingStandardTheme = true;
        private Interfaces _currentAppState;
        private bool _cancelClose = false;

        public CalendarWindow(Presenter presenter)
        {
            InitializeComponent();

            _presenter = presenter;
            _currentAppState = Interfaces.CalendarView;
            SwitchForms(Interfaces.CalendarView, null);
        }
        private void SetPageButtonColor(System.Windows.Controls.Button button, bool clicked)
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

        //==============================================
        //  Event Handlers
        //==============================================
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            _currentAppState = Interfaces.CalendarView;
            
            SwitchForms(Interfaces.CalendarView, null);
        }

        #region EVENT
        private void CreateEventButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.PromptCreateEvent(this);
        }
        public void ShowCreateEventForm()
        {
            _currentAppState = Interfaces.CreateEvent;

            PageBG.Source = new BitmapImage(new Uri("../assets/Event.jpg", UriKind.Relative));

            foreach (System.Windows.Controls.Button b in PageBar.Children)
            {
                if (b == CreateEventButton)
                    SetPageButtonColor(b, true);
                else
                    SetPageButtonColor(b, false);
            }
            // Set the content of the frame to the xaml of the create Event form.
            SwitchForms(Interfaces.CreateEvent, null);
        }
        #endregion

        #region CATEGORY
        private void CreateCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            _presenter.PromptCreateCategory(this);
        }
        public void ShowCreateCategoryForm()
        {
            _currentAppState = Interfaces.CreateCategory;

            PageBG.Source = new BitmapImage(new Uri("../assets/Category.jpg", UriKind.Relative));

            foreach (System.Windows.Controls.Button b in PageBar.Children)
            {
                if (b == CreateCategoryButton)
                    SetPageButtonColor(b, true);
                else
                    SetPageButtonColor(b, false);
            }
            // Set the content of the frame to the xaml of the create Event form.
            SwitchForms(Interfaces.CreateCategory, null);
        }
        #endregion

        private void Header_Grab(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            // Set cancel bool to false incase the user tries to close window a second time after canceling  previously.
            _cancelClose = false;

            // 
            if (_currentAppState == Interfaces.ChooseToCreate)
            {
                _presenter.PromptCreateWindowClosing(this);
            }
            else if (_currentAppState == Interfaces.CreateEvent)
            {
                _presenter.PromptCreateWindowClosing(this);
            }
            else if (_currentAppState == Interfaces.CreateCategory)
            {
                _presenter.PromptCreateWindowClosing(this);
            }

            // No changes made, close app.

            // Check for cancel variable.
            if (!_cancelClose)
            {
                // Closes opperation.
                System.Windows.Application.Current.Shutdown();
            }
        }
        public void AskToSaveOrDiscardPromptCreate(bool changesMade)
        {
            // Get whether or not user want's to save changes.
            MessageBoxResult userChoice = System.Windows.MessageBox.Show("You are about to quit and your changes may not be saved.", "SAVE ME!", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (userChoice == MessageBoxResult.OK)
            {
                _presenter.SaveChangesAndClose();
            }
            else if (userChoice == MessageBoxResult.Cancel)
            {
                // Set cancel bool to true. (Cancels close event handler.)
                _cancelClose = true;
            }
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

        //===========================================================================
        //  Unreachable Methods (Presenter can't use) (Event and Category can use.)
        //===========================================================================
        public void SwitchForms(Interfaces switchTo, CreateEventPage? existingEventForm)
        {
            if (switchTo == Interfaces.ChooseToCreate)
            {
                main.Content = null;
            }
            else if (switchTo == Interfaces.CalendarView && existingEventForm is null)
            {
                // Set the content of the frame to the calendar view.

                main.Content = new HomePage();
                PageBG.Source = new BitmapImage(new Uri("../assets/Home.jpg", UriKind.Relative));

                foreach (System.Windows.Controls.Button b in PageBar.Children)
                {
                    if (b == HomeButton)
                        SetPageButtonColor(b, true);
                    else
                        SetPageButtonColor(b, false);
                }
            }
            else if (switchTo == Interfaces.CreateEvent)
            {
                main.Content = new CreateEventPage(_presenter, this);
            }
            else if (switchTo == Interfaces.CreateCategory && existingEventForm is not null)
            {
                // Set the content of the frame to the xaml of the create Category form.
                main.Content = new CreateCategoryPage(_presenter, this, existingEventForm);
            }
            else if (switchTo == Interfaces.CreateCategory && existingEventForm is null)
            {
                // Set the content of the frame to the xaml of the create Category form.
                main.Content = new CreateCategoryPage(_presenter, this, null);
            }
           
        }
    }
}
