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
    public partial class OperatorColonistManagement : Form
    {
        // Connection string
        string connectionString = @"Server=DESKTOP-2VPR9IB\SQLEXPRESS;Database=ESpace_Solution;Trusted_Connection=True;";

        public OperatorColonistManagement()
        {
            InitializeComponent();
            LoadColonistData();
            LoadDependentData();
        }

        private void LoadColonistData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ColonizationID, FirstName, MiddleName, SurName, DateOfBirth, Qualification, Age, EarthAddress, Gender, ContactNo, CivilStatus, NumberOfDependents FROM Colonist";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvColonists.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message);
                }
            }
        }

        private void LoadDependentData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT DependentID, ColonizationID, Name, DateOfBirth, Gender, Relationship FROM Dependent";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDependent.DataSource = dt;

                    dgvDependent.Width = 848;

                    int columnCount = dgvDependent.Columns.Count;
                    int columnWidth = 848 / columnCount;

                    foreach (DataGridViewColumn column in dgvDependent.Columns)
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
            if (string.IsNullOrWhiteSpace(txtFirstname.Text) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(rtbAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                cmbGender.SelectedItem == null ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill out all required fields before adding a colonist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Colonist (FirstName, MiddleName, SurName, DateOfBirth, Qualification, Age, EarthAddress, Gender, ContactNo, CivilStatus, NumberOfDependents) " +
                                   "VALUES (@FirstName, @MiddleName, @SurName, @DateOfBirth, @Qualification, @Age, @EarthAddress, @Gender, @ContactNo, @CivilStatus, @NumberOfDependents)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", txtMiddlename.Text);
                        cmd.Parameters.AddWithValue("@SurName", txtSurname.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpBirth.Value);
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                        cmd.Parameters.AddWithValue("@Age", nudAge.Value);
                        cmd.Parameters.AddWithValue("@EarthAddress", rtbAddress.Text);
                        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                        cmd.Parameters.AddWithValue("@CivilStatus", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@NumberOfDependents", nudDependents.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Colonist added successfully!");
                        LoadColonistData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        // Edit an existing colonist's details
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvColonists.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a colonist to edit.");
                return;
            }

            // Input validation
            if (string.IsNullOrWhiteSpace(txtFirstname.Text) ||
                string.IsNullOrWhiteSpace(txtSurname.Text) ||
                string.IsNullOrWhiteSpace(txtQualification.Text) ||
                string.IsNullOrWhiteSpace(rtbAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                cmbGender.SelectedItem == null ||
                cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please fill out all required fields before editing a colonist.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int colonizationID = Convert.ToInt32(dgvColonists.SelectedRows[0].Cells["ColonizationID"].Value);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Colonist SET FirstName = @FirstName, MiddleName = @MiddleName, SurName = @SurName, DateOfBirth = @DateOfBirth, Qualification = @Qualification, " +
                                   "Age = @Age, EarthAddress = @EarthAddress, Gender = @Gender, ContactNo = @ContactNo, CivilStatus = @CivilStatus, NumberOfDependents = @NumberOfDependents " +
                                   "WHERE ColonizationID = @ColonizationID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ColonizationID", colonizationID);
                        cmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text);
                        cmd.Parameters.AddWithValue("@MiddleName", txtMiddlename.Text);
                        cmd.Parameters.AddWithValue("@SurName", txtSurname.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpBirth.Value);
                        cmd.Parameters.AddWithValue("@Qualification", txtQualification.Text);
                        cmd.Parameters.AddWithValue("@Age", nudAge.Value);
                        cmd.Parameters.AddWithValue("@EarthAddress", rtbAddress.Text);
                        cmd.Parameters.AddWithValue("@Gender", cmbGender.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@ContactNo", txtContact.Text);
                        cmd.Parameters.AddWithValue("@CivilStatus", cmbStatus.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@NumberOfDependents", nudDependents.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Colonist updated successfully!");
                        LoadColonistData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        // Delete a selected colonist
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvColonists.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a colonist to delete.");
                return;
            }

            int colonizationID = Convert.ToInt32(dgvColonists.SelectedRows[0].Cells["ColonizationID"].Value);

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this colonist?", "Confirm Deletion", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM Colonist WHERE ColonizationID = @ColonizationID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ColonizationID", colonizationID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Colonist deleted successfully!");
                            LoadColonistData();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void btnAddDependent_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtColonizationID.Text) ||
                string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(cmbGenderD.Text) ||
                string.IsNullOrWhiteSpace(txtRelationship.Text))
            {
                MessageBox.Show("Please fill in all required dependent fields.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Dependent (ColonizationID, Name, DateOfBirth, Age, Gender, Relationship) " +
                                   "VALUES (@ColonizationID, @Name, @DateOfBirth, @Age, @Gender, @Relationship)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text);
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.Parameters.AddWithValue("@DateOfBirth", dtpBirthDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Age", nudAgeD.Value);
                    cmd.Parameters.AddWithValue("@Gender", cmbGenderD.Text);
                    cmd.Parameters.AddWithValue("@Relationship", txtRelationship.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Dependent added successfully!");
                    LoadDependentData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding dependent: " + ex.Message);
                }
            }
        }

        private void btnEditDependent_Click(object sender, EventArgs e)
        {
            if (dgvDependent.SelectedRows.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtColonizationID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(cmbGenderD.Text) ||
                    string.IsNullOrWhiteSpace(txtRelationship.Text))
                {
                    MessageBox.Show("Please fill in all required dependent fields.");
                    return;
                }

                int dependentID = Convert.ToInt32(dgvDependent.SelectedRows[0].Cells["DependentID"].Value);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Dependent SET ColonizationID=@ColonizationID, Name=@Name, DateOfBirth=@DateOfBirth, " +
                                       "Age=@Age, Gender=@Gender, Relationship=@Relationship WHERE DependentID=@DependentID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ColonizationID", txtColonizationID.Text);
                        cmd.Parameters.AddWithValue("@Name", txtName.Text);
                        cmd.Parameters.AddWithValue("@DateOfBirth", dtpBirthDate.Value.Date);
                        cmd.Parameters.AddWithValue("@Age", nudAgeD.Value);
                        cmd.Parameters.AddWithValue("@Gender", cmbGenderD.Text);
                        cmd.Parameters.AddWithValue("@Relationship", txtRelationship.Text);
                        cmd.Parameters.AddWithValue("@DependentID", dependentID);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Dependent updated successfully!");
                        LoadDependentData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating dependent: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a dependent to edit.");
            }
        }

        private void btnDeleteDependent_Click(object sender, EventArgs e)
        {
            if (dgvDependent.SelectedRows.Count > 0)
            {
                int dependentID = Convert.ToInt32(dgvDependent.SelectedRows[0].Cells["DependentID"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this dependent?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();
                            string query = "DELETE FROM Dependent WHERE DependentID=@DependentID";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@DependentID", dependentID);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Dependent deleted successfully!");
                            LoadDependentData();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error deleting dependent: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a dependent to delete.");
            }
        }
    }
}
