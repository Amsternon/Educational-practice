using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Windows;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using System;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;

namespace Ermolenko_YP;

public class Addition
{
    public int ID { get; set; }
    public string Name { get; set; }
    public Bitmap? PhotoService { get; set; }
    public string path_to_photo { get; set; }
    public double price { get; set; }
    public int discount { get; set; }
    public double discounted_price { get; set; }
}

public class Signup
{
    public int ID_zap { get; set; }
    public int client_id { get; set; }
    public int Master { get; set; }
    public int ID { get; set; }
    public string Date_of_recording { get; set; }
}