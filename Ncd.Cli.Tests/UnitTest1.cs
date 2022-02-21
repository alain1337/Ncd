using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace Ncd.Cli.Tests
{
    public class UnitTest1
    {


        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        private readonly ITestOutputHelper _testOutputHelper;

        const string BaseFolder = "Ncd.Tests FS";

        [Theory]
        [InlineData(null, ReturnCode.InvalidParameters, -1)]
        [InlineData(@"H", ReturnCode.FoundDirectory, 0)]
        [InlineData(@"F", ReturnCode.MultipleFound, -1)]
        [InlineData(@"F\P\A", ReturnCode.FoundDirectory, 1)]
        [InlineData(@"F\Gi\c", ReturnCode.FoundDirectory, 5)]
        [InlineData(@"f\gi\c", ReturnCode.FoundDirectory, 5)]
        [InlineData(@"F\G\c", ReturnCode.MultipleFound, -1)]
        [InlineData(@"X", ReturnCode.NothingFound, -1)]

        public void When_UsingReasonableParameters_Expect_ExpectedReturnCodes(string pathExpression, ReturnCode expectedReturnCode, int expectedPathIndex)
        {
            var directories = new[]
            {
                @"Hello World",
                @"Food\Pies\Apple",
                @"Food\Pies\Cherry",
                @"Fun\Games\Carcassone",
                @"Fun\Games\MTG",
                @"Fun\Gifts\Christmas",
                @"Fun\Gifts\New Years Eve",
            };
            _testOutputHelper.WriteLine($"Test {pathExpression} {expectedReturnCode} {expectedPathIndex}");

            MakeDirectories(directories);

            var actual = pathExpression is not null
                ? RunNcd(pathExpression)
                : RunNcd();
            Assert.Equal(expectedReturnCode, actual.returnCode);
            if (expectedReturnCode == ReturnCode.FoundDirectory)
                Assert.Equal(directories[expectedPathIndex], actual.message);

        }

        public (ReturnCode returnCode, string message) RunNcd(params string[] args)
        {
            if (args.Length > 0)
                args[0] = Path.Combine(BaseFolder, args[0]);



            var ncd = new Ncd.Cli.NcdProgram(args);
            var actual = ncd.Run();
            _testOutputHelper.WriteLine($"returned {actual}");

            if (actual.returnCode == ReturnCode.FoundDirectory)
                actual.message = Path.GetRelativePath(BaseFolder, actual.message);



            return actual;
        }

        private static void MakeDirectories(IEnumerable<string> directories)
        {

            try { Directory.Delete(BaseFolder, true); } catch {; }
            foreach (var directory in directories)
                Directory.CreateDirectory(Path.Combine(BaseFolder, directory));

        }
    }
}