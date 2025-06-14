namespace vKitchenv
{
    partial class First
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
            prijavaBtn = new Button();
            registracijaBtn = new Button();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            label2 = new Label();
            label3 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // prijavaBtn
            // 
            prijavaBtn.Dock = DockStyle.Bottom;
            prijavaBtn.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            prijavaBtn.ForeColor = SystemColors.ControlText;
            prijavaBtn.Location = new Point(0, 464);
            prijavaBtn.Name = "prijavaBtn";
            prijavaBtn.Size = new Size(490, 89);
            prijavaBtn.TabIndex = 0;
            prijavaBtn.Text = "Prijavi se";
            prijavaBtn.UseVisualStyleBackColor = true;
            prijavaBtn.Click += prijavaBtn_Click;
            // 
            // registracijaBtn
            // 
            registracijaBtn.Dock = DockStyle.Bottom;
            registracijaBtn.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            registracijaBtn.ForeColor = SystemColors.ControlText;
            registracijaBtn.Location = new Point(0, 464);
            registracijaBtn.Name = "registracijaBtn";
            registracijaBtn.Size = new Size(490, 89);
            registracijaBtn.TabIndex = 1;
            registracijaBtn.Text = "Registruj se";
            registracijaBtn.UseVisualStyleBackColor = true;
            registracijaBtn.Click += registracijaBtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Info;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(prijavaBtn);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(490, 553);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label1.Location = new Point(146, 120);
            label1.Name = "label1";
            label1.Size = new Size(195, 31);
            label1.TabIndex = 1;
            label1.Text = "Već imate nalog?";
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaption;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(registracijaBtn);
            panel2.Dock = DockStyle.Right;
            panel2.Location = new Point(492, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(490, 553);
            panel2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold);
            label2.Location = new Point(151, 120);
            label2.Name = "label2";
            label2.Size = new Size(186, 31);
            label2.TabIndex = 2;
            label2.Text = "Napravite nalog";
            // 
            // label3
            // 
            label3.Font = new Font("Segoe UI", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 238);
            label3.Location = new Point(112, 162);
            label3.Name = "label3";
            label3.Size = new Size(257, 72);
            label3.TabIndex = 2;
            label3.Text = "PREPORUKA: Koristiti nalog slavi1337:pw za testiranje, posto su za njega vec dodati svi parametri za potpuno testiranje";
            // 
            // First
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(panel2);
            Controls.Add(panel1);
            MaximizeBox = false;
            Name = "First";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "vKitchenv";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button prijavaBtn;
        private Button registracijaBtn;
        private Panel panel1;
        private Label label1;
        private Panel panel2;
        private Label label2;
        private Label label3;
    }
}