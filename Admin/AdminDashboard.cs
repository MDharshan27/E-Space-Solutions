using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E___Space_Solution_System.E_Space_Solution.Admin
{
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            User_Management manageUsers = new User_Management();
            manageUsers.Show();

            this.Hide();
        }

        private void btnColonist_Click(object sender, EventArgs e)
        {
            Colonist_Management manageColonist = new Colonist_Management();
            manageColonist.Show();

            this.Hide();
        }

        private void btnTrip_Click(object sender, EventArgs e)
        {
            Trip_Managemant manageTrip = new Trip_Managemant();
            manageTrip.Show();

            this.Hide();
        }

        private void btnJet_Click(object sender, EventArgs e)
        {
            Jet_Managemant manageJet = new Jet_Managemant();
            manageJet.Show();

            this.Hide();
        }

        private void btnPilot_Click(object sender, EventArgs e)
        {
            Pilot_Management managePilot = new Pilot_Management();
            managePilot.Show();

            this.Hide();
        }

        private void btnHouse_Click(object sender, EventArgs e)
        {
            House_Management manageHouse = new House_Management();
            manageHouse.Show();

            this.Hide();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            Job_Assignment manageJob = new Job_Assignment();
            manageJob.Show();

            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Reporting manageReport = new Reporting();
            manageReport.Show();

            this.Hide();

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void picLogout_Click(object sender, EventArgs e)
        {
            Login manageSAReport = new Login();
            manageSAReport.Show();

            this.Hide();
        }
    }
}
