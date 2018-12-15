#!/bin/bash

# This script should be executed within service directory with dockerfile

# $1 - docker image tag

if [ "$#" -ne 1 ]; then
    echo "Incorrect number of parameters passed in."
    echo "Usage: $0 docker_image_tag"
    exit 1
fi

# clean-up docker images
docker images $1 | awk '{print $3}' | xargs docker rmi -f

exit 0