# Program sledující změny ve vybraném adresáři
## Práce s programem
Po zadání cesty k adresáři - možné z klávesnice do textboxu "Vybraný adresář" nebo dialogem přes tlačítko "Výběr adresáře" - a stisku tlačítka "Výběr adresáře", se zobrazí změny v adresáři od poslední kontroly. Program očekává zadání nového adresáře dokud není stisknuto tlačítko "Konec programu".
## Sledované z změny
### Podadresáře
#### Změny podadresářů
- Vytvoření podadresáře
- Smazání podadresáře
- Přejmenování podadresáře je zachyceno jako smazání a vytvoření
#### Porovnání s předchozím stavem
- Ohlášení, že nedošlo ke změnám v podadresářích
- Ohlášení, že adresář neobsahoval podadresáře 
### Soubory
#### Změny souborů
- Vytvoření souboru
- Smazání souboru
- Přejmenování souboru je sledováno odděleně od změny obsahu souboru
- Změna obsahu
#### Porovnání s předchozím stavem
- Ohlášení, že nedošlo ke změnám v souborech
- Ohlášení, že adresář neobsahoval soubory
## Princip práce
Program ukládá záznam o stavu adresáře do sledovaného xml souboru. Při další kontrole adresáře je ze souboru načten stav a porovnán se současným. Neexistence xml souboru je ohlášena uživateli jako začátek sledování vybraného adresáře. Změny xml souboru program nesleduje.
## Instalace
Program nevyžaduje instalaci.