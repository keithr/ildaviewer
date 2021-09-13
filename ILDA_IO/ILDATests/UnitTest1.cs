using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILDATests;
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
        var ilda = ILDAIO.ILDAIO.Read(new MemoryStream(Resource1.American_Flag_Float));
        Assert.IsNotNull(ilda);
    }
}