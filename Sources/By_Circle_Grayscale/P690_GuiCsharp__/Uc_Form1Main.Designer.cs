namespace Grayscale.P699_Form_______
{
    partial class Uc_Form1Main
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
            this.components = new System.ComponentModel.Container();
            this.gameEngineTimer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // gameEngineTimer1
            // 
            this.gameEngineTimer1.Interval = 50;
            this.gameEngineTimer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Uc_Form1Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.DoubleBuffered = true;
            this.Name = "Uc_Form1Main";
            this.Size = new System.Drawing.Size(1074, 761);
            this.Load += new System.EventHandler(this.Uc_Form1Main_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Uc_Form1Main_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Uc_Form1Main_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Uc_Form1Main_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Uc_Form1Main_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer gameEngineTimer1;
    }
}
