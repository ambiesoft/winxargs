using System;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Ambiesoft;
namespace winxargs
{
    public partial class FormMain : Form
    {
        private void btnAbout_Click(object sender, EventArgs e)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append(Application.ProductName).Append(" ver ");
            sb.Append(AmbLib.getAssemblyVersion(Assembly.GetExecutingAssembly()));

            CenteredMessageBox.Show(this,
                sb.ToString(),
                Application.ProductName,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

   

    }
}
