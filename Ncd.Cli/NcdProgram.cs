namespace Ncd.Cli
{
    public class NcdProgram
    {
        private readonly string[] args;
        public NcdProgram(string[] args)
        {
            this.args = args;
        }

        public (ReturnCode returnCode, string message) Run()
        {
            if (args.Length == 0)
                return (ReturnCode.InvalidParameters, "usage: Ncd path [index]");


            var dirs = args[0].Split('\\');
            var dirpaths = string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("NCD_PATH"))
                ? Array.Empty<string>()
                : Environment.GetEnvironmentVariable("NCD_PATH")
                    ?.Split(';');

            var found = DirectoryFinder.Find(Directory.GetCurrentDirectory(), dirs);
            if (found is null || !found.Any())
                found = dirpaths?.SelectMany(d => DirectoryFinder.Find(d, dirs)).ToArray();

            if (found is null || !found.Any())
                return (ReturnCode.NothingFound, "Nothing found");

            if (found.Length == 1)
                return (ReturnCode.FoundDirectory, $"{found.First()}");

            if (args.Length > 1)
            {
                var index = int.Parse(args[1]);
                if (index >= found.Length)
                    return (ReturnCode.InvalidIndex, $"Invalid index. Max is {found.Length - 1}");

                return (ReturnCode.FoundDirectory, found[index]);
            }


            var multipleFoundMessage = "Found:\n";
            Console.WriteLine("Found:");
            for (var i = 0; i < found.Length; i++)
                multipleFoundMessage += $"[{i}] {found[i]}\n";

            return (ReturnCode.MultipleFound, multipleFoundMessage);
        }
    }
}
