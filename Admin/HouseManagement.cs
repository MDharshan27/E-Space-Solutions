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
    public partial class House_Management : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public House_Management()
        {
            InitializeComponent();
            LoadHouseData();
        }

        private void LoadHouseData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT HouseID, NumberOfRooms, SquareFeet FROM House";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvHouse.DataSource = dt;

                    dgvHouse.Width = 848;

                    int columnCount = dgvHouse.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvHouse.Columns)
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
            // Validation
            if (string.IsNullOrWhiteSpace(nudRooms.Text) || string.IsNullOrWhiteSpace(txtFeet.Text))
            {
                MessageBox.Show("Please fill in all fields before adding a house.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO House (NumberOfRooms, SquareFeet) VALUES (@NumberOfRooms, @SquareFeet)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NumberOfRooms", nudRooms.Text);
                        cmd.Parameters.AddWithValue("@SquareFeet", txtFeet.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("House added successfully!");
                        LoadHouseData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding house: " + ex.Message);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvHouse.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a house to edit.");
                return;
            }

            if (string.IsNullOrWhiteSpace(nudRooms.Text) || string.IsNullOrWhiteSpace(txtFeet.Text))
            {
                MessageBox.Show("Please fill in all fields before editing the house.");
                return;
            }

            int houseID = Convert.ToInt32(dgvHouse.SelectedRows[0].Cells["HouseID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE House SET NumberOfRooms = @NumberOfRooms, SquareFeet = @SquareFeet WHERE HouseID = @HouseID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HouseID", houseID);
                        cmd.Parameters.AddWithValue("@NumberOfRooms", nudRooms.Text);
                        cmd.Parameters.AddWithValue("@SquareFeet", txtFeet.Text);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("House updated successfully!");
                        LoadHouseData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating house: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvHouse.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a house to delete.");
                return;
            }

            int houseID = Convert.ToInt32(dgvHouse.SelectedRows[0].Cells["HouseID"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this house?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM House WHERE HouseID = @HouseID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@HouseID", houseID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("House deleted successfully!");
                            LoadHouseData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting house: " + ex.Message);
                    }
                }
            }
        }
    }
}
