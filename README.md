# Birder-x-Runner
## Skelett
* Poängräkning: Separata för fåglar och tid, egen highscore
* Spelmekanik: Observera fåglar, visa del av löpsträckan
* Assets: Fåglar, fågelläten, bakgrunder

## Utveckla
* Poängräkning: fåglar/tid, arter/tid, vikta arter, highscore för segment
* Spelmekanik: ~Viktad fågelpoäng~, ~inzoomat spelfält (bug)~, ~lutnings vektor~, ~pulsmätare~, dynamisk pace difficulty, visa observationsstatus på fåglarna, fågelläten som cue, stegljud
* Assets: fler arter, baktrunder, animerad spelare, animationer, ljud, musik,

## Poängräkning
### Fåglar
* Antal arter
* Antal fåglar
* Viktning av arter
* Över visst antal

### Löptid
* Sekunder

### Kombo
* Arter/löptid
* Fåglar/löptid

### Highscore
* Egen
* Global
* För del/segment av sträckan (tex uppförsbacke)

## Spelmekanik

### Styra löparen
* Knapptryck
* Tracker
* Trackerfart ökar med knapptryck
* Visa del man löpt av hela sträckan
* Pulsmätare som minskar grönt fält i trackern med högre puls
* Pulsen minskar för varje observerad fågel

### Svårigheter
* Inzoomning av synfält vid hög fart
* Koordination: styra mycket samtidigt
* Flera fåglar per art. Observera redan observerad fågel ger ej extra poäng.
* Uppmärksamhet splittras mellan observation och löpning

### Fågelentre
* Läten som cue

### Fågelobservation
* Musklick på fågelns hitbox startar observation
* Observation visas som progress bar runt fågeln, med låg konstant fart
* Progressfart ökar när muspekaren är över fågeln
* Full progress bar är observerad fågel
* Fortsätta observationen när individen kommer tillbaka
* Visa att fågeln har observerats

### Intro
* Introbild: nåt med springa/löpning och nåt helt annat med fågelskådning
* Backstory: två separata utan koppling.
  * Fågel + tid: We are currently in Earths sixth mass extinction. Species after species are falling prey to humans every hour and minute. In order to save them, we must observe them before they disappear - as fast as humanly possible! 
   * Löpning + sträcka: Every morning, Anna goes for a run just after her coffee. At work, she's competing with Isaiah who's doing the run in just X [seconds/minutes]. Will she beat him?

### Övrigt
* Tidigare spelomgång som skugga i nuvarande spel

## Assets

### Grafik
* Fåglar
* Progress bar

### Ljud
* Fågelläten
* Observation börjar
* Observation slutar
* Stegljud pacing
