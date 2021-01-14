using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for Center.xaml
    /// calendar w functionality
    /// datatable via db + xlsx export -> planer
    /// </summary>
    public partial class Center : Window
    {

        SqlConnection planerConn = new SqlConnection(@"C:\Users\Radagast\source\repos\.git\db\dbo.PlanerTable.sql");

        public Center()
        {
            InitializeComponent();
        }

        public void readGrid()
        {
            SqlCommand readCmd = planerConn.CreateCommand();
            readCmd.CommandType = System.Data.CommandType.Text;
            readCmd.CommandText = "SELECT * from [PlanerTable]";
            readCmd.ExecuteNonQuery();
            planerConn.Close();

            SqlCommand cmdQuery = new SqlCommand();   //hier weiterbasteln
        }

        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
