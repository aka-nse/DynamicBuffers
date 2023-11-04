# Dynamic Buffers for CSharp

## Usage

### basic usage

```CSharp
public static void Sample()
{
    YourMessage message = CreateYourMessage();
    var typedMessage = ToTypedMessage(message);
    ReadDynamically(typedMessage);
}

public static TypedMessage ToTypedMessage(YourMessage message)
{
    // append metadata to enable to read dynamically
    return TypedMessage.Create(message);
}

public static void ReadDynamically(TypedMessage message)
{
    // prepare to read dynamically
    // you can choose field name style
    dynamic dynamicMessage = message.AsDynamic(fieldNamePattern: FieldNamePattern.CSharpName);

    // You can access single field with its field name.
    // In this example, CSharp field name style is available.
    Console.WriteLine(dynamicMessage.Field1);

    // Also you can access repeated field.
    // The field instance supports indexer access.
    Console.WriteLine(dynamicMessage.Field2[0]);

    // If the type of the field is a message excepts google.protobuf.Any,
    // also you can access inner field recursively.
    Console.WriteLine(dynamicMessage.Field3.Field4.Field5[0].Field6);

    // If the type of the field is google.protobuf.Any,
    // the return value of the field is unpacked type.
    Console.WriteLine(dynamicMessage.AnyField.SomeFieldOfPackedType);

    // For oneof fields, DynamicMessage provides following APIs:
    // - <oneof-name>Case : [string] returns the identifier of field actually assigned
    Console.WriteLine(dynamicMessage.OneOf1Case);
    // - <oneof-name> : [type of actually assigned] returns the actual value of the oneof;
    //   or returns default(object) if any oneof field is not assigned.
    Console.WriteLine(dynamicMessage.OneOf1);
    // - Has<field-name> : [bool] returns true if <field-name> is assigned; otherwise false.
    Console.WriteLine(dynamicMessage.HasOneOf1Field1);
    // - <field-name> : [type of the field] returns the actual field value if it is assigned;
    //   otherwise default value.
    Console.WriteLine(dynamicMessage.OneOf1Field1);
}
```

## Coding Conventions

- generally, coding styles shall be obeyed [.Net C# Coding Style](https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md).
  - exceptly double brank lines at out of method is allowed.
