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
using System.Windows.Navigation;
using System.Data;
using System.Data.SqlClient;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for CredentialsWindow.xaml
    /// </summary>
    public partial class CredentialsWindow : Window
    {
        public CredentialsWindow()
        {
            InitializeComponent();
        }
        
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf;Integrated Security=True;Connect Timeout=30");
        
        public void connectionState()
        {
            if (ConnectionState.Open.Equals(true))
            {
                conn.Close();
            }
            else
            {
                if (ConnectionState.Closed.Equals(true))
                {
                    conn.Open();
                }
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

           
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * from [PasswordTable] WHERE Name = ('" + nametextBox.Text + "') AND Password = ('" + pwtextBox.Text + "') ", conn); 
            DataTable loginData = new DataTable();

            dataAdapter.Fill(loginData);

            if (loginData.Rows.Count == 1) //Geht nicht in schleife rein (?)
            {
                MessageBox.Show("Login sucessful!");

                this.Hide();
                Center centerWindow = new Center();
                centerWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Wrong Password!");
            }

            nametextBox.Text = "";
            pwtextBox.Text = "";

            connectionState();
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

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {

            //SqlConnection regConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf;Integrated Security=True;Connect Timeout=30");
            
            SqlDataAdapter regReadAdapter = new SqlDataAdapter("SELECT * from [RegisterTable] WHERE RegisterNumber = ('" + regtextBox.Text +"')", conn); //analyse!
            DataTable regReadTable = new DataTable();

            try
            {
                regReadAdapter.Fill(regReadTable);
                
                if (regReadTable.Rows.Count == 1)
                {
                    MessageBox.Show("Correct code number given!");
                }
                else
                {
                    if(regtextBox.Text.Equals(regtextBox.Text = ""))
                    {
                        MessageBox.Show("No register number given");
                    }
                    MessageBox.Show("Incorrect number!");
                }
            }
            catch(SqlException sqlException)
            {
                sqlException.Message.Contains("Conversion failed when converting the varchar value 'reg' to data type int.");
                MessageBox.Show("Conversion failed when converting the varchar value 'reg' to data type int. Number needed, no text");
                regtextBox.Text = "";
                return;
            }

            connectionState();

            //-- reg write area --//
            SqlDataAdapter regWriteAdapter = new SqlDataAdapter("INSERT INTO [PasswordTable] (Name,Password) VALUES('" + nametextBox.Text + "' , '" + pwtextBox.Text + "')", conn); //Inkorrekte sql syntax
            DataTable regWriteTable = new DataTable();

            regWriteAdapter.Fill(regWriteTable); //useless?!

            nametextBox.Text = "";
            pwtextBox.Text = "";
            regtextBox.Text = "";

            connectionState();
        }
    }
}
