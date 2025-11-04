# Uppgift 4 – Rock Paper Arena (CiP-04-RockPaperArena)

I den här uppgiften bygger du vidare på föregående uppgift **CiP-03-eWorldCup**.  
Du ska nu skapa ett spelbart **Sten–Sax–Påse**-system som körs som en turnering.  
Du spelar som **en av alla deltagare**, medan övriga matcher körs automatiskt i bakgrunden.  
Resultat och ställning sparas i backend, och frontend byggs i **React eller Next.js**.

> Använd [**MUI**](https://mui.com/material-ui/getting-started/) för att snabbt bygga gränssnitt med knappar, tabeller och statuskort.

---

## Koppling till tidigare uppgift (CiP-03)

I **CiP-03-eWorldCup** byggde du logiken för att ta fram vilka spelare som möter varandra i varje runda med hjälp av **round-robin-metoden**.  
Den logiken ska du nu **återanvända** i den här uppgiften:

- använd din funktion för att skapa alla par per runda
- varje par spelar en Sten-Sax-Påse-match bestående av upp till 3 rundor, där den som vinner 2 rundor vinner matchen
- alla övriga matcher i varje runda spelas också som bäst av 3 och simuleras i backend
- användaren spelar **sin egen match manuellt**, medan övriga körs automatiskt i bakgrunden
- resultaten sparas och visas mellan rundor

Det innebär att du inte ska skapa “vem-möter-vem” igen — slå ihop logiken från ditt befintliga system ifrån CiP-03.

---

## Del ett – API (backend)

### Beskrivning

API:et ska hantera hela turneringen:

- start av turnering (namn + antal spelare)
- körning av rundor med matcher från din round-robin-logik
- varje match består av upp till 3 rundor, och den som vinner 2 rundor vinner matchen
- alla övriga matcher i varje runda spelas också som bäst av 3 och simuleras i backend
- användarens manuella match i varje runda
- simulering av övriga matcher
- uppdatering av scoreboard och slutresultat

### API-endpoints

| Metod    | Sökväg                | Funktion                                                                                                                                                                                                                                                      |
| -------- | --------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **POST** | `/tournament/start`   | Startar ny turnering. Skapar par för runda 1 baserat på din round-robin-funktion. Body: `{ "name": "Alice", "players": 8 }`.                                                                                                                                  |
| **GET**  | `/tournament/status`  | Returnerar aktuell runda, din nästa motståndare och scoreboard, samt information om delrundor i matchen, t.ex. `"round": 1 of 3`, `"playerWins": 1`, `"opponentWins": 0`. Även status för om övriga matcher i rundan är färdigspelade (bäst av 3) kan ingå.   |
| **POST** | `/tournament/play`    | Du gör ditt drag (`rock`, `paper`, `scissors`). Backend avgör resultatet för delrundan, uppdaterar poäng och sparar resultatet för denna delrunda i matchen. Returnerar status för matchens delrundor och aktuell ställning.                                  |
| **POST** | `/tournament/advance` | Simulerar alla **övriga/automatiska matcher** i den pågående rundan som **bäst av 3** (tills någon har 2 delrundevinster), uppdaterar scoreboard och sätter upp nästa runda via round-robin-logiken **först när samtliga matcher i rundan är färdigspelade**. |
| **GET**  | `/tournament/final`   | Returnerar slutresultatet och vinnare när alla rundor är spelade.                                                                                                                                                                                             |

Poängsystem: **Vinst = 3 p**, **Oavgjort = 1 p**, **Förlust = 0 p**  
Round-robin-logiken från CiP-03 bestämmer paren; spelet bestämmer resultaten.

---

## Spelupplägg

1. **Starta turneringen**

   - Ange ditt namn och antal spelare.
   - API:et skapar spelare (du + "AI"-spelare) och genererar alla matcher med din round-robin-funktion.
   - Du får första motståndaren (t.ex. Bob, id 2), uträknad enligt round‑robin från CiP‑03, och ett tomt scoreboard.

2. **Runda 1**  
   Du möter Bob (id 2) — motståndaren är bestämd via round‑robin från CiP‑03. Alla andra matcher i rundan körs **automatiskt** i bakgrunden.

   - Du gör upp till tre drag per match, där varje drag representerar en delrunda.
   - Vid varje drag avgörs delrundan och poängen uppdateras.
   - Matchen avslutas när någon har vunnit två delrundor (bäst av 3).
   - Övriga par i samma runda spelar också bäst av 3, och deras matcher simuleras automatiskt i backend.
   - Backend returnerar status för delrundorna, t.ex. `"round": 2 of 3`, `"playerWins": 1`, `"opponentWins": 1`.
   - Poängen sparas.
   - API:et returnerar ditt resultat och uppdaterad scoreboard.

3. **Mellanrunda**

   - Scoreboard visas med alla spelares poäng.
   - Knapp “Nästa runda” anropar `/tournament/advance`, som först **spelar klart alla övriga matcher i pågående runda som bäst av 3** och därefter ställer upp nästa runda.

4. **Nästa rundor**  
   Du fortsätter spela dina matcher (bäst av 3) medan resterande par i varje runda också spelas (bäst av 3) i bakgrunden.  
   Efter varje match visas scoreboarden.

5. **Final**  
   När alla rundor är spelade visar systemet slutställningen och vinnaren.

---

## Del två – Frontend (React eller Next.js)

Bygg gränssnittet så att turneringen går att **spela** runda för runda.

### Vykrav

**Start**

- Textfält för ditt namn
- Välj antal spelare
- Knapp _Starta turnering_ → anropa `/tournament/start`

- Visa text: “Runda 1 – Du möter Bob (id 2) (enligt round‑robin från CiP‑03)”
- Tre knappar: Sten, Sax, Påse
- Vid klick: anropa `/tournament/play` och visa resultat för delrundan ("AI" val + vinst/förlust/oavgjort) samt status för matchens delrundor (t.ex. `"round": 2 of 3`, `"playerWins": 1`, `"opponentWins": 1`)

**Scoreboard**

- Visa poängtabell (alla spelare)
- Knapp _Nästa runda_ → `/tournament/advance`
- Scoreboarden ska spegla att alla matcher i rundan är avslutade (bäst av 3) innan nästa runda visas.

**Final**

- Visa slutresultat och vinnare
- Knapp _Starta ny turnering_

---

## Sammanfattning

- Använd round-robin-funktionen från CiP-03 för att bestämma par i varje runda.
- Bygg spelet Sten-Sax-Påse ovanpå detta schema.
- Varje match spelas som bäst av 3 rundor, där den som vinner 2 rundor vinner matchen.
- Spara poäng och uppdatera scoreboarden i backend efter varje runda.
- Frontend låter spelaren delta manuellt i varje delrunda medan resten körs automatiskt.
- Alla matcher — även de simulerade — spelas som bäst av 3 delrundor.
