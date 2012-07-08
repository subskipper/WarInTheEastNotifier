using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WarInTheEastLogMonitor.Core;

namespace WarInTheEastLogMonitor
{
    public class FileMonitorApplicationContext : ApplicationContext
    {
        private IContainer m_Components;
        private NotifyIcon m_LogNotifier;
        private ContextMenu m_Menu;
        private Form m_MainForm;
        private MenuItem m_ExitMenuItem;
        private MenuItem m_ShowContextMenuItem;
        //private LogMonitor m_LogMonitor; 

        public FileMonitorApplicationContext()
        {
            InitializeContext();
        }

        private void InitializeContext()
        {
            //Hard code path for now while testing...
            //m_LogMonitor = new LogMonitor(@"C:\LogMonitorTest\");

            m_Components = new Container();
            m_LogNotifier = new NotifyIcon(m_Components);
            m_Menu = new ContextMenu();
            m_ExitMenuItem = new MenuItem();
            m_ShowContextMenuItem = new MenuItem();

            m_LogNotifier.ContextMenu = m_Menu;
            m_LogNotifier.DoubleClick += m_LogNotifier_DoubleClick;
            m_LogNotifier.Icon = new Icon(typeof(FileMonitorApplicationContext), "LogMonitor.ico");
            m_LogNotifier.Text = "This is where the meat goes";
            m_LogNotifier.Visible = true;

            m_Menu.MenuItems.AddRange(new[]{m_ShowContextMenuItem, m_ExitMenuItem});

            m_ShowContextMenuItem.Index = 0;
            m_ShowContextMenuItem.Text = "&Show options";
            m_ShowContextMenuItem.DefaultItem = true;
            m_ShowContextMenuItem.Click += ShowContextMenuItemOnClick;

            m_ExitMenuItem.Index = 1;
            m_ExitMenuItem.Text = "&Exit";
            m_ExitMenuItem.Click += ExitMenuItemOnClick;
        }

        private void ExitMenuItemOnClick(object sender, EventArgs eventArgs)
        {
            ExitThread();
        }

        private void ShowContextMenuItemOnClick(object sender, EventArgs eventArgs)
        {
            ShowForm();
        }

        private void ShowForm()
        {
            if (m_MainForm == null)
            {
                m_MainForm = new NotifierOptionsForm();
                m_MainForm.Show();
                m_MainForm.Closed += MainFormOnClosed;
            }
            else
            {
                m_MainForm.Activate();
            }
        }

        private void MainFormOnClosed(object sender, EventArgs eventArgs)
        {
            m_MainForm = null;
        }

        private void m_LogNotifier_DoubleClick(object sender, EventArgs e)
        {
            ShowForm();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_Components != null)
                    m_Components.Dispose();
            }
        }

        protected override void ExitThreadCore()
        {
            if (m_MainForm != null)
            {
                m_MainForm.Close();
            }
            base.ExitThreadCore();
        }
    }
}
