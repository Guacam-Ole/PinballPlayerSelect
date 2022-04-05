using System.Windows.Forms;

namespace PPS
{
    public partial class DisplayText : Form
    {
        public DisplayText()
        {
            InitializeComponent();
        }

        public DisplayText(string title, string caption, string contents) : this()
        {
            Text = title;
            Caption.Text = caption;
            Contents.Text = contents;
        }

        private void Close_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}