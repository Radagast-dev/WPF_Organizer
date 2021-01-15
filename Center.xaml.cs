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
using System.Data;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for Center.xaml
    /// calendar w functionality
    /// datatable via db + xlsx export -> planer
    /// datagrid vid: https://www.youtube.com/watch?v=dOZYOnFb56Q
    /// texttab http://openbook.rheinwerk-verlag.de/einstieg_vb_2012/1959_06_003.html
    /// </summary>
    public partial class Center : Window
    {

        SqlConnection planerConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename= C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf; Integrated Security = True; Connect Timeout = 30");

        public Center()
        {
            InitializeComponent();
            readGrid();
        }

        public void readGrid()
        {
            SqlCommand readCmd = planerConn.CreateCommand();
            planerConn.Open();
            readCmd.CommandType = System.Data.CommandType.Text;
            readCmd.CommandText = "SELECT * from [PlanerTable]";
            readCmd.ExecuteNonQuery();
            planerConn.Close();

            SqlCommand cmdQuery = new SqlCommand(readCmd.CommandText, planerConn);   //hier weiterbasteln
            SqlDataAdapter gridAdapter = new SqlDataAdapter(cmdQuery);
            DataSet gridDataSet = new DataSet();
            gridAdapter.Fill(gridDataSet);

            dataGrid.ItemsSource = gridDataSet.Tables[0].DefaultView;
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
