namespace Ncd.Cli
{
    internal static class DirectoryFinder
    {
        static readonly string[] NoResult = Array.Empty<string>();

        public static string[] Find(string root, IEnumerable<string> parts)
        {
            if (!parts.Any())
                return new[] { root };

            var de = DirectoryCache.Get(root);
            var subs = de.Subdirectories.Where(s => s.StartsWith(parts.First(), StringComparison.OrdinalIgnoreCase));
            if (!subs.Any())
                return NoResult;

            var results = new List<string>();
            foreach (var sub in subs)
                foreach (var found in Find(Path.Combine(root, sub), parts.Skip(1)))
                    results.Add(found);
            return results.ToArray();
        }
    }
}
