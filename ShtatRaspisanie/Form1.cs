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

        private void UnitListButton_Click(object sender, EventArgs e)
        {
            if (openUnitListFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(@"Файл со списком подразделений выбран!");
                StaffDao staffDao = new StaffDao();
                staffDao.GetAllUnits(ParseExcelFile.ParseUnitFile(openUnitListFile.FileName));
                staffDao.SetChildToUnitList();
                // TO DO Delete.
                //var filename = openUnitListFile.FileName;
                //var stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                //var unitBuilder = new UnitBuilder(ParseExcelFile.ParseParentList(stream));
                //unitBuilder.SetChildByAll();
                //unitBuilder.DisplayUnitList();
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
                var filename = openSpisokShtatnEdinic.FileName;
                var stream = File.Open(filename, FileMode.Open, FileAccess.Read);
                ParseExcelFile.ParseSpisokShtatnEdinicaFile(stream);
            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            WriteExcelFile.WriteShtatnoeRaspisanie(ParseExcelFile.ParseSpisokPodrazdeleniyFile,
                ParseExcelFile.ParseSpisokShtatnEdinicaFile);
        }
    }
}