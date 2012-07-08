using System.IO;

namespace WarInTheEastLogMonitor.Core
{
    public class LogMonitor
    {
        private string m_WatchedDirectory;
        private FileSystemWatcher m_Watcher;

        public LogMonitor(string pathToWatch)
        {
            m_WatchedDirectory = pathToWatch;
            //Only support .txt for now as that is what's needed for WarInTheEast
            m_Watcher = new FileSystemWatcher(pathToWatch, "*.txt");
            m_Watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.LastAccess |
                                     NotifyFilters.DirectoryName;

            m_Watcher.EnableRaisingEvents = true;
        }

        public string GetWatchedDirectory
        {
            get { return m_WatchedDirectory; }
            private set { m_WatchedDirectory = value; }
        }
    }
}
