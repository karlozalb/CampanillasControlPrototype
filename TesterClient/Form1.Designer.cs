namespace TesterClient
{
    partial class ReportGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonGetClockIns = new System.Windows.Forms.Button();
            this.dataGridFichajes = new System.Windows.Forms.DataGridView();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoraEntradaReal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Retraso = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoFichaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonMarkClockIns = new System.Windows.Forms.Button();
            this.comboBoxProfesor = new System.Windows.Forms.ComboBox();
            this.dateTimeInit = new System.Windows.Forms.DateTimePicker();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.anunciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirAnuncioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrarAnuncioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarInformeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportarEnXLSExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPanel = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridFaltas = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonGetFaltas = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerFaltas1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFaltas2 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFichajes)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFaltas)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonGetClockIns
            // 
            this.buttonGetClockIns.Location = new System.Drawing.Point(12, 161);
            this.buttonGetClockIns.Name = "buttonGetClockIns";
            this.buttonGetClockIns.Size = new System.Drawing.Size(296, 33);
            this.buttonGetClockIns.TabIndex = 1;
            this.buttonGetClockIns.Text = "Obtener fichajes";
            this.buttonGetClockIns.UseVisualStyleBackColor = true;
            this.buttonGetClockIns.Click += new System.EventHandler(this.buttonGetClockIns_Click);
            // 
            // dataGridFichajes
            // 
            this.dataGridFichajes.AllowUserToAddRows = false;
            this.dataGridFichajes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridFichajes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridFichajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFichajes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.HoraLlegada,
            this.HoraEntradaReal,
            this.Retraso,
            this.TipoFichaje});
            this.dataGridFichajes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFichajes.Location = new System.Drawing.Point(3, 3);
            this.dataGridFichajes.MaximumSize = new System.Drawing.Size(8000, 8000);
            this.dataGridFichajes.Name = "dataGridFichajes";
            this.dataGridFichajes.ReadOnly = true;
            this.dataGridFichajes.Size = new System.Drawing.Size(596, 479);
            this.dataGridFichajes.TabIndex = 3;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // HoraLlegada
            // 
            this.HoraLlegada.HeaderText = "Hora de llegada/salida";
            this.HoraLlegada.Name = "HoraLlegada";
            this.HoraLlegada.ReadOnly = true;
            this.HoraLlegada.Width = 150;
            // 
            // HoraEntradaReal
            // 
            this.HoraEntradaReal.HeaderText = "Hora de entrada";
            this.HoraEntradaReal.Name = "HoraEntradaReal";
            this.HoraEntradaReal.ReadOnly = true;
            this.HoraEntradaReal.Width = 110;
            // 
            // Retraso
            // 
            this.Retraso.HeaderText = "Retraso";
            this.Retraso.Name = "Retraso";
            this.Retraso.ReadOnly = true;
            // 
            // TipoFichaje
            // 
            this.TipoFichaje.HeaderText = "Tipo de fichaje";
            this.TipoFichaje.Name = "TipoFichaje";
            this.TipoFichaje.ReadOnly = true;
            this.TipoFichaje.Width = 120;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(200, 200);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(108, 20);
            this.textBox3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Marcar si retraso es mayor de ";
            // 
            // buttonMarkClockIns
            // 
            this.buttonMarkClockIns.Location = new System.Drawing.Point(12, 226);
            this.buttonMarkClockIns.Name = "buttonMarkClockIns";
            this.buttonMarkClockIns.Size = new System.Drawing.Size(296, 33);
            this.buttonMarkClockIns.TabIndex = 6;
            this.buttonMarkClockIns.Text = "Marcar";
            this.buttonMarkClockIns.UseVisualStyleBackColor = true;
            this.buttonMarkClockIns.Click += new System.EventHandler(this.buttonMarkClockIns_Click);
            // 
            // comboBoxProfesor
            // 
            this.comboBoxProfesor.FormattingEnabled = true;
            this.comboBoxProfesor.Location = new System.Drawing.Point(12, 82);
            this.comboBoxProfesor.Name = "comboBoxProfesor";
            this.comboBoxProfesor.Size = new System.Drawing.Size(296, 21);
            this.comboBoxProfesor.TabIndex = 7;
            // 
            // dateTimeInit
            // 
            this.dateTimeInit.Location = new System.Drawing.Point(70, 109);
            this.dateTimeInit.Name = "dateTimeInit";
            this.dateTimeInit.Size = new System.Drawing.Size(238, 20);
            this.dateTimeInit.TabIndex = 8;
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.Location = new System.Drawing.Point(70, 135);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(238, 20);
            this.dateTimeEnd.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Desde:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Hasta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Persona:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.anunciosToolStripMenuItem,
            this.exportarInformeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(936, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // anunciosToolStripMenuItem
            // 
            this.anunciosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirAnuncioToolStripMenuItem,
            this.borrarAnuncioToolStripMenuItem});
            this.anunciosToolStripMenuItem.Name = "anunciosToolStripMenuItem";
            this.anunciosToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.anunciosToolStripMenuItem.Text = "Anuncios";
            // 
            // añadirAnuncioToolStripMenuItem
            // 
            this.añadirAnuncioToolStripMenuItem.Name = "añadirAnuncioToolStripMenuItem";
            this.añadirAnuncioToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.añadirAnuncioToolStripMenuItem.Text = "Añadir anuncio";
            this.añadirAnuncioToolStripMenuItem.Click += new System.EventHandler(this.añadirAnuncioToolStripMenuItem_Click);
            // 
            // borrarAnuncioToolStripMenuItem
            // 
            this.borrarAnuncioToolStripMenuItem.Name = "borrarAnuncioToolStripMenuItem";
            this.borrarAnuncioToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.borrarAnuncioToolStripMenuItem.Text = "Borrar anuncio";
            this.borrarAnuncioToolStripMenuItem.Click += new System.EventHandler(this.borrarAnuncioToolStripMenuItem_Click);
            // 
            // exportarInformeToolStripMenuItem
            // 
            this.exportarInformeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportarEnXLSExcelToolStripMenuItem});
            this.exportarInformeToolStripMenuItem.Name = "exportarInformeToolStripMenuItem";
            this.exportarInformeToolStripMenuItem.Size = new System.Drawing.Size(107, 20);
            this.exportarInformeToolStripMenuItem.Text = "Exportar informe";
            // 
            // exportarEnXLSExcelToolStripMenuItem
            // 
            this.exportarEnXLSExcelToolStripMenuItem.Name = "exportarEnXLSExcelToolStripMenuItem";
            this.exportarEnXLSExcelToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.exportarEnXLSExcelToolStripMenuItem.Text = "Exportar en XLS (Excel)";
            this.exportarEnXLSExcelToolStripMenuItem.Click += new System.EventHandler(this.exportarEnXLSExcelToolStripMenuItem_Click);
            // 
            // tabPanel
            // 
            this.tabPanel.Controls.Add(this.tabPage1);
            this.tabPanel.Controls.Add(this.tabPage2);
            this.tabPanel.Location = new System.Drawing.Point(314, 36);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.SelectedIndex = 0;
            this.tabPanel.Size = new System.Drawing.Size(610, 511);
            this.tabPanel.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridFichajes);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(602, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Fichajes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridFaltas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(602, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Faltas";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridFaltas
            // 
            this.dataGridFaltas.AllowUserToAddRows = false;
            this.dataGridFaltas.AllowUserToDeleteRows = false;
            this.dataGridFaltas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFaltas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridFaltas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridFaltas.Location = new System.Drawing.Point(3, 3);
            this.dataGridFaltas.Name = "dataGridFaltas";
            this.dataGridFaltas.ReadOnly = true;
            this.dataGridFaltas.Size = new System.Drawing.Size(596, 479);
            this.dataGridFaltas.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Persona";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 400;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Fecha";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // buttonGetFaltas
            // 
            this.buttonGetFaltas.Location = new System.Drawing.Point(12, 364);
            this.buttonGetFaltas.Name = "buttonGetFaltas";
            this.buttonGetFaltas.Size = new System.Drawing.Size(296, 36);
            this.buttonGetFaltas.TabIndex = 15;
            this.buttonGetFaltas.Text = "Obtener faltas";
            this.buttonGetFaltas.UseVisualStyleBackColor = true;
            this.buttonGetFaltas.Click += new System.EventHandler(this.buttonGetFaltas_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 16);
            this.label5.TabIndex = 19;
            this.label5.Text = "Hasta:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 16);
            this.label6.TabIndex = 18;
            this.label6.Text = "Desde:";
            // 
            // dateTimePickerFaltas1
            // 
            this.dateTimePickerFaltas1.Location = new System.Drawing.Point(70, 309);
            this.dateTimePickerFaltas1.Name = "dateTimePickerFaltas1";
            this.dateTimePickerFaltas1.Size = new System.Drawing.Size(238, 20);
            this.dateTimePickerFaltas1.TabIndex = 16;
            // 
            // dateTimePickerFaltas2
            // 
            this.dateTimePickerFaltas2.Location = new System.Drawing.Point(70, 335);
            this.dateTimePickerFaltas2.Name = "dateTimePickerFaltas2";
            this.dateTimePickerFaltas2.Size = new System.Drawing.Size(238, 20);
            this.dateTimePickerFaltas2.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 277);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "Informe de faltas";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 20);
            this.label8.TabIndex = 21;
            this.label8.Text = "Informe de fichajes";
            // 
            // ReportGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 559);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dateTimePickerFaltas1);
            this.Controls.Add(this.dateTimePickerFaltas2);
            this.Controls.Add(this.buttonGetFaltas);
            this.Controls.Add(this.tabPanel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dateTimeInit);
            this.Controls.Add(this.dateTimeEnd);
            this.Controls.Add(this.comboBoxProfesor);
            this.Controls.Add(this.buttonMarkClockIns);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.buttonGetClockIns);
            this.Controls.Add(this.menuStrip1);
            this.Name = "ReportGenerator";
            this.Text = "ReportGenerator";
            this.Resize += new System.EventHandler(this.ReportGenerator_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFichajes)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPanel.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFaltas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonGetClockIns;
        private System.Windows.Forms.DataGridView dataGridFichajes;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonMarkClockIns;
        private System.Windows.Forms.ComboBox comboBoxProfesor;
        private System.Windows.Forms.DateTimePicker dateTimeInit;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraLlegada;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoraEntradaReal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Retraso;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoFichaje;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem anunciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirAnuncioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrarAnuncioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarInformeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportarEnXLSExcelToolStripMenuItem;
        private System.Windows.Forms.TabControl tabPanel;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridFaltas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button buttonGetFaltas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dateTimePickerFaltas1;
        private System.Windows.Forms.DateTimePicker dateTimePickerFaltas2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

