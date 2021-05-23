using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MFIL.lib;
using MFIL.lib.Analyzers;

namespace MFIL.tests
{
    [TestClass]
    public class MFILAnalyzerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MFILAnalyzer_NullArgument()
        {
            var motra = new MFILAnalyzer();

            motra.Analyze(null);
        }

        [TestMethod]
        public void MFILAnalyzer_NotValidArgument()
        {
            var motra = new MFILAnalyzer();

            var result = motra.Analyze(Path.Combine(AppContext.BaseDirectory, "PESample.exe"));

            Assert.IsTrue(result.Scannable);
            Assert.IsNotNull(result.FileType);
            Assert.AreEqual(new PEAnalyzer().Name, result.FileType);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void MFILAnalyzer_InvalidFileArgument()
        {
            var motra = new MFILAnalyzer();

            motra.Analyze("wick");
        }

        [TestMethod]
        public void MFILAnalyzer_ELFArgument()
        {
            var motra = new MFILAnalyzer();

            motra.Analyze("7c6665aaba3b7da391ca8a6dd152bd32fafbad88");
        }
    }
}