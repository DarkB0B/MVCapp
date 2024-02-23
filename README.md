# MVCapp

## Jest to aplikacja umożliwiająca przeglądanie kolejek ligowych oraz tabel ligowych. Użytkownik zalgowoany jest w stanie dodawać drużyny do ulubionych przez nacisśnięcie gwiazdki w tabeli ligowej. po naciśnięciu na drużynę w tabeli ligowej lub w ulubionych drużynach wysunięte zostanie okno z listą rozegranych przez tą drużynę meczy.

Do uruchomienia aplikacji wymagane jest połączenie z bazą danych MSSQL, aby ustanowić takie połączenie trzeba zmienić connection string znajdujący się w pliku appsettings.json oraz uruchomić komendę "dotnet ef database update". Aplikacja przy pierwszym uruchomieniu sama uzupełni bazę danych o podstawowe dane.

- W peirwszym kroku zrobiłem wstępny rozeznanie jak wyglądają podobne aplikacje/strony internetowe odarz czy istnieje prosty sposób pozyskania wymaganych danych.
- W drugim kroku utworzyłem projekt zawierający obsługę autoryzacji microsoft identity, oraz utworzyłem podstawowe moedele i strutkurę plików .
- W trzecim kroku zkonfigurowałem seedowanie danych.
- W czwartym kroku utworzyłem widok pozwalający wyświetlać mecze podzielone na rundy ligowe.
- W piątym kroku rozpoczołem budowę tabeli ligowej oraz przeniosłem wyświetlanie meczy do innego widoku niż "home".
- W szóstym kroku ukończyłem budowę tabeli ligowej oraz aby to osiągnąć znalazłem oraz poprawiłem błąd w modelu meczu który powodował tworzenie się nie właściwej relacji pomiędzy moedelami Team oraz Match.
- W siódmym kroku dodałem wysuwane okno wyświetlające historię meczy danej drużyny.
- W ósmym kroku zająłem się funkcjonalnością dodawania zespołów do listy ulubionych.
- Na samym końcu wykonałem testy manuale aplikacji aby wyeliminowac najłatwiejsze do znalezienia błędy.

Kolejnymi etapami rozwoju aplikacji powinno być utworzenie testów jednostkowych oraz przeprowadzenie dogłębnych testów manualnych aby wyeliminować potencjalne problemy, aplikację możnaby również wzbogacić o więcej funkcjonalności takich jak stronicowanie listy meczy w wysuwanym oknie historii rozgrywek drużyny lub segregacja lig na kraje.
