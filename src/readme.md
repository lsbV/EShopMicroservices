# EShop Microservices

This project is a microservices-based e-commerce application using ASP.NET Core, Docker, and YARP (Yet Another Reverse Proxy). The application includes several services such as catalog, basket, ordering, and a YARP API Gateway.

## Services

- **Catalog API**: Manages product catalog.
- **Basket API**: Manages user shopping baskets.
- **Ordering API**: Manages orders.
- **Discount gRPC**: Provides discount services.
- **YARP API Gateway**: Acts as a reverse proxy to route requests to the appropriate services.

## Architecture

The application is built using the microservices architecture pattern. Each service is a separate project that can be developed, tested, and deployed independently. The services communicate with each other using HTTP or gRPC.

# Technologies Used

1. **ASP.NET Core**
   - Catalog API
   - Basket API
   - Ordering API
   - Discount gRPC

2. **YARP (Yet Another Reverse Proxy)**
   - API Gateway

3. **Docker**
   - Containerization

4. **Docker Compose**
   - Multi-container orchestration

5. **PostgreSQL**
   - Database for Catalog and Basket services

6. **SQL Server**
   - Database for Ordering service

7. **RabbitMQ**
   - Message broker

8. **Redis**
   - Distributed cache for Basket service

9. **gRPC**
   - Communication protocol for Discount service

10. **Rate Limiting**
    - ASP.NET Core Rate Limiting middleware

11. **Entity Framework Core**
    - ORM for database interactions

12. **.NET 9 SDK**
    - Development framework