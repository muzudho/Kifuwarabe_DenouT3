namespace Grayscale.P720_FvWriter___
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
            this.btnWriter = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnMakeRandom = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKifuFilepath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReadKifu = new System.Windows.Forms.Button();
            this.txtSasiteList = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnMakeZero_Tv = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCreateRv0Clear = new System.Windows.Forms.Button();
            this.btn_HyoHenkeiFvKK = new System.Windows.Forms.Button();
            this.btn_1pKP_Write = new System.Windows.Forms.Button();
            this.btnWriteFvPp = new System.Windows.Forms.Button();
            this.btnWriteFvScale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWriter
            // 
            this.btnWriter.Enabled = false;
            this.btnWriter.Location = new System.Drawing.Point(20, 57);
            this.btnWriter.Name = "btnWriter";
            this.btnWriter.Size = new System.Drawing.Size(111, 23);
            this.btnWriter.TabIndex = 0;
            this.btnWriter.Text = "サンプル書出し";
            this.btnWriter.UseVisualStyleBackColor = true;
            this.btnWriter.Click += new System.EventHandler(this.btnWriter_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(20, 86);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(115, 23);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "サンプル読込み";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnMakeRandom
            // 
            this.btnMakeRandom.Location = new System.Drawing.Point(20, 28);
            this.btnMakeRandom.Name = "btnMakeRandom";
            this.btnMakeRandom.Size = new System.Drawing.Size(167, 23);
            this.btnMakeRandom.TabIndex = 2;
            this.btnMakeRandom.Text = "サンプルをランダム値で作成";
            this.btnMakeRandom.UseVisualStyleBackColor = true;
            this.btnMakeRandom.Click += new System.EventHandler(this.btnMakeRandom_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "フィーチャー・ベクター";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "棋譜";
            // 
            // txtKifuFilepath
            // 
            this.txtKifuFilepath.Location = new System.Drawing.Point(20, 205);
            this.txtKifuFilepath.Name = "txtKifuFilepath";
            this.txtKifuFilepath.Size = new System.Drawing.Size(746, 19);
            this.txtKifuFilepath.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "棋譜(.csa)ファイルへのパス";
            // 
            // btnReadKifu
            // 
            this.btnReadKifu.Location = new System.Drawing.Point(27, 230);
            this.btnReadKifu.Name = "btnReadKifu";
            this.btnReadKifu.Size = new System.Drawing.Size(121, 23);
            this.btnReadKifu.TabIndex = 7;
            this.btnReadKifu.Text = "棋譜読込";
            this.btnReadKifu.UseVisualStyleBackColor = true;
            this.btnReadKifu.Click += new System.EventHandler(this.btnReadKifu_Click);
            // 
            // txtSasiteList
            // 
            this.txtSasiteList.Location = new System.Drawing.Point(20, 281);
            this.txtSasiteList.Multiline = true;
            this.txtSasiteList.Name = "txtSasiteList";
            this.txtSasiteList.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSasiteList.Size = new System.Drawing.Size(235, 200);
            this.txtSasiteList.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 260);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "指し手リスト";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(269, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "タイアド・ベクター";
            // 
            // btnMakeZero_Tv
            // 
            this.btnMakeZero_Tv.Location = new System.Drawing.Point(271, 28);
            this.btnMakeZero_Tv.Name = "btnMakeZero_Tv";
            this.btnMakeZero_Tv.Size = new System.Drawing.Size(148, 23);
            this.btnMakeZero_Tv.TabIndex = 11;
            this.btnMakeZero_Tv.Text = "0クリアーでファイル作成";
            this.btnMakeZero_Tv.UseVisualStyleBackColor = true;
            this.btnMakeZero_Tv.Click += new System.EventHandler(this.btnMakeZero_Tv_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(483, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "リザルト・ベクター";
            // 
            // btnCreateRv0Clear
            // 
            this.btnCreateRv0Clear.Location = new System.Drawing.Point(475, 30);
            this.btnCreateRv0Clear.Name = "btnCreateRv0Clear";
            this.btnCreateRv0Clear.Size = new System.Drawing.Size(165, 23);
            this.btnCreateRv0Clear.TabIndex = 13;
            this.btnCreateRv0Clear.Text = "0クリアーでファイル作成";
            this.btnCreateRv0Clear.UseVisualStyleBackColor = true;
            this.btnCreateRv0Clear.Click += new System.EventHandler(this.btnCreateRv0Clear_Click);
            // 
            // btn_HyoHenkeiFvKK
            // 
            this.btn_HyoHenkeiFvKK.Location = new System.Drawing.Point(74, 124);
            this.btn_HyoHenkeiFvKK.Name = "btn_HyoHenkeiFvKK";
            this.btn_HyoHenkeiFvKK.Size = new System.Drawing.Size(113, 23);
            this.btn_HyoHenkeiFvKK.TabIndex = 14;
            this.btn_HyoHenkeiFvKK.Text = "表変形KK";
            this.btn_HyoHenkeiFvKK.UseVisualStyleBackColor = true;
            this.btn_HyoHenkeiFvKK.Click += new System.EventHandler(this.btn_HyoHenkeiFvKK_Click);
            // 
            // btn_1pKP_Write
            // 
            this.btn_1pKP_Write.Location = new System.Drawing.Point(222, 124);
            this.btn_1pKP_Write.Name = "btn_1pKP_Write";
            this.btn_1pKP_Write.Size = new System.Drawing.Size(104, 23);
            this.btn_1pKP_Write.TabIndex = 15;
            this.btn_1pKP_Write.Text = "1P玉 KP 書出し";
            this.btn_1pKP_Write.UseVisualStyleBackColor = true;
            this.btn_1pKP_Write.Click += new System.EventHandler(this.btn_1pKP_Write_Click);
            // 
            // btnWriteFvPp
            // 
            this.btnWriteFvPp.Location = new System.Drawing.Point(344, 124);
            this.btnWriteFvPp.Name = "btnWriteFvPp";
            this.btnWriteFvPp.Size = new System.Drawing.Size(139, 23);
            this.btnWriteFvPp.TabIndex = 16;
            this.btnWriteFvPp.Text = "PP書出し";
            this.btnWriteFvPp.UseVisualStyleBackColor = true;
            this.btnWriteFvPp.Click += new System.EventHandler(this.btnWriteFvPp_Click);
            // 
            // btnWriteFvScale
            // 
            this.btnWriteFvScale.Location = new System.Drawing.Point(500, 124);
            this.btnWriteFvScale.Name = "btnWriteFvScale";
            this.btnWriteFvScale.Size = new System.Drawing.Size(140, 23);
            this.btnWriteFvScale.TabIndex = 17;
            this.btnWriteFvScale.Text = "fv_00_Scale.csv書出";
            this.btnWriteFvScale.UseVisualStyleBackColor = true;
            this.btnWriteFvScale.Click += new System.EventHandler(this.btnWriteFvScale_Click);
            // 
            // Uc_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnWriteFvScale);
            this.Controls.Add(this.btnWriteFvPp);
            this.Controls.Add(this.btn_1pKP_Write);
            this.Controls.Add(this.btn_HyoHenkeiFvKK);
            this.Controls.Add(this.btnCreateRv0Clear);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnMakeZero_Tv);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtSasiteList);
            this.Controls.Add(this.btnReadKifu);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKifuFilepath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnMakeRandom);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnWriter);
            this.Name = "Uc_Main";
            this.Size = new System.Drawing.Size(780, 499);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWriter;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnMakeRandom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKifuFilepath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReadKifu;
        private System.Windows.Forms.TextBox txtSasiteList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnMakeZero_Tv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCreateRv0Clear;
        private System.Windows.Forms.Button btn_HyoHenkeiFvKK;
        private System.Windows.Forms.Button btn_1pKP_Write;
        private System.Windows.Forms.Button btnWriteFvPp;
        private System.Windows.Forms.Button btnWriteFvScale;
    }
}
