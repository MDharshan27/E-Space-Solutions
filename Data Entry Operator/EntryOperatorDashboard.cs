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

namespace E___Space_Solution_System.E_Space_Solution.Data_Entry_Operator
{
    public partial class Entry_Operator_Dashboard : Form
    {
        public Entry_Operator_Dashboard()
        {
            InitializeComponent();
        }

        private void btnColonist_Click(object sender, EventArgs e)
        {
            OperatorColonistManagement manageColonist = new OperatorColonistManagement();
            manageColonist.Show();

            this.Hide();
        }

        private void btnTrip_Click(object sender, EventArgs e)
        {
            OperatorTripManagemant manageTrip = new OperatorTripManagemant();
            manageTrip.Show();

            this.Hide();
        }

        private void btnJet_Click(object sender, EventArgs e)
        {
            OperatorJetManagemant manageJet = new OperatorJetManagemant();
            manageJet.Show();

            this.Hide();
        }

        private void btnPilot_Click(object sender, EventArgs e)
        {
            OperatorPilotManagement managePilot = new OperatorPilotManagement();
            managePilot.Show();

            this.Hide();
        }

        private void btnHouse_Click(object sender, EventArgs e)
        {
            OperatorHouseManagement manageHouse = new OperatorHouseManagement();
            manageHouse.Show();

            this.Hide();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            OperatorJobAssignment manageJob = new OperatorJobAssignment();
            manageJob.Show();

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
