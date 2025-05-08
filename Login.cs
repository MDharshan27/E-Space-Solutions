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
using E___Space_Solution_System.E_Space_Solution.Admin;
using E___Space_Solution_System.E_Space_Solution.Colony_Superintendent;
using E___Space_Solution_System.E_Space_Solution.Data_Entry_Operator;
using E___Space_Solution_System.E_Space_Solution.Pilot;
using E___Space_Solution_System.E_Space_Solution.System_Administrator;


namespace E___Space_Solution_System.E_Space_Solution
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            cmbRole.Items.AddRange(new string[]
            {
                "Admin",
                "Data Entry Operator",
                "System Administrator",
                "Colony Superintendent",
                "Pilot"
            });
        }

        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Please fill in all fields.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM [User] WHERE Username=@Username AND Password=@Password AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();

                    switch (role)
                    {
                        case "Admin":
                            new Admin_Dashboard().Show();
                            break;
                        case "Data Entry Operator":
                            new Entry_Operator_Dashboard().Show();
                            break;
                        case "System Administrator":
                            new AdministratorDashboard().Show();
                            break;
                        case "Colony Superintendent":
                            new Superintendent_Dashboard().Show();
                            break;
                        case "Pilot":
                            new Pilot_Dashboard().Show();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username, password, or role.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                reader.Close();
            }
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkPassword.Checked;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
