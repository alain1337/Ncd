using System.Collections.Concurrent;

namespace Ncd.Cli
{
    internal static class DirectoryCache
    {
        static readonly ConcurrentDictionary<string, DirectoryCacheEntry> _cache = new ();

        public static DirectoryCacheEntry Get(string path)
        {
            return _cache.GetOrAdd(path, p => new DirectoryCacheEntry(p));
        }
    }
}
