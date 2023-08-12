#!/usr/bin/env python
from setuptools import setup, find_packages

setup(
    name="dynamicbuffers",
    version="0.1.0.0",
    license='Apache v2',
    author="aka-nse",
    url="https://github.com/aka-nse/DynamicBuffers",
    description="introduce dynamic programming into Protocol Buffers",
    packages=find_packages(),
    include_package_data=True,
    install_requires=[
        "protobuf>=4.0.0",
    ],
)
