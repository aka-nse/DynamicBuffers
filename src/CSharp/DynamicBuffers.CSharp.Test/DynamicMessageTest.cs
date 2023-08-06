using DynamicBuffers.Test.TestCases;
using Google.Protobuf;
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
    public static IEnumerable<object[]> TestCases()
    {
        static object[] core(TestCaseMessage message, Func<dynamic, bool> validate)
            => new object[] { message, validate, };

        // single field
        yield return core(
            new TestCaseMessage { FieldDouble = 123.45, },
            message => message.FieldDouble == 123.45
            );
        yield return core(
            new TestCaseMessage { FieldFloat = 123.45f, },
            message => message.FieldFloat == 123.45f
            );
        yield return core(
            new TestCaseMessage { FieldInt32 = 12345, },
            message => message.FieldInt32 == 12345
            );
        yield return core(
            new TestCaseMessage { FieldInt64 = 12345L, },
            message => message.FieldInt64 == 12345L
            );
        yield return core(
            new TestCaseMessage { FieldUint32 = 12345u, },
            message => message.FieldUint32 == 12345u
            );
        yield return core(
            new TestCaseMessage { FieldUint64 = 12345uL, },
            message => message.FieldUint64 == 12345uL
            );
        yield return core(
            new TestCaseMessage { FieldSint32 = 12345, },
            message => message.FieldSint32 == 12345
            );
        yield return core(
            new TestCaseMessage { FieldSint64 = 12345L, },
            message => message.FieldSint64 == 12345L
            );
        yield return core(
            new TestCaseMessage { FieldFixed32 = 12345u, },
            message => message.FieldFixed32 == 12345u
            );
        yield return core(
            new TestCaseMessage { FieldFixed64 = 12345uL, },
            message => message.FieldFixed64 == 12345uL
            );
        yield return core(
            new TestCaseMessage { FieldSfixed32 = 12345, },
            message => message.FieldSfixed32 == 12345
            );
        yield return core(
            new TestCaseMessage { FieldSfixed64 = 12345L, },
            message => message.FieldSfixed64 == 12345L
            );
        yield return core(
            new TestCaseMessage { FieldBool = true, },
            message => message.FieldBool == true
            );
        yield return core(
            new TestCaseMessage { FieldString = "foobar", },
            message => message.FieldString == "foobar"
            );
        yield return core(
            new TestCaseMessage { FieldBytes = ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33), },
            message => Enumerable.SequenceEqual(message.FieldBytes, new byte[] { 0x00, 0x11, 0x22, 0x33 })
            );
        yield return core(
            new TestCaseMessage { FieldEnum1 = TestEnum1.Value1, },
            message => message.FieldEnum1 == (int)TestEnum1.Value1
            );
        yield return core(
            new TestCaseMessage { FieldEnum2 = TestCaseMessage.Types.TestEnum2.Value1, },
            message => message.FieldEnum2 == (int)TestCaseMessage.Types.TestEnum2.Value1
            );
        yield return core(
            new TestCaseMessage { FieldMessage = new InnerMessage3 { Field01 = new InnerMessage1 { Field01 = 123, }, Field02 = new InnerMessage3.Types.InnerMessage2 { Field01 = 456, }, }, },
            message => message.FieldMessage.Field01.Field01 == 123 && message.FieldMessage.Field02.Field01 == 456
            );

        // repeated field
        yield return core(
            new TestCaseMessage { FieldRepeatedDouble = { 123.45, 678.90, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedDouble, new[] { 123.45, 678.90, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedFloat = { 123.45f, 678.90f, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedFloat, new [] { 123.45f, 678.90f, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedInt32 = { 12345, 67890, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedInt32, new [] { 12345, 67890, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedInt64 = { 12345L, 67890L, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedInt64, new [] { 12345L, 67890L, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedUint32 = { 12345u, 67890u }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedUint32, new [] { 12345u, 67890u, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedUint64 = { 12345uL, 67890uL }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedUint64, new [] { 12345uL, 67890uL, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedSint32 = { 12345, 67890, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedSint32, new [] { 12345, 67890, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedSint64 = { 12345L, 67890L }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedSint64, new [] { 12345L, 67890L, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedFixed32 = { 12345u, 67890u, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedFixed32, new [] { 12345u, 67890u, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedFixed64 = { 12345uL, 67890uL, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedFixed64, new [] { 12345uL, 67890uL, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedSfixed32 = { 12345, 67890, } },
            message => Enumerable.SequenceEqual(message.FieldRepeatedSfixed32, new [] { 12345, 67890, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedSfixed64 = { 12345L, 67890L, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedSfixed64, new [] { 12345L, 67890L, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedBool = { true, false, true, false, }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedBool, new [] { true, false, true, false, })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedString = { "foobar", "barbaz", "bazfoo", }, },
            message => Enumerable.SequenceEqual(message.FieldRepeatedString, new [] { "foobar", "barbaz", "bazfoo", })
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedBytes = {
                    ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33),
                    ByteString.CopyFrom(0x44, 0x55, 0x66, 0x77),
                }, },
            message =>
            {
                return Enumerable.SequenceEqual(message.FieldRepeatedBytes[0], new byte[] { 0x00, 0x11, 0x22, 0x33, })
                    && Enumerable.SequenceEqual(message.FieldRepeatedBytes[1], new byte[] { 0x44, 0x55, 0x66, 0x77, });
            });
        yield return core(
            new TestCaseMessage { FieldRepeatedEnum1 = { TestEnum1.Value1, TestEnum1.Value2, } },
            message => message.FieldRepeatedEnum1[0] == (int)TestEnum1.Value1
                && message.FieldRepeatedEnum1[1] == (int)TestEnum1.Value2
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedEnum2 = { TestCaseMessage.Types.TestEnum2.Value1, TestCaseMessage.Types.TestEnum2.Value2, }, },
            message => message.FieldRepeatedEnum2[0] == (int)TestCaseMessage.Types.TestEnum2.Value1
                && message.FieldRepeatedEnum2[1] == (int)TestCaseMessage.Types.TestEnum2.Value2
            );
        yield return core(
            new TestCaseMessage { FieldRepeatedMessage = {
                    new InnerMessage3 { Field01 = new InnerMessage1 { Field01 = 123, }, },
                    new InnerMessage3 { Field02 = new InnerMessage3.Types.InnerMessage2 { Field01 = 456, }, },
                    new InnerMessage3 { Field03 = Any.Pack(new InnerMessage1{ Field02 = "foobar", }), },
                    new InnerMessage3 { Field04 = Any.Pack(new InnerMessage3.Types.InnerMessage2{ Field02 = "barbaz", }), },
                    new InnerMessage3 { Field05 = { new InnerMessage1{ Field01 = 1, }, new InnerMessage1 { Field02 = "foo", } }, },
                    new InnerMessage3 { Field06 = { new InnerMessage3.Types.InnerMessage2 { Field01 = 2, }, new InnerMessage3.Types.InnerMessage2 { Field02 = "bar", } }, },
                    new InnerMessage3 { Field07 = {
                        Any.Pack(new InnerMessage1 { Field01 = 3, }),
                        Any.Pack(new InnerMessage3.Types.InnerMessage2{ Field02 = "baz", }),
                    }, },
                } },
            message => message.FieldRepeatedMessage[0].Field01.Field01 == 123
                && message.FieldRepeatedMessage[1].Field02.Field01 == 456
                && message.FieldRepeatedMessage[2].Field03.Field02 == "foobar"
                && message.FieldRepeatedMessage[3].Field04.Field02 == "barbaz"
                && message.FieldRepeatedMessage[4].Field05[0].Field01 == 1
                && message.FieldRepeatedMessage[4].Field05[1].Field02 == "foo"
                && message.FieldRepeatedMessage[5].Field06[0].Field01 == 2
                && message.FieldRepeatedMessage[5].Field06[1].Field02 == "bar"
                && message.FieldRepeatedMessage[6].Field07[0].Field01 == 3
                && message.FieldRepeatedMessage[6].Field07[1].Field02 == "baz"

            );

        yield break;
    }

    [Theory]
    [MemberData(nameof(TestCases))]
    public void Test(TestCaseMessage message, Func<dynamic, bool> validate)
    {
        var typedMessage = TypedMessage.Create(message);
        Assert.True(validate(typedMessage.AsDynamic()));
    }
}
