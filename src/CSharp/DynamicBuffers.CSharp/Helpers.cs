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
}
