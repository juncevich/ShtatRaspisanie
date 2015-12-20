using Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShtatRaspisanie
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void SpisokPodrazdeleniyButton_Click(object sender, EventArgs e)
        {
            if (openFileSpisokPodrazdeleniy.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Файл со списком подразделений выбран!");
                string filename = openFileSpisokPodrazdeleniy.FileName;
                System.IO.FileStream stream = File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                ParseExcelFile.parseSpisokPodrazdeleniyFile(stream);
            }
        }

        private void openSpisokShtatnEdinic_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void openFileDialog1_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void SpisokShtatnEdinicButton_Click(object sender, EventArgs e)
        {
            
            if (openSpisokShtatnEdinic.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Файл со списком штатных единиц выбран!");
                string filename = openSpisokShtatnEdinic.FileName;
                System.IO.FileStream stream = File.Open(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                ParseExcelFile.parseSpisokShtatnEdinicaFile(stream);
            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            WriteExcelFile.writeShtatnoeRaspisanie(ParseExcelFile.parseSpisokPodrazdeleniyFile, ParseExcelFile.parseSpisokShtatnEdinicaFile);
        }
    }
}
