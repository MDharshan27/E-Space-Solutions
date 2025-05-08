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
    public partial class OperatorJetManagemant : Form
    {
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public OperatorJetManagemant()
        {
            InitializeComponent();
            LoadJetData();
        }

        private void LoadJetData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT JetID, JetCode, PilotID, EnginePower, MadeYear, Weight, NumberOfSeats  FROM Jet";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvJet.DataSource = dt;

                    dgvJet.Width = 848;

                    int columnCount = dgvJet.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvJet.Columns)
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
            // Validate required fields
            if (string.IsNullOrWhiteSpace(txtJetCode.Text) ||
                string.IsNullOrWhiteSpace(txtPilotID.Text) ||
                string.IsNullOrWhiteSpace(txtPower.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtWeight.Text) ||
                nudSeats.Value == 0)
            {
                MessageBox.Show("Please fill in all the fields before adding.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Jet (JetCode, PilotID, EnginePower, MadeYear, Weight, NumberOfSeats) " +
                                   "VALUES (@JetCode, @PilotID, @EnginePower, @MadeYear, @Weight, @NumberOfSeats)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@JetCode", txtJetCode.Text);
                        cmd.Parameters.AddWithValue("@PilotID", txtPilotID.Text);
                        cmd.Parameters.AddWithValue("@EnginePower", txtPower.Text);
                        cmd.Parameters.AddWithValue("@MadeYear", txtYear.Text);
                        cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                        cmd.Parameters.AddWithValue("@NumberOfSeats", nudSeats.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Jet detail added successfully!");
                        LoadJetData();
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
            if (dgvJet.SelectedRows.Count > 0)
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtJetCode.Text) ||
                    string.IsNullOrWhiteSpace(txtPilotID.Text) ||
                    string.IsNullOrWhiteSpace(txtPower.Text) ||
                    string.IsNullOrWhiteSpace(txtYear.Text) ||
                    string.IsNullOrWhiteSpace(txtWeight.Text) ||
                    nudSeats.Value == 0)
                {
                    MessageBox.Show("Please fill in all the fields before editing.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int jetID = Convert.ToInt32(dgvJet.SelectedRows[0].Cells["JetID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Jet SET JetCode = @JetCode, PilotID = @PilotID, EnginePower = @EnginePower, " +
                                       "MadeYear = @MadeYear, Weight = @Weight, NumberOfSeats = @NumberOfSeats " +
                                       "WHERE JetID = @JetID";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@JetID", jetID);
                            cmd.Parameters.AddWithValue("@JetCode", txtJetCode.Text);
                            cmd.Parameters.AddWithValue("@PilotID", txtPilotID.Text);
                            cmd.Parameters.AddWithValue("@EnginePower", txtPower.Text);
                            cmd.Parameters.AddWithValue("@MadeYear", txtYear.Text);
                            cmd.Parameters.AddWithValue("@Weight", txtWeight.Text);
                            cmd.Parameters.AddWithValue("@NumberOfSeats", nudSeats.Value);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Jet detail updated successfully!");
                            LoadJetData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a jet detail to edit.");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvJet.SelectedRows.Count > 0)
            {
                int jetID = Convert.ToInt32(dgvJet.SelectedRows[0].Cells["JetID"].Value);

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this Jet Detail?", "Confirm Deletion", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Jet WHERE JetID = @JetID";
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@JetID", jetID);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Jet detail deleted successfully!");
                                LoadJetData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a jet detail to delete.");
            }
        }
    }
}
