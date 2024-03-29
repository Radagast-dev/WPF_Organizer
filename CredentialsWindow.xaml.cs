﻿using System;
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
using System.IO;

namespace WPF_Organizer
{
    /// <summary>
    /// Interaction logic for CredentialsWindow.xaml
    /// </summary>
    /// https://www.dev-insider.de/4-kostenfreie-mockup-tools-a-872594/
    public partial class CredentialsWindow : Window
    {
        public CredentialsWindow()
        {
            InitializeComponent();
        }

        //Console.WriteLine("UserName: {0}", Environment.UserName)


        public static string userEnv = Environment.UserName;                        //finde env.user
        public static string dbPath = @$"C:\Users\{userEnv}\source\repos\.git\db\WPF_Organizer_DB.mdf"; //bastele env.user in string
        public static string dbPathDefault = $@"C:\Users\{userEnv}\source\repos\.git\db";
        public static string connString = @$"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename= {dbPath} ;Integrated Security = True; Connect Timeout = 30"; //db path in connstring


        SqlConnection conn = new SqlConnection(connString);
        
        public void directoryCheck() //einbauen
        {
            if (Directory.Exists(dbPath))
            {
                MessageBox.Show("DB user path established");
            }
            else
            {
                connString = dbPathDefault; // funzt nicht
            }
        }

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
                    MessageBox.Show("Database connection closed!");
                    //conn.Open();   //sinn?!
                }
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * from [PasswordTable] WHERE Name = ('" + nametextBox.Text + "') AND Password = ('" + pwtextBox.Text + "') ", conn); 
            DataTable loginData = new DataTable();

            dataAdapter.Fill(loginData);

            if (loginData.Rows.Count == 1) 
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

            regWriteAdapter.Fill(regWriteTable);

            nametextBox.Text = "";
            pwtextBox.Text = "";
            regtextBox.Text = "";

            connectionState();
        }
    }
}
