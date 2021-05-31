# Security Center

## Unterstützte Plattformen
Die Anwendung ist speziell für die Windows-Plattform entwickelt und bietet
daher **noch** keine Möglichkeit, Linux-Systeme zu unterstützen. Erfolgreich
getestete Plattformen sind:

* Microsoft Windows 10 Home / Pro (64-Bit)

## Plugin Management
Das *Security Center* unterstützt folgende Sprachen zum Entwickeln von
Erweiterungen.

* PowerShell-Skripte (`.ps1`)

Um eine Erweiterung hinzuzufügen, muss sich die Datei in einem speziell dafür
vorgesehenen Ordner befinden. Standardmäßig befindet sich dieser im
Hauptverzeichnis der Anwendung unter `./Plugins`. Ein Beispielpfad für ein
PowerShell-Skript ist `path/to/SecurityCenter/Plugins/script.ps1`.

### Erstellen von kompatiblen PowerShell-Skripten
Die PowerShell-Skripte sollten immer einen Metadaten-Header aufweisen. Dieser
beinhaltet Informationen über z.B. Name, Beschreibung und Autor des Skripts.
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
