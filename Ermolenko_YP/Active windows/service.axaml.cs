using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using Avalonia.Media;
using Avalonia.Controls;
using System.IO;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using System;
using System.Globalization;
using Avalonia.Platform;
using Ermolenko_YP;

namespace Ermolenko_YP
{

   public partial class service : Window
   {
    private List<Addition> _services;
    string fullTable = "SELECT * FROM services";
    string connStr = "server=localhost;database=autoservice;port=3306;User Id=root;password=12345";
    private MySqlConnection conn;

    public service()
    {
        InitializeComponent();
        ShowTable(fullTable);
        FillCmb();
    }

    public void ShowTable(string sql)
    {
        _services = new List<Addition>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var Serv = new Addition()
            {
                ID = reader.GetInt32("ID"),
                Name = reader.GetString("Name"),
                price = reader.GetInt32("price"),
                PhotoService = LoadImage("avares://Ermolenko_YP/Gallery/" + reader.GetString("PhotoService")),
                path_to_photo = reader.GetString("PhotoService"),
                discount = reader.GetInt32("discount"),
                discounted_price = reader.GetInt32("price") - (reader.GetInt32("price") * reader.GetInt32("discount") / 100)
            };
            _services.Add(Serv);
        }

        conn.Close();
        DataGrid.ItemsSource = _services;
        DataGrid.LoadingRow += DataGrid_LoadingRow;
    }
    public Bitmap LoadImage(string Uri)
    {
        return new Bitmap(AssetLoader.Open(new Uri(Uri)));
    }
    private void SearchService(object? sender, TextChangedEventArgs e)
    {
        var srv = _services;
        srv = srv.Where(x => x.Name.Contains(ServSearch.Text)).ToList();
        DataGrid.ItemsSource = srv;
    }

    private void DiscountFilter(object? sender, SelectionChangedEventArgs e)
    {
        var discontComboBox = (ComboBox)sender;
        var selectedDiscount = discontComboBox.SelectedItem as string;

        int startDiscount = 0;
        int endDiscount = 0;

        if (selectedDiscount == "Все скидки")
        {
            DataGrid.ItemsSource = _services;
        }
        else if (selectedDiscount == "От 0% до 5%")
        {
            startDiscount = 1;
            endDiscount = 5;
        }
        else if (selectedDiscount == "От 5% до 15%")
        {
            startDiscount = 5;
            endDiscount = 15;
        }
        else if (selectedDiscount == "От 15% до 30%")
        {
            startDiscount = 15;
            endDiscount = 30;
        }
        else if (selectedDiscount == "От 30% до 70%")
        {
            startDiscount = 30;
            endDiscount = 71;
        }

        if (startDiscount != 0 && endDiscount != 0)
        {
            var filteredUsers = _services
                .Where(x => x.discount >= startDiscount && x.discount < endDiscount)
                .ToList();

            DataGrid.ItemsSource = filteredUsers;
        }
    }
     public void FillCmb()
        {
            _services = new List<Addition>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand(fullTable, conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var Serv = new Addition()
                {
                    ID = reader.GetInt32("ID"),
                    Name = reader.GetString("Name"),
                    price = reader.GetInt32("price"),
                    PhotoService = LoadImage("avares://Ermolenko_YP/Gallery/" + reader.GetString("PhotoService")),
                    path_to_photo = reader.GetString("PhotoService"),
                    discount = reader.GetInt32("discount"),
                    discounted_price = reader.GetInt32("price") - (reader.GetInt32("price") * reader.GetInt32("discount") / 100)
                };
                _services.Add(Serv);
            }
            conn.Close();

            var discontComboBox = this.Find<ComboBox>("DiscontComboBox");
            discontComboBox.ItemsSource = new List<string>
            {
                "Все скидки",
                "От 0% до 5%",
                "От 5% до 15%",
                "От 15% до 30%",
                "От 30% до 70%"
            };
        }

        private void SortAscending(object sender, RoutedEventArgs e)
        {
            var sortedItems = DataGrid.ItemsSource.Cast<Addition>().OrderBy(s => s.price).ToList();
            DataGrid.ItemsSource = sortedItems;
        }

        private void SortDescending(object sender, RoutedEventArgs e)
        {
            var sortedItems = DataGrid.ItemsSource.Cast<Addition>().OrderByDescending(s => s.price).ToList();
            DataGrid.ItemsSource = sortedItems;
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            Addition _service = e.Row.DataContext as Addition;
            if (_service != null)
            {
                if (0 <= _service.discount && _service.discount < 5)
                {
                    e.Row.Background = Brushes.DarkGreen;
                }
                else if (5 <= _service.discount && _service.discount < 15)
                {
                    e.Row.Background = Brushes.Green;
                }
                else if (15 <= _service.discount && _service.discount < 30)
                {
                    e.Row.Background = Brushes.LimeGreen;
                }
                else if (30 <= _service.discount && _service.discount <= 70)
                {
                    e.Row.Background = Brushes.PaleGreen;
                }
                else
                {
                    e.Row.Background = Brushes.Transparent;
                }
            }
        }

        private void AddData(object? sender, RoutedEventArgs e)
        {
            Addition newServ = new Addition();
            CrudForm add = new CrudForm(newServ, _services);
            add.Title = "Добавление данных";
            add.Zag.Text = "Добавление данных";
            add.Show();
            this.Close();
        }

        private void Edit(object? sender, RoutedEventArgs e)
        {
            Addition currentServ = DataGrid.SelectedItem as Addition;
            if (currentServ == null)
                return;
            CrudForm edit = new CrudForm(currentServ, _services);
            edit.Title = "Редактирование данных";
            edit.Zag.Text = "Редактирование данных";
            edit.Show();
            this.Close();
        }

        public void Exit_Program(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LogOut(object? sender, RoutedEventArgs e)
        {
            MainWindow logout = new MainWindow();
            logout.Show();
            this.Close();
        }

        private void DeleteData(object? sender, RoutedEventArgs e)
        {
            try
            {
                Addition srv = DataGrid.SelectedItem as Addition;
                if (srv == null)
                {
                    return;
                }
                conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = "DELETE FROM services WHERE ID = " + srv.ID;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                _services.Remove(srv);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Sign_OnClick(object? sender, RoutedEventArgs e)
        {
                signups logout = new signups();
                logout.Show();
                this.Close();
        }
   }
}