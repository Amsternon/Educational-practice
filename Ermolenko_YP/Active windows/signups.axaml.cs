using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.Windows;

namespace Ermolenko_YP;

public partial class signups : Window
{
    private List<Signup> _signups;

    public signups()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    string connStr = "server=localhost;database=autoservice;port=3306;User Id=root;password=12345";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string add = "INSERT INTO sign_up (client_id, Master, serviceID, Date_of_recording) VALUES ('" + Convert.ToInt32(client_id.Text) + "', '" + Convert.ToInt32(Master.Text) + "', '" + Convert.ToInt32(serviceID.Text) + "','" + Date_of_recording.Text + "');";
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void GoBack(object? sender, RoutedEventArgs e)
    {
        service back = new service();
        this.Close();
        back.Show();
    }
}