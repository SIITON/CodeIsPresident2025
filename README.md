# CodeIsPresident2025

Samling av uppgiftsbeskrivningar

Döp dina repon till `CiP-xx-valfritt` där xx motsvarar uppgiftens nummer.  
ex. `CiP-01-FizzlyBizz`

## Tidigare uppgifter

1.  [CiP-01-FizzlyBizz](CiP-01-FizzlyBizz.md)
2.  [CiP-02-eWorldCup](CiP-02-eWorldCup.md)
3.  [CiP-03-eWorldCup-Manager](CiP-03-eWorldCup-Manager.md)
4.  [CiP-04-RockPaperArena](CiP-04-RockPaperArena.md)
5.  [CiP-05-FlightfrontMETAR](CiP-05–FlightfrontMETAR.md)

---

# Projekt - DashyBord (CiP-25-DashyBord)

Ni har i uppgift att skapa en applikation som ska ta hallspegeln till framtiden. Hallspegeln visar relevant och personlig information en individ kan tänkas behöva ta in när de ska lämna hemmet.

Men ni har ett val. Antingen går ni spåret att utveckla applikationen för individen, eller så bygger ni en version av applikationen mer lämpad åt hotell, specifikt flygplanshotell.
| Spår                                   | Målgrupp                | Fokus                                       |
| -------------------------------------- | ----------------------- | ------------------------------------------- |
| **B2C – Personlig DashyBord**          | Privatpersoner          | Personlig, social och anpassningsbar        |
| **B2B – Hotellversion (Pilot-hotell)** | Hotell för flygpersonal | Central administration och destinationsinfo |

## Spår B2C - Personlig DashyBord
"Tänk dig en smart och personlig dashboard med relevant information när du beger dig ut."
Sa Dasha Ybor till Bor Daniel under en kafferast, och som ledde de till att starta bolag. Idag är ni anställda för att förverkliga den idén. 
Efterfrågan har vuxit och förväntningarna på systemet är skyhöga. 
Det sägs att alla kommer ha en DashyBord i hallen. Det ryktas redan om samarbeten med bl.a. IKEA, Balettakademien och The Mirror.
Själva grunden till DashyBord är personligheten, anpassningen och det sociala. Användare ska kunna logga in och spara sin egna konfiguration av vad som visas.

En personlig dashboard som innehåller 
- Klocka
- Lokaltrafik (t.ex. SL)
  - Avgångstider
  - Förseningar
  - Hållplats eller station som användaren väljer/bor på
- Lokalväder
  - Temperatur,
  - Vind
  - Väderikoner 
  - Prognos
- Interaktion med andra användare
  - Lägg till som vän
  - Skicka puff/hälsning/meddelande till vänner 
- Valfri interaktiv extern datakälla
  - Aktiebevakning
  - RSS Feed
  - Guldpriser
  - Statussida
  - Google calender
  - Sportresultat
  - Turneringsstatistik
  - Spel

## Spår B2B - Hotellversion Pilow
Hotellkedjan **Pilows** riktar sig till trötta piloter världen över och ska starta upp ett nytt koncept precis intill en flygplats. Det är helt upp till er vilken flygplats hotellet ska etablera sig på. 
Varje rum ska bestyckas med en smart spegel. Det är er uppgift att utveckla systemet för dessa speglar.

I hotellversionen krävs ingen personlig inloggning och vädret bestäms av hotellets position. Däremot kräver personalen en central kontrollstation som kan hantera gästernas speglar/dashboard. Från kontrollstationen ska personal kunna konfigurera rummens speglar efter gästens information.
Som till exempel namn och pilotens nästa resmål.

Hotellspegeln innehåller:
- Klocka
- Lokalväder enl. hotellets ICAO kod
- Destination
  - Väder på flygplatsen
  - Gate (om finns)
  - Avgångstid (om finns)
- Flygplatsinformation
  - Arrivals
  - Departures + Boarding gates

Utöver innehåll ska hotellpersonalen kunna administrera sina enheter via en konsol eller applikation där de ska kunna:
- Anpassa gästinformationen till varje rum/enhet
  - Namn
  - Nästa destination eller flygning 
- Skicka ut meddelanden och information som visas på gästernas enheter

|DashyBord features   |Personlig (B2C)                     |Hotellversion (B2B)                         |
|----------------|-------------------------------|-----------------------------|
| Inloggning|   ✅      |❌           |
|Klocka|✅|✅|
|Lokaltrafik (t.ex. SL)|✅|✅|
|Lokalväder|✅|❌|
|Valfri extern datakälla|✅|❌|
|Användare kan bli vänner med varandra|✅|❌|
|Vänner kan skicka hälsningar till varandra|✅|❌|
|Användaren kan anpassa|✅|❌|
|Adminverktyg för att anpassa|❌            |✅          |
|ICAO anpassat väder|❌|✅|
|Flygplatsens boarding gates|❌|✅|
|Flygplatsens arrivals|❌|✅|
|Meddelandetjänst main -> sub|❌|✅|

# Arkitektur
Ni väljer själva teknikstack. Men vi kräver en separat front- och backend.
Vi rekommenderar React + TypeScript samt .Net/C#

Kommunikation mellan - REST API, inga custom oklara "specialare" endpoints

