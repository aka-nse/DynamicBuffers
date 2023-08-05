using DynamicBuffers.Test.DynamicMessageTestCase;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicBuffers.Test;

public class DynamicMessageTest
{
    [Fact]
    public void Test()
    {
        var msg = TypedMessage.Create(new TestCase01Message());
        foreach(var type in msg.Metadata)
        {
            foreach(var field in type.Field)
            {
                Console.WriteLine(field);
            }
        }
    }
}
