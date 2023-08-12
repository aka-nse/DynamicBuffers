using DynamicBuffers.Test.TestCases;
using System;
using System.Linq;
using Xunit;

namespace DynamicBuffers.Test;

public partial class TypedMessageTest
{
    [Fact]
    public void Create()
    {
        var x = new TestCaseMessage
        {
            FieldOneOf1Bool = true,
        };
        Console.WriteLine(x.FieldOneOf1Double);


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
            Assert.Contains(msg.Metadata, x => x.Name == typeName);
        }

        Assert.Equal(content, msg.Content.Unpack<TestCaseMessage>());
    }
}
