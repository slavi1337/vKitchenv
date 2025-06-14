namespace vKitchenv
{
    partial class Notifikacije
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
            buttonNazad = new Button();
            dataGridViewNotifikacije = new DataGridView();
            colProcitano = new DataGridViewCheckBoxColumn();
            colVrijeme = new DataGridViewTextBoxColumn();
            colTekst = new DataGridViewTextBoxColumn();
            colAutor = new DataGridViewTextBoxColumn();
            colNotifID = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridViewNotifikacije).BeginInit();
            SuspendLayout();
            // 
            // buttonNazad
            // 
            buttonNazad.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            buttonNazad.Location = new Point(12, 12);
            buttonNazad.Name = "buttonNazad";
            buttonNazad.Size = new Size(100, 40);
            buttonNazad.TabIndex = 1;
            buttonNazad.Text = "nazad";
            buttonNazad.UseVisualStyleBackColor = true;
            // 
            // dataGridViewNotifikacije
            // 
            dataGridViewNotifikacije.AllowUserToAddRows = false;
            dataGridViewNotifikacije.AllowUserToDeleteRows = false;
            dataGridViewNotifikacije.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewNotifikacije.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewNotifikacije.Columns.AddRange(new DataGridViewColumn[] { colProcitano, colVrijeme, colTekst, colAutor, colNotifID });
            dataGridViewNotifikacije.Location = new Point(275, 12);
            dataGridViewNotifikacije.Name = "dataGridViewNotifikacije";
            dataGridViewNotifikacije.RowHeadersWidth = 51;
            dataGridViewNotifikacije.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewNotifikacije.Size = new Size(695, 459);
            dataGridViewNotifikacije.TabIndex = 2;
            // 
            // colProcitano
            // 
            colProcitano.FalseValue = "false";
            colProcitano.HeaderText = "Procitano";
            colProcitano.MinimumWidth = 6;
            colProcitano.Name = "colProcitano";
            colProcitano.Resizable = DataGridViewTriState.False;
            colProcitano.SortMode = DataGridViewColumnSortMode.Automatic;
            colProcitano.TrueValue = "true";
            colProcitano.Width = 101;
            // 
            // colVrijeme
            // 
            colVrijeme.HeaderText = "Datum i vrijeme";
            colVrijeme.MinimumWidth = 6;
            colVrijeme.Name = "colVrijeme";
            colVrijeme.ReadOnly = true;
            colVrijeme.Width = 144;
            // 
            // colTekst
            // 
            colTekst.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTekst.HeaderText = "Tekst";
            colTekst.MinimumWidth = 6;
            colTekst.Name = "colTekst";
            colTekst.ReadOnly = true;
            // 
            // colAutor
            // 
            colAutor.HeaderText = "Autor";
            colAutor.MinimumWidth = 6;
            colAutor.Name = "colAutor";
            colAutor.ReadOnly = true;
            colAutor.Width = 75;
            // 
            // colNotifID
            // 
            colNotifID.HeaderText = "Column1";
            colNotifID.MinimumWidth = 6;
            colNotifID.Name = "colNotifID";
            colNotifID.Visible = false;
            colNotifID.Width = 97;
            // 
            // Notifikacije
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(982, 553);
            Controls.Add(dataGridViewNotifikacije);
            Controls.Add(buttonNazad);
            MaximizeBox = false;
            Name = "Notifikacije";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchenv - Notifikacije";
            ((System.ComponentModel.ISupportInitialize)dataGridViewNotifikacije).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Button buttonNazad;
        private DataGridView dataGridViewNotifikacije;
        private DataGridViewCheckBoxColumn colProcitano;
        private DataGridViewTextBoxColumn colVrijeme;
        private DataGridViewTextBoxColumn colTekst;
        private DataGridViewTextBoxColumn colAutor;
        private DataGridViewTextBoxColumn colNotifID;
    }
}