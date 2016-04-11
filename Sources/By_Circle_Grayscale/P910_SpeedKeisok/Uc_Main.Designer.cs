namespace Grayscale.P910_SpeedKeisok
{
    partial class Uc_Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnKeisoku = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtFvFilepath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnOpenFv = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "計測する評価関数";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 15);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(186, 256);
            this.listBox1.TabIndex = 1;
            // 
            // btnKeisoku
            // 
            this.btnKeisoku.Location = new System.Drawing.Point(238, 115);
            this.btnKeisoku.Name = "btnKeisoku";
            this.btnKeisoku.Size = new System.Drawing.Size(75, 23);
            this.btnKeisoku.TabIndex = 2;
            this.btnKeisoku.Text = "計測";
            this.btnKeisoku.UseVisualStyleBackColor = true;
            this.btnKeisoku.Click += new System.EventHandler(this.btnKeisoku_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(215, 154);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(300, 263);
            this.txtResult.TabIndex = 3;
            // 
            // txtFvFilepath
            // 
            this.txtFvFilepath.Location = new System.Drawing.Point(213, 30);
            this.txtFvFilepath.Name = "txtFvFilepath";
            this.txtFvFilepath.Size = new System.Drawing.Size(245, 19);
            this.txtFvFilepath.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(213, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "Fv_00_Komawari.csvファイルパス";
            // 
            // btnOpenFv
            // 
            this.btnOpenFv.Location = new System.Drawing.Point(464, 30);
            this.btnOpenFv.Name = "btnOpenFv";
            this.btnOpenFv.Size = new System.Drawing.Size(75, 23);
            this.btnOpenFv.TabIndex = 6;
            this.btnOpenFv.Text = "開く";
            this.btnOpenFv.UseVisualStyleBackColor = true;
            this.btnOpenFv.Click += new System.EventHandler(this.btnOpenFv_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Uc_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOpenFv);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFvFilepath);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnKeisoku);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label1);
            this.Name = "Uc_Main";
            this.Size = new System.Drawing.Size(542, 437);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button btnKeisoku;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtFvFilepath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenFv;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}
