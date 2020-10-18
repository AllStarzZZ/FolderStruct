# FolderStruct

Cél:
- könyvtár fa struktúra építése a felhasználó által írható és olvasható útvonalak tartalmazó listákból

Megkötések:
- a fa nem tartalmazhat olyan elemet, ami se nem írható se nem olvasható
- nem tartalmazhat olyan elemet, aminek nincs írható leszármazottja
- csak egy root mappa van (!)

Megvalósítás:
- a könyvtár struktúra építéséhez párhuzamosan építünk egy Dictionary struktúrát, ami már előszűri a listát és csak azokat az elemeket tárolja, amik vagy olvashatóak vagy írhatóak
- addig iterálunk az input listán amíg csökken az elemszáma vagy üres nem lesz
  - minden körben meghatározzuk az adott mappa nevét (abszolút elérési út nélkül), majd lekérjük a Dictionary-ből, hogy van e már regisztrált szülő mappája, ha van akkor beírjuk
    a szótárba és a fába is. Ha a kör végén nem változik az elemszám, akkor csak olyan mappák maradtak a listában amik olvashatatlan mappákon keresztül érhetőek csak el
  - a fa elemet olvashatóként jönnek létre, ezért végigmegyünk az írható listán és beállítjuk a megfelelő mappákat írhatóra
- az így létrejött fán mélységi bejárással meghatározzuk minden elem súlyát, ami minden írható leszármazott után +1, önmagát is beleértve
- szélességi bejárással töröljük azokat az elemeket amiknek 0 a súlya (vagyis amik nem tartalmaznak írható mappát és önmaguk sem írhatóak)
- szintaktikailag érvénytelen bemenetre leáll a program elkapott kivétellel

A mainben definiált listák lefedik a következő eseteket:
- csak olvasható levél elem
- olvasható elem majd írható leszármazottal
- két írható leszármazott között csak olvasható elemek
- rejtett elem írható és olvasható leszármazottal
- bemeneten útvonal duplikáció
