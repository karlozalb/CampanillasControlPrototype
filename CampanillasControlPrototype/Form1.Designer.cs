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
            this.labelHoraActualMissing = new System.Windows.Forms.Label();
            this.missingPersonalListBox = new System.Windows.Forms.ListBox();
            this.accummulatedAbsenceListBox = new System.Windows.Forms.ListBox();
            this.labelAusenciasAcumuladas = new System.Windows.Forms.Label();
            this.rightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftSplitContainer)).BeginInit();
            this.leftSplitContainer.Panel1.SuspendLayout();
            this.leftSplitContainer.Panel2.SuspendLayout();
            this.leftSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).BeginInit();
            this.rightSplitContainer.SuspendLayout();
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
            this.timeLabel.BackColor = System.Drawing.Color.SlateGray;
            this.timeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.timeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.ForeColor = System.Drawing.Color.Black;
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
            this.hourLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hourLabel.Location = new System.Drawing.Point(565, 0);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new System.Drawing.Size(278, 100);
            this.hourLabel.TabIndex = 2;
            this.hourLabel.Text = "label1";
            this.hourLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dayLabel
            // 
            this.dayLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dayLabel.Location = new System.Drawing.Point(284, 0);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(275, 100);
            this.dayLabel.TabIndex = 1;
            this.dayLabel.Text = "label1";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.BackColor = System.Drawing.Color.SlateGray;
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
            this.leftSplitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftSplitContainer.Panel1.Controls.Add(this.labelHoraActualMissing);
            this.leftSplitContainer.Panel1.Controls.Add(this.missingPersonalListBox);
            // 
            // leftSplitContainer.Panel2
            // 
            this.leftSplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.leftSplitContainer.Panel2.Controls.Add(this.accummulatedAbsenceListBox);
            this.leftSplitContainer.Panel2.Controls.Add(this.labelAusenciasAcumuladas);
            this.leftSplitContainer.Size = new System.Drawing.Size(402, 445);
            this.leftSplitContainer.SplitterDistance = 252;
            this.leftSplitContainer.SplitterWidth = 16;
            this.leftSplitContainer.TabIndex = 0;
            // 
            // labelHoraActualMissing
            // 
            this.labelHoraActualMissing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelHoraActualMissing.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHoraActualMissing.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHoraActualMissing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelHoraActualMissing.Location = new System.Drawing.Point(0, 0);
            this.labelHoraActualMissing.Name = "labelHoraActualMissing";
            this.labelHoraActualMissing.Size = new System.Drawing.Size(402, 29);
            this.labelHoraActualMissing.TabIndex = 2;
            this.labelHoraActualMissing.Text = "Ausencias en la hora actual:";
            this.labelHoraActualMissing.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // missingPersonalListBox
            // 
            this.missingPersonalListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.missingPersonalListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.missingPersonalListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.missingPersonalListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.missingPersonalListBox.ForeColor = System.Drawing.Color.DarkGray;
            this.missingPersonalListBox.FormattingEnabled = true;
            this.missingPersonalListBox.ItemHeight = 25;
            this.missingPersonalListBox.Location = new System.Drawing.Point(5, 32);
            this.missingPersonalListBox.Name = "missingPersonalListBox";
            this.missingPersonalListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.missingPersonalListBox.Size = new System.Drawing.Size(397, 200);
            this.missingPersonalListBox.TabIndex = 1;
            // 
            // accummulatedAbsenceListBox
            // 
            this.accummulatedAbsenceListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.accummulatedAbsenceListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.accummulatedAbsenceListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.accummulatedAbsenceListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.accummulatedAbsenceListBox.ForeColor = System.Drawing.Color.DarkGray;
            this.accummulatedAbsenceListBox.FormattingEnabled = true;
            this.accummulatedAbsenceListBox.ItemHeight = 25;
            this.accummulatedAbsenceListBox.Location = new System.Drawing.Point(0, 32);
            this.accummulatedAbsenceListBox.Name = "accummulatedAbsenceListBox";
            this.accummulatedAbsenceListBox.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.accummulatedAbsenceListBox.Size = new System.Drawing.Size(402, 145);
            this.accummulatedAbsenceListBox.TabIndex = 4;
            // 
            // labelAusenciasAcumuladas
            // 
            this.labelAusenciasAcumuladas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelAusenciasAcumuladas.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAusenciasAcumuladas.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAusenciasAcumuladas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.labelAusenciasAcumuladas.Location = new System.Drawing.Point(0, 0);
            this.labelAusenciasAcumuladas.Name = "labelAusenciasAcumuladas";
            this.labelAusenciasAcumuladas.Size = new System.Drawing.Size(402, 32);
            this.labelAusenciasAcumuladas.TabIndex = 3;
            this.labelAusenciasAcumuladas.Text = "Ausencias acumuladas:";
            this.labelAusenciasAcumuladas.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.rightSplitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            // 
            // rightSplitContainer.Panel2
            // 
            this.rightSplitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rightSplitContainer.Size = new System.Drawing.Size(404, 445);
            this.rightSplitContainer.SplitterDistance = 350;
            this.rightSplitContainer.SplitterWidth = 16;
            this.rightSplitContainer.TabIndex = 0;
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(846, 100);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SlateGray;
            this.ClientSize = new System.Drawing.Size(846, 554);
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
            ((System.ComponentModel.ISupportInitialize)(this.rightSplitContainer)).EndInit();
            this.rightSplitContainer.ResumeLayout(false);
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
    }
}

