FROM microsoft/aspnetcore:2.0

COPY bin/dist /app

COPY wait-for-it.sh /app/wait-for-it.sh

RUN chmod +x /app/wait-for-it.sh

WORKDIR /app

EXPOSE 80

ENV WAITHOST=postgres WAITPORT=5432

ENTRYPOINT ./wait-for-it.sh $WAITHOST:$WAITPORT --timeout=0 \
	&& exec dotnet DockerDemo.dll
