using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;

namespace Ermolenko_YP;

public partial class Registration : Window
{
    public Registration()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=autoservice;port=3306;User Id=root;password=12345";
    
    private void Regist(object? sender, RoutedEventArgs e)
    {
        conn = new MySqlConnection(connStr);
        conn.Open();
        string regist = "INSERT INTO Client(Surname, Name, Lastname, phone, GenderCode, login, password, RegistrationDate) VALUES ('" + Surname.Text + "', '" + Name.Text + "', '" + lastname.Text + "', '" + Telephone.Text + "', " + Convert.ToInt32(Gender.Text) + ", '" + Log.Text + "', '" + Password.Text + "', '" + DateReg.Text + "')";
        MySqlCommand cmd = new MySqlCommand(regist, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        MainWindow auth = new MainWindow();
        this.Close();
        auth.Show();
    }
}