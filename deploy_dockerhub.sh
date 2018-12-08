#!/bin/sh

docker login e="$DOCKER_EMAIL" -u="$DOCKER_USER" -p="$DOCKER_PASS"
if [ "$TRAVIS_BRANCH" = "master" ]; then
    TAG="latest"
else
    TAG="$TRAVIS_BRANCH"

fi
REPONAME="$TRAVIS_REPO_SLUG"
REPONAME="${REPONAME,,}"
docker build -t $REPONAME:$TAG .
docker push $REPONAME