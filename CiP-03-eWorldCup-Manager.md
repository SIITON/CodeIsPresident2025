# Uppgift 3 – eWorldCup Manager (CiP-03-eWorldCup Manager)

Bygg en komplett lösning för turneringen från **CiP-02** som består av:

1. ett **REST-API** som exponerar era round-robin-funktioner, och
2. en **frontend i React eller Next.js** som visar rundor, matcher och “vem möter jag när?”.

> Använd gärna [**MUI**](https://mui.com/material-ui/getting-started/) som UI-ramverk för snabbare byggande av komponenter.

---

## Del ett – API (backend)

### Beskrivning

Skapa ett REST-API som använder er befintliga turneringslogik från CiP-02.  
Deltagarlistan antas redan finnas i koden (samma som i CiP-02 eller importerad därifrån).

### Endpoints

### API-endpoints

| HTTP-metod | URL-sökväg               | Funktion / Beskrivning                                                     |
| ---------- | ------------------------ | -------------------------------------------------------------------------- |
| **GET**    | `/rounds/:d`             | Returnerar alla matcher i runda `d` (`1 ≤ d ≤ n−1`).                       |
| **GET**    | `/rounds/max?n=`         | Returnerar max antal rundor för `n` deltagare (`n−1`).                     |
| **GET**    | `/match/remaining?n=&D=` | Returnerar antal återstående unika par efter att `D` rundor har spelats.   |
| **GET**    | `/match?n=&i=&d=`        | Returnerar direkt vem spelare `i` möter i runda `d` (0-baserat index).     |
| **GET**    | `/player/:i/schedule`    | Returnerar hela schemat för spelare `i` över rundor `1..n−1`.              |
| **GET**    | `/player/:i/round/:d`    | Alias till “direktfråga” för spelare `i` i runda `d`, men med namn/objekt. |
| **POST**   | `/player`                | **(Bonus)** Lägg till en ny deltagare i listan.                            |
| **DELETE** | `/player/:id`            | **(Bonus)** Ta bort en deltagare ur listan baserat på ID.                  |

### Regler och antaganden

- `n` är jämnt och ≥ 2
- `1 ≤ d ≤ n−1`, `0 ≤ i < n`
- Inga par får upprepas mellan rundor (round-robin / polygonmetoden)
- Deltagarnamn hämtas från listan i koden (samma som i CiP-02). Om namn saknas kan ni svara med index.

### Exempel

**GET /round/2**

```json
{
  "round": 2,
  "pairs": [
    { "home": "Alice", "away": "Charlie" },
    { "home": "Bob", "away": "Fiona" },
    { "home": "Diana", "away": "Ethan" }
  ]
}
```

**GET /player/2/schedule** _(i = 2 → “Charlie”)_

```json
{
  "player": "Charlie",
  "n": 6,
  "schedule": [
    { "round": 1, "opponent": "Ethan" },
    { "round": 2, "opponent": "Alice" },
    { "round": 3, "opponent": "Fiona" },
    { "round": 4, "opponent": "Bob" },
    { "round": 5, "opponent": "Diana" }
  ]
}
```

---

## Del två – Frontend (React eller Next.js)

### Beskrivning

Bygg ett webbgränssnitt som använder API:et från del ett med hjälp av React eller Next.js

> Använd gärna [**MUI**](https://mui.com/material-ui/getting-started/) som UI-ramverk för snabbare byggande av komponenter.

### Vykrav

#### 1. Rundvy

- Välj runda `d` via input eller dropdown.
- Visa matcherna för runda `d` (data från `GET /round/:d`), exempelvis i lista eller tabell.

#### 2. Spelarschema (“Vem möter jag när?”)

- Välj spelare via dropdown/sök (namn hämtas från listan i koden).
- Visa spelarens schema (data från `GET /player/:i/schedule`) i tabellform:  
  `Runda | Motståndare`
- Klick på en runda kan länka till Rundvyn för samma `d`.

#### 3. Informationsfält

- Visa `n` (antal deltagare) och max antal rundor (hämtat via `GET /rounds/max` eller beräknat i frontend).

### UI-förslag med MUI

- `TextField` eller `Select` för runda och spelare
- `Table` eller `List` för match- och schemavyer
- `Container`, `Stack` och `Card` för layout

---

## Indata & Exempeldata

Använd samma deltagaresample som i CiP-02 (index 0..n−1 mappas till namn):

```json
[
  { "id": 1, "name": "Alice" },
  { "id": 2, "name": "Bob" },
  { "id": 3, "name": "Charlie" },
  { "id": 4, "name": "Diana" },
  { "id": 5, "name": "Ethan" },
  { "id": 6, "name": "Fiona" },
  { "id": 7, "name": "George" },
  { "id": 8, "name": "Hannah" },
  { "id": 9, "name": "Isaac" },
  { "id": 10, "name": "Julia" },
  { "id": 11, "name": "Kevin" },
  { "id": 12, "name": "Laura" },
  { "id": 13, "name": "Michael" },
  { "id": 14, "name": "Nina" },
  { "id": 15, "name": "Oscar" },
  { "id": 16, "name": "Paula" },
  { "id": 17, "name": "Quentin" },
  { "id": 18, "name": "Rachel" },
  { "id": 19, "name": "Samuel" },
  { "id": 20, "name": "Tina" }
]
```

---

### README.md

Dokumentera i en README hur man startar backend och frontend  
(t.ex. separata `npm run dev`, eller gemensamt script).
