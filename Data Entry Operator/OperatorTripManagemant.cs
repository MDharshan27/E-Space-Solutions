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
using E___Space_Solution_System.E_Space_Solution.Colony_Superintendent;

namespace E___Space_Solution_System.E_Space_Solution.Data_Entry_Operator
{
    public partial class OperatorTripManagemant : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public OperatorTripManagemant()
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
            if (string.IsNullOrWhiteSpace(txtColonizationID.Text) ||
                string.IsNullOrWhiteSpace(txtDependentID.Text) ||
                string.IsNullOrWhiteSpace(txtPilotID.Text) ||
                string.IsNullOrWhiteSpace(txtJetCode.Text))
            {
                MessageBox.Show("Please fill in all required fields (Colonization ID, Dependent ID, Pilot ID, Jet Code).");
                return;
            }

            if (dtpReturn.Value < dtpLaunch.Value)
            {
                MessageBox.Show("Return date cannot be earlier than launch date.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Trip (ColonizationID, DependentID, PilotID, JetCode, LaunchDate, ReturnDate) " +
                                   "VALUES (@ColonizationID, @DependentID, @PilotID, @JetCode, @LaunchDate, @ReturnDate)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text.Trim());
                        cmd.Parameters.AddWithValue("@DependentID", txtDependentID.Text.Trim());
                        cmd.Parameters.AddWithValue("@PilotID", txtPilotID.Text.Trim());
                        cmd.Parameters.AddWithValue("@JetCode", txtJetCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@LaunchDate", dtpLaunch.Value);
                        cmd.Parameters.AddWithValue("@ReturnDate", dtpReturn.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Trip added successfully!");
                        LoadTripData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding trip: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvTrip.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtColonizationID.Text) ||
                    string.IsNullOrWhiteSpace(txtDependentID.Text) ||
                    string.IsNullOrWhiteSpace(txtPilotID.Text) ||
                    string.IsNullOrWhiteSpace(txtJetCode.Text))
                {
                    MessageBox.Show("Please fill in all required fields before editing.");
                    return;
                }

                if (dtpReturn.Value < dtpLaunch.Value)
                {
                    MessageBox.Show("Return date cannot be earlier than launch date.");
                    return;
                }

                int tripID = Convert.ToInt32(dgvTrip.SelectedRows[0].Cells["TripID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Trip SET ColonizationID = @ColonizationID, DependentID = @DependentID, PilotID = @PilotID, JetCode = @JetCode, " +
                                       "LaunchDate = @LaunchDate, ReturnDate = @ReturnDate WHERE TripID = @TripID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@TripID", tripID);
                            cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text.Trim());
                            cmd.Parameters.AddWithValue("@DependentID", txtDependentID.Text.Trim());
                            cmd.Parameters.AddWithValue("@PilotID", txtPilotID.Text.Trim());
                            cmd.Parameters.AddWithValue("@JetCode", txtJetCode.Text.Trim());
                            cmd.Parameters.AddWithValue("@LaunchDate", dtpLaunch.Value);
                            cmd.Parameters.AddWithValue("@ReturnDate", dtpReturn.Value);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Trip updated successfully!");
                            LoadTripData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating trip: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a trip to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTrip.SelectedRows.Count > 0)
            {
                int tripID = Convert.ToInt32(dgvTrip.SelectedRows[0].Cells["TripID"].Value);

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this trip?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Trip WHERE TripID = @TripID";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@TripID", tripID);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Trip deleted successfully!");
                                LoadTripData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting trip: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a trip to delete.");
            }
        }
    }
}
