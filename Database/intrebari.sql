CREATE TABLE IF NOT EXISTS Intrebari (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    RaspunsAfisat TEXT,
    VarianteIntrebari TEXT,
    IntrebareCorecta TEXT,
    Categorie TEXT,
    Punctaj INTEGER
);

DELETE FROM Intrebari;

INSERT INTO Intrebari (RaspunsAfisat, VarianteIntrebari, IntrebareCorecta, Categorie, Punctaj) VALUES


("Varietatea organismelor vii de pe Pamant",
"Ce este biodiversitatea?;Ce este urbanizarea?;Ce este poluarea?;Ce este industrializarea?",
"Ce este biodiversitatea?","Biodiversitate_Usoara",0),

("Locul unde traieste un organism",
"Ce este habitatul?;Ce este ecosistemul?;Ce este relieful?;Ce este clima?",
"Ce este habitatul?","Biodiversitate_Usoara",0),

("Grup de organisme din aceeasi specie",
"Ce este populatia?;Ce este ecosistemul?;Ce este biosfera?;Ce este clima?",
"Ce este populatia?","Biodiversitate_Usoara",0),

("Interactiunea dintre organisme si mediu",
"Ce este ecosistemul?;Ce este habitatul?;Ce este clima?;Ce este relieful?",
"Ce este ecosistemul?","Biodiversitate_Usoara",0),

("Animale care mananca plante",
"Ce sunt erbivorele?;Ce sunt omnivorele?;Ce sunt carnivorele?;Ce sunt bacteriile?",
"Ce sunt erbivorele?","Biodiversitate_Usoara",0),

("Animale care mananca carne",
"Ce sunt carnivorele?;Ce sunt erbivorele?;Ce sunt omnivorele?;Ce sunt parazitele?",
"Ce sunt carnivorele?","Biodiversitate_Usoara",0),

("Specii care nu mai exista",
"Ce sunt speciile extincte?;Ce sunt speciile rare?;Ce sunt speciile invazive?;Ce sunt speciile domestice?",
"Ce sunt speciile extincte?","Biodiversitate_Usoara",0),

("Protejarea naturii",
"Ce este conservarea?;Ce este poluarea?;Ce este industrializarea?;Ce este urbanizarea?",
"Ce este conservarea?","Biodiversitate_Usoara",0),

("Distrugerea mediului",
"Ce este poluarea?;Ce este reciclarea?;Ce este adaptarea?;Ce este migratia?",
"Ce este poluarea?","Biodiversitate_Usoara",0),

("Animale care mananca plante si carne",
"Ce sunt omnivorele?;Ce sunt erbivorele?;Ce sunt carnivorele?;Ce sunt virusii?",
"Ce sunt omnivorele?","Biodiversitate_Usoara",0),


("Totalitatea ecosistemelor",
"Ce este biosfera?;Ce este atmosfera?;Ce este hidrosfera?;Ce este litosfera?",
"Ce este biosfera?","Biodiversitate_Medie",0),

("Specii care afecteaza ecosistemul",
"Ce sunt speciile invazive?;Ce sunt speciile protejate?;Ce sunt speciile rare?;Ce sunt speciile domestice?",
"Ce sunt speciile invazive?","Biodiversitate_Medie",0),

("Disparitia speciilor",
"Ce este extinctia?;Ce este evolutia?;Ce este migratia?;Ce este adaptarea?",
"Ce este extinctia?","Biodiversitate_Medie",0),

("Organisme care descompun materia",
"Ce sunt descompunatorii?;Ce sunt producatorii?;Ce sunt consumatorii?;Ce sunt pradatorii?",
"Ce sunt descompunatorii?","Biodiversitate_Medie",0),

("Deplasarea animalelor",
"Ce este migratia?;Ce este extinctia?;Ce este fotosinteza?;Ce este digestia?",
"Ce este migratia?","Biodiversitate_Medie",0),

("Capacitatea de adaptare",
"Ce este adaptarea?;Ce este extinctia?;Ce este migratia?;Ce este poluarea?",
"Ce este adaptarea?","Biodiversitate_Medie",0),

("Transferul energiei",
"Ce este lantul trofic?;Ce este ecosistemul?;Ce este habitatul?;Ce este clima?",
"Ce este lantul trofic?","Biodiversitate_Medie",0),

("Plantele sunt",
"Ce sunt producatorii?;Ce sunt consumatorii?;Ce sunt descompunatorii?;Ce sunt pradatorii?",
"Ce sunt producatorii?","Biodiversitate_Medie",0),

("Relatie benefica pentru ambele",
"Ce este mutualismul?;Ce este parazitismul?;Ce este competitia?;Ce este pradarea?",
"Ce este mutualismul?","Biodiversitate_Medie",0),

("Organism care traieste pe altul",
"Ce este parazitismul?;Ce este mutualismul?;Ce este competitia?;Ce este cooperarea?",
"Ce este parazitismul?","Biodiversitate_Medie",0),


("Varietatea genetica intr-o specie",
"Ce este diversitatea genetica?;Ce este ecosistemul?;Ce este habitatul?;Ce este populatia?",
"Ce este diversitatea genetica?","Biodiversitate_Grea",0),

("Interconectarea lanturilor trofice",
"Ce este reteaua trofica?;Ce este lantul trofic?;Ce este biosfera?;Ce este habitatul?",
"Ce este reteaua trofica?","Biodiversitate_Grea",0),

("Specii esentiale pentru ecosistem",
"Ce sunt speciile cheie?;Ce sunt speciile invazive?;Ce sunt speciile domestice?;Ce sunt speciile rare?",
"Ce sunt speciile cheie?","Biodiversitate_Grea",0),

("Schimbarea speciilor in timp",
"Ce este evolutia?;Ce este extinctia?;Ce este adaptarea?;Ce este migratia?",
"Ce este evolutia?","Biodiversitate_Grea",0),

("Disparitie masiva rapida",
"Ce este extinctia in masa?;Ce este migratia?;Ce este adaptarea?;Ce este conservarea?",
"Ce este extinctia in masa?","Biodiversitate_Grea",0),

("Specii limitate geografic",
"Ce sunt speciile endemice?;Ce sunt speciile invazive?;Ce sunt speciile migratoare?;Ce sunt speciile domestice?",
"Ce sunt speciile endemice?","Biodiversitate_Grea",0),

("Refacerea ecosistemelor",
"Ce este succesiunea ecologica?;Ce este extinctia?;Ce este migratia?;Ce este poluarea?",
"Ce este succesiunea ecologica?","Biodiversitate_Grea",0),

("Consumatori in lantul trofic",
"Ce sunt consumatorii?;Ce sunt producatorii?;Ce sunt descompunatorii?;Ce sunt bacteriile?",
"Ce sunt consumatorii?","Biodiversitate_Grea",0),

("Echilibrul naturii",
"Ce este echilibrul ecologic?;Ce este poluarea?;Ce este urbanizarea?;Ce este industrializarea?",
"Ce este echilibrul ecologic?","Biodiversitate_Grea",0),

("Ecosistem afectat de oameni",
"Ce este degradarea mediului?;Ce este adaptarea?;Ce este evolutia?;Ce este mutualismul?",
"Ce este degradarea mediului?","Biodiversitate_Grea",0);