using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFind_Myself.Core;
using NFind_Myself.Models;

namespace NFind_Myself.Tests.Core
{
    [TestClass]
    public class ExtractParamsTests
    {
        [TestMethod]
        public void ExtractFileNames_PathValid_ReturnListFileNames()
        {
            string arg = "find \\n \\v \"Tan\" E:\\csharp\\Clone_MSFindCommand\\test1.txt";
            var fileNames = ExtractParams.ExtractFileNames(arg);

            Assert.AreEqual(1, fileNames.Count);
            Assert.AreEqual(fileNames[0], "E:\\csharp\\Clone_MSFindCommand\\test1.txt");
        }

        [TestMethod]
        public void ExtractFileNames_PathIncludeWildcards_ReturnListFileNames()
        {
            string arg = "find \\n \\v \"Tan\" E:\\eng\\csharp\\Clone_MSFindCommand\\NFind_Myself\\TestData\\*.txt";
        }

        [TestMethod]
        public void ExtractFileNames_ManyPathsValid__ReturnListFileNames() { }

        [TestMethod]
        public void ExtractFileNames_PathNotExist__ExceptionFileNotExist() { }

        [TestMethod]
        public void ExtractFileNames_NotExistFileExtValidInArgument__ExceptionInvalidArgument() { }
    }
}
