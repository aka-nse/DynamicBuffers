#!/usr/bin/env python3
import google.protobuf.descriptor as _pbd
import google.protobuf.descriptor_pb2 as _pbd2


def to_modified_proto(descr: _pbd.Descriptor):
    proto = _pbd2.DescriptorProto()
    descr.CopyToProto(proto)
    proto.name = descr.full_name
    return proto
