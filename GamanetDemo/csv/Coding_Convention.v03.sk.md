# Gamanet Coding Convention (SK)

Tento dokument popisuje špecifikáciu štruktúry aplikačného kódu aplikovanú na vývoj akéhokoľvek modulu alebo aplikácie v Gamanete. Tieto pravidlá kódovania a pomenovania sú povinné pre programátorov Gamanetu.

Akýkoľvek aplikačný kód (exe) alebo kód modulu (dll) je možné vnímať z viacerých perspektív. Napríklad sa naň môžeme pozrieť z pohľadu jeho životného cyklu, t.j. kedy sa kód aktivuje, kedy sa používa a kedy sa následne deaktivuje. Teda kedy sa programovací objekt (trieda) vytvára a kedy sa ničí. Na kód sa môžeme pozerať aj z perspektívy jeho použitia, či ide o kód, ktorý definuje štruktúru dát, alebo o kód, ktorý dáta následne spracúva.

Štruktúra kódu v rámci modulu je rozdelená na dve časti. Štandardný klientsky modul má svoju prezentačnú vrstvu (User Interface) a pozaďovú logiku (Business Logic). Business Logic spracúva dáta zadané používateľom a zabezpečuje ich získanie alebo uloženie z/do externého zdroja (servera). Serverové moduly, ako sú Drivery, IS Konektory alebo Smart Rutiny, majú len vrstvu Business Logic a nemajú UI časť. Business Logic je ďalej rozdelená do niekoľkých samostatných vrstiev. Každá vrstva má svoj vlastný špecifický prípad použitia a svoj vlastný životný cyklus.

---

## Štruktúra projektu

V projekte sú jednotlivé časti kódu spravované v adresárovej štruktúre podľa kategórie.

V rámci projektu je Business Logic rozdelená do adresárov podľa kategórie. Entity a ich pridružené Repository a Service sa nachádzajú v adresári Model. Podporné služby ako Hoster a DataSource majú vlastné priečinky. Prezentačná vrstva sa nachádza pod adresárom UI a je organizovaná podľa typu objektu (Window, Panel, Converter, Control, ...). Pravidlom je, že View a ModelView musia byť vždy uložené vedľa seba. Vďaka konvencii pomenovania sú vždy zoradené vedľa seba.

---

## Čo je objekt Context a na čo sa používa?

Každý modul v rámci svojej business logiky spravuje určité dáta, ktoré musia byť dostupné počas celého životného cyklu modulu a musia byť dostupné pre všetky časti kódu v module. Všetky takto zdieľané dáta sú registrované a distribuované prostredníctvom objektu Context. Každý modul má v danom čase len jeden Context.

Jeho pomenovanie je založené na predpone „_", názve modulu, za ktorým nasleduje prípona „Context". V prípade finálnej aplikácie ide o Application Context, ktorého názov je vždy **_AppContext**. V prípade DLL ide o Module Context, kde názov je tvorený predponou „_", za ktorou nasleduje názov modulu a prípona „Context". Ak modul vo vývoji používa detské moduly, ktoré majú vlastný Context, tieto sú registrované ako inštancie v Contexte vyvíjaného modulu. Názov inštancie Contextu je vždy v tvare názvu Contextu v Camel konvencii pomenovania, prípadne skrátením názvu modulu na iniciály. Napríklad, ak ide o _AppContext, jeho názov inštancie bude definovaný ako _appContext. Ak ide o _DemoPanelContext, názov inštancie je v tvare _dpContext.

Context je potom distribuovaný ako povinný parameter v konštruktore každého kódového objektu v rámci business logiky modulu. Výnimkou sú objekty typu Entity a EntityExt, ktoré nemajú definovaný konštruktor.

---

## Čo je objekt typu Entity

Entity je objekt, ktorý definuje dátovú štruktúru pre konkrétnu logickú jednotku. Tieto typy objektov majú v konvencii pomenovania príponu Entity. Výhradne obsahujú len vlastnosti jednotlivých dátových typov.

V rámci každého modulu môže existovať jedna špeciálna Entity typu AppConfigEntity. Špecifiká tejto Entity popíšeme na konci tejto kapitoly.

Každý objekt Entity musí dediť z objektu **PropertyChangedBase**, ktorý sa nachádza v knižnici **Gamanet.Common**. Gamanet.Common je knižnica základných funkcií, ktorá sa používa vo väčšine aplikácií vyvíjaných Gamanetom. Nuget je dostupný na verejných Nuget Serveroch Gamanetu.

Entity musí byť definovaná v nasledujúcej štruktúre. Každá vlastnosť musí obsahovať privátne pole, ktoré začína podčiarkovníkom, a musí byť inicializované na predvolenú hodnotu. V tomto prípade ide o string s predvolenou hodnotou String.Empty. Ďalej musí obsahovať verejnú vlastnosť typu getter a setter. V rámci settera musí mať vlastnosť implementovanú notifikáciu zmien. Táto notifikácia sa vyvolá len vtedy, ak bola vlastnosti skutočne priradená nová hodnota. Táto kontrola je veľmi dôležitou súčasťou celého kódového modelu. V prípade komplexných typov vlastností typu Collection alebo Object sa kontrola na Equals vynecháva.

