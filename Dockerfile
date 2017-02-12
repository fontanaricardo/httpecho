FROM debian:8

COPY ./bin/Release/netstandard1.6/debian.8-x64/publish .

ENTRYPOINT ["./httpecho"]
