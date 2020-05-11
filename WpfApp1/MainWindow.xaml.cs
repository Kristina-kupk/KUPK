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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;


namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btVhod_Click(object sender, RoutedEventArgs e)
        {
            try
               
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))
                {
                    DataClasses1DataContext dc = new DataClasses1DataContext();
                    var userLogin = (from u in dc.Users
                                     where u.log_user == tbLog.Text
                                     select u).ToArray();
                    var userPass = (from u in dc.Users
                                    where u.pas_user == tbPass.Text
                                    select u).ToArray();
                    if (tbLog.Text == userLogin[0].log_user)
                    {
                        if (tbPass.Text == userPass[0].pas_user)
                        {
                            Window1 w1 = new Window1();
                            w1.Show();
                            this.Hide();
                        }
                    }
                }
            }
           
            catch
            {
                MessageBox.Show("Введите корректные данные");
            }
        }

        private void btReg_Click(object sender, RoutedEventArgs e)
        {
            Window2 w2 = new Window2();
            w2.Show();
            this.Hide();
        }
    }
}
