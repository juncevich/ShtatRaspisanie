using ShtatRaspisanie.DataReader;
using ShtatRaspisanie.DataWriter;
using ShtatRaspisanie.Handlers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ShtatRaspisanie
{
    public partial class Form1 : Form
    {
        private string _unitFileName;
        private string _staffUnitFileName;

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
                _unitFileName = openUnitListFile.FileName;
                ExcelParser parser = new ExcelParser();
                parser.ValidateUnitFile(_unitFileName);
            }
        }

        private void openSpisokShtatnEdinic_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void SpisokShtatnEdinicButton_Click(object sender, EventArgs e)
        {
            if (openStaffUnitListFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(@"Файл со списком штатных единиц выбран!");
                _staffUnitFileName = openStaffUnitListFile.FileName;
                ExcelParser parser = new ExcelParser();
                parser.ValidateStaffUnitFile(_staffUnitFileName);
            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            ConsoleDataWriter consoleDataWriter = new ConsoleDataWriter();
            ExcelDataWriter excelDataWriter = new ExcelDataWriter();
            DataHandler dataHandler = new DataHandler();
            ExcelParser parser = new ExcelParser();
            consoleDataWriter.WriteData(dataHandler.HandleUnitTable(parser.GetUnitList(_unitFileName), parser.GetStaffUnitList(_staffUnitFileName)));
            excelDataWriter.WriteData(dataHandler.HandleUnitTable(parser.GetUnitList(_unitFileName), parser.GetStaffUnitList(_staffUnitFileName)));

            //WriteExcelFile.WriteShtatnoeRaspisanie(ExcelParser.ParseSpisokPodrazdeleniyFile,
            //    ExcelParser.ParseStaffUnitsFile);
        }
    }
}