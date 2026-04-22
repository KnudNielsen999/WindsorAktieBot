# TODO

## 1. Faa loesningen til at bygge

- [x] Tilfoej manglende NuGet-pakker til `AktieBotLibrary`, eller fjern `using`-referencer som ikke bruges.
- [x] Beslut om `Alpaca.Markets` er den valgte broker/data-provider, og tilpas projektfilen derefter.
- [x] Implementer eller fjern tomme filer som `AktieBotLibrary/Database/BotDatabase.cs`.
- [x] Implementer `AktieBotLibrary/Services/SteuchRsiBot.cs`, som lige nu ikke indeholder nogen botlogik.
- [x] Koer `dotnet build` uden compile-fejl for hele loesningen.

## 2. Implementer rigtig botlogik

- [ ] Opret service til hentning af markedsdata for de symboler botten skal handle.
- [ ] Implementer beregning af RSI, SMA20, SMA50 og SMA200.
- [ ] Definer konkrete regler for buy, sell, hard stop loss og trailing stop.
- [ ] Implementer state-haandtering, saa botten ved om den ejer en position i et symbol.
- [ ] Tilfoej ordrehistorik og matchning mellem koeb og salg.

## 3. Udvid databasen

- [ ] Beslut om botdata skal ligge i samme SQL Server database som webappen eller i separat database.
- [ ] Udvid `ApplicationDbContext` eller opret separat bot-db-context til handler, signaler, positioner og pending orders.
- [ ] Opret EF Core migrationer for bot-tabeller.
- [ ] Gem signaler, handler og aaben position-state persistent.

## 4. Goer koerslen automatisk

- [ ] Tilfoej en `BackgroundService` eller `IHostedService` til automatisk koersel.
- [ ] Registrer background service i `WindsorAktieBot/Program.cs`.
- [ ] Definer hvor tit botten skal evaluere markedet.
- [ ] Indbyg markedstider, saa botten kun handler naar boersen er aaben.
- [ ] Sikr at samme signal ikke kan sende dublette ordrer ved genstart eller fejl.

## 5. Tilfoej konfiguration og secrets

- [ ] Flyt broker-api-noegler, connection strings og andre secrets ud af kildekoden.
- [ ] Definer appsettings for `Development`, `PaperTrading` og `Production`.
- [ ] Tilfoej konfiguration for symboler, risiko, max position size og stop loss.
- [ ] Beslut hvordan valuta og eventuel DKK-rapportering skal beregnes.

## 6. Goer driften sikker

- [ ] Start med paper trading, ikke live trading.
- [ ] Tilfoej max eksponering pr. symbol og samlet dagligt tabsloft.
- [ ] Indfoer validering af ordrefoer afsendelse.
- [ ] Haandter broker-fejl, rate limits og midlertidige netvaerksfejl med retries/backoff.
- [ ] Log alle beslutninger, ikke kun eksekverede handler.

## 7. Deployment og automatisk opstart

- [ ] Beslut hostingmodel: Windows Service, container eller cloud worker.
- [ ] Tilfoej setup til automatisk opstart efter reboot.
- [ ] Opret CI workflow i `.github/workflows` til build og test.
- [ ] Tilfoej release/deploy-proces, saa botten kan opdateres kontrolleret.

## 8. Overvaagning og drift

- [ ] Tilfoej structured logging.
- [ ] Opret health check eller heartbeat, saa det er synligt om botten koerer.
- [ ] Tilfoej alarmering ved fejl, stop eller afviste ordrer.
- [ ] Lav en simpel status-side i webappen med seneste signal, seneste handel og nuvaerende positioner.

## 9. Test foer unattended drift

- [ ] Skriv unit tests for indikatorberegninger og signalregler.
- [ ] Skriv tests for edge cases som manglende data, market closed og dublette events.
- [ ] Verificer paper trading end-to-end foer live trading overvejes.
- [ ] Dokumenter en checkliste for go-live.

## Aktuelle blokeringer fundet i repoet

- [ ] `AktieBotLibrary/Services/SteuchRsiBot.cs` refererer til `Alpaca.Markets`, men pakken er ikke tilfoejet i `AktieBotLibrary.csproj`.
- [ ] `AktieBotLibrary/Services/SteuchRsiBot.cs` refererer til namespace `AktieBotLibrary.Database`, men `BotDatabase.cs` er tom.
- [ ] Webappen indeholder endnu ingen background worker eller scheduler.
- [ ] Forsiden og UI er stadig standard-skabelon og viser ikke bot-status.
