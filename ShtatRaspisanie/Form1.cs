using System;
using System.ComponentModel;
using System.IO;
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
            if (openFileSpisokPodrazdeleniy.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Файл со списком подразделений выбран!");
                string filename = openFileSpisokPodrazdeleniy.FileName;
                FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
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
            
            if (openSpisokShtatnEdinic.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Файл со списком штатных единиц выбран!");
                string filename = openSpisokShtatnEdinic.FileName;
                FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                ParseExcelFile.parseSpisokShtatnEdinicaFile(stream);
            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            WriteExcelFile.writeShtatnoeRaspisanie(ParseExcelFile.parseSpisokPodrazdeleniyFile, ParseExcelFile.parseSpisokShtatnEdinicaFile);
        }
    }
}
