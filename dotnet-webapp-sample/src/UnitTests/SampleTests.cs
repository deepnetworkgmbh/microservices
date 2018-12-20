using FluentAssertions;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class SampleTests
    {
        // NUnit. take a look on "writing tests" clause in the wiki: https://github.com/nunit/docs/wiki/NUnit-Documentation
        // Fluent samples: https://fluentassertions.com/examples/

        [Test]
        public void Succeed()
        {
            true.Should().Be(true, "happy path");
        }
    }
}
