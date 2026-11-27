using NUnit.Framework;

namespace Category_Sequential_Parallel_Demo.Tests;

[TestFixture]
[Category("Login")]
public class LoginTests
{
    [Test]
    public void Login_WithValidAccount_ShouldSucceed()
    {
        bool loginSuccessful = true;
        Assert.That(loginSuccessful, Is.True);
    }

    [Test]
    public void Login_WithInvalidAccount_ShouldFail()
    {
        bool loginSuccessful = false;
        Assert.That(loginSuccessful, Is.False);
    }
}
