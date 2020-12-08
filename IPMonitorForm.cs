using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;

namespace IP_Monitor
{
    public partial class IPMonitorForm : Form
    {
        public IPMonitorForm()
        {
            InitializeComponent();
        }
        private bool PingIP(string ip)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            PingReply reply = pingSender.Send(ip, timeout, buffer, options);
            if (reply.Status != IPStatus.Success)
                return false;
            else
                return true;
        }
        private void IPMonitorForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'devicesDataSet.Device' table. You can move, or remove it, as needed.
            this.deviceTableAdapter.Fill(this.devicesDataSet.Device);
        }
        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i =0; i < dataGridView1.RowCount -1; i++)
            {
                string ip = dataGridView1.Rows[i].Cells[0].Value.ToString();
                if (PingIP(ip) == false)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.LimeGreen;
                }
                dataGridView1.Update();
            }
        }
        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows.Count -1; i++)
            {
                dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Empty;
                dataGridView1.Update();
            }
        }
    }
}
