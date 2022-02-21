namespace Ncd.Cli
{
    internal class DirectoryCacheEntry
    {
        public DirectoryCacheEntry(string path)
        {
            Path = path;
            Subdirectories = Directory.GetDirectories(path).Select(p=>System.IO.Path.GetFileName(p)).ToArray();
        }

        public DirectoryCacheEntry(string path, string[] subdirectories)
        {
            Path = path;
            Subdirectories = subdirectories;
        }

        public string Path { get; }
        public string[] Subdirectories { get; }
    }
}
