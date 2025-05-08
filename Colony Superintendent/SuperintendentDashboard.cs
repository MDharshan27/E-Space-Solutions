using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using E___Space_Solution_System.E_Space_Solution.Admin;
using E___Space_Solution_System.E_Space_Solution.System_Administrator;

namespace E___Space_Solution_System.E_Space_Solution.Colony_Superintendent
{
    public partial class Superintendent_Dashboard : Form
    {
        public Superintendent_Dashboard()
        {
            InitializeComponent();
        }

        private void btnColonist_Click(object sender, EventArgs e)
        {
            SuperColonistView manageColonist = new SuperColonistView();
            manageColonist.Show();

            this.Hide();
        }

        private void btnTrip_Click(object sender, EventArgs e)
        {
            SuperTripView manageTrip = new SuperTripView();
            manageTrip.Show();

            this.Hide();
        }

        private void btnJet_Click(object sender, EventArgs e)
        {
            SuperJetView manageJet = new SuperJetView();
            manageJet.Show();

            this.Hide();
        }

        private void btnPilot_Click(object sender, EventArgs e)
        {
            SuperPilotView managePilot = new SuperPilotView();
            managePilot.Show();

            this.Hide();
        }

        private void btnHouse_Click(object sender, EventArgs e)
        {
            SuperHouseManagement manageHouse = new SuperHouseManagement();
            manageHouse.Show();

            this.Hide();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            SuperJobAssignment manageJob = new SuperJobAssignment();
            manageJob.Show();

            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            SuperReporting manageReport = new SuperReporting();
            manageReport.Show();

            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
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
