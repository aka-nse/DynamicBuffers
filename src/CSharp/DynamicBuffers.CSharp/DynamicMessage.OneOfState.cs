using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicBuffers;

partial class DynamicMessage
{
    private class OneOfState
    {
        public string Name { get; }
        public IReadOnlyList<FieldDescriptorProto> CandidateFields { get; }
        public string CurrentCase { get; private set; } = "None";

        public OneOfState(int index, string name, IEnumerable<FieldDescriptorProto> fields)
        {
            Name = name;
            CandidateFields = fields
                .Where(field => field.HasOneofIndex && field.OneofIndex == index)
                .ToList();
        }

        public void SetCase(FieldDescriptorProto field)
        {
            // escape special name
            CurrentCase = field.Name switch
            {
                "None" => "None_",
                _ => field.Name,
            };
        }

        public (bool hasValue, object? value) TryGetValue(string memberName, IFieldNameComparer comparer, IReadOnlyDictionary<string, object?> fields)
        {
            if (comparer.Equals(memberName, Name))
            {
                return (true, fields.TryGetValue(CurrentCase, out var value) ? value : null);
            }
            if (CandidateFields.SingleOrDefault(field => comparer.Equals(field.Name, memberName)) is { } unassignedField)
            {
                return (true, unassignedField.GetDefault());
            }
            if (comparer.Equals(memberName, Name + "Case"))
            {
                return (
                    true,
                    CurrentCase switch
                    {
                        // handle special name
                        "None_" => "None_",
                        _ => comparer.ConvertName(CurrentCase, stackalloc char[CurrentCase.Length]).ToString(),
                    });
            }
            if (memberName.StartsWith("Has"))
            {
                var hasMember = memberName.Substring("Has".Length);
                if (CandidateFields.SingleOrDefault(field => comparer.Equals(field.Name, hasMember)) is { } field)
                {
                    return (true, comparer.Equals(CurrentCase, field.Name));
                }
            }
            return (false, default);
        }
    }
}
