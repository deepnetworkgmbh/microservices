#!/bin/bash

# $1 - service name
# $2 - version
# $3 - ACR name
# $4 - path to store tests results

if [ "$#" -ne 4 ]; then
    echo "Incorrect number of parameters passed in."
    echo "Usage: $0 service_name test_results_path version acr_name"
    exit 1
fi

TAG_LATEST="$3.azurecr.io/$1:latest"
TAG_VERSIONED="$3.azurecr.io/$1:$2"

./../cicd/runUnitTests.sh $1 $4
./../cicd/buildAndPushDockerImage.sh $3 $TAG_LATEST $TAG_VERSIONED
./../cicd/updatePlaceholdersInDeployment.sh $TAG_VERSIONED $2
./../cicd/deployToK8s.sh $1
./../cicd/cleanUp.sh $1