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
    #region single field

    [Fact]
    public void FieldDouble() => TestCore(
        new TestCaseMessage { FieldDouble = 123.45, },
        message => message.FieldDouble == 123.45
        );

    [Fact]
    public void FieldFloat() => TestCore(
        new TestCaseMessage { FieldFloat = 123.45f, },
        message => message.FieldFloat == 123.45f
        );

    [Fact]
    public void FieldInt32() => TestCore(
        new TestCaseMessage { FieldInt32 = 12345, },
        message => message.FieldInt32 == 12345
        );

    [Fact]
    public void FieldInt64() => TestCore(
        new TestCaseMessage { FieldInt64 = 12345L, },
        message => message.FieldInt64 == 12345L
        );

    [Fact]
    public void FieldUint32() => TestCore(
        new TestCaseMessage { FieldUint32 = 12345u, },
        message => message.FieldUint32 == 12345u
        );

    [Fact]
    public void FieldUint64() => TestCore(
        new TestCaseMessage { FieldUint64 = 12345uL, },
        message => message.FieldUint64 == 12345uL
        );

    [Fact]
    public void FieldSint32() => TestCore(
        new TestCaseMessage { FieldSint32 = 12345, },
        message => message.FieldSint32 == 12345
        );

    [Fact]
    public void FieldSint64() => TestCore(
        new TestCaseMessage { FieldSint64 = 12345L, },
        message => message.FieldSint64 == 12345L
        );

    [Fact]
    public void FieldFixed32() => TestCore(
        new TestCaseMessage { FieldFixed32 = 12345u, },
        message => message.FieldFixed32 == 12345u
        );

    [Fact]
    public void FieldFixed64() => TestCore(
        new TestCaseMessage { FieldFixed64 = 12345uL, },
        message => message.FieldFixed64 == 12345uL
        );

    [Fact]
    public void FieldSfixed32() => TestCore(
        new TestCaseMessage { FieldSfixed32 = 12345, },
        message => message.FieldSfixed32 == 12345
        );

    [Fact]
    public void FieldSfixed64() => TestCore(
        new TestCaseMessage { FieldSfixed64 = 12345L, },
        message => message.FieldSfixed64 == 12345L
        );

    [Fact]
    public void FieldBool() => TestCore(
        new TestCaseMessage { FieldBool = true, },
        message => message.FieldBool == true
        );

    [Fact]
    public void FieldString() => TestCore(
        new TestCaseMessage { FieldString = "foobar", },
        message => message.FieldString == "foobar"
        );

    [Fact]
    public void FieldBytes() => TestCore(
        new TestCaseMessage { FieldBytes = ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33), },
        message => Enumerable.SequenceEqual(message.FieldBytes, new byte[] { 0x00, 0x11, 0x22, 0x33 })
        );

    [Fact]
    public void FieldEnum1() => TestCore(
        new TestCaseMessage { FieldEnum1 = TestEnum1.Value1, },
        message => message.FieldEnum1 == (int)TestEnum1.Value1
        );
    [Fact]
    public void FieldEnum2() => TestCore(
        new TestCaseMessage { FieldEnum2 = TestCaseMessage.Types.TestEnum2.Value1, },
        message => message.FieldEnum2 == (int)TestCaseMessage.Types.TestEnum2.Value1
        );

    [Fact]
    public void FieldMessage() => TestCore(
        new TestCaseMessage { FieldMessage = new InnerMessage3 { Field01 = new InnerMessage1 { Field01 = 123, }, Field02 = new InnerMessage3.Types.InnerMessage2 { Field01 = 456, }, }, },
        message => message.FieldMessage.Field01.Field01 == 123 && message.FieldMessage.Field02.Field01 == 456
        );

    #endregion

    #region repeated field

    [Fact]
    public void FieldRepeatedDouble() => TestCore(
        new TestCaseMessage { FieldRepeatedDouble = { 123.45, 678.90, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedDouble, new[] { 123.45, 678.90, })
        );

    [Fact]
    public void FieldRepeatedFloat() => TestCore(
        new TestCaseMessage { FieldRepeatedFloat = { 123.45f, 678.90f, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedFloat, new[] { 123.45f, 678.90f, })
        );

    [Fact]
    public void FieldRepeatedInt32() => TestCore(
        new TestCaseMessage { FieldRepeatedInt32 = { 12345, 67890, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedInt32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedInt64() => TestCore(
        new TestCaseMessage { FieldRepeatedInt64 = { 12345L, 67890L, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedInt64, new[] { 12345L, 67890L, })
        );
    [Fact]
    public void FieldRepeatedUint32() => TestCore(
        new TestCaseMessage { FieldRepeatedUint32 = { 12345u, 67890u }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedUint32, new[] { 12345u, 67890u, })
        );

    [Fact]
    public void FieldRepeatedUint64() => TestCore(
        new TestCaseMessage { FieldRepeatedUint64 = { 12345uL, 67890uL }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedUint64, new[] { 12345uL, 67890uL, })
        );

    [Fact]
    public void FieldRepeatedSint32() => TestCore(
        new TestCaseMessage { FieldRepeatedSint32 = { 12345, 67890, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedSint32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedSint64() => TestCore(
        new TestCaseMessage { FieldRepeatedSint64 = { 12345L, 67890L }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedSint64, new[] { 12345L, 67890L, })
        );

    [Fact]
    public void FieldRepeatedFixed32() => TestCore(
        new TestCaseMessage { FieldRepeatedFixed32 = { 12345u, 67890u, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedFixed32, new[] { 12345u, 67890u, })
        );

    [Fact]
    public void FieldRepeatedFixed64() => TestCore(
        new TestCaseMessage { FieldRepeatedFixed64 = { 12345uL, 67890uL, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedFixed64, new[] { 12345uL, 67890uL, })
        );

    [Fact]
    public void FieldRepeatedSfixed32() => TestCore(
        new TestCaseMessage { FieldRepeatedSfixed32 = { 12345, 67890, } },
        message => Enumerable.SequenceEqual(message.FieldRepeatedSfixed32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedSfixed64() => TestCore(
        new TestCaseMessage { FieldRepeatedSfixed64 = { 12345L, 67890L, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedSfixed64, new[] { 12345L, 67890L, })
        );

    [Fact]
    public void FieldRepeatedBool() => TestCore(
        new TestCaseMessage { FieldRepeatedBool = { true, false, true, false, }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedBool, new[] { true, false, true, false, })
        );

    [Fact]
    public void FieldRepeatedString() => TestCore(
        new TestCaseMessage { FieldRepeatedString = { "foobar", "barbaz", "bazfoo", }, },
        message => Enumerable.SequenceEqual(message.FieldRepeatedString, new[] { "foobar", "barbaz", "bazfoo", })
        );

    [Fact]
    public void FieldRepeatedBytes() => TestCore(
        new TestCaseMessage
        {
            FieldRepeatedBytes = {
                    ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33),
                    ByteString.CopyFrom(0x44, 0x55, 0x66, 0x77),
            },
        },
        message =>
        {
            return Enumerable.SequenceEqual(message.FieldRepeatedBytes[0], new byte[] { 0x00, 0x11, 0x22, 0x33, })
                && Enumerable.SequenceEqual(message.FieldRepeatedBytes[1], new byte[] { 0x44, 0x55, 0x66, 0x77, });
        });

    [Fact]
    public void FieldRepeatedEnum1() => TestCore(
        new TestCaseMessage { FieldRepeatedEnum1 = { TestEnum1.Value1, TestEnum1.Value2, } },
        message => message.FieldRepeatedEnum1[0] == (int)TestEnum1.Value1
            && message.FieldRepeatedEnum1[1] == (int)TestEnum1.Value2
        );

    [Fact]
    public void FieldRepeatedEnum2() => TestCore(
        new TestCaseMessage { FieldRepeatedEnum2 = { TestCaseMessage.Types.TestEnum2.Value1, TestCaseMessage.Types.TestEnum2.Value2, }, },
        message => message.FieldRepeatedEnum2[0] == (int)TestCaseMessage.Types.TestEnum2.Value1
            && message.FieldRepeatedEnum2[1] == (int)TestCaseMessage.Types.TestEnum2.Value2
        );

    [Fact]
    public void FieldRepeatedMessage() => TestCore(
        new TestCaseMessage
        {
            FieldRepeatedMessage = {
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
            }
        },
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

    #endregion

    #region oneof field

    [Fact]
    public void FieldOneOfDouble() => TestCore(
        new TestCaseMessage { FieldOneOf1Double = 123.45, },
        message => message.FieldOneOf1Double == 123.45
        );

    [Fact]
    public void FieldOneOfFloat() => TestCore(
        new TestCaseMessage { FieldOneOf1Float = 123.45f, },
        message => message.FieldOneOf1Float == 123.45f
        );

    [Fact]
    public void FieldOneOfInt32() => TestCore(
        new TestCaseMessage { FieldOneOf1Int32 = 12345, },
        message => message.FieldOneOf1Int32 == 12345
        );

    [Fact]
    public void FieldOneOfInt64() => TestCore(
        new TestCaseMessage { FieldOneOf1Int64 = 12345L, },
        message => message.FieldOneOf1Int64 == 12345L
        );
    [Fact]
    public void FieldOneOfUint32() => TestCore(
        new TestCaseMessage { FieldOneOf1Uint32 = 12345u, },
        message => message.FieldOneOf1Uint32 == 12345u
        );

    [Fact]
    public void FieldOneOfUint64() => TestCore(
        new TestCaseMessage { FieldOneOf1Uint64 = 12345uL, },
        message => message.FieldOneOf1Uint64 == 12345uL
        );

    [Fact]
    public void FieldOneOfSint32() => TestCore(
        new TestCaseMessage { FieldOneOf1Sint32 = 12345, },
        message => message.FieldOneOf1Sint32 == 12345
        );

    [Fact]
    public void FieldOneOfSint64() => TestCore(
        new TestCaseMessage { FieldOneOf1Sint64 = 12345L, },
        message => message.FieldOneOf1Sint64 == 12345L
        );

    [Fact]
    public void FieldOneOfFixed32() => TestCore(
        new TestCaseMessage { FieldOneOf1Fixed32 = 12345u, },
        message => message.FieldOneOf1Fixed32 == 12345u
        );

    [Fact]
    public void FieldOneOfFixed64() => TestCore(
        new TestCaseMessage { FieldOneOf1Fixed64 = 12345uL, },
        message => message.FieldOneOf1Fixed64 == 12345uL
        );

    [Fact]
    public void FieldOneOfSfixed32() => TestCore(
        new TestCaseMessage { FieldOneOf1Sfixed32 = 12345 },
        message => message.FieldOneOf1Sfixed32 == 12345
        );

    [Fact]
    public void FieldOneOfSfixed64() => TestCore(
        new TestCaseMessage { FieldOneOf1Sfixed64 = 12345L, },
        message => message.FieldOneOf1Sfixed64 == 12345L
        );

    [Fact]
    public void FieldOneOfBool() => TestCore(
        new TestCaseMessage { FieldOneOf1Bool = true, },
        message => message.FieldOneOf1Bool == true
        );

    [Fact]
    public void FieldOneOfString() => TestCore(
        new TestCaseMessage { FieldOneOf1String =  "foobar", },
        message => message.FieldOneOf1String == "foobar"
        );

    [Fact]
    public void FieldOneOfBytes() => TestCore(
        new TestCaseMessage { FieldOneOf1Bytes = ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33), },
        message => Enumerable.SequenceEqual(message.FieldOneOf1Bytes, new byte[] { 0x00, 0x11, 0x22, 0x33, })
        );

    [Fact]
    public void FieldOneOfEnum1() => TestCore(
        new TestCaseMessage { FieldOneOf1Enum1 = TestEnum1.Value1, },
        message => message.FieldOneOf1Enum1 == (int)TestEnum1.Value1
        );

    [Fact]
    public void FieldOneOfEnum2() => TestCore(
        new TestCaseMessage { FieldOneOf1Enum2 = TestCaseMessage.Types.TestEnum2.Value1, },
        message => message.FieldOneOf1Enum2 == (int)TestCaseMessage.Types.TestEnum2.Value1
        );

    [Fact]
    public void FieldOneOfMessage() => TestCore(
        new TestCaseMessage
        {
            FieldOneOf1Message = 
                    new InnerMessage3 { Field07 = {
                        Any.Pack(new InnerMessage1 { Field01 = 3, }),
                        Any.Pack(new InnerMessage3.Types.InnerMessage2{ Field02 = "baz", }),
                    }, },
        },
        message => message.FieldOneOf1Message.Field07[0].Field01 == 3
            && message.FieldOneOf1Message.Field07[1].Field02 == "baz"
        );

    #endregion


    private void TestCore(TestCaseMessage message, Func<dynamic, bool> validate)
    {
        var typedMessage = TypedMessage.Create(message);
        Assert.True(validate(typedMessage.AsDynamic()));
    }
}
