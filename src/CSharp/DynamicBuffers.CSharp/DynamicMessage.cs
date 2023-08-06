using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Xml.Schema;
using ProtoFieldType = Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type;
using ProtoFieldLabel = Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Linq;
using System.Xml.Linq;

namespace DynamicBuffers;

/// <summary>
/// The dynamic access surface type for <see cref="TypedMessage"/> instance.
/// </summary>
public sealed partial class DynamicMessage : DynamicObject
{
    private static int GetHashCode(ReadOnlySpan<char> name)
    {
        var hash = 0;
        foreach (var c in name)
        {
            hash = ((hash << 5) | (hash >> 27)) ^ c;
        }
        return hash;
    }




    private readonly Dictionary<string, object?> _fields;

    private DynamicMessage(string targetTypeName, IReadOnlyDictionary<string, DescriptorProto> metadata, CodedInputStream content, FieldNamePattern fieldNamePattern)
    {
        static bool IsPackedRepeated(WireFormat.WireType wireType, FieldDescriptorProto fieldInfo)
        {
            if(wireType != WireFormat.WireType.LengthDelimited)
            {
                return false;
            }
            return fieldInfo.Type switch
            {
#pragma warning disable format
                ProtoFieldType.Double   or
                ProtoFieldType.Float    or
                ProtoFieldType.Int64    or
                ProtoFieldType.Uint64   or
                ProtoFieldType.Int32    or
                ProtoFieldType.Fixed64  or
                ProtoFieldType.Fixed32  or
                ProtoFieldType.Bool     or
                ProtoFieldType.Uint32   or
                ProtoFieldType.Enum     or
                ProtoFieldType.Sfixed32 or
                ProtoFieldType.Sfixed64 or
                ProtoFieldType.Sint32   or
                ProtoFieldType.Sint64   => true,
                _ => false,
#pragma warning restore format
            };
        }

        static object? readNext(FieldDescriptorProto fieldInfo, IReadOnlyDictionary<string, DescriptorProto> metadata, CodedInputStream content, FieldNamePattern fieldNamePattern)
        {
            object? fieldValue = fieldInfo.Type switch
            {
#pragma warning disable format
                ProtoFieldType.Double   => content.ReadDouble  (),
                ProtoFieldType.Float    => content.ReadFloat   (),
                ProtoFieldType.Int64    => content.ReadInt64   (),
                ProtoFieldType.Uint64   => content.ReadUInt64  (),
                ProtoFieldType.Int32    => content.ReadInt32   (),
                ProtoFieldType.Fixed64  => content.ReadFixed64 (),
                ProtoFieldType.Fixed32  => content.ReadFixed32 (),
                ProtoFieldType.Bool     => content.ReadBool    (),
                ProtoFieldType.String   => content.ReadString  (),
                ProtoFieldType.Message  => new DynamicMessage(fieldInfo.TypeName, metadata, content.ReadBytes().CreateCodedInput(), fieldNamePattern),
                ProtoFieldType.Bytes    => content.ReadBytes   (),
                ProtoFieldType.Uint32   => content.ReadUInt32  (),
                ProtoFieldType.Enum     => content.ReadEnum    (),
                ProtoFieldType.Sfixed32 => content.ReadSFixed32(),
                ProtoFieldType.Sfixed64 => content.ReadSFixed64(),
                ProtoFieldType.Sint32   => content.ReadSInt32  (),
                ProtoFieldType.Sint64   => content.ReadSInt64  (),

                ProtoFieldType.Group or
                _ => throw new NotSupportedException("Group field is not supported."),
#pragma warning restore format
            };
            if (TypeNameComparer.Equals(fieldInfo.TypeName, Any.Descriptor.FullName))
            {
                if (fieldValue is not DynamicMessage dynamicFieldValue)
                {
                    throw new ArgumentException("Member resolution was failed.");
                }
                var typeUrl = dynamicFieldValue.GetMemberOrThrow<string>(nameof(Any.TypeUrl), () => new ArgumentException("Member resolution was failed."));
                var value = dynamicFieldValue.GetMemberOrThrow<ByteString>(nameof(Any.Value), () => new ArgumentException("Member resolution was failed."));
                return new DynamicMessage(typeUrl, metadata, value.CreateCodedInput(), fieldNamePattern);
            }
            return fieldValue;
        }

        static void readNextPackedRepeated(FieldDescriptorProto fieldInfo, CodedInputStream content, IList destination)
        {
            var buffer = content.ReadBytes().CreateCodedInput();
            switch (fieldInfo.Type)
            {
                case ProtoFieldType.Double:
                    var doubleList = (List<double>)destination;
                    while (!buffer.IsAtEnd) { doubleList.Add(buffer.ReadDouble()); }
                    break;
                case ProtoFieldType.Float:
                    var floatList = (List<float>)destination;
                    while (!buffer.IsAtEnd) { floatList.Add(buffer.ReadFloat()); }
                    break;
                case ProtoFieldType.Int64:
                    var int64List = (List<long>)destination;
                    while (!buffer.IsAtEnd) { int64List.Add(buffer.ReadInt64()); }
                    break;
                case ProtoFieldType.Uint64:
                    var uint64List = (List<ulong>)destination;
                    while (!buffer.IsAtEnd) { uint64List.Add(buffer.ReadUInt64()); }
                    break;
                case ProtoFieldType.Int32:
                    var int32List = (List<int>)destination;
                    while (!buffer.IsAtEnd) { int32List.Add(buffer.ReadInt32()); }
                    break;
                case ProtoFieldType.Fixed64:
                    var fixed64List = (List<ulong>)destination;
                    while(!buffer.IsAtEnd) { fixed64List.Add(buffer.ReadFixed64()); }
                    break;
                case ProtoFieldType.Fixed32:
                    var fixed32List = (List<uint>)destination;
                    while (!buffer.IsAtEnd) { fixed32List.Add(buffer.ReadFixed32()); }
                    break;
                case ProtoFieldType.Bool:
                    var boolList = (List<bool>)destination;
                    while (!buffer.IsAtEnd) { boolList.Add(buffer.ReadBool()); }
                    break;
                case ProtoFieldType.Uint32:
                    var uint32List = (List<uint>)destination;
                    while (!buffer.IsAtEnd) { uint32List.Add(buffer.ReadUInt32()); }
                    break;
                case ProtoFieldType.Enum:
                    var enumList = (List<int>)destination;
                    while (!buffer.IsAtEnd) { enumList.Add(buffer.ReadEnum()); }
                    break;
                case ProtoFieldType.Sfixed32:
                    var sfixed32List = (List<int>)destination;
                    while (!buffer.IsAtEnd) { sfixed32List.Add(buffer.ReadSFixed32()); }
                    break;
                case ProtoFieldType.Sfixed64:
                    var sfixed64List = (List<long>)destination;
                    while (!buffer.IsAtEnd) { sfixed64List.Add(buffer.ReadSFixed64()); }
                    break;
                case ProtoFieldType.Sint32:
                    var sint32List = (List<int>)destination;
                    while (!buffer.IsAtEnd) { sint32List.Add(buffer.ReadSInt32()); }
                    break;
                case ProtoFieldType.Sint64:
                    var sint64List = (List<long>)destination;
                    while (!buffer.IsAtEnd) { sint64List.Add(buffer.ReadSInt64()); }
                    break;
                default:
                    throw new ArgumentException("Invalid packed repeated field.");
            }
        }

        _fields = new(fieldNamePattern switch
        {
            FieldNamePattern.CSharpName => CSharpNameComparer.Instance,
            FieldNamePattern.JsonName => JsonNameComparer.Instance,
            FieldNamePattern.ProtoName => EqualityComparer<string>.Default,
            _ => throw new ArgumentOutOfRangeException("Invalid 'fieldNamePattern'."),
        });
        var typeInfo = metadata.TryGetValue(targetTypeName, out var info)
            ? info
            : throw new ArgumentException("Member resolution was failed.");
        while(!content.IsAtEnd)
        {
            var tag = content.ReadTag();
            var wireType = WireFormat.GetTagWireType(tag);
            var fieldNumber = WireFormat.GetTagFieldNumber(tag);
            var fieldInfo = typeInfo.Field.Single(field => field.Number == fieldNumber);
            switch (fieldInfo.Label)
            {
                case ProtoFieldLabel.Repeated:
                    var list = _fields.TryGetValue(fieldInfo.Name, out var value)
                        ? (IList)value!
                        : (IList)(_fields[fieldInfo.Name] = fieldInfo.Type switch
                        {
#pragma warning disable format
                            ProtoFieldType.Int32    or
                            ProtoFieldType.Sint32   or
                            ProtoFieldType.Sfixed32 => new List<int>(),

                            ProtoFieldType.Int64    or
                            ProtoFieldType.Sint64   or
                            ProtoFieldType.Sfixed64 => new List<long>(),

                            ProtoFieldType.Uint32   or
                            ProtoFieldType.Fixed32  => new List<uint>(),

                            ProtoFieldType.Uint64   or
                            ProtoFieldType.Fixed64  => new List<ulong>(),

                            ProtoFieldType.Float    => new List<float>(),
                            ProtoFieldType.Double   => new List<double>(),
                            ProtoFieldType.Bool     => new List<bool>(),
                            ProtoFieldType.String   => new List<string>(),
                            ProtoFieldType.Message  => new List<DynamicMessage>(),
                            ProtoFieldType.Bytes    => new List<ByteString>(),
                            ProtoFieldType.Enum     => new List<int>(),

                            ProtoFieldType.Group or
                            _ => throw new NotSupportedException("Group field is not supported."),
#pragma warning restore format
                        });
                    if(IsPackedRepeated(wireType, fieldInfo))
                    {
                        // packed repeated
                        readNextPackedRepeated(fieldInfo, content, list);
                    }
                    else
                    {
                        // non-packed repeated
                        var fieldValue = readNext(fieldInfo, metadata, content, fieldNamePattern);
                        list.Add(fieldValue);
                    }
                    continue;

                case ProtoFieldLabel.Optional:
                    {
                        var fieldValue = readNext(fieldInfo, metadata, content, fieldNamePattern);
                        _fields[fieldInfo.Name] = fieldValue;
                    }
                    continue;

                case ProtoFieldLabel.Required:
                    throw new NotSupportedException();

                default:
                    throw new ArgumentException();
            }
        }
    }


