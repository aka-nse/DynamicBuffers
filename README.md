# DynamicBuffers

introduce dynamic programming into Protocol Buffers

## general conventions

Dynamic Buffers provides a way to access fields of Protocol Buffers message dynamically accordance with each language style.
The common principle via all languages are followings:

- In default, field access is based on the field name which is converted into each language naming style;
  in some case there are ways to access by other naming style.

- If the field type is a message, its return value is also supported dynamically access.

- To access a field in the languages which supports dynamic typing, you can just write field access in normal style;
  otherwise, the method to access field with name is provided.

- If the type of field in .proto is `google.protobuf.Any`, the value of its field is unpacked automatically.

- If the field is candidate of `oneof`, following APIs are provided:
  - *oneof case selector*:  
    the way to determine which field is assigned actually is provided by styles each language protoc implementation.
  - *oneof name as field*:  
    the `oneof` name can be as a field name; its return value is actually assigned value.
  - *the field itself*:  
    also normally field access is supported.

- Sorry, mapped field is not supported.
