namespace vKitchenv
{
    partial class DodajRecept
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            dataGridView1 = new DataGridView();
            NamirnicaDGV = new DataGridViewTextBoxColumn();
            KolicinaDGV = new DataGridViewTextBoxColumn();
            JedinicaDGV = new DataGridViewTextBoxColumn();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            label4 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            textBox1.Location = new Point(200, 78);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(235, 34);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(200, 133);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(235, 81);
            textBox2.TabIndex = 4;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(200, 238);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(235, 271);
            textBox3.TabIndex = 5;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.ForeColor = SystemColors.Highlight;
            button1.Location = new Point(835, 444);
            button1.Name = "button1";
            button1.Size = new Size(135, 65);
            button1.TabIndex = 6;
            button1.Text = "dodaj";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(100, 40);
            button2.TabIndex = 7;
            button2.Text = "nazad";
            button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NamirnicaDGV, KolicinaDGV, JedinicaDGV });
            dataGridView1.Location = new Point(485, 78);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(485, 304);
            dataGridView1.TabIndex = 8;
            // 
            // NamirnicaDGV
            // 
            NamirnicaDGV.HeaderText = "Namirnica";
            NamirnicaDGV.MinimumWidth = 6;
            NamirnicaDGV.Name = "NamirnicaDGV";
            // 
            // KolicinaDGV
            // 
            KolicinaDGV.HeaderText = "Kolicina";
            KolicinaDGV.MinimumWidth = 6;
            KolicinaDGV.Name = "KolicinaDGV";
            // 
            // JedinicaDGV
            // 
            JedinicaDGV.HeaderText = "Jedinica";
            JedinicaDGV.MinimumWidth = 6;
            JedinicaDGV.Name = "JedinicaDGV";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label3.Location = new Point(109, 238);
            label3.Name = "label3";
            label3.Size = new Size(68, 28);
            label3.TabIndex = 11;
            label3.Text = "Upute";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label2.Location = new Point(124, 133);
            label2.Name = "label2";
            label2.Size = new Size(53, 28);
            label2.TabIndex = 10;
            label2.Text = "Opis";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            label1.Location = new Point(104, 78);
            label1.Name = "label1";
            label1.Size = new Size(73, 28);
            label1.TabIndex = 9;
            label1.Text = "Naslov";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(109, 527);
            label4.Name = "label4";
            label4.Size = new Size(671, 17);
            label4.TabIndex = 12;
            label4.Text = "NAPOMENA: Moguce je dodati recept bez sastojaka, pa ga poslije urediti, jer je na konceptualnom modelu 1:0..N";
            // 
            // DodajRecept
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(982, 553);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            MaximizeBox = false;
            Name = "DodajRecept";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchenv - Dodavanje Recepta";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NamirnicaDGV;
        private DataGridViewTextBoxColumn KolicinaDGV;
        private DataGridViewTextBoxColumn JedinicaDGV;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label label4;
    }
}