#!/bin/bash

# Changes the container image name and version in ./k8s/deployment.yaml
#
# This script should be executed within service directory with dockerfile

# $1 - versioned image tag
# $2 - version


if [ "$#" -ne 2 ]; then
    echo "Incorrect number of parameters passed in."
    echo "Usage: $0 tag_versioned version"
    exit 1
fi

# update the deployment.yaml to match this image
sed -i 's|${containerImage}|'$1'|' ./k8s/deployment.yaml
sed -i 's|${version}|'$2'|' ./k8s/deployment.yaml