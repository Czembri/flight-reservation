# Flight Reservation System

## 📌 Opis projektu
Ten projekt to pełnoprawny system zarządzania rezerwacjami lotniczymi, składający się z **backendu** w **ASP.NET Core 9 (Minimal API)** i **frontendu** w **Angular 18**.

---

## 📂 Struktura projektu
- `ReservationAPI/` → Backend w **.NET 8**
- `flight-reservation-frontend/` → Frontend w **Angular 19**

---

## 🛠 Wymagania
Aby uruchomić aplikację, potrzebujesz:
- **.NET 8 SDK** → https://dotnet.microsoft.com/en-us/download
- **Node.js (v18+)** → https://nodejs.org/
- **Angular CLI (v19)** → `npm install -g @angular/cli`
- **SQLite (opcjonalnie)** do przechowywania danych

---

## 🚀 Uruchomienie Backend (ASP.NET Core 8)
1. **Przejdź do katalogu backendu:**
   ```sh
   cd ReservationAPI
   ```
2. **Zainstaluj zależności:**
   ```sh
   dotnet restore
   ```
3. **Uruchom backend:**
   ```sh
   dotnet run
   ```
4. **Sprawdź, czy działa** → Otwórz w przeglądarce `https://localhost:7222/swagger`


---

## 🌍 Uruchomienie Frontend (Angular 19)
1. **Przejdź do katalogu frontendu:**
   ```sh
   cd flight-reservation-frontend
   ```
2. **Zainstaluj zależności:**
   ```sh
   npm install
   ```
3. **Uruchom frontend:**
   ```sh
   ng serve
   ```
4. **Otwórz w przeglądarce** → `http://localhost:4200`

---

## ⚡ Funkcjonalności
✅ **Backend (Minimal API .NET 9)**
- CRUD dla rezerwacji
- JSON jako baza danych
- Swagger UI do testowania API
- Obsługa błędów i walidacja danych

✅ **Frontend (Angular 18)**
- Wyświetlanie listy rezerwacji (tabela z paginacją, sortowaniem i filtrowaniem)
- Formularz dodawania i edycji rezerwacji (z walidacją)
- Usuwanie rezerwacji
- API obsługiwane przez Angular Service
- Bootstrap + Angular Material dla UI

---

## 🐞 Debugowanie
- Jeśli backend nie działa, sprawdź czy **port 5000/7222** jest dostępny.
- Jeśli frontend nie pobiera danych, sprawdź czy CORS jest włączony w backendzie.
- Możesz użyć `console.log()` w Angularze i `dotnet watch run` w .NET do debugowania na żywo.

---

## 📌 Autor
Projekt został stworzony przez **Aleksandrę Czembrowską**. Jeśli masz pytania, możesz się ze mną skontaktować! 🎉

🚀 **Miłego kodowania!** ✈️
