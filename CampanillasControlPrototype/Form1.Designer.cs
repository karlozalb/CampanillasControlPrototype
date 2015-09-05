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
            this.dateTimer = new System.Windows.Forms.Timer(this.components);
            this.timeLabel = new System.Windows.Forms.Label();
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.hourLabel = new System.Windows.Forms.Label();
            this.dayLabel = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
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
            this.rightSplitContainer.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.timeLabel.Location = new System.Drawing.Point(3, 0);
            this.timeLabel.MinimumSize = new System.Drawing.Size(300, 80);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(300, 100);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "label1";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // hourLabel
            // 
            this.hourLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hourLabel.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hourLabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.hourLabel.Location = new System.Drawing.Point(567, 0);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(277, 100);
            this.hourLabel.TabIndex = 2;
            this.hourLabel.Text = "label1";
            this.hourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dayLabel
            // 
            this.dayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayLabel.Font = new System.Drawing.Font("Tahoma", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayLabel.ForeColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.dayLabel.Location = new System.Drawing.Point(285, 0);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(276, 100);
            this.dayLabel.TabIndex = 1;
            this.dayLabel.Text = "label1";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.mainSplitContainer.DataBindings.Add(new System.Windows.Forms.Binding("SplitterDistance", global::CampanillasControlPrototype.Properties.Settings.Default, "MainSplitSplitter", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.mainSplitContainer.Location = new System.Drawing.Point(12, 97);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.leftSplitContainer);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.rightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(822, 445);
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
            this.leftSplitContainer.Size = new System.Drawing.Size(482, 445);
            this.leftSplitContainer.SplitterDistance = 255;
            this.leftSplitContainer.SplitterWidth = 16;
            this.leftSplitContainer.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
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
            this.tableLayoutPanel2.Size = new System.Drawing.Size(482, 255);
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
            this.missingPersonalListBox.ItemHeight = 29;
            this.missingPersonalListBox.Location = new System.Drawing.Point(3, 41);
            this.missingPersonalListBox.Name = "missingPersonalListBox";
            this.missingPersonalListBox.ScrollAlwaysVisible = true;
            this.missingPersonalListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.missingPersonalListBox.Size = new System.Drawing.Size(476, 211);
            this.missingPersonalListBox.TabIndex = 1;
            this.missingPersonalListBox.SelectedIndexChanged += new System.EventHandler(this.missingPersonalListBox_SelectedIndexChanged);
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
            this.labelHoraActualMissing.Size = new System.Drawing.Size(482, 38);
            this.labelHoraActualMissing.TabIndex = 2;
            this.labelHoraActualMissing.Text = "Ausencias en la hora actual";
            this.labelHoraActualMissing.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(482, 174);
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
            this.accummulatedAbsenceListBox.ItemHeight = 29;
            this.accummulatedAbsenceListBox.Location = new System.Drawing.Point(3, 39);
            this.accummulatedAbsenceListBox.Name = "accummulatedAbsenceListBox";
            this.accummulatedAbsenceListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.accummulatedAbsenceListBox.Size = new System.Drawing.Size(476, 132);
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
            this.labelAusenciasAcumuladas.Size = new System.Drawing.Size(482, 36);
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
            this.rightSplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightSplitContainer.Size = new System.Drawing.Size(324, 445);
            this.rightSplitContainer.SplitterDistance = 350;
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(324, 350);
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
            this.tablonAnunciosLabel.Size = new System.Drawing.Size(324, 38);
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
            this.tableLayoutPanel5.Size = new System.Drawing.Size(318, 306);
            this.tableLayoutPanel5.TabIndex = 4;
            // 
            // adInfoText
            // 
            this.adInfoText.AutoSize = true;
            this.adInfoText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adInfoText.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adInfoText.Location = new System.Drawing.Point(3, 0);
            this.adInfoText.Name = "adInfoText";
            this.adInfoText.Size = new System.Drawing.Size(312, 29);
            this.adInfoText.TabIndex = 0;
            this.adInfoText.Text = "label1";
            this.adInfoText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // adText
            // 
            this.adText.AutoSize = true;
            this.adText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adText.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.adText.Location = new System.Drawing.Point(3, 29);
            this.adText.Name = "adText";
            this.adText.Size = new System.Drawing.Size(312, 277);
            this.adText.TabIndex = 1;
            this.adText.Text = "label1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.hourLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dayLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.timeLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(847, 100);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(83)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(847, 555);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.mainSplitContainer);
            this.Name = "MainWindow";
            this.Text = "PersonalControl v0.2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
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
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
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
    }
}

