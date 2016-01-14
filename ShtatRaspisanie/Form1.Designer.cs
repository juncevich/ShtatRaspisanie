namespace ShtatRaspisanie
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.SpisokPodrazdeleniyButton = new System.Windows.Forms.Button();
            this.SpisokShtatnEdinicButton = new System.Windows.Forms.Button();
            this.openUnitListFile = new System.Windows.Forms.OpenFileDialog();
            this.openSpisokShtatnEdinic = new System.Windows.Forms.OpenFileDialog();
            this.shtatnoeRaspisanieButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SpisokPodrazdeleniyButton
            // 
            this.SpisokPodrazdeleniyButton.Location = new System.Drawing.Point(12, 12);
            this.SpisokPodrazdeleniyButton.Name = "SpisokPodrazdeleniyButton";
            this.SpisokPodrazdeleniyButton.Size = new System.Drawing.Size(180, 45);
            this.SpisokPodrazdeleniyButton.TabIndex = 0;
            this.SpisokPodrazdeleniyButton.Text = "Выбрать список подразделений";
            this.SpisokPodrazdeleniyButton.UseVisualStyleBackColor = true;
            this.SpisokPodrazdeleniyButton.Click += new System.EventHandler(this.UnitListButton_Click);
            // 
            // SpisokShtatnEdinicButton
            // 
            this.SpisokShtatnEdinicButton.Location = new System.Drawing.Point(211, 12);
            this.SpisokShtatnEdinicButton.Name = "SpisokShtatnEdinicButton";
            this.SpisokShtatnEdinicButton.Size = new System.Drawing.Size(180, 45);
            this.SpisokShtatnEdinicButton.TabIndex = 1;
            this.SpisokShtatnEdinicButton.Text = "Выбрать список штатных единиц";
            this.SpisokShtatnEdinicButton.UseVisualStyleBackColor = true;
            this.SpisokShtatnEdinicButton.Click += new System.EventHandler(this.SpisokShtatnEdinicButton_Click);
            // 
            // openUnitListFile
            // 
            this.openUnitListFile.FileName = "openUnitListFile";
            this.openUnitListFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // openSpisokShtatnEdinic
            // 
            this.openSpisokShtatnEdinic.FileName = "openSpisokShtatnEdinic";
            this.openSpisokShtatnEdinic.FileOk += new System.ComponentModel.CancelEventHandler(this.openSpisokShtatnEdinic_FileOk);
            // 
            // shtatnoeRaspisanieButton
            // 
            this.shtatnoeRaspisanieButton.Location = new System.Drawing.Point(409, 12);
            this.shtatnoeRaspisanieButton.Name = "shtatnoeRaspisanieButton";
            this.shtatnoeRaspisanieButton.Size = new System.Drawing.Size(199, 44);
            this.shtatnoeRaspisanieButton.TabIndex = 2;
            this.shtatnoeRaspisanieButton.Text = "Сформировать штатное расписание";
            this.shtatnoeRaspisanieButton.UseVisualStyleBackColor = true;
            this.shtatnoeRaspisanieButton.Click += new System.EventHandler(this.shtatnoeRaspisanieButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(620, 70);
            this.Controls.Add(this.shtatnoeRaspisanieButton);
            this.Controls.Add(this.SpisokShtatnEdinicButton);
            this.Controls.Add(this.SpisokPodrazdeleniyButton);
            this.Name = "Form1";
            this.Text = "Штатное расписание v.0.01";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SpisokPodrazdeleniyButton;
        private System.Windows.Forms.Button SpisokShtatnEdinicButton;
        private System.Windows.Forms.OpenFileDialog openUnitListFile;
        private System.Windows.Forms.OpenFileDialog openSpisokShtatnEdinic;
        private System.Windows.Forms.Button shtatnoeRaspisanieButton;
    }
}

