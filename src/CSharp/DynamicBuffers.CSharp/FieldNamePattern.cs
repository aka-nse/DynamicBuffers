using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicBuffers;

/// <summary>
/// Specifies field name matching logic.
/// </summary>
public enum FieldNamePattern
{
    /// <summary> Checks only CSharp naming style. </summary>
    CSharpName,

    /// <summary> Checks only JSON naming style. </summary>
    JsonName,

    /// <summary> Checks only .proto default name. </summary>
    ProtoName,
}
