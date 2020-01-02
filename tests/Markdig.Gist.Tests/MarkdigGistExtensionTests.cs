using System;
using Xunit;

namespace Markdig.Gist.Tests
{
    public class MarkdigGistExtensionTests
    {
        [Fact]
        public void ConstructorDoesNotRequireWrapper()
        {
            // Does not throw
            var extension = new MarkdigGistExtension();
        }
    }
}
