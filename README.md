# 🃏 PokerHands

**Submission:** Game Server Engineer Coding Challenge  
**Author:** Regina da Silva Lafont  
**Date:** November 2025  

![.NET Build & Test](https://github.com/reginasilva/pokerHands/actions/workflows/dotnet.yml/badge.svg)
![Coverage](https://img.shields.io/badge/Coverage-96%25-brightgreen)

---

## 📘 Overview

**PokerHands** is a backend application written in **C# (.NET 8)** that generates, evaluates, and compares five-card poker hands from a standard 52-card deck.

The application exposes a REST API with endpoints for generating random hands, evaluating hand ranks, and comparing two hands to determine the winner.

---

## 🎯 Features

- Generates **five-card poker hands** from a full deck (no jokers)  
- Evaluates hands according to **official poker rules**  
- Compares two hands and determines the higher-ranked one  
- Supports all **10 poker hand ranks** (from *Royal Flush* to *High Card*)  
- Fully tested — 95%+ coverage with unit and integration tests  
- CI/CD workflow enforcing coverage thresholds via **GitHub Actions**

---

## 🧩 Architecture

| Layer | Description |
|--------|-------------|
| **Core** | Domain layer containing business logic, models, enums, rules, and services. |
| **API** | Web layer exposing endpoints for generating, evaluating, and comparing hands. |
| **Tests** | Isolated test projects for Core and API, ensuring full coverage and verifiable quality. |

The system applies the **Open/Closed Principle**, keeping rules isolated via `IHandRule`.  
New games or evaluation rules can be added without touching existing logic.

---

## 🧠 Technical Decisions

| Area | Decision | Rationale |
|-------|-----------|------------|
| **Language / Framework** | C# + .NET 8 | Modern, stable, high-performance backend stack. |
| **Business Logic** | 10 distinct rule classes implementing `IHandRule` | Promotes modularity and testability. |
| **Input Validation** | `Hand` and `Card` constructors enforce validity | Guarantees domain integrity. |
| **Enums** | `Rank` and `Suit` with `Undefined` fallback | Simplifies parsing and error handling. |
| **Parsing** | `CardParser` / `HandParser` utilities | Converts text input (`"AS"`) into typed cards. |
| **Service Layer** | `EvaluatorService` | Orchestrates rules and comparison logic. |
| **Logging** | `ILogger` interface | Enables observability without tight coupling. |
| **Testing** | xUnit + FluentAssertions | Readable, maintainable tests. |
| **CI/CD** | GitHub Actions + XPlat Coverage | Automated quality enforcement. |

---

## ⚙️ Setup & Usage

### 🧱 Requirements
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio / VS Code / Rider (optional)

### 🚀 Run the API
```bash
dotnet build
dotnet run --project PokerHands.Api
```

###  Open Swagger
```bash
https://localhost:5001/swagger
```

---

### 🔗 Available endpoints

| Method | Route | Description |
|--------|--------|-------------|
| `GET` | `/generate` | Returns a random five-card hand |
| `POST` | `/evaluate` | Evaluates and ranks a poker hand |
| `POST` | `/compare` | Compares two hands and determines the winner |

---

## 🧪 Run Tests & Coverage

### **Run all tests**
```bash
dotnet test --settings coverage.runsettings --collect:"XPlat Code Coverage"
```

--- 

## 🔄 Continuous Integration

The CI pipeline runs automatically on every push and pull request:

- Builds the solution  
- Runs all tests  
- Validates coverage (≥95%)  
- Uploads `.trx` and `coverage.cobertura.xml` artifacts  

Workflow file: `.github/workflows/dotnet.yml`

---

## 🧱 Example API Calls (`PokerHands.http`)

### **Evaluate a Royal Flush**
```http
POST https://localhost:5001/evaluate
Content-Type: application/json

["AS", "KS", "QS", "JS", "10S"]
```

### **Compare Two Hands**
```http
POST https://localhost:5001/compare
Content-Type: application/json

{
  "handA": ["AS", "KS", "QS", "JS", "10S"],
  "handB": ["9S", "9D", "9H", "8S", "2C"]
}
```

## 📊 Quality Metrics

| Metric | Result |
|---------|--------|
| **Unit Test Coverage** | 95%+ |
| **Build Success Rate** | 100% |
| **API Integration Tests** | ✅ All endpoints verified |
| **Rules Coverage** | ✅ 10/10 poker hand ranks tested |
| **Pipeline Enforcement** | ✅ Coverage threshold integrated |

---

## 💡 Values Alignment

| Value | Reflected In |
|----------------|--------------|
| **We boldly go** | Clear, independent technical decisions and willingness to confront ambiguity when defining architecture and trade-offs. |
| **We stay curious** | Exploration of better design patterns, refactoring of parsers, and constant pursuit of cleaner abstractions. |
| **We pursue excellence with candor** | Continuous improvement, strict testing discipline, and refusal to accept “good enough” code. |
| **We make each other better** | Readable, reusable, and teachable codebase designed to help teammates succeed and scale. |


---

## 👤 Author

**Regina da Silva Lafont**  
Staff Software Engineer 

[LinkedIn](https://www.linkedin.com/in/reginalafont)

---

## 🏁 Submission

**Repository Tag:** `v1.0-final`  
**Coverage Threshold:** 95% enforced via CI  
**Deliverable:** GitHub repository or ZIP file of the full solution