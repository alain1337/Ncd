using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ncd
{
    internal static class DirectoryCache
    {
        static readonly ConcurrentDictionary<string,DirectoryCacheEntry> _cache = new ConcurrentDictionary<string, DirectoryCacheEntry>();

        public static DirectoryCacheEntry Get(string path)
        {
            return _cache.GetOrAdd(path, p => new DirectoryCacheEntry(p));
        }
    }

    internal class DirectoryCacheEntry
    {
        public DirectoryCacheEntry(string path)
        {
            Path = path;
            Subdirectories = Directory.GetDirectories(path).Select(System.IO.Path.GetFileName).ToArray();
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
