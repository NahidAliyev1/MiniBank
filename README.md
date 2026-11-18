# 🏦 MiniBank API

N-Tier (Multi-Layered) arxitektura ilə hazırlanmış sadə bankçılıq və kart əməliyyatları RESTful API layihəsi.

## 🚀 İstifadə olunan texnologiyalar və arxitektura
* **.NET / ASP.NET Core Web API**
* **Entity Framework Core** (Code-First yanaşması)
* **N-Tier Architecture:** `MiniBank.DAL` (Data Access Layer), `MiniBank.BL` (Business Logic Layer), `MiniBank.API` (Presentation Layer)
* **Generic Repository Pattern** (`IRepository<T>` və `IQueryable` əsaslı sorğular)
* **Dependency Injection** & **DTO Pattern**
* **Swagger / OpenAPI**

## 📌 Əsas Funksionallıqlar
* **İstifadəçi İdarəetməsi (`UsersController`):** Müştəri qeydiyyatı, məlumatların yenilənməsi, silinməsi və müştərinin bütün kartları ilə birlikdə gətirilməsi.
* **Kart Əməliyyatları (`CardsController`):** Müştəri adına 16 rəqəmli unikal kart və CVV generasiyası, balansa mədaxil (`Deposit`), kartın bloklanması/blokdan çıxarılması (`Toggle Block`).
* **Pul Köçürmələri (`TransactionsController`):** Kartdan karta pul köçürmə (`TransferMoneyAsync`), balans və blok yoxlamaları, uğurlu və uğursuz əməliyyatların tarixçəyə yazılması.