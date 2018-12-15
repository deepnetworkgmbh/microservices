#!/bin/bash

# This script should be executed within service directory with dockerfile

# $1 - service name
# $2 - path to store tests results

if [ "$#" -ne 2 ]; then
    echo "Incorrect number of parameters passed in."
    echo "Usage: $0 service_name test_results_path"
    exit 1
fi

# figure out the tag
TAG=$1:tests

docker build --no-cache --force-rm --target testrunner -t $TAG .

mkdir -p $2/testresults
docker run --rm --mount type=bind,source=$2/testresults,target=/src/UnitTests/TestResults/ $TAG