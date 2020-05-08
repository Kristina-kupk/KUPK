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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Ателье
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DB connect = new DB();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btVhod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (connect.GetConnect())
                {
                    connect.ConnectOpen();
                    SqlCommand command = new SqlCommand("select * from [Пользователи] where [Пользователи].[Login]='"
                        + tbLog.Text + "'and [Пользователи].[Password]='" + tbPass.Text + "'", connect.GetConnect());
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string rol = reader.GetValue(4).ToString();
                            string fio = reader.GetValue(1).ToString();
                            MessageBox.Show("Добро пожаловать! " + rol + " " + fio);
                           
                        }
                    }
                    else
                        MessageBox.Show("Такого пользователя нет");
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
        }
    }
}
