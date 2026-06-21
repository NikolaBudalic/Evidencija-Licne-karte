# Sistem za izdavanje ličnih karata

## Opis projekta

Aplikacija predstavlja informacioni sistem za evidenciju i obradu zahteva za izdavanje ličnih karata. Sistem omogućava unos i pregled građana, kreiranje i obradu zahteva, evidenciju dokumentacije, vođenje istorije statusa zahteva, štampu obrazaca i pristup podacima putem REST servisa.

Projekat je realizovan kao višeslojna aplikacija u okviru predmeta Razvoj višeslojnog softvera.

---

## Korišćene tehnologije

- ASP.NET MVC (.NET Framework)
- C#
- SQL Server
- Entity Framework Database First
- Repository Pattern
- REST API (ASP.NET Web API)
- Bootstrap
- HTML, CSS i JavaScript

---

## Arhitektura sistema

Projekat je organizovan kroz četiri sloja:

### 1. Sloj podataka (KlasePodataka)

Sadrži:

- Entity Framework model
- EDMX model baze podataka
- Entitetske klase:
  - Gradjanin
  - Zahtev
  - Dokumentacija
  - RoditeljStaratelj
  - IstorijaStatusaZahteva
  - Korisnik

### 2. Sloj poslovne logike (DBUtils)

Sadrži:

- Repository Pattern
- Interfejse repozitorijuma
- Implementacije repozitorijuma
- Poslovna pravila sistema
- Tehnološke klase

Primer:

- IGradjaninRepozitorijum
- GradjaninRepozitorijum
- IZahtevRepozitorijum
- ZahtevRepozitorijum

### 3. Sloj servisa (RESTServis)

Implementiran je REST servis za rad sa zahtevima.

Podržane operacije:

- GET
- POST
- PUT
- DELETE

Primer:

```
GET /api/zahtevi
```

Servis vraća podatke u JSON formatu.

### 4. Prezentacioni sloj (KorisnickiInterfejs)

ASP.NET MVC aplikacija koja omogućava:

- prijavu korisnika
- pregled građana
- unos građana
- pregled zahteva
- unos zahteva
- izmenu zahteva
- brisanje zahteva
- prikaz detalja
- štampu pojedinačnog zahteva
- parametarsku štampu

---

## Poslovna pravila

### Maloletna lica

Ukoliko građanin nije punoletan, sistem zahteva unos podataka o roditelju ili staratelju.

### Aktivni zahtevi

Građanin ne može imati više aktivnih zahteva za izdavanje lične karte.

---

## Funkcionalnosti sistema

- Evidencija građana
- Evidencija zahteva
- Evidencija dokumentacije
- Istorija statusa zahteva
- Validacija podataka
- REST API
- Repository Pattern
- Entity Framework
- Štampa zahteva
- Parametarska štampa

---

## Baza podataka

Glavne tabele:

- Gradjanin
- Zahtev
- Dokumentacija
- RoditeljStaratelj
- IstorijaStatusaZahteva
- Korisnik

Relacije između tabela realizovane su putem Entity Framework Database First pristupa.

---

## Pokretanje projekta

1. Kreirati bazu podataka u SQL Server-u.
2. Izvršiti SQL skripte iz foldera:

```
KOMPLETNA BAZA PODATAKA
```

3. Podesiti connection string.
4. Pokrenuti:
   - RESTServis
   - KorisnickiInterfejs

---

## Autor

Nikola Budalić

Fakultet tehničkih nauka „Mihajlo Pupin“ Zrenjanin

Predmet: Razvoj višeslojnog softvera
