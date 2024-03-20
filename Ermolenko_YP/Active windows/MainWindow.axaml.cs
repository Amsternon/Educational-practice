using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Ermolenko_YP;

public partial class MainWindow : Window
{
     public MainWindow()
    {
        InitializeComponent();
    }
    
    string connectionString = "server=localhost;database=autoservice;port=3306;User Id=root;password=12345";
    
    public void Authorization(object? sender, RoutedEventArgs e)
    {
        string username = Login.Text;
        string password = Password.Text;
        bool isClient = IsUserValidClient(username, password);
        bool isEmployee = IsUserValidEmployee(username, password);
        if (isClient || isEmployee)
        {
            if (isClient)
            {
                service client = new service();
                client.AddButton.IsVisible = false;
                client.EditButton.IsVisible = false;
                client.DeleteButton.IsVisible = false;
                Hide();
                client.Show();
            }
            else
            {
                service employee = new service();
                employee.AddButton.IsVisible = true;
                employee.EditButton.IsVisible = true;
                employee.DeleteButton.IsVisible = true;
                Hide();
                employee.Show();
            }
        }
        else
        {
            Console.WriteLine("Неверный логин или пароль");
        }
    }
    
    private bool IsUserValidEmployee(string username, string password)
        {
            bool isValid = false;
        
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM employee WHERE email = @Username AND password = @Password";
        
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
        
                    connection.Open();
        
                    object result = command.ExecuteScalar();
                    int count = Convert.ToInt32(result);
        
                    if (count == 1)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
    private bool IsUserValidClient(string username, string password) //проверка пользователей по БД
    {
        bool isValid = false;
    
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            string query = "SELECT COUNT(1) FROM client WHERE login = @Username AND password= @Password";
    
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
    
                connection.Open();
    
                object result = command.ExecuteScalar();
                int count = Convert.ToInt32(result);
    
                if (count == 1)
                {
                    isValid = true;
                }
            }
        }
        return isValid;
    }
    private void Registration(object? sender, RoutedEventArgs e)
    {
        Registration reg = new Registration();
        Hide();
        reg.Show();
    }
    public void Exit_Program(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}