**Dôležité upozornenie:**
Na zabezpečenie správnych bindingov sa komplexné vlastnosti nikdy nerozdelia. Namiesto toho sa vyčistia a znovu naplnia prostredníctvom objektu typu EntityExt.

---

## Čo je objekt typu EntityExt

Pre každý základný objekt typu Entity sa vždy vytvorí dodatočný objekt typu EntityExt. Tento objekt je vytvorený ako statická extenzia pre Entity. Jeho poslaním je zabezpečiť kopírovanie dát v rámci objektov rovnakého typu. Slúži tiež ako miesto na kopírovanie dát z externých objektov. Túto úlohu zabezpečuje funkcia CopyFrom s parametrom typu entity, z ktorej sa kopírovanie vykonáva.

V prípade spracovania externých dát, ako sú dáta SimpleClient, funkcia CopyFrom je jediné miesto v kóde, kde sa vymieňajú dáta medzi internou Entity a externými dátami. Validácia sa musí vykonať v rámci funkcie CopyFrom. Ak externé dáta nezodpovedajú očakávanej štruktúre alebo hodnoty nezodpovedajú špecifikácii, musí sa vyvolať výnimka.

Názov EntityExt je vždy odvodený od názvu Entity s príponou EntityExt. V príklade objekt PersonEntityExt definuje operácie vykonávané na objekte PersonEntity.

---

## Čo je objekt Repository?

Repository je objekt, ktorý drží inštanciu zodpovedajúcej Entity. V prípade zoznamu entít sú tieto zoznamy vždy udržiavané v objekte typu **ObservableCollection** na zabezpečenie notifikácie zmien pri vytváraní bindingov. Úlohou Repository je získavať a ukladať dáta z/do externého zdroja prostredníctvom objektov typu DataSource, alebo poskytovať základné CRUD operácie pre Entity. Následne poskytuje tieto dáta všetkým konzumentom v rámci modulu.

Každý jednotlivý Repository musí prijať Context modulu, v ktorom je vytvorený, v rámci svojho konštruktora. Jedinou výnimkou je Repository typu AppConfigRepository, ktorý sám slúži na poskytovanie konfiguračných dát ostatným Repository.

Názov repository sa skladá z názvu Entity, ktorej inštancie spravuje, s koncovkou Repository. Nachádza sa tiež v adresári Model. Následný názov inštancie repository je skrátený na názov Repo, aby sa zabezpečilo, že Deklarácie a inštancia objektu sú rozlíšiteľné.

**Dôležité upozornenie:**
Rovnako ako pri objektoch typu EntityExt, zoznamy entít (ObservableCollection<Entity>) sa v Repository nikdy nemažú na zabezpečenie správnych bindingov. Namiesto toho sa vyčistia a znovu naplnia.

Repository je pamäťové úložisko, ktoré udržiava dáta a vykonáva len základné operácie na ich získanie a udržiavanie aktuálneho stavu dát. Všetky ostatné operácie s dátami vykonávajú špeciálne objekty typu Service.

---

## Čo je objekt Service

Všetky rozširujúce funkcie, ktoré nejakým spôsobom používajú dáta z Repository, sa vykonávajú v objektoch Service. V prípade komplexnejších modulov je možné vytvoriť viac takýchto objektov. Objekt Service má krátky životný cyklus. Vždy sa vytvorí v momente použitia a okamžite zanikne po zavolaní zodpovedajúcej funkcie. Každý Service musí mať Context modulu ako parameter v konštruktore. Tým má prístup ku všetkým Repository s dátami v rámci svojho modulu.

Service má rovnaký názov ako Entity a Repository, ale koncová prípona je Service. Ak potrebujeme viac Service, môžeme vložiť za názov Entity (Person) a príponu (Service) charakteristiku Service. Napríklad v tomto prípade PersonStatisticService.

---

## Čo sú objekty DataSource?

Z pohľadu business logiky existuje niekoľko špecifických podporných typov objektov. Prvou kategóriou sú služby, ktoré poskytujú prístup k externým dátam. Externé z pohľadu vyvíjaného modulu. Táto kategória objektov sa nazýva služby DataSource.

Sú to objekty, ktoré zabezpečujú získavanie dát alebo ukladanie dát z/do externého zdroja. DataSource je jediná kategória objektov, ktorá pracuje s externými definíciami Entity. Tieto sú potom konvertované na interné Entity používané v rámci objektu pomocou objektov EntityExt. Návratová hodnota z funkcií DataSource musia byť už objekty s Entity definovanými v rámci modulu.

DataSource je tiež jediný objekt, kde je povolené použitie objektov Task na asynchrónne spracovanie dát. Asynchrónna funkcia je vždy definovaná ako rozšírenie synchrónnej funkcie. To zabezpečí jednoduchšie volanie testovacích funkcií v rámci Unit Testov. Takisto je to jediný objekt, kde je vyžadované použitie volania funkcie **.ConfigureAwait(false)** pri volaní externých funkcií s neznámym správaním.

