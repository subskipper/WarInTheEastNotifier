using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarInTheEastLogMonitor
{
    public partial class NotifierOptionsForm : Form
    {
        private NotifyIcon m_NotifyIcon = new NotifyIcon();

        public NotifierOptionsForm()
        {
            InitializeComponent();
            SetBalloonNotifier();
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
    }
}
