using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOAppSample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOAppSample.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void TestDatabaseConnectionTest()
        {
            Program.TestDatabaseConnection();
        }
    }
}