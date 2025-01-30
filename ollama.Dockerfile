FROM ollama/ollama:latest

USER root
RUN apt-get update && \
    apt-get install -y curl && \
    rm -rf /var/lib/apt/lists/* && \
    mkdir -p /root/.ollama && \
    chmod 777 /root/.ollama

USER 1000

EXPOSE 11434
ENTRYPOINT ["/bin/ollama"]
CMD ["serve"] 