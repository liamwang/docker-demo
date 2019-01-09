## Essential Commands

```bash
# remove all images and containers
docker rmi -f $(docker images -q)
docker rm -f $(docker ps -aq)

# pull postgres image
docker pull postgres

# create postgres volume
docker volume create --name postgresdata
docker volume ls

# get volume path
docker inspect postgres

# create and run postgres container
docker run -d --name postgres -v postgresdata:/var/lib/postgresql/data -e POSTGRES_PASSWORD=postgres.123 -e bind-address=0.0.0.0 postgres

# get postgres ip
docker network inspect bridge

# publish app
rm bin/dist
dotnet publish -c Release -o bin/dist

# build app image
docker build . -t liamwang/dockerdemo -f Dockerfile

# create and run app container
docker run -d --name dockerdemo -p 3000:80 -e DBHOST=172.17.0.2 liamwang/dockerdemo

# view app logs
docker logs -f dockerdemo

# publish image
docker tag liamwang/dockerdemo liamwang/dockerdemo:1.0
docker login -u <username> -p <password optional> <registry.cn-hangzhou.aliyuncs.com optional>
docker push liamwang/dockerdemo:1.0

```

## Creating Custom Networks

```bash
# create two new SDNs called frontend and backend
docker network create frontend
docker network create backend
docker network ls

# creating postgres container connected to a network
docker run -d --name postgres -v postgresdata:/var/lib/postgresql/data --network=backend -e POSTGRES_PASSWORD=postgres.123 -e bind-address=0.0.0.0 postgres

# Testing the Docker DNS Feature(在同一个 network 内才能互通)
docker run -it --rm --network backend alpine ping -c 3 postgres

# Creating the App Containers
docker create --name dockerdemo1 -e DBHOST=postgres -e MESSAGE="1st Server" --network backend liamwang/dockerdemo
docker create --name dockerdemo2 -e DBHOST=postgres -e MESSAGE="2nd Server" --network backend liamwang/dockerdemo

# Connecting the App Containers to Another Network
docker network connect frontend dockerdemo1
docker network connect frontend dockerdemo2

# Starting the App Containers
docker start dockerdemo1 dockerdemo2

# Creating and Starting the Load Balancer Container
# (Should add haproxy.cfg file with ASCII encoding and Unix(LF) line endings first)
docker run -d --name haproxy --network frontend -v "$(pwd)/haproxy.cfg:/usr/local/etc/haproxy/haproxy.cfg" -p 3000:80 haproxy

```

## Docker Compose

```bash
# Removing the Containers, Networks, and Volumes
docker rm -f $(docker ps -aq)
docker network rm $(docker network ls -q)
docker volume rm $(docker volume ls -q)

# Installing Docker Compose for Linux
curl -L https://github.com/docker/compose/releases/download/<version>/docker-compose-`uname -s`-`uname -m` > /usr/local/bin/docker-compose
chmod +x /usr/local/bin/docker-compose
docker-compose --version

# Building the Application
docker-compose -f docker-compose.yml build
# Or omit the name of the file
docker-compose build

# Running the Composed Application
docker-compose up
docker-compose up dbinit
docker-compose up -d mvc haproxy

# Scaling Up or Down the MVC Application Containers
docker-compose scale mvc=4
docker-compose scale mvc=1
docker-compose ps

# Stopping and Starting the Application Containers
docker-compose stop
docker-compose start

# Removing the Networks and Containers(but not the volume)
docker-compose down
# Removing the Networks, Containers and Volumes
docker-compose down -v


```

