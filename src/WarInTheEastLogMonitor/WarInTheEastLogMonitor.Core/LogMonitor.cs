namespace WarInTheEastLogMonitor.Core
{
    public class LogMonitor
    {
        private string m_WatchedDirectory;

        public LogMonitor(string pathToWatch)
        {
            m_WatchedDirectory = pathToWatch;
        }

        public string GetWatchedDirectory
        {
            get { return m_WatchedDirectory; }
            private set { m_WatchedDirectory = value; }
        }
    }
}
