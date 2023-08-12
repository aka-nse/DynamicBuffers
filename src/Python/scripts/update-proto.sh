#!/usr/bin/env bash
ROOT_DIR=$(cd $(dirname $0); pwd)/..
protoc --proto_path=$ROOT_DIR/../proto/ \
       --python_out=$ROOT_DIR/dynamicbuffers/proto/ \
       $ROOT_DIR/../proto/*.proto