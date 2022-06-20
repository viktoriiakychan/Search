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
        int page = 1; // 2 - another page
        IQueryable<User> query;
        List<User> result;
        int skip;
        int pageSize = 10;
        ObservableCollection<User> collection = null;
        int pages;
        ChooseFieldsToSearchWindow choose = new ChooseFieldsToSearchWindow();
        private void btnSeach_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(AuthUser.UserEmail);
            
            choose.ShowDialog();
            //collection.Clear();
            string name = choose.Name;
            MyDataContext context = new MyDataContext();
            query = context.Users.AsQueryable();
            string surname = choose.Surname;
            string email = choose.Email;
            string phone = choose.Phone;
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
            else
            {
                query = context.Users.AsQueryable();
            }
            //page 1


           
            skip = (page - 1) * pageSize;
            int count = query.Count();
            pages = (int)Math.Ceiling((double)count / (double)pageSize);
            MessageBox.Show("Pages =>" + pages);
            query = query.Skip(skip).Take(10);
            result = query.ToList();
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

        private void forward_Click(object sender, RoutedEventArgs e)
        {
            if (page < pages)
            {
                page += 1;
                collection.Clear();
                string name = choose.Name;
                MyDataContext context = new MyDataContext();
                query = context.Users.AsQueryable();
                string surname = choose.Surname;
                string email = choose.Email;
                string phone = choose.Phone;
                if (!string.IsNullOrWhiteSpace(name))
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
                else
                {
                    query = context.Users.AsQueryable();
                }
                skip = (page - 1) * pageSize;
                //collection.Clear();
                query = query.Skip(skip).Take(10);
                result = query.ToList();
                //collection = new ObservableCollection<User>();
                if (collection == null)
                {
                    collection = new ObservableCollection<User>();
                    dgUsers.ItemsSource = collection;
                }
                List<string> imageList = new List<string>();
                if (result.Count == 0)
                {
                    MessageBox.Show("0 person founded");
                }
                for (int i = 0; i < result.Count; i++)
                {
                    collection.Add(new User() { Id = i + 1, Name = result[i].Name, Email = result[i].Email, Image = result[i].Image, Surname = result[i].Surname, Password = result[i].Password, Phone = result[i].Phone });
                }
            }
            else if(page-1 == pages)
            {
                forward.IsEnabled = false;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (page > 1)
            {
                page -= 1;


                collection.Clear();
                string name = choose.Name;
                MyDataContext context = new MyDataContext();
                query = context.Users.AsQueryable();
                string surname = choose.Surname;
                string email = choose.Email;
                string phone = choose.Phone;
                if (!string.IsNullOrWhiteSpace(name))
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
                else
                {
                    query = context.Users.AsQueryable();
                }
                skip = (page - 1) * pageSize;
                query = query.Skip(skip).Take(10);
                result = query.ToList();
                if (collection == null)
                {
                    collection = new ObservableCollection<User>();
                    dgUsers.ItemsSource = collection;
                }
                List<string> imageList = new List<string>();
                if (result.Count == 0)
                {
                    MessageBox.Show("0 person founded");
                }
                for (int i = 0; i < result.Count; i++)
                {
                    collection.Add(new User() { Id = i + 1, Name = result[i].Name, Email = result[i].Email, Image = result[i].Image, Surname = result[i].Surname, Password = result[i].Password, Phone = result[i].Phone });
                }
            }
        }
    }
}
