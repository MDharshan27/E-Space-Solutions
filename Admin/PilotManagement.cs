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

namespace E___Space_Solution_System.E_Space_Solution.Admin
{
    public partial class Pilot_Management : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public Pilot_Management()
        {
            InitializeComponent();
            LoadPilotData();
        }
        private void LoadPilotData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PilotID, PilotName, Qualification, Level FROM Pilot";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPilot.DataSource = dt;

                    dgvPilot.Width = 848;

                    int columnCount = dgvPilot.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvPilot.Columns)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Input Validation
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(txtLevel.Text))
            {
                MessageBox.Show("Please fill in all fields before adding a pilot.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Pilot (PilotName, Qualification, Level) " +
                                   "VALUES (@PilotName, @Qualification, @Level)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PilotName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                        cmd.Parameters.AddWithValue("@Level", txtLevel.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pilot added successfully!");
                        LoadPilotData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding pilot: " + ex.Message);
                }
            }
        }

        // Edit Button Click
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvPilot.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a pilot to edit.");
                return;
            }

            // Input Validation
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(txtLevel.Text))
            {
                MessageBox.Show("Please fill in all fields before editing the pilot.");
                return;
            }

            int pilotID = Convert.ToInt32(dgvPilot.SelectedRows[0].Cells["PilotID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Pilot SET PilotName = @PilotName, Qualification = @Qualification, Level = @Level " +
                                   "WHERE PilotID = @PilotID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PilotID", pilotID);
                        cmd.Parameters.AddWithValue("@PilotName", txtName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text.Trim());
                        cmd.Parameters.AddWithValue("@Level", txtLevel.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Pilot updated successfully!");
                        LoadPilotData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating pilot: " + ex.Message);
                }
            }
        }

        // Delete Button Click
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPilot.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a pilot to delete.");
                return;
            }

            int pilotID = Convert.ToInt32(dgvPilot.SelectedRows[0].Cells["PilotID"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this pilot?",
                                                        "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Pilot WHERE PilotID = @PilotID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@PilotID", pilotID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Pilot deleted successfully!");
                            LoadPilotData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting pilot: " + ex.Message);
                    }
                }
            }
        }
    }
}
