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

namespace E___Space_Solution_System.E_Space_Solution.Colony_Superintendent
{
    public partial class SuperReporting : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public SuperReporting()
        {
            InitializeComponent();
            LoadReportData();
            cmbReport.Items.AddRange(new string[]
            {
                "Colonist Report",
                "Trip Report with Passengers",
                "Jet Detail Report",
                "Housing Report",
                "Job Assignment Report"
            });
        }
        private void LoadReportData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ReportID, ID, ReportType, ReportMessage FROM Report";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvReport.DataSource = dt;

                    dgvReport.Width = 848;

                    int columnCount = dgvReport.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvReport.Columns)
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
            Superintendent_Dashboard manageColonist = new Superintendent_Dashboard();
            manageColonist.Show();

            this.Hide();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Please enter the ID.");
                return;
            }

            if (cmbReport.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a report type.");
                return;
            }

            if (string.IsNullOrWhiteSpace(rtbType.Text))
            {
                MessageBox.Show("Please enter your report message.");
                return;
            }

            // Proceed with database insert
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Report (ID, ReportType, ReportMessage) VALUES (@ID, @ReportType, @ReportMessage)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                        cmd.Parameters.AddWithValue("@ReportType", cmbReport.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ReportMessage", rtbType.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Report submitted successfully!");
                        LoadReportData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting report: " + ex.Message);
                }
            }
        }
    }
}
