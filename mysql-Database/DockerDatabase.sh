#!/usr/bin/env bash

RED='\033[0;31m'
GREEN='\033[0;32m'
NC='\033[0m' # No Color
CONTAINER_NAME='myrmidon-sql'
IMAGE_NAME='myrmidon-sql'
DUMP_PATH="./myrmidon_init.sql"
if [ $# -gt 1 ]; then
    DUMP_PATH=$2
fi





if [ "$UID" -ne 0 ]; then
    echo -e "${RED} Please run this script as root or using sudo ${NC}"
    exit 1

elif [ "$1" == "-c" ]; then
    docker build -t ${IMAGE_NAME} . 1> /dev/null;
    docker run -p 3306:3306 --name ${CONTAINER_NAME} -d ${IMAGE_NAME} 1> /dev/null;
    docker start ${CONTAINER_NAME} 1> /dev/null;
    sleep 30;
    mysql -h localhost -P 3306 -u myrmidon_admin -pMyrmidonProject myrmidon < ${DUMP_PATH} 1> /dev/null;
    # mysql -h localhost -P 3306 -u myrmidon_admin -pMyrmidonProject myrmidon < myrmidon_init.sql
    echo -e "${GREEN}Docker container ${CONTAINER_NAME} started${NC}"
elif [ "$1" == "-r" ]; then
    echo "Running commands for -r argument"
    docker stop ${CONTAINER_NAME} 1> /dev/null;
    docker rm ${CONTAINER_NAME} 1> /dev/null;
    docker rmi ${IMAGE_NAME} 1> /dev/null;
    echo -e "${GREEN}Docker container ${CONTAINER_NAME} Deleted${NC}"
    
else
    # display error message for invalid argument
    echo "Invalid argument. Please use -c or -r."
fi