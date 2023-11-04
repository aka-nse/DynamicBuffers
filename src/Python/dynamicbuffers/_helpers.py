#!/usr/bin/env python3
from typing import Callable, TypeVar
from collections.abc import Iterable
import google.protobuf.descriptor as _pbd
import google.protobuf.descriptor_pb2 as _pbd2


T = TypeVar('T')


def find_single(
        source: Iterable[T],
        predicate: Callable[[T], bool]) -> T:
    found = False
    retval = None
    for item in source:
        if predicate(item):
            if found:
                raise ValueError()
            found = True
            retval = item
    if not found:
        raise ValueError()
    return retval


def last_index_of(x: list[T], search: T) -> int:
    try:
        return len(x) - 1 - x[::-1].index(search)
    except ValueError:
        return -1


def to_modified_proto(descr: _pbd.Descriptor):
    proto = _pbd2.DescriptorProto()
    descr.CopyToProto(proto)
    proto.name = descr.full_name
    return proto


def to_modified_type_name(name: str) -> str:
    last_slash = last_index_of(name, '/')
    if last_slash >= 0:
        return name[last_slash + 1:]
    if len(name) > 0 and name[0] == '.':
        return name[1:]
    return name


if __name__ == '__main__':
    print(to_modified_type_name('hogehoge/'))
    print(to_modified_type_name('hogehoge/fugafuga/'))
    print(to_modified_type_name('hogehoge/fugafuga'))
    print(to_modified_type_name('.hogehoge'))
