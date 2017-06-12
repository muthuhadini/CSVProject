using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSVReader;

namespace CSVUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ManipulateFile _ManipulateFile = new ManipulateFile();      
            _ManipulateFile.CSVInput("C:\\Users\\shomishanang\\Downloads\\data.csv");
        }
    }
}
