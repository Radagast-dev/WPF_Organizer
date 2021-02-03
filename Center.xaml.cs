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
using System.IO;
using System.Security.AccessControl;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for Center.xaml
    /// calendar w functionality
    /// datatable via db + xlsx export -> planer
    /// datagrid vid: https://www.youtube.com/watch?v=dOZYOnFb56Q
    /// https://stackoverflow.com/questions/50279736/how-to-get-all-header-text-from-datagrid-in-wpf-c-sharp-using-code-behind
    /// texttab http://openbook.rheinwerk-verlag.de/einstieg_vb_2012/1959_06_003.html
    /// https://www.youtube.com/watch?v=9mUuJIKq40M
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

        //write to datagrid
        //write cmd + read dataGrid.Cells + write to table via sql command
        //view auslesen + datagrid cells prüfen (for loop) + werte in sql write-command integrieren und tabelle aktualisieren

        public void writeToPlanerTable() //einbinden in button event methode
        {
            SqlCommand cmd = planerConn.CreateCommand();

            planerConn.Open();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "INSERT INTO [PlanerTable] VALUES ('" + dataGrid.Items + "')";    //wie lese ich hier korrekt die cells ein????
            cmd.ExecuteNonQuery();
            planerConn.Close();

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

        public static string fileName = "Testtest.txt";
        public static string userEnv = Environment.UserName;
        public static string userDefaultPath = @$"C:\Users\{userEnv}\Documents\{fileName}";
        private void saveTextButton_Click(object sender, RoutedEventArgs e)
        {
            //Textbox auslesen
            string textBoxText = readWriteTxtBox.Text;
            //Textbox Inhalt in File schreiben
            File.WriteAllText(userDefaultPath, textBoxText);
            MessageBox.Show("Text sucessfully saved!");
        }

        private void loadTextButton_Click(object sender, RoutedEventArgs e)
        {
            readWriteTxtBox.Text = File.ReadAllText(userDefaultPath);
            MessageBox.Show("Txt File sucessfully loaded!");
        }
    }
}
