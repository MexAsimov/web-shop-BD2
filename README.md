# Fragment aplikacji przedstawiającej sklep internetowy do zakupu części samochodowych
Aplikacja będzie przechowywać klientów, producentów, zamówienia oraz stany magazynowe potrzebne zarządzającemu zamówieniami do szybkiego znajdowania zakupionego produktu. Aplikacja realizuje podstawowe czynności dostępne w sklepie internetowym:
- logowanie oraz rejestracja nowych użytkowników (konsumentów i dostawców)
- sprawdzanie dostępności wystawionych przedmiotów oraz mechanizm filtrów
- wybór przedmiotów i zakup ich (bez realizacji systemu płatności)


# Technologie:
  - Entity Framework
  - C#
  - SQLite3
  - .NET

# Schemat:
![image](https://user-images.githubusercontent.com/58474974/121442113-4af2df00-c98b-11eb-9c85-a0e477cb01d4.png)

W schemacie można wyszczególnić trzy integralne części:
- Users, Customers i Distributors - tabele odpowiedzialne za system uwierzytelniania użytkowników w systemie 
- Products, StoragePlaces - tabele odpowiedzialne za stany magazynowego sklepu internetowego 
- Invoices, Cart - tabele odpowiedzialne za realizacje zamówień i koszyk
Ponadto:
- DistributorsProducts, OrderUnits, CartElements - tabele pomocnicze dodane w celu realizacji relacji wiele do wiele

# Tabele

Users - użytkownicy:
- UsersID - identyfikator użytkownika w systemie
- City, Street, Number, PostCode, Country - dane odpowiedzialne za lokalizacje podmiotu
- Phone, Email - dane kontaktowe użytkownika
- NIP - numer identyfikacji podatkowej podatników w Polsce
- Password - hasło do konta użytkownika w formie zaszyfrowanej

Customers - klienci (Users):
- UsersID - identyfikator użytkownika w systemie
- CustomersID - identyfikator klienta w systemie
- FirstName - imie
- LastName = nazwisko
- CardNumber - numer rachunku bankowego klienta

Distributors - dostawcy (Users):
- UsersID - identyfikator użytkownika w systemie
- DistributorsID - identyfikator dostawcy w systemie
- CompanyName - nazwa firmy dostarczającej
- BankAccountNumber - numer konta bankowego dostawcy

Products - produkty:
- ProductID - identyfikator konkretnego przedmiotu (typ, model) w systemie
- ProductName - nazwa przedmiotu
- ShortDescription - krótki opis produktu zawierający najważniejsze informacje
- Description - pełny opis produkty zawierający wszystkie informacje techniczne
- Quantity - ilość przedmiotów dostępnych na stanach magazynowych
- Price - cena jednostkowa
- MeasureUnit - jednostka miary
- StoragePlacesID - identyfikator przestrzeni magazynowej

StoragePlaces - przestrzenie magazynowe:
- StoragePlacesID - identyfikator przestrzeni magazynowej
- Section, Row, Place - wartości konkretnie definiujące położenie konkretnych przedmiotów na magazynie

Invoices - faktury:
- InvoicesID - identyfikator faktury w systemie
- CustomerUsersID - identyfikator użytkownika będącego stroną w zawieraniu umowy kupna
- InvoiceDate - data realizacji zamówienia
- ProductsID - identyfikator zbioru produktów zakupionych w ramach danej faktury

OrderUnits - zbiór produktów dla danej faktury:
- OrderUnitID - identyfikator zbioru produktów
- ProductsID - identyfikator zakupionego przedmiotu
- NumberOfProducts - ilość produktów danego typu
- unitPrice - cena jednostkowa produktu
- InvoicesID - identyfikator faktury w systemie

Cart - koszyk:
- CartID - identyfikator koszyka
- UsersID - użytkownik, do którego jest przypisany dany koszyk

CartElements - przedmioty w koszyku:
- CartElementsID - identifikator zbioru produktów zawartych w koszyku
- ProductsID - identyfikator przedmiotu w systemie
- NumberOfElements - ilość produktów danego typu
- CartID - identyfikator koszyka

# Funkcje
- Buy - funkcja wywoływana w momencie zakupu przedmiotów z pozycji koszyka
![image](https://user-images.githubusercontent.com/58474974/121480856-d2604280-c9cb-11eb-9e9e-c6025e29e2fe.png)
- AddOrRemove - funkcja wywoływana podczas wrzucania/usuwania przedmiotów z koszyka
![image](https://user-images.githubusercontent.com/58474974/121483784-dcd00b80-c9ce-11eb-8562-d75dbadc4ffa.png)
- Search - funkcja wywoływana podczas filtrowania przedmiotów dostępnych w sklepie
![image](https://user-images.githubusercontent.com/58474974/121484540-9a5afe80-c9cf-11eb-879c-e5c3d44b86c3.png)
![image](https://user-images.githubusercontent.com/58474974/121484609-acd53800-c9cf-11eb-9e81-82a1620ef0d9.png)
- LogIn, LogOut - funkcje wywoływane podczas logowania i wylogowywania
![image](https://user-images.githubusercontent.com/58474974/121481328-4e5a8a80-c9cc-11eb-8f51-faee49084aa4.png)
- Register - funkcja wywoływana w widoku rejestracji jako zwieńczenie procesu stworzenia nowego konta w systemie
![image](https://user-images.githubusercontent.com/58474974/121483347-5ca9a600-c9ce-11eb-8600-973448f216da.png)
![image](https://user-images.githubusercontent.com/58474974/121483443-7cd96500-c9ce-11eb-807d-aac0557e577b.png)
- AddProduct - funkcja wywoływana podczas dodawania nowego produktu przez dostawcę
![image](https://user-images.githubusercontent.com/58474974/121481538-8530a080-c9cc-11eb-8d73-757dd7ece7bb.png)



# Skład grupy:
  - Krzysztof Gądek
  - Mateusz Asimowicz
