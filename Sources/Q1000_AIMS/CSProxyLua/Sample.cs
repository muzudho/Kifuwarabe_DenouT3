using System.Windows.Forms;

namespace ProjectDark.KWSupport {
	public class LUASample{
		public static string HelloWorld(){
			MessageBox.Show("Hello, world! from C#", "ProjectDark.KWSupport", MessageBoxButtons.OK);

			return "Returned Value";
		}
	}
}

