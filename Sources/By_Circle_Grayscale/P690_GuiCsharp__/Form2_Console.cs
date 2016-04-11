using Grayscale.P693_ShogiGui___.L500____GUI;
using System.Windows.Forms;

namespace Grayscale.P699_Form_______
{
    public partial class Form2_Console : Form
    {

        public Form1_Shogi Form1_Shogi { get { return this.form1_Shogi; } }
        private Form1_Shogi form1_Shogi;

        public Uc_Form2Main Uc_Form2Main { get { return this.uc_Form2Main; } }

        public Form2_Console(Form1_Shogi form1_Shogi)
        {
            this.form1_Shogi = form1_Shogi;

            InitializeComponent();
        }



    }
}
