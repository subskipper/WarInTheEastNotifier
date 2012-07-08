using System;
using System.Drawing;
using System.Windows.Forms;
using WarInTheEastLogMonitor.Core;

namespace WarInTheEastLogMonitor
{
    public partial class NotifierOptionsForm : Form
    {
        private NotifyIcon m_NotifyIcon = new NotifyIcon();
        private LogMonitor m_LogMonitor;

        public NotifierOptionsForm()
        {
            InitializeComponent();
            SetBalloonNotifier();
            //Hard code path for now while testing...
            m_LogMonitor = new LogMonitor(@"C:\LogMonitorTest\");
        }

        private void btnNotify_Click(object sender, System.EventArgs e)
        {
            m_NotifyIcon.Visible = true;
            m_NotifyIcon.ShowBalloonTip(5000);
        }

        private void SetBalloonNotifier()
        {
            m_NotifyIcon.Icon = SystemIcons.Application;
            m_NotifyIcon.BalloonTipTitle = "This is a test";
            m_NotifyIcon.BalloonTipText = "Here is where the meat goes";
            m_NotifyIcon.BalloonTipIcon = ToolTipIcon.None;
            Click += btnNotify_Click;
        }

        private void NotifierOptionsForm_Load(object sender, EventArgs e)
        {
            string filePath = m_LogMonitor.GetWatchedDirectory;
            if (string.IsNullOrWhiteSpace(filePath))
                lblPath.Text = "No direcory set. Not watching any log file.";
            else
                lblPath.Text = filePath;
        }
    }
}
