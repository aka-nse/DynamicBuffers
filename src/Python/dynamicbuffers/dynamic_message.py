#!/usr/bin/env python3
"""
Wrapper of TypedMessage to access dynamically
"""
import google.protobuf.descriptor_pb2 as _pbd_pb2
import google.protobuf.any_pb2 as _pb_any
import google.protobuf.internal.decoder as _pb_dec
import google.protobuf.internal.wire_format as _pb_wf
from typing import Any, cast
from collections.abc import Iterable, Mapping
from .field_name_pattern import FieldNamePattern
from ._helpers import find_single, to_modified_type_name


class DynamicMessage:

    _is_packed_repeated_typeset = set([
        _pbd_pb2.FieldDescriptorProto.TYPE_DOUBLE,
        _pbd_pb2.FieldDescriptorProto.TYPE_FLOAT,
        _pbd_pb2.FieldDescriptorProto.TYPE_INT64,
        _pbd_pb2.FieldDescriptorProto.TYPE_UINT64,
        _pbd_pb2.FieldDescriptorProto.TYPE_INT32,
        _pbd_pb2.FieldDescriptorProto.TYPE_FIXED64,
        _pbd_pb2.FieldDescriptorProto.TYPE_FIXED32,
        _pbd_pb2.FieldDescriptorProto.TYPE_BOOL,
        _pbd_pb2.FieldDescriptorProto.TYPE_UINT32,
        _pbd_pb2.FieldDescriptorProto.TYPE_ENUM,
        _pbd_pb2.FieldDescriptorProto.TYPE_SFIXED32,
        _pbd_pb2.FieldDescriptorProto.TYPE_SFIXED64,
        _pbd_pb2.FieldDescriptorProto.TYPE_SINT32,
        _pbd_pb2.FieldDescriptorProto.TYPE_SINT64,
    ])

    @staticmethod
    def _is_packed_repeated(
            wire_type: int,
            field_info: _pbd_pb2.FieldDescriptorProto,
            ) -> bool:
        if wire_type != _pb_wf.WIRETYPE_LENGTH_DELIMITED:
            return False
        return field_info.type in DynamicMessage._is_packed_repeated_typeset

    @staticmethod
    def read_next(
            field_info: _pbd_pb2.FieldDescriptorProto,
            metadata: Mapping[str, _pbd_pb2.DescriptorProto],
            buffer: memoryview,
            pos: int,
            end: int,
            field_name_pattern: FieldNamePattern,
            ) -> tuple[Any, int]:
        ...  # TODO: not implemented

    @staticmethod
    def read_next_packed_repeated(
            field_info: _pbd_pb2.FieldDescriptorProto,
            buffer: memoryview,
            pos: int,
            field_value: list[Any],
            ) -> int:
        ...  # TODO: not implemented

    def __init__(
            self,
            target_type_name: str,
            metadata: Mapping[str, _pbd_pb2.DescriptorProto],
            content: bytes,
            field_name_pattern: FieldNamePattern,
            ):
        fields: dict[str, Any] = {}
        self._fields = fields
        type_info = metadata.get(to_modified_type_name(target_type_name))
        if type_info is None:
            raise RuntimeError('member resolution was failed.')
        buffer = memoryview(content)
        pos = 0
        end = len(buffer)
        while pos < end:
            tag_bytes, pos = _pb_dec.ReadTag(buffer, pos)
            tag, _ = _pb_dec._DecodeVarint(tag_bytes, 0)
            field_number, wire_type = _pb_wf.UnpackTag(tag)
            field_info = find_single(
                type_info.field,
                lambda x: x.number == field_number)

            if field_info.label == field_info.LABEL_REPEATED:
                if field_info.name in fields.keys():
                    field_value = cast(list, fields[field_info.name])
                else:
                    field_value = []
                    fields[field_info.name] = field_value
                if DynamicMessage._is_packed_repeated(wire_type, field_info):
                    pos = DynamicMessage.read_next_packed_repeated(
                        field_info,
                        buffer,
                        pos,
                        end,
                        field_value)
                else:
                    value, pos = DynamicMessage.read_next(
                        field_info,
                        metadata,
                        buffer,
                        pos,
                        end,
                        field_name_pattern)
                    field_value.append(value)
                ...
            elif field_info.label == field_info.LABEL_OPTIONAL:
                value, pos = DynamicMessage.read_next(
                    field_info, metadata, buffer, pos, end, field_name_pattern
                )
                fields[field_info.name] = value
                ...
            elif field_info.label == field_info.LABEL_REQUIRED:
                raise TypeError('Not supported')
                ...
            else:
                raise ValueError()
                ...

    @staticmethod
    def create(
            metadata: Iterable[_pbd_pb2.DescriptorProto],
            message: _pb_any.Any,
            field_name_pattern: FieldNamePattern,
            ):
        metadatamap = {
            to_modified_type_name(desc.name): desc
            for desc in metadata
        }
        return DynamicMessage(
            message.type_url,
            metadatamap,
            message.value,
            field_name_pattern,)
