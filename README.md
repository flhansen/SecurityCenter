# Security Center

## Unterst�tzte Plattformen
Die Anwendung ist speziell f�r die Windows-Plattform entwickelt und bietet
daher **noch** keine M�glichkeit, Linux-Systeme zu unterst�tzen. Erfolgreich
getestete Plattformen sind:

* Microsoft Windows 10 Home / Pro (64-Bit)

## Plugin Management
Das *Security Center* unterst�tzt folgende Sprachen zum Entwickeln von
Erweiterungen.

* PowerShell-Skripte (`.ps1`)

Um eine Erweiterung hinzuzuf�gen, muss sich die Datei in einem speziell daf�r
vorgesehenen Ordner befinden. Standardm��ig befindet sich dieser im
Hauptverzeichnis der Anwendung unter `./Plugins`. Ein Beispielpfad f�r ein
PowerShell-Skript ist `path/to/SecurityCenter/Plugins/script.ps1`.

### Erstellen von kompatiblen PowerShell-Skripten
Die PowerShell-Skripte sollten immer einen Metadaten-Header aufweisen. Dieser
beinhaltet Informationen �ber z.B. Name, Beschreibung und Autor des Skripts.
Das folgende Skript demonstriert einen solchen Header.

```powershell
# ---
# Author: Florian Hansen
# Name: Test Script
# Description: This is a test script.
# ---

Get-ExecutionPolicy

Write-Host 'Press any key to continue...'
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown')
```
