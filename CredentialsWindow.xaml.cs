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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

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

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf;Integrated Security=True;Connect Timeout=30");
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
        }

        private void xButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("REGISTER dummy");

            SqlConnection regConn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Radagast\source\repos\.git\db\WPF_Organizer_DB.mdf;Integrated Security=True;Connect Timeout=30");
            
            SqlDataAdapter regReadAdapter = new SqlDataAdapter("SELECT * from [RegisterTable] WHERE RegisterNumber = ('" + regtextBox.Text +"')", regConn);
            DataTable regReadTable = new DataTable();

            SqlDataAdapter regWriteAdapter = new SqlDataAdapter("INSERT INTO [PasswordTable] WHERE Name = ('" + regtextBox.Text + "')", regConn);
            DataTable regWriteTable = new DataTable();

            regReadAdapter.Fill(regReadTable);
            
            if(regReadTable.Rows.Count == 1)
            {
                MessageBox.Show("Correct code number given!");
            }
            else
            {
                MessageBox.Show("Incorrect number!");
            }
        }
    }
}
