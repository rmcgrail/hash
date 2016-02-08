using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System;
using System.IO;

namespace Hash.Test
{
    [TestClass, SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    public class OutputTests 
    {
        private TextWriter currentWriter = null;
        private TextWriter initialWriter = null;

        // Ignores output of initial and trailing space and new lines.
        private string writerOutput => currentWriter.ToString().Trim();

        [TestInitialize]
        public void Initialize() {
            // Redirect save Console's original writer, then redirect to internal one.
            initialWriter = Console.Out;
            currentWriter = new StringWriter();
            Console.SetOut(currentWriter);
        }

        [TestCleanup]
        public void Cleanup() {
            // Returns console output to orignal state.
            Console.SetOut(initialWriter);

            // Disposal of initialWriter should be handled by its creator.
            initialWriter = null;

            if (currentWriter != null) {
                currentWriter.Dispose();
                currentWriter = null;
            }
        }

        [TestMethod]
        public void Hash_FileFirst() {
            new Output().Hash("file", "hash", true);

            var consoleOutput = writerOutput;
            Assert.IsTrue(consoleOutput.StartsWith("file"));
            Assert.IsTrue(consoleOutput.EndsWith("hash"));
        }

        [TestMethod]
        public void Hash_HashFirst() {
            new Output().Hash("file", "hash", false);

            var consoleOutput = writerOutput;
            Assert.IsTrue(consoleOutput.StartsWith("hash"));
            Assert.IsTrue(consoleOutput.EndsWith("file"));
        }

        [TestMethod]
        public void Error_Message_Simple() {
            new Output().Error("An amazing error message.");

            Assert.IsTrue(writerOutput == "[ ERROR ] An amazing error message.");
        }

        [TestMethod]
        public void Error_Message_Exception() {
            new Output().Error("An amazing error message", new Exception("Exception's message"));
#if DEBUG
            Assert.IsTrue(writerOutput.StartsWith("[ ERROR ] An amazing error message; System.Exception: Exception's message"));
#else
            Assert.IsTrue(writerOutput.StartsWith("[ ERROR ] An amazing error message; Exception's message"));
#endif
        }

        [TestMethod]
        public void Help_Simple() {
            new Output().Help();
            Assert.IsTrue(writerOutput.Contains("Usage:"));
            Assert.IsTrue(writerOutput.Contains("Parameters:"));
        }

        [TestMethod]
        public void Help_WithMessage() {
            new Output().Help("A helpful message");
            Assert.IsTrue(writerOutput.StartsWith("A helpful message"));
            Assert.IsTrue(writerOutput.Contains("Usage:"));
            Assert.IsTrue(writerOutput.Contains("Parameters:"));
        }
    }
}