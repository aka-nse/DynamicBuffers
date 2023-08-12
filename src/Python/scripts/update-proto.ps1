#!/usr/bin/env pwsh
$RootDir = [System.IO.Path]::GetDirectoryName($PSScriptRoot)
protoc --proto_path=$RootDir/../proto/ `
       --python_out=$RootDir/dynamicbuffers/proto/ `
       $RootDir/../proto/*.proto
