using System;
using System.IO;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MFIL.lib;

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

            var result = motra.Analyze(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "explorer.exe"));

            Assert.IsFalse(result.Scannable);
            Assert.IsNull(result.FileType);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void MFILAnalyzer_InvalidFileArgument()
        {
            var motra = new MFILAnalyzer();

            motra.Analyze("wick");
        }
    }
}