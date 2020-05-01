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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if (person.Diagnosis != null)
            {
                DiagnosisTextBox.IsReadOnly = true;
            }
            if (person != null)
            {
                _person = person;
                FirstNameTextBox.Text = _person.FirstName;
                LastNameTextBox.Text = _person.LastName;
                DateTextBox.Text = _person.DateOfBirth.ToShortDateString();
                AddressTextBox.Text = _person.Address;
                SocialSecurityNumberTextBox.Text = _person.SocialSecurityNumber;
                ComplaintTextBox.Text = _person.Complaint;
                DiagnosisTextBox.Text = _person.Diagnosis;
            }
        }


        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                _person.Diagnosis = DiagnosisTextBox.Text;
                PersonDataProvider.UpdatePerson(_person);
                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Do you really want to delete the selected patient?","Question",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                PersonDataProvider.DeletePerson(_person.Id);
                DialogResult = true;
                Close();
            }
        }

        private bool ValidatePerson()
        {
            if (string.IsNullOrEmpty(DiagnosisTextBox.Text)) {
                MessageBox.Show("Please add diagnosis!");
                return false;
            }
            return true;
        }
    }
}
