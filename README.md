# Text-Rollenspiel
> C#

## Story
Spieler landet in einem Verlies, er besiegt Kreaturen, um tiefer in das Verlies zu kommen. Auf dem Weg kann er Charaktere, die er im Verlies findet, in seine Gruppe rekrutieren, um zusammen mit ihnen zu kämpfen.
- Die Entscheidungen des Spielers, wie auch Zufall haben starken Einfluss auf den Verlauf des Spiels.
## Combat System
### Turn-Based-Combat-System
Der Gegner greift immer zuerst an, die Attacke wird zufällig ausgewählt. Daraufhin kann der zufällig gewählte Spieler aus der Gruppe den Gegner angreifen.
### Verteidigung
Alle Charaktere haben eine Verteidigung, die Schaden von Attacken vermindern.
### Flucht
Der Spieler kann versuchen vor einem Kampf zu flüchten. Ob er in der Lage ist zu Flüchten hängt von einem Münzwurf ab.
- Wenn er falsch rät, muss er trotzdem kämpfen.
- Wenn er richtig rät, flieht er ohne Schaden zu nehmen.
#### Gescheiteter Fluchtversuch
Abhängig von der Beweglichkeit des Spielers, kann er bei der Flucht Schaden nehmen, was sich negativ auf folgende Kämpfe auswirkt.
## Charaktere
Der Spieler lernt auf den verschiedenen Leveln verschiedene Charaktere kennen, einige von diesen kann er in seine Gruppe rekrutieren, andere können ihm nützliche Angebote machen.
## Spiel Ende
Sobald der Spieler stirbt ist das Spiel vorbei, es gibt keine Checkpoints, jeder Tod ist endgültig.
