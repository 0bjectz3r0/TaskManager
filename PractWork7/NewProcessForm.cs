using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace PractWork7
{
    public partial class NewProcessForm : Form
    {
        public NewProcessForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {       
            try
            {
                Process.Start(fileNameTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Applications|*.exe";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileNameTextBox.Text = fileDialog.FileName;
            }
        }
    }
}
