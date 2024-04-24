using PresenterInterfaceClasses;
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
using System.Windows.Shapes;

namespace CalendarWPFApp
{
    /// <summary>
    /// Interaction logic for LocationPicker.xaml
    /// </summary>
    public partial class LocationPicker : Window, ILocationPicker
    {
        private Presenter _presenter;
        private IMainView _mainView;

        private string _currentPath;

        public LocationPicker(IMainView mainView, Presenter presenter, string currentPath)
        {
            InitializeComponent();

            _currentPath = currentPath;

            CurrentLocation.Text = _currentPath;

            _presenter = presenter;

            _mainView = mainView;
        }

        public void ShowErrorLocationPicker(string message)
        {
            // Get whether or not user want's to save changes.
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            _presenter.ProcessLocation(_mainView, this, _currentPath);
            this.Close();
        }

        private void Okay_Click(object sender, RoutedEventArgs e)
        {
            _currentPath = NewLocationBox.Text.ToString();
            CurrentLocation.Text = _currentPath;
            _presenter.ProcessLocation(_mainView, this, _currentPath);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_presenter.ProcessLocation(_mainView, this, _currentPath);
        }
    }

}
