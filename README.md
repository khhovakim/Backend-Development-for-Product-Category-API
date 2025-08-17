# Product & Category API

A simple **ASP.NET Core Web API** project for managing **Products** and **Categories** with a many-to-many relationship.  
Each product must belong to **exactly 2 or 3 categories** (enforced with validation).

This project demonstrates:
- .NET 8 Web API
- Entity Framework Core (SQLite)
- Repository + Service pattern
- FluentValidation
- Swagger/OpenAPI
- Error handling & logging
- Pagination, filtering, sorting
- Data seeding for development

---

## 📦 Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- (Optional) SQLite viewer (e.g. [DB Browser for SQLite](https://sqlitebrowser.org/))

---

## 🚀 Getting Started

### 1. Clone & navigate
```bash
git clone https://github.com/yourusername/ProductCategoryApi.git
cd ProductCategoryApi/ProductCategoryApi
```

---

### 2. Install EF CLI (first time only)
```bash
dotnet tool install --global dotnet-ef
```

---

### 3. Apply migrations & update database
```bash
dotnet ef database update
```

---

### 4. Run the API
```bash
dotnet run
```
By default, the API will listen on:
HTTP: http://localhost:5229
HTTPS: https://localhost:7233

---

### 5. Open Swagger UI

When the app starts, your browser will open automatically.
Or manually open:
```bash
http://localhost:5229/swagger
```