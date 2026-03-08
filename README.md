🛒 (E-Commerce Web API)

An enterprise-level E-Commerce RESTful API designed to deliver high performance, security, and scalability. Built using ASP.NET Core 8 and structured with Onion Architecture to ensure clean separation of concerns and long-term maintainability.

🚀 Key Features 🛍️ Product Management

Smart search by brand or type

Sorting (Price / Name)

Pagination & filtering support

🛒 Basket Management

High-speed cart storage using Redis

Add, update, remove, or clear items

Persistent basket per user

💳 Secure Checkout & Payments

Stripe integration for real-world payments

Automatic calculation of totals, taxes, and shipping

Order creation after successful payment

👤 User & Authentication

Secure registration & login

JWT-based authentication

Saved addresses for faster checkout

🏗️ Technical Architecture

The project follows the Onion Architecture pattern to maintain clean and scalable code structure:

🔹 Core Layer

Contains domain entities and business rules (Products, Orders, Delivery Methods, etc.).

🔹 Repository Layer

Handles database operations and data persistence using EF Core.

🔹 Shared Layer

Includes DTOs, Result Pattern implementation, and shared utilities.

🔹 API Layer

Exposes RESTful endpoints and manages authentication & authorization.

🛠️ Technologies Used

C# | ASP.NET Core 8

Entity Framework Core

SQL Server

Redis

JWT Authentication & Identity

Stripe API

Swagger UI

💎 Clean Code & Design Patterns

Generic Repository Pattern

Unit of Work Pattern

Result Pattern

AutoMapper

Separation of Concerns
