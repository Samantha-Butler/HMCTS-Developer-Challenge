# HMCTS Coding Challenge – Task Management App

This project is a full-stack task management system built as part of a technical coding challenge for HMCTS. 
https://github.com/hmcts/dts-developer-challenge

It includes:

- ✅ A .NET 9 Web API for managing tasks
- ✅ An Angular 17 frontend using Angular Material
- ✅ MSTest unit tests for API logic

---

## 🔧 Tech Stack

### Frontend (Angular)
- Angular 17
- Angular Material
- TypeScript
- Reactive Forms + HttpClient

### Backend (API)
- ASP.NET Core 9.0
- Entity Framework Core (In-Memory for tests, SQLite for runtime)
- RESTful endpoints

### Testing
- MSTest
- EF Core InMemory provider
- Parallelized test execution

---

## 📁 Project Structure

HMCTSCodingChallenge/
├── HMCTSCodingChallenge.API/ # ASP.NET Core Web API
├── HMCTSCodingChallenge.UI/ # Angular UI frontend
├── HMCTSCodingChallenge.Tests/ # MSTest-based unit tests


---

## 🚀 Getting Started

### ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Node.js](https://nodejs.org/) & Angular CLI (`npm install -g @angular/cli`)
- IDE (e.g., VS Code)

---

### 🔹 1. Clone the Repository

```bash
git clone https://github.com/your-username/HMCTSCodingChallenge.git
cd HMCTSCodingChallenge
```
### 🔹 2. Run the API

```bash
cd HMCTSCodingChallenge.API
dotnet run
```

### 🔹 3. Run the Angular Frontend

```bash
cd HMCTSCodingChallenge.UI
npm install
ng serve
```

### 🔹 4. Run Unit Tests

```bash
cd HMCTSCodingChallenge.Tests
dotnet test
```

### 🧪 Features
✔️ Create, read, update, and delete tasks

✔️ Track title, description, status, and due date

✔️ Clean Material UI layout (inline form + task list)

✔️ CORS config to connect Angular & API locally

✔️ Date formatted as DD/MM/YY

✔️ Fully tested with MSTest and EF Core InMemory

### 📌 API Documentation

📄 Task Model
```bash
{
  "id": 1,
  "title": "Example Task",
  "description": "Optional text",
  "status": "Pending",
  "dueDate": "2025-07-25T23:00:00Z"
}
```
🔹 GET /api/tasks
```bash
[
  {
    "id": 1,
    "title": "Example",
    "status": "Pending",
    "description": "Optional",
    "dueDate": "2025-07-25T23:00:00Z"
  }
]
```
🔹 GET /api/tasks/{id}
```bash
{
  "id": 1,
  "title": "Example",
  "status": "Completed",
  "description": "Optional",
  "dueDate": "2025-07-25T23:00:00Z"
}

```
🔹 POST /api/tasks
```bash
{
  "title": "New Task",
  "description": "Write README file",
  "status": "In Progress",
  "dueDate": "2025-07-25T23:00:00Z"
}

```

🔹 PUT /api/tasks/{id}
```bash
{
  "id": 1,
  "title": "Updated Task",
  "description": "Updated info",
  "status": "Completed",
  "dueDate": "2025-07-30T10:00:00Z"
}


```
