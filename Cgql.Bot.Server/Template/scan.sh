#!/bin/bash

# Must ensure the following environment variables are set. 
#

# Set project path
CGQL_HOME="$1"
PROJ_PATH="$2"     # Input project path.
RESULT_PATH="$3"   # Output result path.

BUILDER_PATH="${CGQL_HOME}/Python2Graph"
ENGINE_PATH="${CGQL_HOME}/Engine"

# Build graph
OLD=$PWD
cd "$BUILDER_PATH"
bash "${BUILDER_PATH}/build.sh" default "${PROJ_PATH}" "${BUILDER_PATH}/config.yaml" > /dev/null 2> ../Logs/Build.log
cd "$OLD"

if [ $? -ne 0 ]; then
    echo "Failed to build graph."
    exit 1
fi

# Run query
java -jar "${ENGINE_PATH}/CodeGraphQLEngine.jar" --language=python --style=json-compact --config="${ENGINE_PATH}/config.json" > "${RESULT_PATH}" 2>../Logs/Scan.log
if [ $? -ne 0 ]; then
    echo "Failed to run query."
    exit 2
fi
