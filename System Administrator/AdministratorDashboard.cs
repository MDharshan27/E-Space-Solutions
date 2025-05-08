using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E___Space_Solution_System.E_Space_Solution.System_Administrator
{
    public partial class AdministratorDashboard : Form
    {
        public AdministratorDashboard()
        {
            InitializeComponent();
        }

        private void btnColonist_Click(object sender, EventArgs e)
        {
            Colonist_View manageColonist = new Colonist_View();
            manageColonist.Show();

            this.Hide();
        }

        private void btnTrip_Click(object sender, EventArgs e)
        {
            Trip_View manageTrip = new Trip_View();
            manageTrip.Show();

            this.Hide();
        }

        private void btnJet_Click(object sender, EventArgs e)
        {
            Jet_View manageJet = new Jet_View();
            manageJet.Show();

            this.Hide();
        }

        private void btnPilot_Click(object sender, EventArgs e)
        {
            Pilot_View managePilot = new Pilot_View();
            managePilot.Show();

            this.Hide();
        }

        private void btnHouse_Click(object sender, EventArgs e)
        {
            House_View manageHouse = new House_View();
            manageHouse.Show();

            this.Hide();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            Job_View manageJob = new Job_View();
            manageJob.Show();

            this.Hide();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            AministorReport manageSAReport = new AministorReport();
            manageSAReport.Show();

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
