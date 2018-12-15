#!/bin/bash

# Applies kubernetes yaml files.
# The script should be executed within service directory

# $1 - service name

# create config map from env file, please note that k8s supports other formats as well (see --from-file)
kubectl create configmap $1-config --from-env-file ./env.list --dry-run -o yaml | kubectl apply -f -

kubectl apply -f ./k8s/service.yaml

# this should exist for all the services
kubectl apply -f ./k8s/deployment.yaml
