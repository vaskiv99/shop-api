#!/bin/sh

"$DOCKER_PASS" | docker login e="$DOCKER_EMAIL" -u="$DOCKER_USER" --password-stdin
if [ "$TRAVIS_BRANCH" = "master" ]; then
    TAG="latest"
else
    TAG="$TRAVIS_BRANCH"

fi
REPONAME="vaskiv99/shop_api"
docker build -t $REPONAME:$TAG .
docker push $REPONAME:$TAG