namespace Grayscale.P699_Form_______
{
    partial class Uc_Form2Main
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtInputarea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOutputarea = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInputarea
            // 
            this.txtInputarea.Location = new System.Drawing.Point(22, 15);
            this.txtInputarea.Multiline = true;
            this.txtInputarea.Name = "txtInputarea";
            this.txtInputarea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInputarea.Size = new System.Drawing.Size(404, 48);
            this.txtInputarea.TabIndex = 1;
            this.txtInputarea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInputarea_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "入力欄";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 157);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "出力欄";
            // 
            // txtOutputarea
            // 
            this.txtOutputarea.Location = new System.Drawing.Point(22, 172);
            this.txtOutputarea.Multiline = true;
            this.txtOutputarea.Name = "txtOutputarea";
            this.txtOutputarea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutputarea.Size = new System.Drawing.Size(404, 107);
            this.txtOutputarea.TabIndex = 4;
            this.txtOutputarea.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutputarea_KeyDown);
            // 
            // Uc_Form2Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtOutputarea);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInputarea);
            this.Name = "Uc_Form2Main";
            this.Size = new System.Drawing.Size(454, 340);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Uc_Form2Main_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Uc_Form2Main_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Uc_Form2Main_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInputarea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOutputarea;
    }
}
