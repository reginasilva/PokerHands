# PokerHands

### Generates, classifies, and compares five-card poker hands. - .NET 8.0 Web API

---

## Overview

The application simulates a standard 52-card deck and supports three main operations:
- Generating random poker hands.
- Evaluating the strength of a given hand.
- Comparing two hands and identifying the winner.

It supports all standard poker hand categories, from *High Card* to *Royal Flush*, including proper tiebreaker evaluation.  
No wild cards or jokers are used.

---

## Run locally
### Prerequisites
- Requires .NET 8 SDK.

### Steps
- Build:
  - `dotnet build`
- Run API:
  - `dotnet run --project src/AuthorizationGateway.Api/`

## Run in containers
### Prerequisites
- Requires Docker.

### Steps
- Build:
  - `docker build -t emv-gateway-api .`
- Run API:
  - `docker run -d -p 8080:8080 --name emv-gateway emv-gateway-api`

## Tests
- Run all tests:
  - `dotnet test`


## Architecture

The solution is organized into two projects:

PokerHands/
├── PokerHands.Core/
│ ├── Enums/
│ ├── Interfaces/
│ ├── Models/
│ ├── Parsers/
│ ├── Rules/
│ └── Services/
└── PokerHands.Api/
├── Controllers/
├── Models/
└── Program.cs

python
Copy code

The `Core` project contains all business logic related to card creation, ranking, and evaluation.  
The `Api` project is responsible for exposing HTTP endpoints, validating input, and returning results in JSON format.

No external dependencies or infrastructure layers were required, as the project is stateless and self-contained.

---

## Core Components

### DeckService
Creates a standard 52-card deck, shuffles it, and deals five cards.

### PokerEvaluatorService
Determines the rank of a hand (e.g., *Flush*, *FullHouse*, *RoyalFlush*).  
Implements a comparison method that returns:
- `1` → first hand wins  
- `0` → tie  
- `-1` → second hand wins

### CardParser
Converts string inputs such as `"AS"`, `"10H"`, or `"3D"` into `Card` objects.  
This ensures that all parsing logic is isolated from domain behavior.

### HandEvaluationResult
Represents the evaluation result of a hand, including its `HandRank` and numerical tiebreakers.

---

## API Endpoints

### `GET /api/poker/generate`
Generates a random poker hand.

**Response**
```json
{
  "cards": [
    { "rank": "Ten", "suit": "Hearts" },
    { "rank": "Queen", "suit": "Spades" },
    { "rank": "Ace", "suit": "Clubs" },
    { "rank": "Three", "suit": "Diamonds" },
    { "rank": "Seven", "suit": "Hearts" }
  ]
}
POST /api/poker/evaluate
Evaluates a single hand and returns its classification.

Request

json
Copy code
["AS", "KS", "QS", "JS", "10S"]
Response

json
Copy code
{
  "rank": "RoyalFlush",
  "tiebreakers": ["14"]
}
POST /api/poker/compare
Compares two poker hands and returns the result.

Request

json
Copy code
{
  "hand1": ["AS", "KS", "QS", "JS", "10S"],
  "hand2": ["9C", "9D", "9H", "9S", "2C"]
}
Response

json
Copy code
{
  "winner": "Hand A",
  "reason": "Hand A wins: RoyalFlush beats FourOfKind",
  "handARank": "RoyalFlush",
  "handBRank": "FourOfKind",
  "comparison": 1
}
Design Decisions
Separation of responsibilities
Domain logic remains isolated from API handling. Each component has a single, well-defined responsibility.

Use of standard poker terminology
The term HandRank was preserved for clarity and consistency with poker theory.

Stateless and deterministic behavior
No persistence or random state is stored. Every request is independent and reproducible.

No external dependencies
Logging relies on ILogger<T> provided by the .NET runtime. No third-party packages were used.

Comparison contract
The evaluator returns an integer for simplicity and alignment with .NET comparison semantics.

Trade-offs and Future Improvements
No persistence layer
The system doesn’t store generated hands or past comparisons.
Persistence could be added through a minimal infrastructure layer if state tracking were required.

No caching
Hands are evaluated on demand. Caching could improve performance in a real-time or high-frequency scenario.

Testing scope
The Core layer is testable, but test projects were omitted for brevity.
Future iterations could include unit and integration tests covering evaluation accuracy.



## Documentation
Full documentation is available in the **[Project Wiki](../../wiki)**.

The wiki includes:
- **EMV & TLV Explanation**  
- **Architecture Overview**  
- **Security Model**  
- **API Reference & Examples**  
- **Testing Strategy**  
- **Developer Setup (Containers & Local Run)**  

> Visit the [Wiki Home](../../wiki) to explore all sections.