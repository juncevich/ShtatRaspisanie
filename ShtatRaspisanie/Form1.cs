﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using ShtatRaspisanie.DataReader;

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
                StaffDao staffDao = StaffDao.GetInstance();
                staffDao.MakeAllUnits(ExcelParser.ParseUnitFile(openUnitListFile.FileName));
                staffDao.SetChildToUnitList();

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
                MessageBox.Show(@"Файл со списком штатных единиц выбран!");
                StaffDao staffDao = StaffDao.GetInstance();
                staffDao.MakeAllStaffUnits(ExcelParser.ParseStaffUnitsFile(openSpisokShtatnEdinic.FileName));
                staffDao.Init();

            }
        }

        private void shtatnoeRaspisanieButton_Click(object sender, EventArgs e)
        {
            //WriteExcelFile.WriteShtatnoeRaspisanie(ExcelParser.ParseSpisokPodrazdeleniyFile,
            //    ExcelParser.ParseStaffUnitsFile);
        }
    }
}