#!/bin/bash

# Creates a docker image for a given service, tags it with the given version, and pushes it to given registry
#
# This script should be executed within service directory with dockerfile

# $1 - docker registry
# $2 - TAG LATEST
# $3 - TAG VERSIONED

if [ "$#" -ne 3 ]; then
    echo "Incorrect number of parameters passed in."
    echo "Usage: $0 container_registery service_name build_version"
    exit 1
fi

# login to acr
# az acr login --name $1

# build the image
docker build --no-cache --force-rm --target final -t $3 .
docker tag $3 $2

# push it to acr
docker push $2
docker push $3