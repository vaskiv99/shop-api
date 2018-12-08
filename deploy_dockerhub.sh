#!/bin/sh

docker login e="$DOCKER_EMAIL" -u="$DOCKER_USER" -p="$DOCKER_PASS"
if [ "$TRAVIS_BRANCH" = "master" ]; then
    TAG="latest"
else
    TAG="$TRAVIS_BRANCH"
REPO="$TRAVIS_REPO_SLUG"
REPO="${REPO,,}"
echo REPO
fi
docker build -t $REPO:$TAG .
docker push $REPO