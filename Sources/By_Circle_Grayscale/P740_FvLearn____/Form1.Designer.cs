namespace Grayscale.P743_FvLearn____
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.uc_Main1 = new P743_FvLearn____.Uc_Main();
            this.SuspendLayout();
            // 
            // uc_Main1
            // 
            this.uc_Main1.Location = new System.Drawing.Point(12, 12);
            this.uc_Main1.Name = "uc_Main1";
            this.uc_Main1.Size = new System.Drawing.Size(1200, 800);
            this.uc_Main1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 840);
            this.Controls.Add(this.uc_Main1);
            this.Name = "Form1";
            this.Text = "Feature Vector Learn";
            this.ResumeLayout(false);

        }

        #endregion

        private Uc_Main uc_Main1;
    }
}

