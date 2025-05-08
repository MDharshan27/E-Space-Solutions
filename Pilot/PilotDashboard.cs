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

namespace E___Space_Solution_System.E_Space_Solution.Pilot
{
    public partial class Pilot_Dashboard : Form
    {
        public Pilot_Dashboard()
        {
            InitializeComponent();
        }

        private void btnColonist_Click(object sender, EventArgs e)
        {
            PilotColonistView manageColonist = new PilotColonistView();
            manageColonist.Show();

            this.Hide();
        }

        private void btnTrip_Click(object sender, EventArgs e)
        {
            PilotTripView manageTrip = new PilotTripView();
            manageTrip.Show();

            this.Hide();
        }

        private void btnJet_Click(object sender, EventArgs e)
        {
            PilotJetView manageJet = new PilotJetView();
            manageJet.Show();

            this.Hide();
        }

        private void btnPilot_Click(object sender, EventArgs e)
        {
            PilotPilotView managePilot = new PilotPilotView();
            managePilot.Show();

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

        private void btnReport_Click(object sender, EventArgs e)
        {
            PilotReport managePilot = new PilotReport();
            managePilot.Show();

            this.Hide();
        }
    }
}
