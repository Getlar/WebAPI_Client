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
            var window = new Window1(null);
            if (window.ShowDialog() ?? false)
            {
                UpdatePeople();
            }
        }

        private void todayPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = todayPeople.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                todayPeople.UnselectAll();
            }
        }
        private void futurePeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = futurePeople.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                futurePeople.UnselectAll();
            }
        }
        private void diagnosedPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = diagnosedPeople.SelectedItem as Person;
            if (selectedPerson != null)
            {
                var window = new Window1(selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
                diagnosedPeople.UnselectAll();
            }
        }

        private void UpdatePeople()
        {
            people = PersonDataProvider.GetPeople();
            DateTime ActualTime = DateTime.Now;
            dateText.Content = "Today's date is: " + ActualTime.ToShortDateString().ToString();
            List<Person> SortedList = people.OrderBy(o => o.DateOfArrival).ToList();
            List<Person> TodaysList = new List<Person>();
            List<Person> FutureList = new List<Person>();
            List<Person> DiagnosedList = new List<Person>();
            foreach(Person p in SortedList)
            {
                if (p.Diagnosis == null)
                {
                    if (p.DateOfArrival.ToShortDateString() == ActualTime.ToShortDateString())
                    {
                        TodaysList.Add(p);
                    }
                    else
                    {
                        FutureList.Add(p);
                    }
                }
                else
                {
                    DiagnosedList.Add(p);
                }
            }
            todayPeople.ItemsSource = TodaysList;
            futurePeople.ItemsSource = FutureList;
            diagnosedPeople.ItemsSource = DiagnosedList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UpdatePeople();
        }
    }
}
