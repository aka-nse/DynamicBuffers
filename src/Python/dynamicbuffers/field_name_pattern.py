#!/usr/bin/env python3
"""
Field name pattern
"""
from enum import Enum


class FieldNamePattern(Enum):
    """
    Specifies field name matching logic.
    """

    PythonName = 0  # noqa: E221
    """ Checks only Python naming style. """

    JsonName   = 1  # noqa: E221
    """ Checks only JSON naming style. """

    ProtoName  = 2  # noqa: E221
    """ Checks only .proto default name. """
