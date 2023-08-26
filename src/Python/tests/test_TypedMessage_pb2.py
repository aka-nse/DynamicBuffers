#!/usr/bin/env python
# import sys
import setenv  # noqa: F401
import unittest
from google.protobuf.any_pb2 import Any
from dynamicbuffers.proto import TypedMessage_pb2 as pb
from proto import TestCases_pb2 as pbt


class TestTypedMessage(unittest.TestCase):

    s_metadatas = [
        'DynamicBuffers.Test.TestCases.TestCaseMessage',
        'DynamicBuffers.Test.TestCases.InnerMessage3',
        'DynamicBuffers.Test.TestCases.InnerMessage3.InnerMessage2',
        'DynamicBuffers.Test.TestCases.InnerMessage1',
    ]

    def test_create(self):
        content = pbt.TestCaseMessage()

        msg = pb.TypedMessage.create(content)
        metadatanames = list(map(lambda m: m.name, msg.metadata))
        for type_name in TestTypedMessage.s_metadatas:
            self.assertTrue(type_name in metadatanames)

        msg_content: Any = msg.content  # type: ignore
        actual = pbt.TestCaseMessage()
        msg_content.Unpack(actual)
        self.assertEqual(content, actual)


if __name__ == '__main__':
    unittest.main()
