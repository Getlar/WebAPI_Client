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
using WebAPI_Client.DataProviders;
using WebAPI_Client.Models;

namespace WebAPI_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Person> people;

        public MainWindow(string Text)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            nameText.Content = "Welcome Back " + Text + "!";
            UpdatePeople();
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window1(null, false);
            if (window.ShowDialog() ?? false)
            {
                UpdatePeople();
            }
        }

        private void todayPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = PatientTodayDataGrid.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson, false);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                PatientTodayDataGrid.UnselectAll();
            }
        }
        private void futurePeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = PatientLaterDataGrid.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson, true);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                PatientLaterDataGrid.UnselectAll();
            }
        }
        private void diagnosedPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = PatientDiagnosedDataGrid.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson, false);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                PatientDiagnosedDataGrid.UnselectAll();
            }
        }

        private void UpdatePeople()
        {
            people = PersonDataProvider.GetPeople();
            DateTime ActualTime = DateTime.Now;
            dateText.Content = "Today's date is: " + ActualTime.ToShortDateString().ToString();
            List<Person> SortedList = people.OrderBy(o => o.DateOfArrival).ToList();
            PatientDiagnosedDataGrid.Items.Clear();
            PatientLaterDataGrid.Items.Clear();
            PatientTodayDataGrid.Items.Clear();
            foreach(Person p in SortedList)
            {
                if (p.Diagnosis == null)
                {
                    if (p.DateOfArrival.ToShortDateString() == ActualTime.ToShortDateString())
                    {
                        PatientTodayDataGrid.Items.Add(p);
                    }
                    else
                    {
                        PatientLaterDataGrid.Items.Add(p);
                    }
                }
                else
                {
                    PatientDiagnosedDataGrid.Items.Add(p);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdatePeople();
        }
    }
}
