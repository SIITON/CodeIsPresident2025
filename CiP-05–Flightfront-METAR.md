# Uppgift 5 - Flightfronts METAR (CiP-05–Flightfront-METAR)

## Kundscenario – Flightfronts flygklubb

Flightfronts flygklubb vill ha ett enkelt webbaserat verktyg som gör METAR lättare att förstå för både elevpiloter och instruktörer.

Användaren ska kunna:

- ange en ICAO-kod **eller**
- klistra in en METAR-sträng

och få vädret presenterat i ett tydligt UI.  
Den **råa METAR-strängen ska alltid visas** för den som vill verifiera tolkningen.

Systemet ska visa **aktuellt väder (nuläge)** och inte hantera prognoser eller flygplanering.

---

## Datakällor och resurser

- **METAR API (CheckWX)**  
  Skapa ett konto på https://checkwxapi.com/  
  API-endpoint: `https://api.checkwx.com/metar/<ICAO>`  
  Exempel med curl:

  ```bash
  curl 'https://api.checkwx.com/metar/essa' -H 'X-API-Key: xxxxx'
  ```

  Ersätt `xxxxx` med din API-nyckel från CheckWX.

- **Väderikoner (Weather Icons)**  
  https://erikflowers.github.io/weather-icons/

- **Flygplatslista (ICAO-koder)**  
  https://davidmegginson.github.io/ourairports-data/airports.csv

---

## Uppgift

Bygg en webbaserad UI-komponent som:

- tar emot METAR via ICAO eller inklistrad text
- parsar METAR till en egen datamodell
- visar vädret visuellt med ikoner och text
- alltid visar rå METAR under UI:t

Fokus ligger på **struktur, rimliga förenklingar och konsekvent tolkning**.

---

## Avgränsningar

- Endast **nuläge** ska tolkas
- **REMARKS (RMK)** ska ignoreras helt
- Prognoser (TAF), trender och historik ingår inte

---

<details>
<summary><strong>Hur METAR tolkas (klicka för att visa/dölja)</strong></summary>

### Parser-checklista

1. **Förbehandling**

   - Trimma text
   - Ta bort allt efter `RMK`
   - Ta bort `METAR` / `SPECI`

2. **ICAO**

   - Första token med exakt 4 bokstäver (A–Z)

3. **Tid (uppdaterad)**

   - Token `DDHHMMZ`
   - Visas i UI som “Uppdaterad”

4. **Vind**

   - `dddffKT`
   - `VRBffKT`
   - `dddffGggKT`

5. **Sikt**

   - `CAVOK`
   - eller 4 siffror (`9999`)

6. **Väderfenomen**

   - Endast:
     - `SN`, `-SN`
     - `RA`, `-RA`
     - `FG`, `BR`

7. **Moln**

   - `FEW`, `SCT`, `BKN`, `OVC` (+ bas)

8. **Temperatur**

   - `TT/DD`
   - `M` = minusgrader

9. **QNH**
   - `Q` + 4 siffror

Saknas data → visa `—` (appen får inte krascha).

</details>

---

## Ikonregler (exakt 1 ikon)

Prioritet (uppifrån och ned):

1. `FG` eller `BR` → **Fog**
2. `SN` eller `-SN` → **Snow**
3. `RA` eller `-RA` → **Rain**
4. `BKN` eller `OVC` → **Cloudy**
5. Annars → **Clear**

Ikoner ska användas från Weather Icons.

---

## UI-minimum

UI:t ska visa:

- vald väderikon
- uppdaterad tid
- vind
- sikt
- temperatur
- QNH
- moln
- rå METAR (alltid synlig)

---

## Stilpoäng (valfritt)

Följande ger **stilpoäng**, men är inte krav för godkänt:

- Hämta listan med flygplatser från:
  https://davidmegginson.github.io/ourairports-data/airports.csv
- Skapa ett **autocomplete-fält** för ICAO-koder
- Visa gärna:
  - ICAO-kod
  - flygplatsnamn
  - land eller stad (valfritt)

Autocomplete ska:

- hjälpa användaren att välja giltig ICAO
- fortfarande tillåta manuell inmatning

---

## Definition of Done (DoD)

Lösningen anses klar när:

- Användaren kan ange `ESSA` eller `ESSB` och få ett fungerande UI
- Användaren kan klistra in en giltig METAR och få samma UI
- Väderikonen ändras beroende på METAR
- Uppdaterad tid visas
- Rå METAR visas under UI:t
- Ogiltig input ger ett tydligt felmeddelande
- REMARKS påverkar inte resultatet
