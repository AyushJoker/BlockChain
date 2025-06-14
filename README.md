# 🧱 DotNet Blockchain

A simplified blockchain system built using **.NET 9**, simulating real-world concepts such as:

- 🔐 **Digital Signatures** with Public/Private key cryptography
- 💰 **UTXO (Unspent Transaction Output)** model to track user balances
- ⛏️ **Proof-of-Work Mining** with block rewards
- 📦 Transaction validation, block creation, and chain integrity
- 🌐 ASP.NET Core Web API with Swagger UI
- 🗃️ SQL Server via Entity Framework Core

---

## 📌 Features

- ✅ Add user (automatically generates key pair)
- ✅ Submit signed transactions (sender → receiver)
- ✅ UTXO-based balance tracking (like Bitcoin)
- ✅ Mine pending transactions with a reward mechanism
- ✅ Verify blockchain integrity
- ✅ API-first design with Swagger documentation

---

## ⚙️ Tech Stack

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/)
- Entity Framework Core
- SQL Server
- Swagger (OpenAPI)
- RESTful API Design

---

## 🧪 API Demo

Launch the project and go to `https://localhost:{port}/swagger` to test the following endpoints:

- `POST /api/user/add`
- `GET /api/user/balance/{publicKey}`
- `POST /api/transaction/create`
- `POST /api/block/mine`
- `GET /api/block/isChainValid`

---

## 🧠 What I Learned

- Real-world blockchain logic using UTXO
- Cryptography & Digital Signature validation
- How blockchain rewards are issued via coinbase (system) transactions
- Data integrity checks through block hashing
- Software architecture using services and clean separation of layers

---

## 📦 Future Enhancements

- 🤝 Peer-to-peer node broadcasting
- 🔁 Automatic block syncing
- 🔐 Encrypting private keys
- 🧮 Wallet front-end

---

## 🚀 Getting Started

1. Clone the repo
   ```bash
   git clone https://github.com/your-username/DotNetBlockchain.git
