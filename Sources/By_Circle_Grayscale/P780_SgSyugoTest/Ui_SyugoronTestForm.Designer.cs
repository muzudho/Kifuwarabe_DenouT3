namespace Grayscale.P780_SgSyugoTest
{
    partial class Ui_SyugoronTestForm
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
            this.ui_SyugoronTestPanel = new Grayscale.P780_SgSyugoTest.Ui_SyugoronTestPanel();
            this.SuspendLayout();
            // 
            // ui_Main1
            // 
            this.ui_SyugoronTestPanel.Location = new System.Drawing.Point(12, 12);
            this.ui_SyugoronTestPanel.Name = "ui_Main1";
            this.ui_SyugoronTestPanel.Size = new System.Drawing.Size(849, 655);
            this.ui_SyugoronTestPanel.TabIndex = 0;
            // 
            // Ui_Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 679);
            this.Controls.Add(this.ui_SyugoronTestPanel);
            this.Name = "Ui_Form1";
            this.Text = "集合論ライブラリー テスター";
            this.ResumeLayout(false);

        }

        #endregion

        private Ui_SyugoronTestPanel ui_SyugoronTestPanel;
    }
}