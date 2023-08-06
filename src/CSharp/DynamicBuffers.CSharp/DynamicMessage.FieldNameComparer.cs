using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicBuffers;

partial class DynamicMessage
{
    private abstract class FieldNameComparer: IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            var xx = ConvertName(x, stackalloc char[x.Length]);
            var yy = ConvertName(y, stackalloc char[y.Length]);
            return xx.SequenceEqual(yy);
        }

        public int GetHashCode(string obj)
            => DynamicMessage.GetHashCode(ConvertName(obj, stackalloc char[obj.Length]));

        protected abstract ReadOnlySpan<char> ConvertName(ReadOnlySpan<char> input, Span<char> output);
    }


    private sealed class CSharpNameComparer : FieldNameComparer
    {
        public static IEqualityComparer<string> Instance { get; } = new CSharpNameComparer();

        private CSharpNameComparer() { }

        protected override ReadOnlySpan<char> ConvertName(ReadOnlySpan<char> input, Span<char> output)
        {
            var j = 0;
            var isNextUpperCase = true;
            for(var i = 0; i < input.Length; ++i)
            {
                var c = input[i];
                if(c == '_')
                {
                    isNextUpperCase = true;
                }
                else if (isNextUpperCase)
                {
                    output[j] = char.ToUpperInvariant(c);
                    ++j;
                    isNextUpperCase = false;
                }
                else
                {
                    output[j] = c;
                    ++j;
                }
            }
            return output.Slice(0, j);
        }
    }


    private sealed class JsonNameComparer : FieldNameComparer
    {
        public static IEqualityComparer<string> Instance { get; } = new JsonNameComparer();

        private JsonNameComparer() { }

        protected override ReadOnlySpan<char> ConvertName(ReadOnlySpan<char> input, Span<char> output)
        {
            var j = 0;
            var isNextUpperCase = false;
            for (var i = 0; i < input.Length; ++i)
            {
                var c = input[i];
                if (c == '_')
                {
                    isNextUpperCase = true;
                }
                else if (isNextUpperCase)
                {
                    output[j] = char.ToUpperInvariant(c);
                    ++j;
                    isNextUpperCase = false;
                }
                else
                {
                    output[j] = c;
                    ++j;
                }
            }
            return output.Slice(0, j);
        }
    }

}
