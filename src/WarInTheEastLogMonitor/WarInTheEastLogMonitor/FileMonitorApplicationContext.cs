using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WarInTheEastLogMonitor.Core;

namespace WarInTheEastLogMonitor
{
    public class FileMonitorApplicationContext : ApplicationContext
    {
        private IContainer m_Components;
        private NotifyIcon m_NotifyIcon;
        private ContextMenu m_Menu;
        private Form m_MainForm;
        private MenuItem m_ExitMenuItem;
        private MenuItem m_ShowContextMenuItem;
        private static LogMonitor m_LogMonitor = new LogMonitor(@"D:\Spel\Matrix Games\Gary Grigsby's War in the East\Dat\save\");

        public FileMonitorApplicationContext()
        {
            InitializeContext();
        }

        private void InitializeContext()
        {

            m_Components = new Container();
            m_NotifyIcon = new NotifyIcon(m_Components);
            m_Menu = new ContextMenu();
            m_ExitMenuItem = new MenuItem();
            m_ShowContextMenuItem = new MenuItem();

            m_NotifyIcon.ContextMenu = m_Menu;
            m_NotifyIcon.DoubleClick += m_LogNotifier_DoubleClick;
            m_NotifyIcon.Icon = new Icon(typeof(FileMonitorApplicationContext), "LogMonitor.ico");
            m_NotifyIcon.Text = "This is where the meat goes";
            m_NotifyIcon.Visible = true;

            m_Menu.MenuItems.AddRange(new[]{m_ShowContextMenuItem, m_ExitMenuItem});

            m_ShowContextMenuItem.Index = 0;
            m_ShowContextMenuItem.Text = "&Show options";
            m_ShowContextMenuItem.DefaultItem = true;
            m_ShowContextMenuItem.Click += ShowContextMenuItemOnClick;

            m_ExitMenuItem.Index = 1;
            m_ExitMenuItem.Text = "&Exit";
            m_ExitMenuItem.Click += ExitMenuItemOnClick;
            m_LogMonitor.m_Watcher.Changed += WatcherOnChanged;
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs fileSystemEventArgs)
        {
            m_LogMonitor.ChangedFileName = fileSystemEventArgs.Name;
            m_LogMonitor.ChangeType = fileSystemEventArgs.ChangeType;
            var fileName = fileSystemEventArgs.Name;

            SetBalloonNotifier(fileName);

            m_NotifyIcon.Visible = true;
            m_NotifyIcon.ShowBalloonTip(10000);

        }

        private void SetBalloonNotifier(string fileName)
        {
            m_NotifyIcon.Icon = SystemIcons.Application;
            m_NotifyIcon.BalloonTipTitle = "Event logged";
            
            m_NotifyIcon.BalloonTipText = string.Format("Watching file {0} and detected change {1} in {2}", m_LogMonitor.GetWatchedDirectory, m_LogMonitor.ChangeType, fileName);

            m_NotifyIcon.BalloonTipIcon = ToolTipIcon.None;
          
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
