using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicBuffers;

partial class DynamicMessage
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
            => DynamicMessage.GetHashCode(TakeTypeName(obj));

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


}
