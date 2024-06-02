#!/bin/bash

RELEASE=TRUE

# Build program

function BUILD_FOR_RELEASE ()
{
	echo "Building for RELEASE"

	dotnet build --configuration Release

	cp Assets -R ./bin/Release/net8.0/
	cp *.xml -R ./bin/Release/net8.0/

	dotnet ./bin/Release/net8.0/FileMerger.dll
}

function BUILD_FOR_DEBUG ()
{
	echo "Building for DEBUG"

	dotnet build

	cp Assets -R ./bin/Debug/net8.0/
	cp *.xml -R ./bin/Debug/net8.0/

	dotnet ./bin/Debug/net8.0/FileMerger.dll
}

if [[ $RELEASE = TRUE ]]; then
    BUILD_FOR_RELEASE
else
    BUILD_FOR_DEBUG
fi

