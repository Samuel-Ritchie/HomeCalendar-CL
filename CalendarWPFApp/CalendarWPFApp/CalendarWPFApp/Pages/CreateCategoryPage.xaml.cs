﻿using System;
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

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for CreateCategoryPage.xaml
    /// </summary>
    public partial class CreateCategoryPage : Page
    {
        public CreateCategoryPage()
        {
            InitializeComponent();
        }
        private void doneDescBtn(object sender, RoutedEventArgs e)
        {
            MyTextBox.IsEnabled = false;
        }
    }
}