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

namespace WpfApptEST
{
    /// <summary>
    /// Interaction logic for ChooseFieldsToSearchWindow.xaml
    /// </summary>
    public partial class ChooseFieldsToSearchWindow : Window
    {
        public ChooseFieldsToSearchWindow()
        {
            InitializeComponent();
        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Name = name.Text;
            Surname = surname.Text;
            Email = email.Text;
            Phone = phone.Text;
            this.Close();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            name.IsEnabled = true;
            phone.IsEnabled = false;
            surname.IsEnabled = false;
            email.IsEnabled = false;

        }
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            surname.IsEnabled = true;
            name.IsEnabled = false;
            email.IsEnabled = false;
            phone.IsEnabled = false;

        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            email.IsEnabled = true;
            phone.IsEnabled = false;
            surname.IsEnabled = false;
            name.IsEnabled = false;
        }

        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            phone.IsEnabled = true;
            email.IsEnabled = false;
            surname.IsEnabled = false;
            name.IsEnabled = false;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            phone.IsEnabled = false;
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            email.IsEnabled = false;
        }

        private void CheckBox_Unchecked_2(object sender, RoutedEventArgs e)
        {
            surname.IsEnabled = false;
        }

        private void CheckBox_Unchecked_3(object sender, RoutedEventArgs e)
        {
            name.IsEnabled = false;
        }
    }
}
