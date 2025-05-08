using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using E___Space_Solution_System.E_Space_Solution.System_Administrator;

namespace E___Space_Solution_System.E_Space_Solution.Admin
{
    public partial class User_Management : Form
    {
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public User_Management()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT UserID, Username, Role, MobileNumber FROM [User]";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUsers.DataSource = dt;

                    dgvUsers.Width = 863;

                    int columnCount = dgvUsers.Columns.Count;
                    int columnWidth = 863 / columnCount;

                    foreach (DataGridViewColumn column in dgvUsers.Columns)
                    {
                        column.Width = columnWidth;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Admin_Dashboard manageColonist = new Admin_Dashboard();
            manageColonist.Show();

            this.Hide();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
