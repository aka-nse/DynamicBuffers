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
        msg => msg.FieldDouble == 123.45
        );

    [Fact]
    public void FieldFloat() => TestCore(
        new TestCaseMessage { FieldFloat = 123.45f, },
        msg => msg.FieldFloat == 123.45f
        );

    [Fact]
    public void FieldInt32() => TestCore(
        new TestCaseMessage { FieldInt32 = 12345, },
        msg => msg.FieldInt32 == 12345
        );

    [Fact]
    public void FieldInt64() => TestCore(
        new TestCaseMessage { FieldInt64 = 12345L, },
        msg => msg.FieldInt64 == 12345L
        );

    [Fact]
    public void FieldUint32() => TestCore(
        new TestCaseMessage { FieldUint32 = 12345u, },
        msg => msg.FieldUint32 == 12345u
        );

    [Fact]
    public void FieldUint64() => TestCore(
        new TestCaseMessage { FieldUint64 = 12345uL, },
        msg => msg.FieldUint64 == 12345uL
        );

    [Fact]
    public void FieldSint32() => TestCore(
        new TestCaseMessage { FieldSint32 = 12345, },
        msg => msg.FieldSint32 == 12345
        );

    [Fact]
    public void FieldSint64() => TestCore(
        new TestCaseMessage { FieldSint64 = 12345L, },
        msg => msg.FieldSint64 == 12345L
        );

    [Fact]
    public void FieldFixed32() => TestCore(
        new TestCaseMessage { FieldFixed32 = 12345u, },
        msg => msg.FieldFixed32 == 12345u
        );

    [Fact]
    public void FieldFixed64() => TestCore(
        new TestCaseMessage { FieldFixed64 = 12345uL, },
        msg => msg.FieldFixed64 == 12345uL
        );

    [Fact]
    public void FieldSfixed32() => TestCore(
        new TestCaseMessage { FieldSfixed32 = 12345, },
        msg => msg.FieldSfixed32 == 12345
        );

    [Fact]
    public void FieldSfixed64() => TestCore(
        new TestCaseMessage { FieldSfixed64 = 12345L, },
        msg => msg.FieldSfixed64 == 12345L
        );

    [Fact]
    public void FieldBool() => TestCore(
        new TestCaseMessage { FieldBool = true, },
        msg => msg.FieldBool == true
        );

    [Fact]
    public void FieldString() => TestCore(
        new TestCaseMessage { FieldString = "foobar", },
        msg => msg.FieldString == "foobar"
        );

    [Fact]
    public void FieldBytes() => TestCore(
        new TestCaseMessage { FieldBytes = ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33), },
        msg => Enumerable.SequenceEqual(msg.FieldBytes, new byte[] { 0x00, 0x11, 0x22, 0x33 })
        );

    [Fact]
    public void FieldEnum1() => TestCore(
        new TestCaseMessage { FieldEnum1 = TestEnum1.Value1, },
        msg => msg.FieldEnum1 == (int)TestEnum1.Value1
        );
    [Fact]
    public void FieldEnum2() => TestCore(
        new TestCaseMessage { FieldEnum2 = TestCaseMessage.Types.TestEnum2.Value1, },
        msg => msg.FieldEnum2 == (int)TestCaseMessage.Types.TestEnum2.Value1
        );

    [Fact]
    public void FieldMessage() => TestCore(
        new TestCaseMessage { FieldMessage = new InnerMessage3 { Field01 = new InnerMessage1 { Field01 = 123, }, Field02 = new InnerMessage3.Types.InnerMessage2 { Field01 = 456, }, }, },
        msg => msg.FieldMessage.Field01.Field01 == 123 && msg.FieldMessage.Field02.Field01 == 456
        );

    #endregion

    #region repeated field

    [Fact]
    public void FieldRepeatedDouble() => TestCore(
        new TestCaseMessage { FieldRepeatedDouble = { 123.45, 678.90, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedDouble, new[] { 123.45, 678.90, })
        );

    [Fact]
    public void FieldRepeatedFloat() => TestCore(
        new TestCaseMessage { FieldRepeatedFloat = { 123.45f, 678.90f, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedFloat, new[] { 123.45f, 678.90f, })
        );

    [Fact]
    public void FieldRepeatedInt32() => TestCore(
        new TestCaseMessage { FieldRepeatedInt32 = { 12345, 67890, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedInt32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedInt64() => TestCore(
        new TestCaseMessage { FieldRepeatedInt64 = { 12345L, 67890L, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedInt64, new[] { 12345L, 67890L, })
        );
    [Fact]
    public void FieldRepeatedUint32() => TestCore(
        new TestCaseMessage { FieldRepeatedUint32 = { 12345u, 67890u }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedUint32, new[] { 12345u, 67890u, })
        );

    [Fact]
    public void FieldRepeatedUint64() => TestCore(
        new TestCaseMessage { FieldRepeatedUint64 = { 12345uL, 67890uL }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedUint64, new[] { 12345uL, 67890uL, })
        );

    [Fact]
    public void FieldRepeatedSint32() => TestCore(
        new TestCaseMessage { FieldRepeatedSint32 = { 12345, 67890, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedSint32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedSint64() => TestCore(
        new TestCaseMessage { FieldRepeatedSint64 = { 12345L, 67890L }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedSint64, new[] { 12345L, 67890L, })
        );

    [Fact]
    public void FieldRepeatedFixed32() => TestCore(
        new TestCaseMessage { FieldRepeatedFixed32 = { 12345u, 67890u, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedFixed32, new[] { 12345u, 67890u, })
        );

    [Fact]
    public void FieldRepeatedFixed64() => TestCore(
        new TestCaseMessage { FieldRepeatedFixed64 = { 12345uL, 67890uL, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedFixed64, new[] { 12345uL, 67890uL, })
        );

    [Fact]
    public void FieldRepeatedSfixed32() => TestCore(
        new TestCaseMessage { FieldRepeatedSfixed32 = { 12345, 67890, } },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedSfixed32, new[] { 12345, 67890, })
        );

    [Fact]
    public void FieldRepeatedSfixed64() => TestCore(
        new TestCaseMessage { FieldRepeatedSfixed64 = { 12345L, 67890L, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedSfixed64, new[] { 12345L, 67890L, })
        );

    [Fact]
    public void FieldRepeatedBool() => TestCore(
        new TestCaseMessage { FieldRepeatedBool = { true, false, true, false, }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedBool, new[] { true, false, true, false, })
        );

    [Fact]
    public void FieldRepeatedString() => TestCore(
        new TestCaseMessage { FieldRepeatedString = { "foobar", "barbaz", "bazfoo", }, },
        msg => Enumerable.SequenceEqual(msg.FieldRepeatedString, new[] { "foobar", "barbaz", "bazfoo", })
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
        msg =>
        {
            return Enumerable.SequenceEqual(msg.FieldRepeatedBytes[0], new byte[] { 0x00, 0x11, 0x22, 0x33, })
                && Enumerable.SequenceEqual(msg.FieldRepeatedBytes[1], new byte[] { 0x44, 0x55, 0x66, 0x77, });
        });

    [Fact]
    public void FieldRepeatedEnum1() => TestCore(
        new TestCaseMessage { FieldRepeatedEnum1 = { TestEnum1.Value1, TestEnum1.Value2, } },
        msg => msg.FieldRepeatedEnum1[0] == (int)TestEnum1.Value1
            && msg.FieldRepeatedEnum1[1] == (int)TestEnum1.Value2
        );

    [Fact]
    public void FieldRepeatedEnum2() => TestCore(
        new TestCaseMessage { FieldRepeatedEnum2 = { TestCaseMessage.Types.TestEnum2.Value1, TestCaseMessage.Types.TestEnum2.Value2, }, },
        msg => msg.FieldRepeatedEnum2[0] == (int)TestCaseMessage.Types.TestEnum2.Value1
            && msg.FieldRepeatedEnum2[1] == (int)TestCaseMessage.Types.TestEnum2.Value2
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
        msg => msg.FieldRepeatedMessage[0].Field01.Field01 == 123
            && msg.FieldRepeatedMessage[1].Field02.Field01 == 456
            && msg.FieldRepeatedMessage[2].Field03.Field02 == "foobar"
            && msg.FieldRepeatedMessage[3].Field04.Field02 == "barbaz"
            && msg.FieldRepeatedMessage[4].Field05[0].Field01 == 1
            && msg.FieldRepeatedMessage[4].Field05[1].Field02 == "foo"
            && msg.FieldRepeatedMessage[5].Field06[0].Field01 == 2
            && msg.FieldRepeatedMessage[5].Field06[1].Field02 == "bar"
            && msg.FieldRepeatedMessage[6].Field07[0].Field01 == 3
            && msg.FieldRepeatedMessage[6].Field07[1].Field02 == "baz"
        );

    #endregion

    #region oneof field

    private static bool ValidateHasOneOf(dynamic msg, string fieldName)
    {
        var validators = new (string field, Func<dynamic, bool> validator)[]
        {
#pragma warning disable format
            (nameof(TestCaseMessage.HasFieldOneOf1Double  ), msg => msg.HasFieldOneOf1Double  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Float   ), msg => msg.HasFieldOneOf1Float   ),
            (nameof(TestCaseMessage.HasFieldOneOf1Int32   ), msg => msg.HasFieldOneOf1Int32   ),
            (nameof(TestCaseMessage.HasFieldOneOf1Int64   ), msg => msg.HasFieldOneOf1Int64   ),
            (nameof(TestCaseMessage.HasFieldOneOf1Uint32  ), msg => msg.HasFieldOneOf1Uint32  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Uint64  ), msg => msg.HasFieldOneOf1Uint64  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Sint32  ), msg => msg.HasFieldOneOf1Sint32  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Sint64  ), msg => msg.HasFieldOneOf1Sint64  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Fixed32 ), msg => msg.HasFieldOneOf1Fixed32 ),
            (nameof(TestCaseMessage.HasFieldOneOf1Fixed64 ), msg => msg.HasFieldOneOf1Fixed64 ),
            (nameof(TestCaseMessage.HasFieldOneOf1Sfixed32), msg => msg.HasFieldOneOf1Sfixed32),
            (nameof(TestCaseMessage.HasFieldOneOf1Sfixed64), msg => msg.HasFieldOneOf1Sfixed64),
            (nameof(TestCaseMessage.HasFieldOneOf1Bool    ), msg => msg.HasFieldOneOf1Bool    ),
            (nameof(TestCaseMessage.HasFieldOneOf1String  ), msg => msg.HasFieldOneOf1String  ),
            (nameof(TestCaseMessage.HasFieldOneOf1Bytes   ), msg => msg.HasFieldOneOf1Bytes   ),
            (nameof(TestCaseMessage.HasFieldOneOf1Enum1   ), msg => msg.HasFieldOneOf1Enum1   ),
            (nameof(TestCaseMessage.HasFieldOneOf1Enum2   ), msg => msg.HasFieldOneOf1Enum2   ),
            ("hasFieldOneOf1Message"                       , msg => msg.HasFieldOneOf1Message ),
            (nameof(TestCaseMessage.HasFieldOneOf2Double  ), msg => msg.HasFieldOneOf2Double  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Float   ), msg => msg.HasFieldOneOf2Float   ),
            (nameof(TestCaseMessage.HasFieldOneOf2Int32   ), msg => msg.HasFieldOneOf2Int32   ),
            (nameof(TestCaseMessage.HasFieldOneOf2Int64   ), msg => msg.HasFieldOneOf2Int64   ),
            (nameof(TestCaseMessage.HasFieldOneOf2Uint32  ), msg => msg.HasFieldOneOf2Uint32  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Uint64  ), msg => msg.HasFieldOneOf2Uint64  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Sint32  ), msg => msg.HasFieldOneOf2Sint32  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Sint64  ), msg => msg.HasFieldOneOf2Sint64  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Fixed32 ), msg => msg.HasFieldOneOf2Fixed32 ),
            (nameof(TestCaseMessage.HasFieldOneOf2Fixed64 ), msg => msg.HasFieldOneOf2Fixed64 ),
            (nameof(TestCaseMessage.HasFieldOneOf2Sfixed32), msg => msg.HasFieldOneOf2Sfixed32),
            (nameof(TestCaseMessage.HasFieldOneOf2Sfixed64), msg => msg.HasFieldOneOf2Sfixed64),
            (nameof(TestCaseMessage.HasFieldOneOf2Bool    ), msg => msg.HasFieldOneOf2Bool    ),
            (nameof(TestCaseMessage.HasFieldOneOf2String  ), msg => msg.HasFieldOneOf2String  ),
            (nameof(TestCaseMessage.HasFieldOneOf2Bytes   ), msg => msg.HasFieldOneOf2Bytes   ),
            (nameof(TestCaseMessage.HasFieldOneOf2Enum1   ), msg => msg.HasFieldOneOf2Enum1   ),
            (nameof(TestCaseMessage.HasFieldOneOf2Enum2   ), msg => msg.HasFieldOneOf2Enum2   ),
            ("hasFieldOneOf2Message"                       , msg => msg.HasFieldOneOf2Message ),
#pragma warning restore format
        };
        foreach(var (field, validator) in validators)
        {
            return 
                field == fieldName
                ? (bool)(validator(msg) == true)
                : (bool)(validator(msg) == false);
        }
        throw new InvalidOperationException("Never through path");
    }

    [Fact]
    public void FieldOneOfDouble() => TestCore(
        new TestCaseMessage { FieldOneOf1Double = 123.45, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Double)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Double))
            && msg.FieldOneOf1 == 123.45
            && msg.FieldOneOf1Double == 123.45
        );

    [Fact]
    public void FieldOneOfFloat() => TestCore(
        new TestCaseMessage { FieldOneOf1Float = 123.45f, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Float)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Float))
            && msg.FieldOneOf1 == 123.45f
            && msg.FieldOneOf1Float == 123.45f
        );

    [Fact]
    public void FieldOneOfInt32() => TestCore(
        new TestCaseMessage { FieldOneOf1Int32 = 12345, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Int32)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Int32))
            && msg.FieldOneOf1 == 12345
            && msg.FieldOneOf1Int32 == 12345
        );

    [Fact]
    public void FieldOneOfInt64() => TestCore(
        new TestCaseMessage { FieldOneOf1Int64 = 12345L, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Int64)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Int64))
            && msg.FieldOneOf1 == 12345L
            && msg.FieldOneOf1Int64 == 12345L
        );
    [Fact]
    public void FieldOneOfUint32() => TestCore(
        new TestCaseMessage { FieldOneOf1Uint32 = 12345u, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Uint32)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Uint32))
            && msg.FieldOneOf1 == 12345u
            && msg.FieldOneOf1Uint32 == 12345u
        );

    [Fact]
    public void FieldOneOfUint64() => TestCore(
        new TestCaseMessage { FieldOneOf1Uint64 = 12345uL, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Uint64)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Uint64))
            && msg.FieldOneOf1 == 12345uL
            && msg.FieldOneOf1Uint64 == 12345uL
        );

    [Fact]
    public void FieldOneOfSint32() => TestCore(
        new TestCaseMessage { FieldOneOf1Sint32 = 12345, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Sint32)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Sint32))
            && msg.FieldOneOf1 == 12345
            && msg.FieldOneOf1Sint32 == 12345
        );

    [Fact]
    public void FieldOneOfSint64() => TestCore(
        new TestCaseMessage { FieldOneOf1Sint64 = 12345L, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Sint64)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Sint64))
            && msg.FieldOneOf1 == 12345L
            && msg.FieldOneOf1Sint64 == 12345L
        );

    [Fact]
    public void FieldOneOfFixed32() => TestCore(
        new TestCaseMessage { FieldOneOf1Fixed32 = 12345u, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Fixed32)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Fixed32))
            && msg.FieldOneOf1 == 12345u
            && msg.FieldOneOf1Fixed32 == 12345u
        );

    [Fact]
    public void FieldOneOfFixed64() => TestCore(
        new TestCaseMessage { FieldOneOf1Fixed64 = 12345uL, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Fixed64)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Fixed64))
            && msg.FieldOneOf1 == 12345uL
            && msg.FieldOneOf1Fixed64 == 12345uL
        );

    [Fact]
    public void FieldOneOfSfixed32() => TestCore(
        new TestCaseMessage { FieldOneOf1Sfixed32 = 12345 },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Sfixed32)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Sfixed32))
            && msg.FieldOneOf1 == 12345
            && msg.FieldOneOf1Sfixed32 == 12345
        );

    [Fact]
    public void FieldOneOfSfixed64() => TestCore(
        new TestCaseMessage { FieldOneOf1Sfixed64 = 12345L, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Sfixed64)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Sfixed64))
            && msg.FieldOneOf1 == 12345L
            && msg.FieldOneOf1Sfixed64 == 12345L
        );

    [Fact]
    public void FieldOneOfBool() => TestCore(
        new TestCaseMessage { FieldOneOf1Bool = true, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Bool)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Bool))
            && msg.FieldOneOf1 == true
            && msg.FieldOneOf1Bool == true
        );

    [Fact]
    public void FieldOneOfString() => TestCore(
        new TestCaseMessage { FieldOneOf1String =  "foobar", },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1String)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1String))
            && msg.FieldOneOf1 == "foobar"
            && msg.FieldOneOf1String == "foobar"
        );

    [Fact]
    public void FieldOneOfBytes() => TestCore(
        new TestCaseMessage { FieldOneOf1Bytes = ByteString.CopyFrom(0x00, 0x11, 0x22, 0x33), },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Bytes)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Bytes))
            && Enumerable.SequenceEqual(msg.FieldOneOf1, new byte[] { 0x00, 0x11, 0x22, 0x33, })
            && Enumerable.SequenceEqual(msg.FieldOneOf1Bytes, new byte[] { 0x00, 0x11, 0x22, 0x33, })
        );

    [Fact]
    public void FieldOneOfEnum1() => TestCore(
        new TestCaseMessage { FieldOneOf1Enum1 = TestEnum1.Value1, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Enum1)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Enum1))
            && msg.FieldOneOf1 == (int)TestEnum1.Value1
            && msg.FieldOneOf1Enum1 == (int)TestEnum1.Value1
        );

    [Fact]
    public void FieldOneOfEnum2() => TestCore(
        new TestCaseMessage { FieldOneOf1Enum2 = TestCaseMessage.Types.TestEnum2.Value1, },
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Enum2)
            && ValidateHasOneOf(msg, nameof(TestCaseMessage.HasFieldOneOf1Enum2))
            && msg.FieldOneOf1 == (int)TestCaseMessage.Types.TestEnum2.Value1
            && msg.FieldOneOf1Enum2 == (int)TestCaseMessage.Types.TestEnum2.Value1
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
        msg => msg.FieldOneOf1Case == nameof(TestCaseMessage.FieldOneOf1Message)
            && ValidateHasOneOf(msg, "HasFieldOneOf1Message")
            && msg.FieldOneOf1.Field07[0].Field01 == 3
            && msg.FieldOneOf1.Field07[1].Field02 == "baz"
            && msg.FieldOneOf1Message.Field07[0].Field01 == 3
            && msg.FieldOneOf1Message.Field07[1].Field02 == "baz"
        );

    #endregion


    private void TestCore(TestCaseMessage msg, Func<dynamic, bool> validate)
    {
        var typedMessage = TypedMessage.Create(msg);
        Assert.True(validate(typedMessage.AsDynamic()));
    }
}
