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
using Microsoft.Win32;
using System.Windows.Threading;

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

        //Tab I
        SqlConnection planerConn = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename= C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf; Integrated Security = True; Connect Timeout = 30");

        public Center()
        {
            InitializeComponent();

            //Tab I
            readGrid();

            //Tab III
            timeFoo();
        }

        public void readGrid()
        {
            try
            {
                SqlCommand readCmd = planerConn.CreateCommand();
                planerConn.Open();
                readCmd.CommandType = System.Data.CommandType.Text;
                readCmd.CommandText = "SELECT * from [PlanerTable]";
                readCmd.ExecuteNonQuery();
                planerConn.Close();

                SqlCommand cmdQuery = new SqlCommand(readCmd.CommandText, planerConn);
                SqlDataAdapter gridAdapter = new SqlDataAdapter(cmdQuery);
                DataSet gridDataSet = new DataSet();
                gridAdapter.Fill(gridDataSet);

                dataGrid.ItemsSource = gridDataSet.Tables[0].DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        //write to datagrid
        //write cmd + read dataGrid.Cells + write to table via sql command
        //view auslesen + datagrid cells prüfen (for loop) + werte in sql write-command integrieren und tabelle aktualisieren
        private void update_Click(object sender, RoutedEventArgs e)
        {
            readGrid();
            MessageBox.Show("Grid updated!");
        }
        public void writeToPlanerTable() //einbinden in button event methode
        {
            try
            {
                SqlCommand cmd = planerConn.CreateCommand();

                planerConn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "INSERT INTO [PlanerTable] (Uhrzeit, Tätigkeit, Beschreibung, Erledigt) VALUES ('" + uhrzeitBox.Text + "' , '" + tätigkeitBox.Text + "' , '" + beschreibungBox.Text + "' , '" + erledigtBox.Text + "')"; //wie lese ich hier korrekt die cells ein????
                cmd.ExecuteNonQuery();
                planerConn.Close();

                uhrzeitBox.Text = "";
                tätigkeitBox.Text = "";
                beschreibungBox.Text = "";
                erledigtBox.Text = "";
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }

        }
        private void dbInsert_Click(object sender, RoutedEventArgs e)
        {
            writeToPlanerTable();
            MessageBox.Show("Data saved to DB!");
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

        //TAB II
        public static string userEnv = Environment.UserName;
        public static string userDefaultPath = @$"C:\Users\{userEnv}\Documents\";

        private void savefileDirectoryDialog()
        {
            try
            {
                SaveFileDialog savefileDialog = new SaveFileDialog();
                savefileDialog.ShowDialog();
                File.WriteAllText(savefileDialog.FileName, readWriteTxtBox.Text);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void saveTextButton_Click(object sender, RoutedEventArgs e)
        {
            savefileDirectoryDialog();
            MessageBox.Show("Text sucessfully saved!");
        }

        private void cleanBox_Click(object sender, RoutedEventArgs e)
        {
            readWriteTxtBox.Text = "";
            MessageBox.Show("Textbox cleaned!");
        }

        private void fileDirectoryDialog()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                readWriteTxtBox.Text = File.ReadAllText(fileDialog.FileName);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void fileDialog_Click(object sender, RoutedEventArgs e)
        {
            fileDirectoryDialog();
            MessageBox.Show("File opened!");
        }

        //Tab III

        public void timeFoo()
        {
            DispatcherTimer clockTimer = new DispatcherTimer();
            clockTimer.Interval = TimeSpan.FromMilliseconds(1000);
            clockTimer.Tick += clockTimer_Tick;
            clockTimer.Start();
        }

        void clockTimer_Tick(object sender, EventArgs e)
        {
            timeLabel.Content = DateTime.Now.ToLongTimeString();
        }
    }
}
