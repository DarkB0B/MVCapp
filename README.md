# MVCapp

## Jest to aplikacja umożliwiająca przeglądanie kolejek ligowych oraz tabel ligowych. Użytkownik zalgowoany jest w stanie dodawać drużyny do ulubionych przez naciśnięcie gwiazdki w tabeli ligowej. po naciśnięciu na drużynę w tabeli ligowej lub w ulubionych drużynach wysunięte zostanie okno z listą rozegranych przez tą drużynę meczy.

Do uruchomienia aplikacji wymagane jest połączenie z bazą danych MSSQL, aby ustanowić takie połączenie trzeba zmienić connection string znajdujący się w pliku appsettings.json oraz uruchomić komendę "dotnet ef database update". Aplikacja przy pierwszym uruchomieniu sama uzupełni bazę danych o podstawowe dane.

