using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NationGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View =
                RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException(
                            string.Format("Key Not Found: {0}", location));

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException(
                            string.Format("Index Not Found: {0}", name));

                    return machineGuid.ToString();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            guna2TextBox5.Text = Convert.ToBase64String(Encoding.ASCII.GetBytes(GetMachineGuid()));
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(guna2TextBox5.Text);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Successfully logged!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Hide();
                Form2 frm = new Form2(this);
                frm.Show();
            }
            catch(Exception)
            {
                MessageBox.Show("HWID Isn't Activated!", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