**Varovanie:**
DataSource je jediné miesto na vykonanie tohto typu asynchrónneho volania. Akékoľvek iné asynchrónne volanie cez Task.Run alebo .ConfigureAwait(false) vyžaduje schválenie Team Leadera.

Ako objekty kategórie Service, ich životný cyklus je veľmi krátky. Raz sa objekt vytvorí a zavolá sa zodpovedajúca funkcia, automaticky zanikne.

---

## Čo sú objekty typu Handler/Host

Pri vývoji modulov existujú vždy služby, ktoré musia bežať počas celej životnosti modulu. Napríklad inštancia Simple Client sa vytvorí a autorizuje, pričom stále komunikuje so serverom prostredníctvom submodulu MessagePump.

Takéto služby, ktoré sa používajú prerušovane, musia byť zdieľané naprieč aplikáciou, ale nie sú priamymi držiteľmi dát. Pre tento dôvod sú vytvorené a zdieľané v rámci Context modulu.

Objekty sa nachádzajú v adresári Hoster. Podľa použitia sa ich názov skladá z názvu objektu, ktorý spravujú, a koncovky Handler alebo Host, v závislosti od internej logiky. Koncovka Host sa používa pre objekty, ktoré spravujú inštanciu externého objektu. Koncovka Handler sa používa pre objekty, ktoré sú samy osebe spracovateľmi dát medzi objektmi modulu.

---

## Ako sa spravujú konfiguračné dáta?

V rámci celého modulu existuje špeciálny typ Entity nazývaný AppConfig. Je to Entity, ktorá obsahuje konfiguračné parametre potrebné na spustenie celého modulu.

Je potrebné vytvoriť rozšírenie AppConfigEntityExt a tiež zodpovedajúci Repository k AppConfigEntity. AppConfigEntityExt obsahuje špeciálnu funkciu **CopySilentFrom**, ktorá zabezpečuje, že dáta sa načítajú do entity bez notifikácie zvyšku modulu.

AppConfigRepository je jediný Repository, ktorý má prázdny konštruktor, t.j. nemusí sa zapisovať. Ostatné objekty typu Repository, ako PersonRepository, musia už prevziať Context modulu v Repository.

---

## Prezentačná vrstva

Kódy View a ViewModel programované v projektoch Gamanetu obsahujú niekoľko špecifických pravidiel, ktoré musí vývojár rešpektovať.

Prezentačná vrstva je vždy uložená v adresári UI, ktorý obsahuje niekoľko podadresárov v závislosti od typu podporných objektov, ako sú Converters alebo Panels.

Každý objekt typu View zvyčajne obsahuje jeden objekt typu ViewModel. Názvy objektov View a ViewModel musia byť rovnaké a líšiť sa len v koncovke. Musia byť tiež uložené v rovnakom adresári, aby mohli byť logicky jednoducho spárované.

Pravidlo, ktoré platí v Gamanete, je, že spracovanie udalostí z jednotlivých Controls umiestnených vo View musí byť spracované vo funkciách v rámci CodeBehind. **Je prísne zakázané používať RelayCommands.** Teda CodeBehind zvyčajne slúži len ako mapper medzi udalosťami z View a volaniami funkcií vo ViewModeli.

---

## RootContainer a prepojenie View-ViewModel

Prvý detský koreňový UIElement (Grid) pod UserControl/Page/Window v rámci View sa používa na pripojenie pridruženého ViewModelu. Preto musí byť pomenovaný **„RootContainer"**, na ktorý sa bude odkazovať kód v CodeBehind.

Metóda prepojenia View s ViewModelom používa pravidlo View first. Main Panel alebo Main Window inicializuje celkový Context modulu a udržiava jeho inštanciu počas celej životnosti modulu. V prípade Main Window je Context Module pripojený ako hlavný DataContext. Ten je potom automaticky dedený v rámci VisualTree na detské Controls.

ViewModel sa vytvára ako súčasť inicializácie objektu View ihneď po získaní DataContext z nadradeného VisualTree. Keďže DataContext pre View je pripojený až po vytvorení samotného objektu View, nemáme DataContext dostupný v konštruktore View. Získame ho až v udalosti **DataContextChanged**.

Koncept s RootContainer.DataContext sa používa, aby sme mohli udržiavať vlastný DataContext paralelne a tiež zdedený DataContext, ktorý sme získali od nadradeného controlu.

---

## Pravidlá pre kódovanie modulov a aplikácií v Gamanete

V prípade viacvláknovej komunikácie a hromadného spracovania dát vždy používajte **Application.Current.Dispatcher.Invoke** len raz ako celkovú obálku pre hromadné spracovanie.

**Správna štruktúra viacvláknového volania:**
```csharp
Application.Current.Dispatcher.Invoke(() =>
{
    Foreach()
    {
        Command;
    }
});
```

**Zakázaná štruktúra viacvláknového volania!**
```csharp
Foreach()
{
    Application.Current.Dispatcher.Invoke(() => Command);
}
```