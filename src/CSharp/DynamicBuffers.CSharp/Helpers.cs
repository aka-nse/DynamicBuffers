using Google.Protobuf;
using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
namespace DynamicBuffers;


internal static class Helpers
{
    public static DescriptorProto ToModifiedProto(this MessageDescriptor descriptor)
    {
        var retval = descriptor.ToProto();
        retval.Name = descriptor.FullName;
        return retval;
    }

    public static object? GetDefault(this FieldDescriptorProto field)
        => field.Type switch
        {
            FieldDescriptorProto.Types.Type.Int32 or
            FieldDescriptorProto.Types.Type.Sint32 or
            FieldDescriptorProto.Types.Type.Sfixed32 => default(int),
            
            FieldDescriptorProto.Types.Type.Int64 or
            FieldDescriptorProto.Types.Type.Sint64 or
            FieldDescriptorProto.Types.Type.Sfixed64 => default(long),

            FieldDescriptorProto.Types.Type.Uint32 or
            FieldDescriptorProto.Types.Type.Fixed32 => default(uint),

            FieldDescriptorProto.Types.Type.Uint64 or
            FieldDescriptorProto.Types.Type.Fixed64  => default(ulong),
            
            FieldDescriptorProto.Types.Type.Float => default(float),
            FieldDescriptorProto.Types.Type.Double => default(double),
            FieldDescriptorProto.Types.Type.Bool => default(bool),
            FieldDescriptorProto.Types.Type.String => string.Empty,
            FieldDescriptorProto.Types.Type.Bytes => ByteString.Empty,
            FieldDescriptorProto.Types.Type.Enum => default(int),

            _ => null,
        };
}
