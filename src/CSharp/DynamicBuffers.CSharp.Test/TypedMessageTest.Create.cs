using DynamicBuffers.Test.TypedMessageTestCase;
using Xunit;

namespace DynamicBuffers.Test;

public partial class TypedMessageTest
{
    [Fact]
    public void Test01()
    {
        var content = new TestCase01Message();
        var msg = TypedMessage.Create(content);
        Assert.Equal("DynamicBuffers.Test.TypedMessageTestCase.TestCase01Message", msg.Metadata[0].Name);
        Assert.Equal(content, msg.Content.Unpack<TestCase01Message>());
    }
}
