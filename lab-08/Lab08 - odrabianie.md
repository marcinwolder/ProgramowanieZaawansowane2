# Laboratorium 08: SQLite
## Programowanie zaawansowane 2

- Maksymalna liczba punktów: 10

- Skala ocen za punkty:
    - 9-10 ~ bardzo dobry (5.0)
    - 8 ~ plus dobry (4.5)
    - 7 ~ dobry (4.0)
    - 6 ~ plus dostateczny (3.5)
    - 5 ~ dostateczny (3.0)
    - 0-4 ~ niedostateczny (2.0)

Celem laboratorium jest zapoznanie z działaniem funkcji pozwalających na bezpośrednią komunikację z bazą danych SQLite. 

Laboratorium składa się z kilku podpunktów - każdy przetestuj przed pokazaniem go prowadzącemu.

1. [2 punkty] Napisz metodę, która utworzy tabelę w bazie danych SQL Lite, która składa się z trzech kolumn. Nazwa tabeli oraz nazwy kolumn mają być podawane jako parametry metody. Pierwsza kolumna ma być typu integer i być kluczem głównym z autoinkrementem, druga kolumna ma być typu real, trzecia typu text.
2. [3 punkty] Napisz metodę, która wypełni powyższą tabelę losowymi danymi. Nazwa tabeli, nazwy kolumn oraz liczba rekordów, które mają być wstawione mają być parametrami zapytania.
3. [1 punkt] Napisz metodę, która jako parametr przyjmuje nazwę tabeli. Metoda przy pomocy kwerendy SELECT ma wypisać do konsoli wszystkie dane, które znajdują się w tej tabeli. Proszę wypisać również nazwy kolumn.
4. [2 punkty] Napisz metodę, która wyeksportuje dane z powyższej tabeli do pliku CSV. Pierwszą linijką pliku (nagłówkiem) powinny być nazwy kolumn, kolejne linijki powinny zawierać poszczególne rekordy. Metoda jako parametr powinna pobierać nazwę pliku, do którego ma być dokonany eksport, separator pól oraz format liczb zmiennoprzecinkowy (CultureInfo).
5. [2 punkty] Napisz metodę, która dla zadanego pliku, którego format jest zgodny z formatem pliku z zadania 4 sprawdzi, ile znajduje się w nim kolumn oraz rekordów. Metoda powinna również zwrócić informację, czy żaden z rekordów nie zawiera więcej kolumn niż nagłówek pliku. Jeżeli taka sytuacja następuje, proszę wypisać numer rekordu (linijki w pliku) oraz jego zawartość. Metoda jako parametry powinna przyjmować nazwę pliku oraz separator pól.

