# Running the Code Example in Docker Compose

This repository contains a sample code example that can be run in Docker Compose.

## Prerequisites

- Docker and Docker Compose installed on your machine

## Usage

1. Clone this repository to your local machine:

   ```bash
   git clone https://github.com/your-username/your-repo.git  
   ```

2. Navigate to the cloned repository:

      ```bash
      cd your-repo 
      ```

3. Start the Docker containers using docker-compose:

   ```bash
   docker-compose up 
   ```

   This command will start the code example, OpenTelemetry Collector, Jaeger, and Prometheus in Docker containers.

4. Verify that the code example is running by visiting [http://localhost:8080](http://localhost:8080/weatherforecast) in your web browser.

5. Verify that Jaeger is running by visiting <http://localhost:16686> in your web browser.

6. Verify that Prometheus is running by visiting <http://localhost:9090> in your web browser.
