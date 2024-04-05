#!/bin/bash

# Must ensure the following environment variables are set. 
#
# CGQL_BUILDER_HOME=/path/to/graph/builder
# CGQL_SCANNER_HOME=/path/to/query/engine

# Set project path
PROJ_PATH="{0}"     # Input project path.
RESULT_PATH="{1}"   # Output result path.

# Build graph
CGQL_BUILDER_HOME/Py2Graph/build.sh default "${PROJECT_PATH}" > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo "Failed to build graph."
    exit 1
fi

# Run query
java CGQL_SCANNER_HOME/run.jar "${CGQL_SCANNER_HOME}/config.json" "${RESULT_PATH}" > /dev/null 2>&1
if [ $? -ne 0 ]; then
    echo "Failed to run query."
    exit 2
fi
