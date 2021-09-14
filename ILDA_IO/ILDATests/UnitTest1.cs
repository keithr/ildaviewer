using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ILDATests;
[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void ReadFlag()
    {
        var ilda = ILDAIO.ILDAIO.Read(new MemoryStream(Resource1.American_Flag_Float));
        Assert.IsNotNull(ilda);
        Assert.AreEqual(151, ilda.Count);
    }

    [TestMethod]
    public void Normalize()
    {
        var ilda = ILDAIO.ILDAIO.Read(new MemoryStream(Resource1.American_Flag_Float));
        var nilda = ILDAIO.NormalizedILDA.Create(ilda);
        Assert.IsNotNull(nilda);
        Assert.AreEqual(151, nilda.Count);
    }

    [TestMethod]
    public void NormalizeWrite()
    {
        var ilda = ILDAIO.ILDAIO.Read(new MemoryStream(Resource1.American_Flag_Float));
        var nilda = ILDAIO.NormalizedILDA.Create(ilda);
        Assert.IsNotNull(nilda);
        Assert.AreEqual(151, nilda.Count);
        nilda.Write("");
    }
}