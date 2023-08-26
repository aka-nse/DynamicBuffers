#!/usr/bin/env python3
"""
Extensions for TypedMessage
"""
from typing import Iterable, Mapping

import google.protobuf.message as _pbm
import google.protobuf.descriptor as _pbd
import google.protobuf.any_pb2 as _pb_any
from ._helpers import to_modified_proto
from .proto.TypedMessage_pb2 import TypedMessage as _TypedMessage  # type: ignore # noqa


_FieldNumberMap = Mapping[int, _pbd.FieldDescriptor]


def _unpack(
        any_: _pb_any.Any,
        type_registry: frozenset[type]) -> _pbm.Message | None:
    for t in type_registry:
        msg = t()
        if any_.Is(msg.DESCRIPTOR):
            any_.Unpack(msg)
            return msg
    return None


def _TypedMessage__create(
        content: _pbm.Message,
        type_registry: frozenset[type] | None = None) -> _TypedMessage:
    def visit_definition(
            metadata: set[_pbd.Descriptor],
            descriptor: _pbd.Descriptor) -> None:
        if descriptor in metadata:
            return
        metadata.add(descriptor)
        fields: _FieldNumberMap = descriptor.fields_by_number
        for field_id in fields:
            field = fields[field_id]
            if field.type == _pbd.FieldDescriptor.TYPE_MESSAGE:
                visit_definition(metadata, field.message_type)

    def visit_instance(
            metadata: set[_pbd.Descriptor],
            type_registry: frozenset[type],
            message: _pbm.Message | None) -> None:
        if message is None:
            return
        if isinstance(message, _pb_any.Any):
            metadata.add(_pb_any.Any.DESCRIPTOR)
            any_content = _unpack(message, type_registry)
            visit_instance(metadata, type_registry, any_content)
            return
        visit_definition(metadata, message.DESCRIPTOR)
        fields: _FieldNumberMap = \
            message.DESCRIPTOR.fields_by_number
        for field_id in fields:
            field = fields[field_id]
            if (field.label == _pbd.FieldDescriptor.LABEL_REPEATED and
                    field.message_type and
                    field.message_type.GetOptions().map_entry):
                raise TypeError('map field is not supported.')
            if field.type == _pbd.FieldDescriptor.TYPE_MESSAGE:
                if field.label == _pbd.FieldDescriptor.LABEL_REPEATED:
                    children: Iterable[_pbm.Message] = \
                        getattr(message, field.name)
                    for child in children:
                        visit_instance(metadata, type_registry, child)
                else:
                    child = getattr(message, field.name)
                    visit_instance(metadata, type_registry, child)
            else:
                continue

    metadata: set[_pbd.Descriptor] = set()
    if type_registry is None:
        type_registry = frozenset()
    visit_instance(metadata, type_registry, content)
    retval = _TypedMessage()
    for descr in metadata:
        retval.metadata.append(to_modified_proto(descr))
    retval.content.Pack(content)
    return retval


_TypedMessage.create = _TypedMessage__create
