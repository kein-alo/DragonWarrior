using NUnit.Framework;

namespace Category_Sequential_Parallel_Demo.Tests;

[TestFixture]
[Category("Slow")]
public class SlowTests
{
    [Test]
    public void SlowTest_Demo()
    {
        // Test minh họa Category Slow; không cố tình chờ lâu.
        Assert.That(2 + 2, Is.EqualTo(4));
    }
}
