using ShtatRaspisanie.DataReader;
using ShtatRaspisanie.Handlers;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Excel;

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
                var dataHandler = new DataHandler();
                ExcelParser parser = new ExcelParser();
                //parser.GetUnitList(openUnitListFile.FileName);
                FileStream stream = File.Open(openUnitListFile.FileName, FileMode.Open, FileAccess.Read);
                var unitListFileReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                //dataHandler.HandleUnitTable(new ExcelParser().GetUnitList());
                //StaffDao staffDao = StaffDao.GetInstance();
                //staffDao.MakeAllUnits(parser.GetUnitList(openUnitListFile.FileName));
                //staffDao.SetChildToUnitList();
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
            if (openStaffUnitListFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(@"Файл со списком штатных единиц выбран!");
                ExcelParser parser = new ExcelParser();
                parser.GetStaffUnitList(openStaffUnitListFile.FileName);
                //StaffDao staffDao = StaffDao.GetInstance();
                //staffDao.MakeAllStaffUnits(parser.GetStaffUnitList(openStaffUnitListFile.FileName));
                //staffDao.Init();
            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            //WriteExcelFile.WriteShtatnoeRaspisanie(ExcelParser.ParseSpisokPodrazdeleniyFile,
            //    ExcelParser.ParseStaffUnitsFile);
        }
    }
}