    /// <summary>
    /// Creates a new instance of <see cref="DynamicMessage"/>.
    /// </summary>
    /// <param name="targetTypeName"></param>
    /// <param name="metadata"></param>
    /// <param name="content"></param>
    /// <param name="fieldNamePattern"></param>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public DynamicMessage(string targetTypeName, IEnumerable<DescriptorProto> metadata, CodedInputStream content, FieldNamePattern fieldNamePattern = FieldNamePattern.CSharpName)
        : this(targetTypeName, metadata.ToDictionary(type => type.Name, TypeNameComparer.Instance), content, fieldNamePattern)
    {
    }


    /// <summary>
    /// Creates a new instance of <see cref="DynamicMessage"/>.
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="message"></param>
    /// <param name="fieldNamePattern"></param>
    public DynamicMessage(IEnumerable<DescriptorProto> metadata, IMessage message, FieldNamePattern fieldNamePattern = FieldNamePattern.CSharpName)
        : this(message.Descriptor.FullName, metadata, message.ToByteString().CreateCodedInput(), fieldNamePattern)
    {
    }


    /// <summary>
    /// Creates a new instance of <see cref="DynamicMessage"/>.
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="message"></param>
    /// <param name="fieldNamePattern"></param>
    public DynamicMessage(IEnumerable<DescriptorProto> metadata, Any message, FieldNamePattern fieldNamePattern = FieldNamePattern.CSharpName)
        : this(message.TypeUrl, metadata, message.Value.CreateCodedInput(), fieldNamePattern)
    {
    }


    /// <inheritdoc />
    public override bool TryGetMember(GetMemberBinder binder, out object result)
        => TryGetMember(binder.Name, out result!);


    private bool TryGetMember(string memberName, out object? result)
        => _fields.TryGetValue(memberName, out result);


    private T GetMemberOrThrow<T>(string memberName, Func<Exception> error)
        => TryGetMember(memberName, out var result) && result is T tResult
            ? tResult
            : throw error();
}
