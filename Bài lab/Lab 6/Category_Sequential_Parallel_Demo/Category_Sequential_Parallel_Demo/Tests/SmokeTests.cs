using NUnit.Framework;

namespace Category_Sequential_Parallel_Demo.Tests;

[TestFixture]
[Category("Smoke")]
public class SmokeTests
{
    [Test]
    public void HomePage_ShouldLoad()
    {
        Assert.That("Home", Is.Not.Empty);
    }

    [Test]
    public void ProductName_ShouldBeDisplayed()
    {
        Assert.That("Laptop", Is.EqualTo("Laptop"));
    }
}
