# Birder-x-Runner
## Skelett
* Poängräkning: Separata för fåglar och tid, egen highscore
* Spelmekanik: Observera fåglar, visa del av löpsträckan
* Assets: Fåglar, fågelläten

## Utveckla
* Poängräkning: fåglar/tid, arter/tid, vikta arter, highscore för segment
* Spelmekanik: Flera fåglar per art, inzoomat spelfält, fågelläten som cue
* Assets: Flera individer per art

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
