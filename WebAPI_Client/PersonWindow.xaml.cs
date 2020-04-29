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
using WebAPI_Client.DataProviders;
using WebAPI_Client.Models;

namespace WebAPI_Client
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    
    public partial class Window1 : Window
    {
        private readonly Person _person;
        public Window1(Person person)
        {
            InitializeComponent();
            if (person != null)
            {
                _person = person;
                FirstNameTextBox.Text = _person.firstName;
                LastNameTextBox.Text = _person.lastName;
                DateTextBox.SelectedDate = _person.dateOfBirth;
                CreateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                _person = new Person();
                UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
            
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                _person.firstName = FirstNameTextBox.Text;
                _person.lastName = LastNameTextBox.Text;
                _person.dateOfBirth = DateTextBox.SelectedDate.Value;

                PersonDataProvider.UpdatePerson(_person);
                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you really my man?","Question",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                PersonDataProvider.DeletePerson(_person.id);
                DialogResult = true;
                Close();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                _person.firstName = FirstNameTextBox.Text;
                _person.lastName = LastNameTextBox.Text;
                _person.dateOfBirth = DateTextBox.SelectedDate.Value;

                PersonDataProvider.CreatePerson(_person);
                DialogResult = true;
                Close();
            }
        }

        private bool ValidatePerson()
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text)) {
                MessageBox.Show("First name should not be empty");
                return false;
            }
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Last name should not be empty");
                return false;
            }
            if (!DateTextBox.SelectedDate.HasValue)
            {
                MessageBox.Show("Date should not be empty");
                return false;
            }
            return true;
        }
    }
}
