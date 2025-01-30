#!/bin/bash

# Start Ollama in the background
ollama serve &
OLLAMA_PID=$!

# Wait for Ollama to start
echo "Waiting for Ollama to start..."
for i in {1..30}; do
    if curl -s http://127.0.0.1:11434/api/tags >/dev/null; then
        echo "Ollama is running"
        break
    fi
    if [ $i -eq 30 ]; then
        echo "Timeout waiting for Ollama"
        exit 1
    fi
    sleep 1
done

# Pull the model
echo "Pulling deepseek-r1 model..."
ollama pull deepseek-r1

# Check if model was pulled successfully
if ! ollama list | grep -q deepseek-r1; then
    echo "Failed to pull model"
    exit 1
fi
echo "Model pulled successfully"

# Start the .NET application
echo "Starting .NET application..."
exec dotnet MicroParrot.AI.Api.dll 