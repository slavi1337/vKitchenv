namespace vKitchenv
{
    partial class Prijava
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
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            prijaviSeBtn = new Button();
            button1 = new Button();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(175, 123);
            label1.Name = "label1";
            label1.Size = new Size(157, 28);
            label1.TabIndex = 0;
            label1.Text = "Korisnicko ime:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 238);
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(243, 225);
            label2.Name = "label2";
            label2.Size = new Size(89, 28);
            label2.TabIndex = 1;
            label2.Text = "Lozinka:";
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            textBox1.Location = new Point(363, 117);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(223, 34);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 238);
            textBox2.Location = new Point(363, 219);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(223, 34);
            textBox2.TabIndex = 3;
            textBox2.UseSystemPasswordChar = true;
            // 
            // prijaviSeBtn
            // 
            prijaviSeBtn.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 238);
            prijaviSeBtn.ForeColor = SystemColors.Highlight;
            prijaviSeBtn.Location = new Point(363, 354);
            prijaviSeBtn.Name = "prijaviSeBtn";
            prijaviSeBtn.Size = new Size(223, 79);
            prijaviSeBtn.TabIndex = 4;
            prijaviSeBtn.Text = "Prijavi se";
            prijaviSeBtn.UseVisualStyleBackColor = true;
            prijaviSeBtn.Click += prijaviSeBtn_Click;
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            button1.Location = new Point(12, 12);
            button1.Name = "button1";
            button1.Size = new Size(100, 40);
            button1.TabIndex = 12;
            button1.Text = "Nazad";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point, 238);
            checkBox1.Location = new Point(455, 288);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(131, 24);
            checkBox1.TabIndex = 13;
            checkBox1.Text = "prikazi lozinku";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // Prijava
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Info;
            ClientSize = new Size(982, 553);
            Controls.Add(checkBox1);
            Controls.Add(button1);
            Controls.Add(prijaviSeBtn);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "Prijava";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchenv - Prijava";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button prijaviSeBtn;
        private Button button1;
        private CheckBox checkBox1;
    }
}