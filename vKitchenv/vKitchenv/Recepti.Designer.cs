namespace vKitchenv
{
    partial class Recepti
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
            checkBox1 = new CheckBox();
            listBox1 = new ListBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button2 = new Button();
            button1 = new Button();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            button3 = new Button();
            textBox1 = new TextBox();
            button4 = new Button();
            label1 = new Label();
            button5 = new Button();
            label6 = new Label();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            checkBox1.Location = new Point(234, 418);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(306, 27);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Samo recepti koje mogu napraviti";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.BackColor = SystemColors.Info;
            listBox1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 238);
            listBox1.FormattingEnabled = true;
            listBox1.Items.AddRange(new object[] { "ovdje ce se prikazati", "sastojci koji nedostaju", "za selektovani recept", "", "ako sve imate,", "ovo ce ostati prazno" });
            listBox1.Location = new Point(12, 397);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(189, 144);
            listBox1.TabIndex = 3;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(546, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(436, 553);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button2.Location = new Point(12, 12);
            button2.Name = "button2";
            button2.Size = new Size(100, 40);
            button2.TabIndex = 5;
            button2.Text = "nazad";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            button1.ForeColor = SystemColors.Highlight;
            button1.Location = new Point(12, 223);
            button1.Name = "button1";
            button1.Size = new Size(141, 68);
            button1.TabIndex = 6;
            button1.Text = "Dodaj recept";
            button1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            checkBox2.Location = new Point(234, 453);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(129, 27);
            checkBox2.TabIndex = 7;
            checkBox2.Text = "Moji recepti";
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            checkBox3.Location = new Point(234, 488);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(161, 27);
            checkBox3.TabIndex = 8;
            checkBox3.Text = "Omiljeni recepti";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button3.ForeColor = SystemColors.Highlight;
            button3.Location = new Point(12, 88);
            button3.Name = "button3";
            button3.Size = new Size(141, 86);
            button3.TabIndex = 9;
            button3.Text = "Istorija napravljenih recepata";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            textBox1.Location = new Point(260, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(160, 34);
            textBox1.TabIndex = 10;
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            button4.Location = new Point(426, 16);
            button4.Name = "button4";
            button4.Size = new Size(93, 51);
            button4.TabIndex = 11;
            button4.Text = "pretrazi recepte";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.Location = new Point(234, 355);
            label1.Name = "label1";
            label1.Size = new Size(77, 31);
            label1.TabIndex = 12;
            label1.Text = "Filteri";
            // 
            // button5
            // 
            button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button5.Location = new Point(426, 223);
            button5.Name = "button5";
            button5.Size = new Size(93, 68);
            button5.TabIndex = 13;
            button5.Text = "Osvjezi";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // label6
            // 
            label6.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label6.Location = new Point(260, 88);
            label6.Name = "label6";
            label6.Size = new Size(171, 68);
            label6.TabIndex = 14;
            label6.Text = "NAPOMENA: Recepti se filtriraju ili po naslovu ili po filterima ispod, NE OBOJE";
            // 
            // Recepti
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(982, 553);
            Controls.Add(label6);
            Controls.Add(button5);
            Controls.Add(label1);
            Controls.Add(button4);
            Controls.Add(textBox1);
            Controls.Add(button3);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(listBox1);
            Controls.Add(checkBox1);
            MaximizeBox = false;
            Name = "Recepti";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchenv - Recepti";
            FormClosing += Recepti_FormClosing;
            Load += Recepti_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox1;
        private ListBox listBox1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button2;
        private Button button1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
        private Button button3;
        private TextBox textBox1;
        private Button button4;
        private Label label1;
        private Button button5;
        private Label label6;
    }
}