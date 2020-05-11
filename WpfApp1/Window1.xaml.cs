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
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))//берем из App.config
            {
                Table<Uslugi> uslugis = db.GetTable<Uslugi>();// получить табл для запроса
                dgUsl.ItemsSource = uslugis;// источник для автамат. создания столбцов
                Table<Klients> klients = db.GetTable<Klients>();
                dgKlient.ItemsSource = klients;
                Table<Zakaz> zakazs = db.GetTable<Zakaz>();
                //сравнение двух элементов двух последовательностей
                var query = from a in zakazs
                            join b in klients on a.id_klzak equals b.id_kl
                            join c in uslugis on a.id_uslzak equals c.id_uslugi
                            select new { a.id_zakaz, a.date_zak, a.status_zak, b.id_kl, c.name_uslugi, c.cena_uslugi };// созд.таб для заказов 
                dgZakaz.ItemsSource = query;

                DataClasses3DataContext dc = new DataClasses3DataContext();// запол.коббох
                var usl = (from a in dc.Uslugi
                           select a.name_uslugi);
                cbNazUcl.ItemsSource = usl;
                cbZapUsl.ItemsSource = usl;
                DataClasses2DataContext dc1 = new DataClasses2DataContext();
                var kl = (from a in dc1.Klients
                          select a.fio_kl);
                cbFIOKl.ItemsSource = kl;
                cbZapKl.ItemsSource = kl;
            }
        }
        private void btnNewUsl_Click(object sender, RoutedEventArgs e) // NewYslug
        {
            tbNazUsl.IsEnabled = true;
            tbPrice.IsEnabled = true;
            btnNewUslSave.IsEnabled = true;
            btnCansel.IsEnabled = true;
        }

        private void btnNewUslSave_Click(object sender, RoutedEventArgs e) //Добавление услг
        {
            DataClasses3DataContext db = new DataClasses3DataContext();// подкл.услуг.таб
            string name_usl = tbNazUsl.Text;
            int price = Convert.ToInt32(tbPrice.Text);

            Uslugi uslugi = new Uslugi();//созд.перем.таб
            uslugi.name_uslugi = name_usl;//заполнение.
            uslugi.cena_uslugi = price;
            uslugi.status_uslugi = true;
            db.GetTable<Uslugi>().InsertOnSubmit(uslugi);
            db.SubmitChanges();//сохр изм
            Update();//отобрж.

            tbNazUsl.Text = "";//вызв.столб.зад.параметр
            tbPrice.Text = "";
            tbNazUsl.IsEnabled = false;
            tbPrice.IsEnabled = false;
            btnNewUslSave.IsEnabled = false;
            btnCansel.IsEnabled = false;
        }

        private void btnRedUsl_Click(object sender, RoutedEventArgs e) // редакт. услуг
        {
            tbNazUsl.IsEnabled = true;
            tbPrice.IsEnabled = true; //  октив.кнопки

            btnNewUslSave.Visibility = Visibility.Hidden;//есть кнопка нет кнопки. спрятался 1, 2 появился 
            btnRedUslSave.Visibility = Visibility.Visible;

            btnRedUslSave.IsEnabled = true;
            DataClasses3DataContext db = new DataClasses3DataContext();//подключение
            object item = dgUsl.SelectedItem;//перемент.строчку запоминает(1)
            long vb = Convert.ToInt64((dgUsl.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);//вытаскивает номер из 1
            tbNazUsl.Text = (from u in db.Uslugi
                             where u.id_uslugi == vb
                             select u.name_uslugi).FirstOrDefault();//поиск и вывод
            tbPrice.Text = Convert.ToString((from u in db.Uslugi// цена
                             where u.id_uslugi == vb
                             select u.cena_uslugi).FirstOrDefault());
        }

        private void btnRedUslSave_Click(object sender, RoutedEventArgs e) //редактируем 
        {
            object item = dgUsl.SelectedItem;
            long vb = Convert.ToInt64((dgUsl.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            DataClasses3DataContext db = new DataClasses3DataContext();
            Uslugi usl = db.Uslugi.FirstOrDefault(uslg => uslg.id_uslugi.Equals(vb));
            usl.name_uslugi = tbNazUsl.Text;
            usl.cena_uslugi = Convert.ToInt32(tbPrice.Text);
            usl.status_uslugi = true;
            var SelectQuery = 
                from a in db.GetTable<Uslugi>()
                select a;
            db.SubmitChanges();
            dgUsl.ItemsSource = SelectQuery;
            MessageBox.Show("Данные изменены");

            tbNazUsl.IsEnabled = false;
            tbPrice.IsEnabled = false;
            btnCansel.IsEnabled = false;
            tbNazUsl.Text = "";
            tbPrice.Text = "";
            btnNewUslSave.Visibility = Visibility.Visible;//есть кнопка нет кнопки. спрятался 1, 2 появился
            btnRedUslSave.Visibility = Visibility.Hidden;
        }

        private void btnCansel_Click(object sender, RoutedEventArgs e)
        {
            tbNazUsl.Text = "";
            tbPrice.Text = "";
            tbFIONew.Text = "";
            tbNomNew.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e) // услуг удаление
        {
            DataClasses3DataContext db = new DataClasses3DataContext();//подключение
            var usl = (from u in db.Uslugi
                       where u.status_uslugi == true
                       select u);//ищет в таблице true  и удаляет 
            dgUsl.ItemsSource = usl;
        }

        private void btnNewKl_Click(object sender, RoutedEventArgs e)//Новый клиент
        {
            tbFIONew.IsEnabled = true;
            tbNomNew.IsEnabled = true;
            btnCanKl.IsEnabled = true;
            btnNewUslSave.Visibility = Visibility.Visible;//есть кнопка нет кнопки. спрятался 1, 2 появился
            btnSaveRedKl.Visibility = Visibility.Hidden;
        }

        private void tbNomNew_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;//проверка на числа
        }

        private void tbNomNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbNomNew.Text.Length <= 2)
            {
                tbNomNew.Text = "+7";
                tbNomNew.SelectionStart = tbNomNew.Text.Length;
            }
            if ((tbFIONew.Text.Length != 0) && (tbNomNew.Text.Length == 12))//нероб.кноп.текст.бох.пуст
            {
                btnSaveNewKl.IsEnabled = true;
                btnSaveRedKl.IsEnabled = true;
            }
        }

        private void btnSaveNewKl_Click(object sender, RoutedEventArgs e)//ок
        {
            DataClasses2DataContext db = new DataClasses2DataContext();//под.
            Klients klients = new Klients();
            klients.fio_kl = tbFIONew.Text;
            klients.tel_kl = tbNomNew.Text;
            klients.statys_kl = true;//дою нов кли
            db.GetTable<Klients>().InsertOnSubmit(klients);
            db.SubmitChanges();
            Update();

            tbFIONew.Text = "";
            tbNomNew.Text = "";
            tbFIONew.IsEnabled = false;
            tbNomNew.IsEnabled = false;
            btnCanKl.IsEnabled = false;// дел.пуст.текстбох и выкл кноп.
        }

        private void btnCanKl_Click(object sender, RoutedEventArgs e)
        {
            tbFIONew.Text = "";
            tbNomNew.Text = "";
        }

        private void tbNazUsl_TextChanged(object sender, TextChangedEventArgs e) /*название услуг*/
        {
            if ((tbNazUsl.Text.Length != 0) && (tbPrice.Text.Length != 0))
            {
                btnNewUslSave.IsEnabled = true;
                btnRedUslSave.IsEnabled = true;
            }
        }

        private void btnRedKl_Click(object sender, RoutedEventArgs e)
        {
            tbFIONew.IsEnabled = true;
            tbNomNew.IsEnabled = true;
            btnCanKl.IsEnabled = true;
            //есть кнопка нет кнопки. спрятался 1, 2 появился
            btnSaveNewKl.Visibility = Visibility.Hidden;
            btnSaveRedKl.Visibility = Visibility.Visible;
            //подключение
            DataClasses2DataContext db = new DataClasses2DataContext();
            object item = dgKlient.SelectedItem;
            long vb = Convert.ToInt64((dgKlient.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            tbFIONew.Text = (from u in db.Klients
                             where u.id_kl == vb
                             select u.fio_kl).FirstOrDefault();//ищет в таблице
            tbNomNew.Text = Convert.ToString((from u in db.Klients
                                             where u.id_kl == vb
                                             select u.tel_kl).FirstOrDefault());//дальше ищит
        }

        private void btnSaveRedKl_Click(object sender, RoutedEventArgs e)
        {
            object item = dgKlient.SelectedItem;
            long vb = Convert.ToInt64((dgKlient.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            DataClasses2DataContext db = new DataClasses2DataContext();
            Klients kl = db.Klients.FirstOrDefault(uslg => uslg.id_kl.Equals(vb));
            kl.fio_kl = tbFIONew.Text;
            kl.tel_kl = tbNomNew.Text;
            kl.statys_kl = true;
            var SelectQuery =
                from a in db.GetTable<Klients>()
                select a;
            db.SubmitChanges();
            dgKlient.ItemsSource = SelectQuery;
            MessageBox.Show("Данные изменены");

            tbFIONew.IsEnabled = false;
            tbNomNew.IsEnabled = false;
            btnCansel.IsEnabled = false;
            tbFIONew.Text = "";
            tbNomNew.Text = "";
            btnSaveNewKl.Visibility = Visibility.Visible;
            btnSaveRedKl.Visibility = Visibility.Hidden;
        }

        private void btnDelKl_Click(object sender, RoutedEventArgs e)
        {
            DataClasses2DataContext db = new DataClasses2DataContext();
            var kl = (from u in db.Klients
                       where u.statys_kl == true
                       select u);
            dgKlient.ItemsSource = kl;
        }

        private void tbFIONew_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((tbFIONew.Text.Length != 0) && (tbNomNew.Text.Length == 12))
            {
                btnSaveNewKl.IsEnabled = true;
                btnSaveRedKl.IsEnabled = true;
            }
        }

        private void dgZakaz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgZakaz.SelectedItem;
            if (item != null) 
            {
                long ID = Convert.ToInt64((dgZakaz.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))
                {
                    Table<Klients> klients = db.GetTable<Klients>();
                    Table<Zakaz> zakazs = db.GetTable<Zakaz>();
                    var infoKl = from a in klients
                                 join b in zakazs on a.id_kl equals b.id_klzak
                                 where b.id_zakaz == ID
                                 select new { a.fio_kl, a.tel_kl, a.id_kl };
                    dgInfoKl.ItemsSource = infoKl;
                }
            }
        }

        private void btnNewZak_Click(object sender, RoutedEventArgs e)
        {
            cbNazUcl.IsEnabled = true;
            cbFIOKl.IsEnabled = true;
            dpDate.IsEnabled = true;
            btnSaveNewZak.IsEnabled = true;
        }

        private void btnVipZak_Click(object sender, RoutedEventArgs e)//выполнить
        {
            DataClasses4DataContext db = new DataClasses4DataContext();
            object item = dgZakaz.SelectedItem;
            long vb = Convert.ToInt64((dgZakaz.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
            Zakaz zakaz = db.GetTable<Zakaz>().
                FirstOrDefault(uslg => uslg.id_zakaz.Equals(vb));
            zakaz.status_zak = true;
            db.SubmitChanges();
            Update();

        }

        private void btnSaveNewZak_Click(object sender, RoutedEventArgs e)
        {
            string newUsl = Convert.ToString(cbNazUcl.SelectedItem);
            DataClasses3DataContext dc = new DataClasses3DataContext();

            var usl = (from a in dc.Uslugi
                       where a.name_uslugi == newUsl
                       select a).ToArray();// ищет в мс

            string newKl = Convert.ToString(cbFIOKl.SelectedItem);
            DataClasses2DataContext dc1 = new DataClasses2DataContext();
            var kl = (from a in dc1.Klients
                      where a.fio_kl == newKl
                      select a).ToArray();
            DataClasses4DataContext dc2 = new DataClasses4DataContext();
            Zakaz zakaz = new Zakaz();

            zakaz.id_uslzak = usl[0].id_uslugi;//вытаскивает из масс
            zakaz.id_klzak = kl[0].id_kl;
            zakaz.status_zak = false;
            zakaz.date_zak = Convert.ToDateTime(dpDate.Text);
            dc2.GetTable<Zakaz>().InsertOnSubmit(zakaz);
            dc2.SubmitChanges();
            Update();
        }

        private void btnOKDate_Click(object sender, RoutedEventArgs e)//п
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))
            {
                string date = dpZapDate.Text;
                Table<Uslugi> uslugis = db.GetTable<Uslugi>();
                Table<Klients> klients = db.GetTable<Klients>();
                Table<Zakaz> zakazs = db.GetTable<Zakaz>();
                var query = from a in zakazs
                            join b in klients on a.id_klzak equals b.id_kl
                            join c in uslugis on a.id_uslzak equals c.id_uslugi
                            where a.date_zak == Convert.ToDateTime(date)
                            select new { a.id_zakaz, a.date_zak, a.status_zak, b.id_kl, c.name_uslugi, c.cena_uslugi };
                dgZap.ItemsSource = query;
            }
        }

        private void btnOKUsl_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))
            {
                string date = Convert.ToString(cbZapUsl.SelectedItem);
                DataClasses3DataContext dc = new DataClasses3DataContext();
                var usl = (from a in dc.Uslugi
                           where a.name_uslugi == date
                           select a).ToArray();
                Table<Uslugi> uslugis = db.GetTable<Uslugi>();
                Table<Klients> klients = db.GetTable<Klients>();
                Table<Zakaz> zakazs = db.GetTable<Zakaz>();
                var query = from a in zakazs
                            join b in klients on a.id_klzak equals b.id_kl
                            join c in uslugis on a.id_uslzak equals c.id_uslugi
                            where a.id_uslzak == usl[0].id_uslugi
                select new { a.id_zakaz, a.date_zak, a.status_zak, b.id_kl, c.name_uslugi, c.cena_uslugi };
                dgZap.ItemsSource = query;
            }
        }

        private void btnOKKl_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new DataContext(Properties.Settings.Default.AteleConnectionString1))
            {
                string date = Convert.ToString(cbZapKl.SelectedItem);
                DataClasses2DataContext dc1 = new DataClasses2DataContext();
                var kl = (from a in dc1.Klients
                          where a.fio_kl == date
                          select a).ToArray();
                Table<Uslugi> uslugis = db.GetTable<Uslugi>();
                Table<Klients> klients = db.GetTable<Klients>();
                Table<Zakaz> zakazs = db.GetTable<Zakaz>();
                var query = from a in zakazs
                            join b in klients on a.id_klzak equals b.id_kl
                            join c in uslugis on a.id_uslzak equals c.id_uslugi
                            where a.id_klzak == kl[0].id_kl
                            select new { a.id_zakaz, a.date_zak, a.status_zak, b.id_kl, c.name_uslugi, c.cena_uslugi };
                dgZap.ItemsSource = query;
            }
        }

        private void BtnVipUsl_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsl.SelectedItem!=null)// шобб ошибка не вылетала 
            {
                //редктирование, убирает галки
                DataClasses3DataContext db = new DataClasses3DataContext();//подключение
                object item = dgUsl.SelectedItem;
                long vb = Convert.ToInt64((dgUsl.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);// конвектирует
                Uslugi usl = db.GetTable<Uslugi>().FirstOrDefault(uslg => uslg.id_uslugi.Equals(vb));
                usl.status_uslugi = false;
                db.SubmitChanges();
                Update();
            }
           
        }

       
    }
}
