using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WindowsShortcutFactory.Tests;

[TestClass]
public class WindowsShortcutTests
{
    [TestMethod]
    public void TestPath()
    {
        using var s = new WindowsShortcut();
        Assert.IsNull(s.Path);
        s.Path = @"C:\my\test\path.exe";
        Assert.AreEqual(@"C:\my\test\path.exe", s.Path);
    }
    [TestMethod]
    public void TestDescription()
    {
        using var s = new WindowsShortcut();
        Assert.AreEqual(string.Empty, s.Description);
        s.Description = "description";
        Assert.AreEqual("description", s.Description);
    }
    [TestMethod]
    public void TestArguments()
    {
        using var s = new WindowsShortcut();
        Assert.AreEqual(string.Empty, s.Arguments);
        s.Arguments = "my arguments";
        Assert.AreEqual("my arguments", s.Arguments);
    }
    [TestMethod]
    public void TestWorkingDirectory()
    {
        using var s = new WindowsShortcut();
        Assert.AreEqual(string.Empty, s.WorkingDirectory);
        s.WorkingDirectory = @"C:\my\test\path";
        Assert.AreEqual(@"C:\my\test\path", s.WorkingDirectory);
    }
    [TestMethod]
    public void TestShowCommand()
    {
        using var s = new WindowsShortcut();
        Assert.AreEqual(ProcessWindowStyle.Normal, s.ShowCommand);
        s.ShowCommand = ProcessWindowStyle.Maximized;
        Assert.AreEqual(ProcessWindowStyle.Maximized, s.ShowCommand);
    }
    [TestMethod]
    public void TestIconLocation()
    {
        using var s = new WindowsShortcut();
        Assert.AreEqual(IconLocation.None, s.IconLocation);

        var loc = new IconLocation(@"C:\dir\icon.ico");
        s.IconLocation = loc;
        Assert.AreEqual(loc, s.IconLocation);

        loc = new IconLocation(@"C:\dir\icons.dll", 10);
        s.IconLocation = loc;
        Assert.AreEqual(loc, s.IconLocation);
    }
    [TestMethod]
    public void TestSaveAndLoad()
    {
        using var s = new WindowsShortcut();
        s.Path = @"C:\my\test\path.exe";

        var fileName = Path.GetTempFileName();
        try
        {
            s.Save(fileName);
            Assert.IsTrue(File.Exists(fileName));

            using var s2 = WindowsShortcut.Load(fileName);
            Assert.AreEqual(s.Path, s2.Path);
        }
        finally
        {
            File.Delete(fileName);
        }
    }
}
