using System;
using System.Drawing;
using System.Windows.Forms;
using WarInTheEastLogMonitor.Core;

namespace WarInTheEastLogMonitor
{
    public partial class NotifierOptionsForm : Form
    {

        public NotifierOptionsForm()
        {
            InitializeComponent();
        }

        

        private void NotifierOptionsForm_Load(object sender, EventArgs e)
        {
            //string filePath = m_LogMonitor.GetWatchedDirectory;
            //if (string.IsNullOrWhiteSpace(filePath))
                //lblPath.Text = "No direcory set. Not watching any log file.";
            //else
                //lblPath.Text = filePath;
        }
    }
}
