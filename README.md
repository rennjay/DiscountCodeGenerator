# 🎉 DiscountCodeGenerator 🚀

A modern, scalable, and testable microservice for generating and managing discount codes using **gRPC** and **Clean Architecture** principles! 🏗️🔑

---

## ✨ Features

- ⚡ **Generate Unique Discount Codes**: Create batches of unique, secure codes for your campaigns.
- 🔄 **Mark Codes as Used**: Track code usage and prevent re-use.
- 📦 **Pagination Support**: Efficiently fetch codes with pagination.
- 🗄️ **Persistence**: Uses Entity Framework Core with SQLite for reliable storage.
- 🧩 **Clean Architecture**: Separation of concerns across Domain, Application, Infrastructure, and gRPC layers.
- 🧪 **Unit Tested**: Core logic is covered by xUnit tests.
- 🏎️ **Fast & Lightweight**: Built on .NET 8 and gRPC for high performance.

---

## 🏛️ Solution Structure

```
DiscountCodeGeneratorService.Domain        # Domain models, interfaces, and business logic
DiscountCodeGeneratorService.Application   # Application layer (CQRS, MediatR, DTOs)
DiscountCodeGeneratorService.Infrastructure# EF Core, Repositories, DB Context, Migrations
DiscountCodeGeneratorService.Grpc          # gRPC endpoints, service implementation
Tests/DiscountCodeGeneratorService.Domain.Tests # Unit tests
```

---

## 🛠️ Tech Stack

- 🌐 **.NET 8**
- 🟦 **gRPC**
- 🗃️ **Entity Framework Core** (SQLite)
- 🧑‍💻 **MediatR** (CQRS)
- 🧪 **xUnit** (Testing)

---

## 🚦 How It Works

1. **Generate Codes**: Call the gRPC endpoint to generate a batch of codes (customizable length/count).
2. **Store Codes**: Codes are persisted in the database with usage info.
3. **Use Code**: Mark a code as used via the gRPC endpoint.
4. **Query Codes**: Fetch paginated lists of codes and their usage status.

---

## 📡 gRPC API Overview

- `GenerateDiscountCodes` ➡️ Generate new codes
- `GetDiscountCodes` ➡️ List codes (paginated)
- `UseDiscountCode` ➡️ Mark a code as used

See [`Protos/discountcode.proto`](./Services/DiscountCodeGeneratorService/DiscountCodeGeneratorService.Grpc/Protos/discountcode.proto) for full schema.

---

## 🚀 Getting Started

1. **Clone the repo**
2. `dotnet build` 🛠️
3. `dotnet run --project Services/DiscountCodeGeneratorService/DiscountCodeGeneratorService.Grpc` 🏃
4. Use a gRPC client (e.g., Postman, [BloomRPC](https://github.com/bloomrpc/bloomrpc), [grpcurl](https://github.com/fullstorydev/grpcurl)) to interact!

---

## 🧪 Running Tests

```bash
dotnet test Tests/DiscountCodeGeneratorService.Domain.Tests
```

---

## 🤝 Contributing

PRs and issues welcome! Star ⭐ the repo if you find it useful.

---


Made with ❤️, .NET, and tons of ☕️!
