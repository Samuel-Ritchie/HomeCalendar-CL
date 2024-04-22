using System.Windows.Controls;

namespace CalendarWPFApp.Pages
{
    /// <summary>
    /// Interaction logic for CreateEventPage.xaml
    /// </summary>
    public partial class CreateEventPage : Page
    {
        public CreateEventPage()
        {
            InitializeComponent();

            Hours.ItemsSource = GetIntRange(1, 12);
            Minutes.ItemsSource = GetIntRange(0, 59);
        }

        private List<string> GetIntRange(int start, int end)
        {
            List<string> items = new List<string>();

            for (int i = start; i <= end; i++)
                items.Add(i.ToString());

            return items;
        }
    }
}
