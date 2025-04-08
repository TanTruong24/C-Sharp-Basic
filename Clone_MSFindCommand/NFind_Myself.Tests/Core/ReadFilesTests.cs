using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NFind_Myself.Core;
using NFind_Myself.Models;

namespace NFind_Myself.Tests.Core;

[TestClass]
public class ReadFilesTests
{
    private string testDir;

    [TestInitialize]
    public void Setup()
    {
        testDir = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles");
        Directory.CreateDirectory(testDir);
    }

    [TestCleanup]
    public void Cleanup()
    {
    }

    [TestMethod]
    public void ReaderManyFiles_ShouldReturnValidFileData_WhenFileExists()
    {
        var filePath = CreateTestFile("valid1");
        var files = new List<string> { filePath };
        var result = ReadFiles.ReaderManyFiles(files);

        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(result.ContainsKey(filePath));
        Assert.AreEqual(filePath, result[filePath].FilePath);
        Assert.AreEqual(2, result[filePath].Lines.Length);
        Assert.AreEqual("Line 1", result[filePath].Lines[0]);

        File.Delete(filePath);
    }

    [TestMethod]
    public void ReaderManyFiles_ShouldSkipMissingFiles_AndContinue()
    {
        var filePath = CreateTestFile("valid2");
        var missingPath = Path.Combine("TestFiles", "missing_file.txt");

        var files = new List<string> { filePath, missingPath };
        var result = ReadFiles.ReaderManyFiles(files);

        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(result.ContainsKey(filePath));
        Assert.IsFalse(result.ContainsKey(missingPath));

        File.Delete(filePath);
    }

    private string CreateTestFile(string name)
    {
        var path = Path.Combine("TestFiles", $"{name}_{Guid.NewGuid()}.txt");
        File.WriteAllText(path, "Line 1\nLine 2", Encoding.UTF8);
        return path;
    }
}
