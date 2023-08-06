using DynamicBuffers.Test.TestCases;
using System.Linq;
using Xunit;

namespace DynamicBuffers.Test;

public partial class TypedMessageTest
{
    [Fact]
    public void Create()
    {
        var metadatas = new string[]
        {
            "DynamicBuffers.Test.TestCases.TestCaseMessage",
            "DynamicBuffers.Test.TestCases.InnerMessage3",
            "DynamicBuffers.Test.TestCases.InnerMessage3.InnerMessage2",
            "DynamicBuffers.Test.TestCases.InnerMessage1",
        };

        var content = new TestCaseMessage();
        var msg = TypedMessage.Create(content);
        foreach(var typeName in metadatas)
        {
            Assert.True(msg.Metadata.Any(x => x.Name == typeName));
        }

        Assert.Equal(content, msg.Content.Unpack<TestCaseMessage>());
    }
}
