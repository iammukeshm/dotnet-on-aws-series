#!/bin/bash
set -e # Exit immediately if a command exits with a non-zero status

# Update the package index

sudo yum update -y

# Install Git

sudo yum install -y git

# Install .NET SDK 8.0

sudo yum install -y dotnet-sdk-8.0

# Install Docker

sudo yum install -y docker

# Start and enable Docker service

sudo systemctl start docker
sudo systemctl enable docker

# Install Docker Compose

sudo curl -L https://github.com/docker/compose/releases/latest/download/docker-compose-$(uname -s)-$(uname -m) -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose

# Optional: Verify installations

echo "Verifying installations..."
dotnet --version
docker --version
docker-compose --version
