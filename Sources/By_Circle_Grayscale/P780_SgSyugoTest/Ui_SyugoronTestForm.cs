using System.Windows.Forms;

namespace Grayscale.P780_SgSyugoTest
{
    public partial class Ui_SyugoronTestForm : Form
    {

        public Ui_SyugoronTestPanel Ui_SyugoronTestPanel
        {
            get
            {
                return this.ui_SyugoronTestPanel;
            }
        }

        public Ui_SyugoronTestForm()
        {
            InitializeComponent();
        }

    }
}
