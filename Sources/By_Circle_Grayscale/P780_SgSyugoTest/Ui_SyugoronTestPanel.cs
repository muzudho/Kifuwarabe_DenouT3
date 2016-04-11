using Grayscale.P055_Conv_Sy.L500____Converter;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Grayscale.P780_SgSyugoTest
{
    public partial class Ui_SyugoronTestPanel : UserControl
    {

        private SyWordDictionary<SyElement> syDictionary;

        private SyFuncDictionary syFuncDictionary;
        public SyFuncDictionary SyFuncDictionary
        {
            get
            {
                return this.syFuncDictionary;
            }
        }

        public Ui_SyugoronTestPanel()
        {
            this.syDictionary = new SyWordDictionary<SyElement>();

            this.syFuncDictionary = new SyFuncDictionary();

            InitializeComponent();
        }

        /// <summary>
        /// インポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            ulong bitfield = 0;//FIXME: 暫定

            string input = this.txtImport.Text;

            string[] definitions = input.Split(';');

            foreach (string definition in definitions)
            {
                if (-1!=definition.IndexOf('∈'))
                {
                    string[] operands = definition.Split('∈');

                    string[] elementWords;
                    if (-1 != operands[0].IndexOf(','))
                    {
                        elementWords = operands[0].Split(',');
                    }
                    else
                    {
                        elementWords = new string[]{ operands[0]};
                    }

                    SySet<SyElement> sySet = this.syDictionary.GetWord(operands[1]);
                    if (null == sySet)
                    {
                        sySet = new SySet_Default<SyElement>(operands[1]);
                    }

                    foreach(string elementWord in elementWords)
                    {
                        sySet.AddElement(new SyElement_Default(bitfield));
                        Conv_Sy.Put_BitfieldWord(bitfield, elementWord);
                        bitfield++;
                    }

                    this.syDictionary.AddWord(operands[1], sySet);
                }
                else if (-1 != definition.IndexOf('⊂'))
                {
                    string[] operands = definition.Split('⊂');

                    string[] subsets;
                    if (-1 != operands[0].IndexOf(','))
                    {
                        subsets = operands[0].Split(',');
                    }
                    else
                    {
                        subsets = new string[] { operands[0] };
                    }

                    SySet<SyElement> sySet_super = this.syDictionary.GetWord(operands[1]);
                    if (null == sySet_super)
                    {
                        sySet_super = new SySet_Default<SyElement>(operands[1]);
                        this.syDictionary.AddWord(operands[1], sySet_super);
                    }

                    foreach (string subset in subsets)
                    {
                        SySet<SyElement> sySet_subset = this.syDictionary.GetWord(subset);
                        if (null == sySet_subset)
                        {
                            sySet_subset = new SySet_Default<SyElement>(subset);
                            this.syDictionary.AddWord(subset, sySet_subset);
                        }

                        sySet_subset.AddSupersets(sySet_super);
                    }

                    this.syDictionary.AddWord(operands[1], sySet_super);
                }
                else
                {
                }
            }

            //this.txtImport.Text = "";
        }

        /// <summary>
        /// [検索]ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {
            this.txtElements.Text = "";
            this.txtElements2.Text = "";
            this.txtElements3.Text = "";

            string wordStr = this.txtWord.Text.Trim();
            SySet<SyElement> word = this.syDictionary.GetWord(wordStr);

            this.lblElementsComment.Text = "一般的に"+wordStr+"は";
            this.lblElementsComment2.Text = "話題の" + wordStr + "は";
            this.lblElementsComment3.Text = "話題の" + wordStr + "は";
            this.lblSupersetComment.Text = wordStr + "は";

            if (null == word)
            {
                this.txtElements.Text = "φ";
                this.txtSupersets.Text = "φ";
            }
            else
            {
                //要素
                {
                    bool first = true;
                    StringBuilder sb = new StringBuilder();
                    foreach (SyElement syElm in word.Elements)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            sb.Append(",");
                        }

                        sb.Append(Conv_Sy.Query_Word( syElm.Bitfield));
                    }
                    this.txtElements.Text = sb.ToString();
                }

                //スーパーセット
                {
                    bool first = true;
                    StringBuilder sb = new StringBuilder();
                    foreach (SySet<SyElement> sySet in word.Supersets)
                    {
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            sb.Append(",");
                        }

                        sb.Append(sySet.Word);
                    }
                    this.txtSupersets.Text = sb.ToString();
                }
            }

        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            // 文脈
            string context = this.txtContext.Text;

            string[] replacements = context.Split(';');

            Dictionary<string, string> repDic = new Dictionary<string, string>();
            foreach(string replacement in replacements)
            {
                string[] pair = replacement.Split('=');

                repDic.Add(pair[0].Trim(), pair[1].Trim());
            }


            string elements1 = this.txtElements.Text;

            foreach(KeyValuePair<string,string> entry in repDic)
            {
                elements1 = elements1.Replace(entry.Key, entry.Value);
            }

            this.txtElements2.Text = elements1;

        }

        private void Ui_Main_Load(object sender, EventArgs e)
        {
            // UTF-8
            string path = Application.StartupPath+ "\\dictionary.txt";
            //MessageBox.Show(path);
            if (File.Exists(path))
            {
                string dictionary = File.ReadAllText(path);

                this.txtImport.Text = dictionary;
            }
        }

        /// <summary>
        /// [丸括弧を外す]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenParen_Click(object sender, EventArgs e)
        {
            string text = this.txtElements2.Text;

            StringBuilder sb = new StringBuilder();

            string[] elements = text.Split(',');

            foreach (string element in elements)
            {
                if (-1 != element.IndexOf('('))
                {
                    string[] token1 = element.Split('(');
                    string funcName = token1[0].Trim();//関数名

                    string[] token2 = token1[1].Split(')');
                    string prmName = token2[0].Trim();//引数名

                    if (0 < sb.Length)
                    {
                        sb.Append(",");
                    }

                    if (this.syFuncDictionary.ContainsKey(funcName))
                    {
                        sb.Append(this.syFuncDictionary.GetFunc(funcName)(prmName));
                    }
                    else
                    {
                        sb.Append("【");
                        sb.Append(funcName);
                        sb.Append("・");
                        sb.Append(prmName);
                        sb.Append("】");
                    }
                }
                else
                {
                    sb.Append("【");
                    sb.Append(element);
                    sb.Append("】");
                }
            }

            this.txtElements3.Text = sb.ToString();
        }

        /// <summary>
        /// 上書き。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOverwrite_Click(object sender, EventArgs e)
        {
            // UTF-8
            string path = Application.StartupPath + "\\dictionary.txt";
            //MessageBox.Show(path);
            if (File.Exists(path))
            {
                File.WriteAllText(path, this.txtImport.Text);
            }
        }


    }
}
