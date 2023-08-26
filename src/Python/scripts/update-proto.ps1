#!/usr/bin/env pwsh
$RootDir = [System.IO.Path]::GetDirectoryName($PSScriptRoot)
protoc --proto_path=$RootDir/../proto/ `
       --python_out=$RootDir/dynamicbuffers/proto/ `
       $RootDir/../proto/*.proto
protoc --proto_path=$RootDir/../proto.test/ `
       --python_out=$RootDir/tests/proto/ `
       $RootDir/../proto.test/*.proto