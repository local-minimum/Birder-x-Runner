# Birder-x-Runner
## Skelett
* Poängräkning: Separata för fåglar och tid, egen highscore
* Spelmekanik: Observera fåglar, visa del av löpsträckan
* Assets: Fåglar, fågelläten, bakgrunder

## Utveckla
* Poängräkning: fåglar/tid, arter/tid, vikta arter, highscore för segment
* Spelmekanik: ~Viktad fågelpoäng~, ~inzoomat spelfält (bug)~, lutnings vektor, pulsmätare, fågelläten som cue, stegljud
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

### Intro
* Introbild: nåt med springa/löpning och nåt helt annat med fågelskådning
* Backstory: två separata utan koppling.
  * Fågel + tid: Vi är i det sjätte massutdöende. Vi behöver kartlägga vilka fåglar som finns innan de försvinner, så snabbt som möjligt!
  * Löpning + sträcka: Anna brukar springa en kilometer på morgonen varje dag innan hon dricker sitt kaffe. Träningsvärken efter på jobbet känns bra - ett kvitto på att hon åstadkommit något! 

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
