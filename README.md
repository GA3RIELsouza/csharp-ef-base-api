# 📦 About This Project

This is a simple **.NET 8.0 C# ASP.NET API** using **Entity Framework Core** and **MySQL**, featuring:

- ✅ User registration
- 🔐 Login with JWT authentication
- 🧑 User CRUD operations

---

# 🐬 MySQL Migration Guide

## 🔧 Generate a New Migration

Run the following command in the terminal:

```bash
dotnet ef migrations add <desired_name>
```

Replace <desired_name> with a descriptive name for the migration, e.g., InitialCreate.

# 🚀 Update the Database

Apply the latest migration to the database by running:

```bash
dotnet ef database update
```

---

# 📌 Notes

Make sure your DbContext is correctly configured and the dotnet-ef CLI tool is installed.
If your DbContext is in a different project, use the --project and --startup-project flags.