namespace Grayscale.P699_Form_______
{
    partial class Form2_Console
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
            this.uc_Form2Main = new Grayscale.P699_Form_______.Uc_Form2Main();
            this.SuspendLayout();
            // 
            // uc_Form2Main
            // 
            this.uc_Form2Main.Location = new System.Drawing.Point(-1, 0);
            this.uc_Form2Main.Name = "uc_Form2Main";
            this.uc_Form2Main.Size = new System.Drawing.Size(620, 761);
            this.uc_Form2Main.TabIndex = 0;
            // 
            // Ui_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.uc_Form2Main);
            this.ClientSize = new System.Drawing.Size(600, 400);
            this.Name = "Form2_Console";
            this.Text = "きふわらべ コンソール（ゲーム中は閉じないでください）";
            this.ResumeLayout(false);

        }

        #endregion

        private Uc_Form2Main uc_Form2Main;

    }
}