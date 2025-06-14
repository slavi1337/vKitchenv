namespace vKitchenv
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            textBox1 = new TextBox();
            button3 = new Button();
            button4 = new Button();
            buttonNotifikacije = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.BackgroundColor = SystemColors.Window;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Bottom;
            dataGridView1.Location = new Point(0, 331);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(982, 222);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(12, 12);
            label1.Name = "label1";
            label1.Size = new Size(70, 28);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.ForeColor = SystemColors.Highlight;
            button1.Location = new Point(12, 151);
            button1.Name = "button1";
            button1.Size = new Size(115, 66);
            button1.TabIndex = 2;
            button1.Text = "Recepti";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button2.Location = new Point(855, 251);
            button2.Name = "button2";
            button2.Size = new Size(115, 66);
            button2.TabIndex = 3;
            button2.Text = "Sacuvaj stanje";
            button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            textBox1.Location = new Point(368, 32);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(182, 38);
            textBox1.TabIndex = 4;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            button3.Location = new Point(566, 26);
            button3.Name = "button3";
            button3.Size = new Size(103, 48);
            button3.TabIndex = 5;
            button3.Text = "pretrazi korisnika";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button4.ForeColor = SystemColors.Highlight;
            button4.Location = new Point(12, 54);
            button4.Name = "button4";
            button4.Size = new Size(115, 66);
            button4.TabIndex = 6;
            button4.Text = "Moj profil";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // buttonNotifikacije
            // 
            buttonNotifikacije.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            buttonNotifikacije.Location = new Point(844, 12);
            buttonNotifikacije.Name = "buttonNotifikacije";
            buttonNotifikacije.Size = new Size(126, 58);
            buttonNotifikacije.TabIndex = 7;
            buttonNotifikacije.Text = "Notifikacije (0)";
            buttonNotifikacije.UseVisualStyleBackColor = true;
            buttonNotifikacije.Click += buttonNotifikacije_Click;
            // 
            // label2
            // 
            label2.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.Location = new Point(679, 251);
            label2.Name = "label2";
            label2.Size = new Size(170, 66);
            label2.TabIndex = 8;
            label2.Text = "Ukoliko promijenite stanje namirnica, pritisnuti dugme pored";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label3.Location = new Point(12, 286);
            label3.Name = "label3";
            label3.Size = new Size(233, 31);
            label3.TabIndex = 9;
            label3.Text = "Namirnice u kuhinji:";
            // 
            // label4
            // 
            label4.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label4.Location = new Point(12, 230);
            label4.Name = "label4";
            label4.Size = new Size(303, 56);
            label4.TabIndex = 10;
            label4.Text = "NAPOMENA: Samo namirnice koje postoje u tabeli \"namirnica\" se mogu dodati i to sa jedinicom koja postoji kao JedinicaID u tabeli \"jedinica\"";
            // 
            // label5
            // 
            label5.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label5.Location = new Point(331, 272);
            label5.Name = "label5";
            label5.Size = new Size(303, 56);
            label5.TabIndex = 11;
            label5.Text = "NAPOMENA: Dodavanje postojece namirnice overriduje postojecu kolicinu, nije sabiranje kao sto je kod pravljenja recepta oduzimanje";
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(331, 216);
            label6.Name = "label6";
            label6.Size = new Size(303, 56);
            label6.TabIndex = 12;
            label6.Text = "NAPOMENA: Namirnica se brise tako sto se selektuje citav red, pa onda delete na tastaturi, pa onda sacuvaj stanje";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(982, 553);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(buttonNotifikacije);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchend - Pocetna";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Button button1;
        private Button button2;
        private TextBox textBox1;
        private Button button3;
        private Button button4;
        private Button buttonNotifikacije;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
