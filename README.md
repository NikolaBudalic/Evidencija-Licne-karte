# Sistem za izdavanje ličnih karata

## Opis projekta

Sistem za izdavanje ličnih karata predstavlja višeslojnu web aplikaciju razvijenu u okviru seminarskog rada iz predmeta Razvoj višeslojnih softverskih sistema.

Aplikacija omogućava evidenciju građana, podnošenje zahteva za izdavanje lične karte, obradu zahteva i upravljanje pratećom dokumentacijom. Sistem je implementiran korišćenjem ASP.NET tehnologije i organizovan je kroz više logičkih slojeva radi lakšeg održavanja i proširenja.

---

## Funkcionalnosti

- Prijava korisnika
- Evidencija građana
- Evidencija zahteva za izdavanje lične karte
- Unos, pregled, izmena i brisanje podataka
- Obrada zahteva
- Štampa zahteva
- REST servis za razmenu podataka
- Entity Framework pristup podacima
- Validacija korisničkog unosa
- Primena poslovnih pravila sistema

---

## Poslovno pravilo

Sistem implementira sledeće poslovno pravilo:

> Ukoliko je građanin mlađi od 18 godina, obavezno je evidentiranje podataka roditelja ili staratelja prilikom podnošenja zahteva za izdavanje lične karte.

Parametri poslovnih pravila čuvaju se u XML dokumentima radi jednostavnije izmene bez promene izvornog koda.

---

## Arhitektura sistema

Aplikacija je organizovana kroz četiri osnovna sloja:

### 1. Sloj podataka

- SQL Server baza podataka
- Stored procedure
- DBUtils klase
- Entity Framework modeli

### 2. Sloj poslovne logike

- ObradaZahtevaKlasa
- PravilaLicneKarteKlasa
- ValidacijaGradjaninaKlasa
- Interfejsi poslovne logike

### 3. Sloj servisa

- GradjaniServis (ASMX Web Service)
- GradjaniApi (REST servis)
- Klase mapiranja

### 4. Prezentacioni sloj

- ASP.NET Web Forms
- ViewModel pristup
- Validacija korisničkog unosa

---

## Korišćene tehnologije

- C#
- ASP.NET Web Forms
- ADO.NET
- Entity Framework
- SQL Server
- XML
- JavaScript
- REST
- ASMX Web Services
- Git
- GitHub
- Visual Studio

---

## Struktura projekta

```text
1_SlojPodataka
2_SlojPoslovneLogike
3_SlojServisa
4_PrezentacioniSloj
```

---

## Objektno-orijentisani principi

U projektu su primenjeni:

- Klase i objekti
- Enkapsulacija
- Interfejsi
- Apstrakcija
- Dependency Injection
- Višeslojna arhitektura

---

## Autor

Nikola Budalić

Softversko inženjerstvo

Tehnički fakultet „Mihajlo Pupin“ Zrenjanin
