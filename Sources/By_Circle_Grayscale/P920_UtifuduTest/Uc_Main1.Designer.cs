namespace P920_UtifuduTest
{
    partial class Uc_Main1
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
            this.btnUtifudumeTest = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUtifudumeTest
            // 
            this.btnUtifudumeTest.Location = new System.Drawing.Point(24, 37);
            this.btnUtifudumeTest.Name = "btnUtifudumeTest";
            this.btnUtifudumeTest.Size = new System.Drawing.Size(160, 23);
            this.btnUtifudumeTest.TabIndex = 0;
            this.btnUtifudumeTest.Text = "打ち歩詰めテスト";
            this.btnUtifudumeTest.UseVisualStyleBackColor = true;
            this.btnUtifudumeTest.Click += new System.EventHandler(this.btnUtifudumeTest_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(24, 66);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(316, 285);
            this.txtResult.TabIndex = 1;
            // 
            // Uc_Main1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnUtifudumeTest);
            this.Name = "Uc_Main1";
            this.Size = new System.Drawing.Size(395, 382);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUtifudumeTest;
        private System.Windows.Forms.TextBox txtResult;
    }
}
