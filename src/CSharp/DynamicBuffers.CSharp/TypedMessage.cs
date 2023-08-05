using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using Google.Protobuf;
using Google.Protobuf.Reflection;
using Google.Protobuf.WellKnownTypes;

namespace DynamicBuffers;


public partial class TypedMessage
{
    private sealed class DescriptorComparer : IEqualityComparer<MessageDescriptor>
    {
        public static DescriptorComparer Instance { get; } = new ();

        public bool Equals(MessageDescriptor x, MessageDescriptor y)
            => x?.FullName == y?.FullName;

        public int GetHashCode(MessageDescriptor obj)
            => obj?.FullName.GetHashCode() ?? 0;
    }


    /// <summary>
    /// Creates a new instance of <see cref="TypedMessage"/> with specified content.
    /// </summary>
    /// <param name="content"></param>
    /// <param name="registry"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static TypedMessage Create(IMessage content, TypeRegistry? registry = null)
    {
        static void visitDefinition(HashSet<MessageDescriptor> metadata, MessageDescriptor descriptor)
        {
            if (!metadata.Add(descriptor))
            {
                return;
            }
            foreach (var field in descriptor.Fields.InFieldNumberOrder())
            {
                if (field.FieldType == FieldType.Message)
                {
                    visitDefinition(metadata, field.MessageType);
                }
            }
        }

        static void visitInstance(HashSet<MessageDescriptor> metadata, TypeRegistry registry, IMessage message)
        {
            if (message is null)
            {
                return;
            }
            if (message is Any any)
            {
                metadata.Add(Any.Descriptor);
                visitInstance(metadata, registry, any.Unpack(registry));
                return;
            }

            visitDefinition(metadata, message.Descriptor);
            foreach (var field in message.Descriptor.Fields.InFieldNumberOrder())
            {
                if (field.IsMap)
                {
                    throw new NotSupportedException("map field is not supported.");
                }
                switch (field.FieldType)
                {
                    case FieldType.Message:
                    case FieldType.Group:
                        if(field.IsRepeated)
                        {
                            var children = (IReadOnlyList<IMessage>)field.Accessor.GetValue(message);
                            foreach (var child in children)
                            {
                                visitInstance(metadata, registry, child);
                            }
                        }
                        else
                        {
                            var child = (IMessage)field.Accessor.GetValue(message);
                            visitInstance(metadata, registry, child);
                        }
                        continue;
                    default:
                        continue;
                }
            }
        }

        var metadata = new HashSet<MessageDescriptor>(DescriptorComparer.Instance);
        visitInstance(metadata, registry ?? TypeRegistry.Empty, content);
        var retval = new TypedMessage();
        retval.Metadata.AddRange(metadata.Select(descr => descr.ToModifiedProto()));
        retval.Content = Any.Pack(content);
        return retval;
    }


    /// <summary>
    /// Gets dynamic access surface for this <see cref="TypedMessage"/> instance.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public dynamic AsDynamic()
    {
        throw new NotImplementedException();
    }
}
