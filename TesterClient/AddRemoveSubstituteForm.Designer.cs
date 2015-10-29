namespace TesterClient
{
    partial class AddRemoveSubstituteForm
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
            this.comboBoxSubstitute = new System.Windows.Forms.ComboBox();
            this.comboBoxMissing = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.addSubstituteButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.listBoxSubstitute = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxSubstitute
            // 
            this.comboBoxSubstitute.FormattingEnabled = true;
            this.comboBoxSubstitute.Location = new System.Drawing.Point(145, 18);
            this.comboBoxSubstitute.Name = "comboBoxSubstitute";
            this.comboBoxSubstitute.Size = new System.Drawing.Size(335, 21);
            this.comboBoxSubstitute.TabIndex = 0;
            // 
            // comboBoxMissing
            // 
            this.comboBoxMissing.FormattingEnabled = true;
            this.comboBoxMissing.Location = new System.Drawing.Point(145, 47);
            this.comboBoxMissing.Name = "comboBoxMissing";
            this.comboBoxMissing.Size = new System.Drawing.Size(335, 21);
            this.comboBoxMissing.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profesor sustituto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Profesor sustituido:";
            // 
            // addSubstituteButton
            // 
            this.addSubstituteButton.Location = new System.Drawing.Point(16, 74);
            this.addSubstituteButton.Name = "addSubstituteButton";
            this.addSubstituteButton.Size = new System.Drawing.Size(464, 26);
            this.addSubstituteButton.TabIndex = 4;
            this.addSubstituteButton.Text = "Añadir";
            this.addSubstituteButton.UseVisualStyleBackColor = true;
            this.addSubstituteButton.Click += new System.EventHandler(this.addSubstituteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(16, 286);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(225, 27);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancelar";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // listBoxSubstitute
            // 
            this.listBoxSubstitute.FormattingEnabled = true;
            this.listBoxSubstitute.Location = new System.Drawing.Point(16, 107);
            this.listBoxSubstitute.Name = "listBoxSubstitute";
            this.listBoxSubstitute.Size = new System.Drawing.Size(464, 173);
            this.listBoxSubstitute.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(255, 286);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 27);
            this.button1.TabIndex = 7;
            this.button1.Text = "Borrar sustitución";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddRemoveSubstituteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 321);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBoxSubstitute);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addSubstituteButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxMissing);
            this.Controls.Add(this.comboBoxSubstitute);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddRemoveSubstituteForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Añadir sustituto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxSubstitute;
        private System.Windows.Forms.ComboBox comboBoxMissing;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button addSubstituteButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ListBox listBoxSubstitute;
        private System.Windows.Forms.Button button1;
    }
}