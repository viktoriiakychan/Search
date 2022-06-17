using Bogus;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApptEST.Models;

namespace WpfApptEST
{
    /// <summary>
    /// Interaction logic for ListUsersWindow1.xaml
    /// </summary>
    public partial class ListUsersWindow1 : Window
    {
        private ObservableCollection<UserVM> users = new ObservableCollection<UserVM>();
        public ListUsersWindow1()
        {
            InitializeComponent();
            //users.Add(new UserVM()
            //{
            //    Id = 1,
            //    Name = "Sashko",
            //    ImageUrl = "https://image.cnbcfm.com/api/v1/image/105992231-1561667465295gettyimages-521697453.jpeg?v=1561667497&w=1600&h=900"
            //});
            //users.Add(new UserVM()
            //{
            //    Id = 2,
            //    Name = "Petya",
            //    ImageUrl = "https://image.cnbcfm.com/api/v1/image/105992231-1561667465295gettyimages-521697453.jpeg?v=1561667497&w=1600&h=900"
            //});
            //dgUsers.ItemsSource = users;
        }
        
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //RegisterWindow register = new RegisterWindow();
            //register.ShowDialog();

            users.Add(new UserVM
            {
                Id = 3,
                Name = "Jorik",
                ImageUrl = "https://image.cnbcfm.com/api/v1/image/105992231-1561667465295gettyimages-521697453.jpeg?v=1561667497&w=1600&h=900"
            });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if(dgUsers.SelectedItem!= null)
            {
                if (dgUsers.SelectedItem is UserVM)
                {


                    users.Add(new UserVM
                    {
                        Id = 4,
                        Name = "Катюха",
                        ImageUrl = "https://image.cnbcfm.com/api/v1/image/105992231-1561667465295gettyimages-521697453.jpeg?v=1561667497&w=1600&h=900"
                    });
                }
            }
        }

        private void btnRandowUser_Click(object sender, RoutedEventArgs e)
        {
            var faker = new Faker<User>("uk")
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName())
                 .RuleFor(u => u.Surname, (f, u) => f.Name.LastName())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password())
                .RuleFor(u => u.Image, f => f.Image.PicsumUrl());
            MyDataContext context = new MyDataContext();
            for (int i = 0; i < 1000; i++)
            {

                var user = faker.Generate();
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        ObservableCollection<User> collection = null;
        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
           //MessageBox.Show(AuthUser.UserEmail);
            ChooseFieldsToSearchWindow choose = new ChooseFieldsToSearchWindow();
            choose.ShowDialog();
            string name = choose.Name;
            string surname = choose.Surname;
            string email = choose.Email;
            string phone = choose.Phone;
            MyDataContext context = new MyDataContext();
            var query = context.Users.AsQueryable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            else if (!string.IsNullOrWhiteSpace(surname))
            {
                query = query.Where(x => x.Surname.Contains(surname));
            }
            else if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(x => x.Email.Contains(email));
            }
            else if (!string.IsNullOrWhiteSpace(phone))
            {
                query = query.Where(x => x.Phone.Contains(phone));
            }
            //else
            //{
            //    query = context.Users.AsQueryable();
            //}
            var result = query.ToList();
            //collection.Clear();
            if (collection == null)
            {
                collection = new ObservableCollection<User>();
                dgUsers.ItemsSource = collection;
            }
            List<string> imageList = new List<string>();
            if(result.Count == 0)
            {
                MessageBox.Show("0 person founded");
            }
            for (int i = 0; i < result.Count; i++)
            {
                collection.Add(new User() { Id = i + 1, Name =  result[i].Name, Email = result[i].Email, Image = result[i].Image, Surname = result[i].Surname, Password = result[i].Password, Phone = result[i].Phone});
            }
           
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            collection.Clear();
        }
    }
}
