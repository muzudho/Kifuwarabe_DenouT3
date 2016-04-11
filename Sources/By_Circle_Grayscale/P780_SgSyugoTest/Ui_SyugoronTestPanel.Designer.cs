namespace Grayscale.P780_SgSyugoTest
{
    partial class Ui_SyugoronTestPanel
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
            this.txtWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtImport = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtElements = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSupersets = new System.Windows.Forms.TextBox();
            this.btnOverwrite = new System.Windows.Forms.Button();
            this.lblSupersetComment = new System.Windows.Forms.Label();
            this.lblElementsComment = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnOpenParen = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtContext = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReplace = new System.Windows.Forms.Button();
            this.txtElements2 = new System.Windows.Forms.TextBox();
            this.lblElementsComment2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtElements3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblElementsComment3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(57, 123);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(263, 19);
            this.txtWord.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "単語";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(326, 121);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "検索";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtImport
            // 
            this.txtImport.Location = new System.Drawing.Point(73, 3);
            this.txtImport.Multiline = true;
            this.txtImport.Name = "txtImport";
            this.txtImport.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtImport.Size = new System.Drawing.Size(296, 87);
            this.txtImport.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "インポート";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(375, 67);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 5;
            this.btnImport.Text = "インポート";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtElements
            // 
            this.txtElements.Location = new System.Drawing.Point(89, 241);
            this.txtElements.Multiline = true;
            this.txtElements.Name = "txtElements";
            this.txtElements.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtElements.Size = new System.Drawing.Size(312, 74);
            this.txtElements.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(56, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "要素";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(454, 185);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "スーパーセット";
            // 
            // txtSupersets
            // 
            this.txtSupersets.Location = new System.Drawing.Point(529, 182);
            this.txtSupersets.Multiline = true;
            this.txtSupersets.Name = "txtSupersets";
            this.txtSupersets.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSupersets.Size = new System.Drawing.Size(312, 74);
            this.txtSupersets.TabIndex = 9;
            // 
            // btnOverwrite
            // 
            this.btnOverwrite.Location = new System.Drawing.Point(456, 67);
            this.btnOverwrite.Name = "btnOverwrite";
            this.btnOverwrite.Size = new System.Drawing.Size(75, 23);
            this.btnOverwrite.TabIndex = 10;
            this.btnOverwrite.Text = "上書き";
            this.btnOverwrite.UseVisualStyleBackColor = true;
            this.btnOverwrite.Click += new System.EventHandler(this.btnOverwrite_Click);
            // 
            // lblSupersetComment
            // 
            this.lblSupersetComment.AutoSize = true;
            this.lblSupersetComment.Location = new System.Drawing.Point(527, 164);
            this.lblSupersetComment.Name = "lblSupersetComment";
            this.lblSupersetComment.Size = new System.Drawing.Size(57, 12);
            this.lblSupersetComment.TabIndex = 11;
            this.lblSupersetComment.Text = "この集合は";
            // 
            // lblElementsComment
            // 
            this.lblElementsComment.AutoSize = true;
            this.lblElementsComment.Location = new System.Drawing.Point(87, 226);
            this.lblElementsComment.Name = "lblElementsComment";
            this.lblElementsComment.Size = new System.Drawing.Size(57, 12);
            this.lblElementsComment.TabIndex = 12;
            this.lblElementsComment.Text = "この集合は";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 318);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "で構成されています。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(527, 259);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 12);
            this.label6.TabIndex = 14;
            this.label6.Text = "です。";
            // 
            // btnOpenParen
            // 
            this.btnOpenParen.Location = new System.Drawing.Point(89, 494);
            this.btnOpenParen.Name = "btnOpenParen";
            this.btnOpenParen.Size = new System.Drawing.Size(136, 23);
            this.btnOpenParen.TabIndex = 15;
            this.btnOpenParen.Text = "丸括弧があれば外す↓";
            this.btnOpenParen.UseVisualStyleBackColor = true;
            this.btnOpenParen.Click += new System.EventHandler(this.btnOpenParen_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(56, 167);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "文脈";
            // 
            // txtContext
            // 
            this.txtContext.Location = new System.Drawing.Point(91, 164);
            this.txtContext.Multiline = true;
            this.txtContext.Name = "txtContext";
            this.txtContext.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtContext.Size = new System.Drawing.Size(310, 47);
            this.txtContext.TabIndex = 17;
            this.txtContext.Text = "話題の升=８八 ; 話題の先後=先手 ; 話題の動物=アリクイ ; 話題の人物=リンカーン";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(301, 214);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "※単に置換するだけ";
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(89, 333);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(98, 23);
            this.btnReplace.TabIndex = 19;
            this.btnReplace.Text = "まずは置換↓";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnReplace_Click);
            // 
            // txtElements2
            // 
            this.txtElements2.Location = new System.Drawing.Point(86, 402);
            this.txtElements2.Multiline = true;
            this.txtElements2.Name = "txtElements2";
            this.txtElements2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtElements2.Size = new System.Drawing.Size(315, 74);
            this.txtElements2.TabIndex = 20;
            // 
            // lblElementsComment2
            // 
            this.lblElementsComment2.AutoSize = true;
            this.lblElementsComment2.Location = new System.Drawing.Point(87, 387);
            this.lblElementsComment2.Name = "lblElementsComment2";
            this.lblElementsComment2.Size = new System.Drawing.Size(57, 12);
            this.lblElementsComment2.TabIndex = 21;
            this.lblElementsComment2.Text = "この集合は";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(89, 479);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "で構成されています。";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(231, 499);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(104, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "※いわゆる関数計算";
            // 
            // txtElements3
            // 
            this.txtElements3.Location = new System.Drawing.Point(89, 556);
            this.txtElements3.Multiline = true;
            this.txtElements3.Name = "txtElements3";
            this.txtElements3.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtElements3.Size = new System.Drawing.Size(315, 74);
            this.txtElements3.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(89, 633);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "で構成されています。";
            // 
            // lblElementsComment3
            // 
            this.lblElementsComment3.AutoSize = true;
            this.lblElementsComment3.Location = new System.Drawing.Point(87, 541);
            this.lblElementsComment3.Name = "lblElementsComment3";
            this.lblElementsComment3.Size = new System.Drawing.Size(57, 12);
            this.lblElementsComment3.TabIndex = 26;
            this.lblElementsComment3.Text = "この集合は";
            // 
            // Ui_SyugoronTestPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblElementsComment3);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtElements3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblElementsComment2);
            this.Controls.Add(this.txtElements2);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtContext);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnOpenParen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblElementsComment);
            this.Controls.Add(this.lblSupersetComment);
            this.Controls.Add(this.btnOverwrite);
            this.Controls.Add(this.txtSupersets);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtElements);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtImport);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtWord);
            this.Name = "Ui_SyugoronTestPanel";
            this.Size = new System.Drawing.Size(864, 676);
            this.Load += new System.EventHandler(this.Ui_Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtImport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.TextBox txtElements;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSupersets;
        private System.Windows.Forms.Button btnOverwrite;
        private System.Windows.Forms.Label lblSupersetComment;
        private System.Windows.Forms.Label lblElementsComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnOpenParen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtContext;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.TextBox txtElements2;
        private System.Windows.Forms.Label lblElementsComment2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtElements3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblElementsComment3;
    }
}
