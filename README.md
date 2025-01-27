# Car Rental System

Aplikacja internetowa do zarządzania wypożyczeniami samochodów, umożliwiająca dodawanie, edytowanie, usuwanie oraz przeglądanie rezerwacji samochodów, z kontrolą dostępności pojazdów i blokadą terminu w przypadku nakładających się rezerwacji. Aplikacja umożliwia również zarządzanie samochodami, ich dodawanie i edytowanie.

## Funkcjonalności

1. **Tworzenie rezerwacji** – Umożliwia użytkownikowi zarezerwowanie samochodu na określony okres.
2. **Edytowanie rezerwacji** – Pozwala na edycję istniejącej rezerwacji, np. zmiana terminu wypożyczenia lub samochodu.
3. **Usuwanie rezerwacji** – Umożliwia usunięcie istniejącej rezerwacji.
4. **Wyszukiwanie dostępnych samochodów** – Sprawdzenie dostępności samochodów w wybranym okresie.
5. **Oznaczenie samochodu jako niedostępnego** – Dodanie opcji oznaczenia samochodu jako "niedostępnego" (np. w przypadku awarii).
6. **Kontrola konfliktów terminów** – Sprawdzanie, czy wybrany termin rezerwacji nie nakłada się na istniejące rezerwacje.
7. **Zarządzanie pojazdami** – Możliwość dodawania nowych samochodów do systemu, edytowania ich danych oraz usuwania pojazdów.
8. **Wyświetlanie samochodów** – Lista dostępnych samochodów, ich szczegóły (marka, model, status dostępności).

## Jak działa aplikacja?

### 1. **Tworzenie rezerwacji**:

   - Użytkownik wybiera dostępny samochód (na podstawie marki i modelu), ustala daty wypożyczenia oraz zwrotu.
   - Aplikacja sprawdza, czy wybrany samochód nie jest już zarezerwowany w tym okresie.
   - Jeśli samochód jest dostępny, rezerwacja jest zapisywana w bazie danych.

### 2. **Edycja rezerwacji**:

   - Użytkownik może zmienić daty wypożyczenia, daty zwrotu lub samochód, pod warunkiem, że termin nie nakłada się na istniejącą rezerwację.

### 3. **Usuwanie rezerwacji**:

   - Użytkownik może usunąć rezerwację, co usunie ją z bazy danych.

### 4. **Dodawanie i edytowanie samochodów**:

   - Administrator może dodawać nowe samochody do systemu poprzez formularz w aplikacji, wprowadzając markę, model, status dostępności (dostępny/niedostępny) oraz inne dane.
   - Administrator może również edytować dane samochodów oraz usuwać je z bazy, jeżeli już nie są potrzebne.

### 5. **Dostępność samochodów**:

   - Administrator może oznaczyć samochód jako niedostępny (np. w przypadku awarii), co blokuje możliwość jego rezerwacji.

### 6. **Dostępność terminów**:

   - System sprawdza, czy wybrany termin rezerwacji nie nakłada się na już istniejącą rezerwację. Jeżeli termin koliduje z inną rezerwacją, wyświetlany jest odpowiedni komunikat o błędzie.
