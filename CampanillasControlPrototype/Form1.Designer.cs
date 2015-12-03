namespace CampanillasControlPrototype
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.dateTimer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.hourLabel = new System.Windows.Forms.Label();
            this.dayLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.leftSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.missingPersonalListBox = new System.Windows.Forms.ListBox();
            this.labelHoraActualMissing = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.accummulatedAbsenceListBox = new System.Windows.Forms.ListBox();
            this.labelAusenciasAcumuladas = new System.Windows.Forms.Label();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tablonAnunciosLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.adInfoText = new System.Windows.Forms.Label();
            this.adText = new System.Windows.Forms.Label();
            this.splitGuardiayDirectiva = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.executiveListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.guardListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.Panel1.SuspendLayout();
            this.rightSplitContainer.Panel2.SuspendLayout();
            this.rightSplitContainer.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGuardiayDirectiva)).BeginInit();
            this.splitGuardiayDirectiva.Panel1.SuspendLayout();
            this.splitGuardiayDirectiva.Panel2.SuspendLayout();
            this.splitGuardiayDirectiva.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimer
            // 
            this.dateTimer.Enabled = true;
            this.dateTimer.Interval = 1000;
            this.dateTimer.Tick += new System.EventHandler(this.dateTimer_Tick);
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.timeLabel.Location = new System.Drawing.Point(460, 0);
            this.timeLabel.MinimumSize = new System.Drawing.Size(300, 80);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(323, 94);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "label1";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hourLabel
            // 
            this.hourLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hourLabel.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hourLabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.hourLabel.Location = new System.Drawing.Point(131, 0);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(323, 94);
            this.hourLabel.TabIndex = 2;
            this.hourLabel.Text = "label1";
            this.hourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dayLabel
            // 
            this.dayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayLabel.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayLabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dayLabel.Location = new System.Drawing.Point(789, 0);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(325, 94);
            this.dayLabel.TabIndex = 1;
            this.dayLabel.Text = "label1";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.hourLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dayLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1117, 94);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Image = global::CampanillasControlPrototype.Properties.Resources.logocampanillas1;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(122, 94);
            this.label10.TabIndex = 3;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.mainSplitContainer, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel9, 0, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(1123, 659);
            this.tableLayoutPanel8.TabIndex = 5;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.mainSplitContainer.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::CampanillasControlPrototype.Properties.Settings.Default, "MainSplitSplitter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(3, 103);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(1117, 478);
            this.mainSplitContainer.SplitterDistance = global::CampanillasControlPrototype.Properties.Settings.Default.MainSplitSplitter;
            this.mainSplitContainer.SplitterWidth = 16;
            this.mainSplitContainer.TabIndex = 3;
            // 
            // leftSplitContainer
            // 
            this.leftSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.leftSplitContainer.Name = "leftSplitContainer";
            this.leftSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // leftSplitContainer.Panel1
            // 
            this.leftSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.leftSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel2);
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.leftSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.leftSplitContainer.Size = new System.Drawing.Size(653, 478);
            this.leftSplitContainer.SplitterDistance = 273;
            this.leftSplitContainer.SplitterWidth = 16;
            this.leftSplitContainer.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.missingPersonalListBox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.labelHoraActualMissing, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(653, 273);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // missingPersonalListBox
            // 
            this.missingPersonalListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.missingPersonalListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.missingPersonalListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.missingPersonalListBox.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingPersonalListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.missingPersonalListBox.FormattingEnabled = true;
            this.missingPersonalListBox.IntegralHeight = false;
            this.missingPersonalListBox.ItemHeight = 29;
            this.missingPersonalListBox.Location = new System.Drawing.Point(3, 41);
            this.missingPersonalListBox.Name = "missingPersonalListBox";
            this.missingPersonalListBox.ScrollAlwaysVisible = true;
            this.missingPersonalListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.missingPersonalListBox.Size = new System.Drawing.Size(647, 229);
            this.missingPersonalListBox.TabIndex = 1;
            // 
            // labelHoraActualMissing
            // 
            this.labelHoraActualMissing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(66)))));
            this.labelHoraActualMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHoraActualMissing.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoraActualMissing.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelHoraActualMissing.Location = new System.Drawing.Point(0, 0);
            this.labelHoraActualMissing.Margin = new System.Windows.Forms.Padding(0);
            this.labelHoraActualMissing.Name = "labelHoraActualMissing";
            this.labelHoraActualMissing.Size = new System.Drawing.Size(653, 38);
            this.labelHoraActualMissing.TabIndex = 2;
            this.labelHoraActualMissing.Text = "Ausencias en la hora actual";
            this.labelHoraActualMissing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.accummulatedAbsenceListBox, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.labelAusenciasAcumuladas, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(653, 189);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // accummulatedAbsenceListBox
            // 
            this.accummulatedAbsenceListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.accummulatedAbsenceListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.accummulatedAbsenceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accummulatedAbsenceListBox.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accummulatedAbsenceListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.accummulatedAbsenceListBox.FormattingEnabled = true;
            this.accummulatedAbsenceListBox.IntegralHeight = false;
            this.accummulatedAbsenceListBox.ItemHeight = 29;
            this.accummulatedAbsenceListBox.Location = new System.Drawing.Point(3, 39);
            this.accummulatedAbsenceListBox.Name = "accummulatedAbsenceListBox";
            this.accummulatedAbsenceListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.accummulatedAbsenceListBox.Size = new System.Drawing.Size(647, 147);
            this.accummulatedAbsenceListBox.TabIndex = 4;
            // 
            // labelAusenciasAcumuladas
            // 
            this.labelAusenciasAcumuladas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(66)))));
            this.labelAusenciasAcumuladas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelAusenciasAcumuladas.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAusenciasAcumuladas.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.labelAusenciasAcumuladas.Location = new System.Drawing.Point(0, 0);
            this.labelAusenciasAcumuladas.Margin = new System.Windows.Forms.Padding(0);
            this.labelAusenciasAcumuladas.Name = "labelAusenciasAcumuladas";
            this.labelAusenciasAcumuladas.Size = new System.Drawing.Size(653, 36);
            this.labelAusenciasAcumuladas.TabIndex = 3;
            this.labelAusenciasAcumuladas.Text = "Ausencias acumuladas";
            this.labelAusenciasAcumuladas.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rightSplitContainer
            // 
            this.rightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.rightSplitContainer.Name = "rightSplitContainer";
            this.rightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // rightSplitContainer.Panel1
            // 
            this.rightSplitContainer.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rightSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel4);
            this.rightSplitContainer.Panel1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.rightSplitContainer.Panel2.Controls.Add(this.splitGuardiayDirectiva);
            this.rightSplitContainer.Size = new System.Drawing.Size(448, 478);
            this.rightSplitContainer.SplitterDistance = 216;
            this.rightSplitContainer.SplitterWidth = 16;
            this.rightSplitContainer.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.tablonAnunciosLabel, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(448, 216);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // tablonAnunciosLabel
            // 
            this.tablonAnunciosLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(66)))));
            this.tablonAnunciosLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablonAnunciosLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tablonAnunciosLabel.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tablonAnunciosLabel.Location = new System.Drawing.Point(0, 0);
            this.tablonAnunciosLabel.Margin = new System.Windows.Forms.Padding(0);
            this.tablonAnunciosLabel.Name = "tablonAnunciosLabel";
            this.tablonAnunciosLabel.Size = new System.Drawing.Size(448, 38);
            this.tablonAnunciosLabel.TabIndex = 3;
            this.tablonAnunciosLabel.Text = "Tablón de anuncios";
            this.tablonAnunciosLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.adInfoText, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.adText, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 41);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.803922F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.19608F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(442, 172);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // adInfoText
            // 
            this.adInfoText.AutoSize = true;
            this.adInfoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adInfoText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adInfoText.Location = new System.Drawing.Point(3, 0);
            this.adInfoText.Name = "adInfoText";
            this.adInfoText.Size = new System.Drawing.Size(436, 16);
            this.adInfoText.TabIndex = 0;
            this.adInfoText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adText
            // 
            this.adText.AutoSize = true;
            this.adText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adText.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adText.Location = new System.Drawing.Point(3, 16);
            this.adText.Name = "adText";
            this.adText.Size = new System.Drawing.Size(436, 156);
            this.adText.TabIndex = 1;
            // 
            // splitGuardiayDirectiva
            // 
            this.splitGuardiayDirectiva.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.splitGuardiayDirectiva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGuardiayDirectiva.Location = new System.Drawing.Point(0, 0);
            this.splitGuardiayDirectiva.Name = "splitGuardiayDirectiva";
            this.splitGuardiayDirectiva.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGuardiayDirectiva.Panel1
            // 
            this.splitGuardiayDirectiva.Panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitGuardiayDirectiva.Panel1.Controls.Add(this.tableLayoutPanel6);
            // 
            // splitGuardiayDirectiva.Panel2
            // 
            this.splitGuardiayDirectiva.Panel2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.splitGuardiayDirectiva.Panel2.Controls.Add(this.tableLayoutPanel7);
            this.splitGuardiayDirectiva.Size = new System.Drawing.Size(448, 246);
            this.splitGuardiayDirectiva.SplitterDistance = 117;
            this.splitGuardiayDirectiva.SplitterWidth = 16;
            this.splitGuardiayDirectiva.TabIndex = 0;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.executiveListBox, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(448, 117);
            this.tableLayoutPanel6.TabIndex = 4;
            // 
            // executiveListBox
            // 
            this.executiveListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.executiveListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.executiveListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.executiveListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.executiveListBox.Enabled = false;
            this.executiveListBox.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executiveListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.executiveListBox.FormattingEnabled = true;
            this.executiveListBox.IntegralHeight = false;
            this.executiveListBox.ItemHeight = 29;
            this.executiveListBox.Location = new System.Drawing.Point(3, 41);
            this.executiveListBox.Name = "executiveListBox";
            this.executiveListBox.ScrollAlwaysVisible = true;
            this.executiveListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.executiveListBox.Size = new System.Drawing.Size(442, 73);
            this.executiveListBox.TabIndex = 1;
            this.executiveListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.directListBox_DrawItem);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(66)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(448, 38);
            this.label1.TabIndex = 2;
            this.label1.Text = "Equipo directivo";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.tableLayoutPanel7.ColumnCount = 1;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.guardListBox, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(448, 113);
            this.tableLayoutPanel7.TabIndex = 4;
            // 
            // guardListBox
            // 
            this.guardListBox.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.guardListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.guardListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guardListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guardListBox.Enabled = false;
            this.guardListBox.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guardListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.guardListBox.FormattingEnabled = true;
            this.guardListBox.IntegralHeight = false;
            this.guardListBox.ItemHeight = 29;
            this.guardListBox.Location = new System.Drawing.Point(3, 41);
            this.guardListBox.Name = "guardListBox";
            this.guardListBox.ScrollAlwaysVisible = true;
            this.guardListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.guardListBox.Size = new System.Drawing.Size(442, 69);
            this.guardListBox.TabIndex = 1;
            this.guardListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.guardListBox_DrawItem);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(43)))), ((int)(((byte)(66)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(448, 38);
            this.label2.TabIndex = 2;
            this.label2.Text = "Profesores de guardia";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 650F));
            this.tableLayoutPanel9.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.tableLayoutPanel10, 1, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(3, 587);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(1117, 69);
            this.tableLayoutPanel9.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(461, 69);
            this.label3.TabIndex = 0;
            this.label3.Text = "Leyenda de colores de directivos y profesores de guardia";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.660377F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.33962F));
            this.tableLayoutPanel10.Controls.Add(this.label9, 1, 2);
            this.tableLayoutPanel10.Controls.Add(this.label8, 1, 1);
            this.tableLayoutPanel10.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel10.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.label5, 0, 1);
            this.tableLayoutPanel10.Controls.Add(this.label7, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(470, 3);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 3;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(644, 63);
            this.tableLayoutPanel10.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label9.Location = new System.Drawing.Point(39, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(602, 21);
            this.label9.TabIndex = 6;
            this.label9.Text = "(Rojo) Directivo/a o profesor/a de guardia no presentes";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label8.Location = new System.Drawing.Point(39, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(602, 21);
            this.label8.TabIndex = 5;
            this.label8.Text = "(Azul) Directivo/a presente con función de guardia directiva";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Red;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 21);
            this.label6.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 21);
            this.label4.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Blue;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(3, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 21);
            this.label5.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label7.Location = new System.Drawing.Point(39, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(602, 21);
            this.label7.TabIndex = 4;
            this.label7.Text = "(Negro) Directivo/a presente con función directiva - Profesor/a de guardia presen" +
    "te";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1123, 659);
            this.Controls.Add(this.tableLayoutPanel8);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "PresenceInformer v1.1.2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.leftSplitContainer.Panel1.ResumeLayout(false);
            this.leftSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).EndInit();
            this.leftSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.rightSplitContainer.Panel1.ResumeLayout(false);
            this.rightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.splitGuardiayDirectiva.Panel1.ResumeLayout(false);
            this.splitGuardiayDirectiva.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGuardiayDirectiva)).EndInit();
            this.splitGuardiayDirectiva.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.SplitContainer leftSplitContainer;
        private System.Windows.Forms.SplitContainer rightSplitContainer;
        private System.Windows.Forms.Timer dateTimer;
        private System.Windows.Forms.ListBox missingPersonalListBox;
        private System.Windows.Forms.Label timeLabel;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
        private System.Windows.Forms.Label labelHoraActualMissing;
        private System.Windows.Forms.Label labelAusenciasAcumuladas;
        private System.Windows.Forms.ListBox accummulatedAbsenceListBox;
        private System.Windows.Forms.Label hourLabel;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label tablonAnunciosLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label adInfoText;
        private System.Windows.Forms.Label adText;
        private System.Windows.Forms.SplitContainer splitGuardiayDirectiva;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.ListBox executiveListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.ListBox guardListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
    }
}

