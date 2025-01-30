# micro-parrot

Llama LLM integration demo in micro-services architecture

## Overview

This project demonstrates a micro-services architecture with Llama LLM integration. It includes three main services:

1. **Client App** - A simple spa using react

2. **API Gateway** - Manages API requests and routes them to the appropriate services

3. **NGINX** - Reverse proxy for the API Gateway

4. **AI Service** - Processes questions using LlamaSharp and the Llama model

5. **RabbitMQ** - Message queue for asynchronous processing

6. **Docker** - Containerization for easy deployment

7. **Kubernetes k3s** - Orchestration for easy deployment

## Client App (React)

The Client App is a simple spa using react. It is used to test the API Gateway and the AI Service.

## API Gateway (nginx)

The API Gateway service manages API requests and routes them to the appropriate services. It provides a RESTful API interface for the client.

## NGINX (nginx)

The NGINX service is a reverse proxy for the API Gateway. It is used to route requests to the appropriate services.

## AI Service (.NET 8)

The AI service is responsible for processing questions using LlamaSharp and the Llama model. It communicates with other services through RabbitMQ for asynchronous processing.

## RabbitMQ (rabbitmq)

The RabbitMQ service is a message queue for asynchronous processing. It is used to communicate between the API Gateway and the AI Service.

## Docker (docker)

The Docker service is used to containerize the services.

## Kubernetes k3s (k3s)

The Kubernetes k3s service is used to orchestrate the services.

## Features

- LlamaSharp integration for AI inference
- RabbitMQ message handling
- RESTful API endpoints
- Async processing for long-running inference tasks
- Docker containerization

### Tech Stack

- React
- ASP.NET Core 8
- LlamaSharp
- Docker
- NGINX
- RabbitMQ
- Kubernetes k3s

### Project Structure

.
├── Client App
│ ├── Dockerfile
│ └── src/
├── API Gateway
│ ├── Dockerfile
│ └── nginx.conf
├── NGINX
│ ├── Dockerfile
│ └── nginx.conf
├── AI Service
│ ├── Dockerfile
│ └── src/
├── RabbitMQ
│ └── Dockerfile
└── Kubernetes k3s
├── deployments/
├── services/
├── configmaps/
└── secrets/

This sets up the basic AI service with LlamaSharp integration. The service can now:

1. Load and initialize the Llama model
2. Process chat requests through a REST API
3. Generate responses using the model

Next steps would be to:

1. Add the RabbitMQ integration for async processing
2. Create a Dockerfile for containerization
3. Add health checks and monitoring
4. Implement proper error handling and retry policies

Would you like me to help with any of these next steps?
