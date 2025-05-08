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

namespace E___Space_Solution_System.E_Space_Solution.Data_Entry_Operator
{
    public partial class OperatorJobAssignment : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public OperatorJobAssignment()
        {
            InitializeComponent();
            LoadJobData();
            cmbCategories.Items.AddRange(new string[]
            {
                "Construction",
                "Mechanical",
                "Power Generation",
                "Medical",
                "Security",
                "Administration",
                "Education",
                "Research & Observation"
            });
        }
        private void LoadJobData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT JobID, ColonizationID, Categories FROM Job";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJob.DataSource = dt;

                    dgvJob.Width = 848;

                    int columnCount = dgvJob.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvJob.Columns)
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
            Entry_Operator_Dashboard manageColonist = new Entry_Operator_Dashboard();
            manageColonist.Show();

            this.Hide();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtColonizationID.Text) || cmbCategories.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields before adding a job.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Job (ColonizationID, Categories) VALUES (@ColonizationID, @Categories)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text);
                        cmd.Parameters.AddWithValue("@Categories", cmbCategories.SelectedItem.ToString());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Job added successfully!");
                        LoadJobData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvJob.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a job to edit.");
                return;
            }

            // Input validation
            if (string.IsNullOrWhiteSpace(txtColonizationID.Text) || cmbCategories.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields before editing the job.");
                return;
            }

            int jobID = Convert.ToInt32(dgvJob.SelectedRows[0].Cells["JobID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Job SET ColonizationID = @ColonizationID, Categories = @Categories WHERE JobID = @JobID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@JobID", jobID);
                        cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text);
                        cmd.Parameters.AddWithValue("@Categories", cmbCategories.SelectedItem.ToString());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Job updated successfully!");
                        LoadJobData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJob.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a job to delete.");
                return;
            }

            int jobID = Convert.ToInt32(dgvJob.SelectedRows[0].Cells["JobID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this job?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Job WHERE JobID = @JobID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@JobID", jobID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Job deleted successfully!");
                            LoadJobData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }
    }
}
