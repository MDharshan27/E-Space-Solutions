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

namespace E___Space_Solution_System.E_Space_Solution.System_Administrator
{
    public partial class Trip_View : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public Trip_View()
        {
            InitializeComponent();
            LoadTripData();
        }
        private void LoadTripData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT TripID, ColonizationID, DependentID, PilotID, JetCode, LaunchDate, ReturnDate  FROM Trip";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTrip.DataSource = dt;

                    dgvTrip.Width = 848;

                    int columnCount = dgvTrip.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvTrip.Columns)
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
            AdministratorDashboard manageColonist = new AdministratorDashboard();
            manageColonist.Show();

            this.Hide();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
