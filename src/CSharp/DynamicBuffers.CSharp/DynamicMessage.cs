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

namespace DynamicBuffers;

/// <summary>
/// The dynamic access surface type for <see cref="TypedMessage"/> instance.
/// </summary>
public sealed class DynamicMessage : DynamicObject
{
    private sealed class TypeNameComparer : IEqualityComparer<string>
    {
        public static IEqualityComparer<string> Instance { get; } = new TypeNameComparer();

        private TypeNameComparer() { }

        bool IEqualityComparer<string>.Equals(string x, string y) => Equals(x, y);
        public static bool Equals(string x, string y)
            => TakeTypeName(x).SequenceEqual(TakeTypeName(y));

        int IEqualityComparer<string>.GetHashCode(string obj) => GetHashCode(obj);
        public static int GetHashCode(string obj)
        {
            var name = TakeTypeName(obj);
            var hash = 0;
            foreach (var c in name)
            {
                hash = ((hash << 5) | (hash >> 27)) ^ c;
            }
            return hash;
        }

        private static ReadOnlySpan<char> TakeTypeName(ReadOnlySpan<char> typeUrlLike)
        {
            var lastSlash = typeUrlLike.LastIndexOf('/');
            if (lastSlash >= 0)
            {
                return typeUrlLike.Slice(lastSlash + 1);
            }
            if (typeUrlLike.Length > 0 && typeUrlLike[0] == '.')
            {
                return typeUrlLike.Slice(1);
            }
            return typeUrlLike;
        }
    }


    private readonly Dictionary<string, object?> _fields;


    /// <summary>
    /// Creates a new instance of <see cref="DynamicMessage"/>.
    /// </summary>
    /// <param name="targetTypeName"></param>
    /// <param name="metadata"></param>
    /// <param name="content"></param>
    /// <exception cref="NotSupportedException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public DynamicMessage(string targetTypeName, IReadOnlyDictionary<string, DescriptorProto> metadata, CodedInputStream content)
    {
        static object? readNext(FieldDescriptorProto fieldInfo, IReadOnlyDictionary<string, DescriptorProto> metadata, CodedInputStream content)
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
                ProtoFieldType.Message  => new DynamicMessage(fieldInfo.TypeName, metadata, content.ReadBytes().CreateCodedInput()),
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
                var typeUrl = dynamicFieldValue.GetMemberOrThrow<string>(nameof(Any.TypeUrl), new ArgumentException("Member resolution was failed."));
                var value = dynamicFieldValue.GetMemberOrThrow<ByteString>(nameof(Any.Value), new ArgumentException("Member resolution was failed."));
                return new DynamicMessage(typeUrl, metadata, value.CreateCodedInput());
            }
            return fieldValue;
        }

        _fields = new(TypeNameComparer.Instance);
        var typeInfo = metadata.TryGetValue(targetTypeName, out var info)
            ? info
            : throw new ArgumentException("Member resolution was failed.");
        foreach (var fieldInfo in typeInfo.Field)
        {
            var fieldValue = readNext(fieldInfo, metadata, content);
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
                    list.Add(fieldValue);
                    continue;
                case ProtoFieldLabel.Optional:
                    _fields.Add(fieldInfo.Name, fieldValue);
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
    /// <param name="metadata"></param>
    /// <param name="message"></param>
    public DynamicMessage(IReadOnlyDictionary<string, DescriptorProto> metadata, IMessage message)
        : this(message.Descriptor.FullName, metadata, message.ToByteString().CreateCodedInput())
    {
    }


    /// <summary>
    /// Creates a new instance of <see cref="DynamicMessage"/>.
    /// </summary>
    /// <param name="metadata"></param>
    /// <param name="message"></param>
    public DynamicMessage(IReadOnlyDictionary<string, DescriptorProto> metadata, Any message)
        : this(message.TypeUrl, metadata, message.Value.CreateCodedInput())
    {
    }


    /// <inheritdoc />
    public override bool TryGetMember(GetMemberBinder binder, out object result)
        => TryGetMember(binder.Name, out result!);


    private bool TryGetMember(string memberName, out object? result)
        => _fields.TryGetValue(memberName, out result);


    private T GetMemberOrThrow<T>(string memberName, Exception error)
        => TryGetMember(memberName, out var result) && result is T tResult
            ? tResult
            : throw error;
}
