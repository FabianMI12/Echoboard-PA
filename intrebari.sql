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

-- 100
("Unitatea de baza a organismelor vii",
 "Ce este celula?;Ce este tesutul?;Ce este organul?;Ce este sistemul?",
 "Ce este celula?",
 "Biologie", 100),

("Pigmentul verde al plantelor",
 "Ce este clorofila?;Ce este hemoglobina?;Ce este melanina?;Ce este keratina?",
 "Ce este clorofila?",
 "Biologie", 100),

("Gazul necesar respiratiei",
 "Care este oxigenul?;Care este azotul?;Care este dioxidul de carbon?;Care este hidrogenul?",
 "Care este oxigenul?",
 "Biologie", 100),

("Organul care pompeaza sangele",
 "Ce este inima?;Ce este ficatul?;Ce este plamanul?;Ce este rinichiul?",
 "Ce este inima?",
 "Biologie", 100),

("Locul unde are loc fotosinteza",
 "Ce este cloroplastul?;Ce este mitocondria?;Ce este nucleul?;Ce este vacuola?",
 "Ce este cloroplastul?",
 "Biologie", 100),

("Unitatea de informatie genetica",
 "Ce este gena?;Ce este proteina?;Ce este enzima?;Ce este lipida?",
 "Ce este gena?",
 "Biologie", 100),

("Organul responsabil de respiratie",
 "Ce este plamanul?;Ce este inima?;Ce este stomacul?;Ce este rinichiul?",
 "Ce este plamanul?",
 "Biologie", 100),

-- 200
("Procesul prin care plantele isi produc hrana",
 "Ce este fotosinteza?;Ce este respiratia?;Ce este digestia?;Ce este fermentatia?",
 "Ce este fotosinteza?",
 "Biologie", 200),

("Organitul responsabil de energie",
 "Ce este mitocondria?;Ce este nucleul?;Ce este ribozomul?;Ce este vacuola?",
 "Ce este mitocondria?",
 "Biologie", 200),

("Partea celulei care controleaza activitatea",
 "Ce este nucleul?;Ce este citoplasma?;Ce este membrana?;Ce este vacuola?",
 "Ce este nucleul?",
 "Biologie", 200),

("Sistemul care coordoneaza organismul",
 "Ce este sistemul nervos?;Ce este sistemul digestiv?;Ce este sistemul respirator?;Ce este sistemul circulator?",
 "Ce este sistemul nervos?",
 "Biologie", 200),

("Substantele care formeaza proteinele",
 "Ce sunt aminoacizii?;Ce sunt glucidele?;Ce sunt lipidele?;Ce sunt vitaminele?",
 "Ce sunt aminoacizii?",
 "Biologie", 200),

("Celula fara nucleu",
 "Ce este procariota?;Ce este eucariota?;Ce este neuronul?;Ce este globula?",
 "Ce este procariota?",
 "Biologie", 200),

("Procesul de diviziune celulara simpla",
 "Ce este mitoza?;Ce este meioza?;Ce este fertilizarea?;Ce este mutatia?",
 "Ce este mitoza?",
 "Biologie", 200),

-- 300
("Procesul prin care celulele obtin energie",
 "Ce este respiratia celulara?;Ce este fotosinteza?;Ce este digestia?;Ce este osmoza?",
 "Ce este respiratia celulara?",
 "Biologie", 300),

("Organul care filtreaza sangele",
 "Ce este rinichiul?;Ce este ficatul?;Ce este plamanul?;Ce este stomacul?",
 "Ce este rinichiul?",
 "Biologie", 300),

("Structura care contine ADN-ul",
 "Ce este cromozomul?;Ce este proteina?;Ce este enzima?;Ce este lipida?",
 "Ce este cromozomul?",
 "Biologie", 300),

("Procesul de difuzie a apei prin membrana",
 "Ce este osmoza?;Ce este digestia?;Ce este fotosinteza?;Ce este respiratia?",
 "Ce este osmoza?",
 "Biologie", 300),

("Cel mai mare organ al corpului",
 "Ce este pielea?;Ce este ficatul?;Ce este creierul?;Ce este plamanul?",
 "Ce este pielea?",
 "Biologie", 300),

("Tesutul care transporta sangele",
 "Ce este tesutul sanguin?;Ce este tesutul muscular?;Ce este tesutul nervos?;Ce este tesutul epitelial?",
 "Ce este tesutul sanguin?",
 "Biologie", 300),

("Organul principal al sistemului nervos",
 "Ce este creierul?;Ce este inima?;Ce este ficatul?;Ce este stomacul?",
 "Ce este creierul?",
 "Biologie", 300),

-- 400
("Procesul de diviziune pentru reproducere",
 "Ce este meioza?;Ce este mitoza?;Ce este respiratia?;Ce este digestia?",
 "Ce este meioza?",
 "Biologie", 400),

("Structura care protejeaza celula",
 "Ce este membrana celulara?;Ce este nucleul?;Ce este citoplasma?;Ce este ribozomul?",
 "Ce este membrana celulara?",
 "Biologie", 400),

("Organ responsabil de digestie",
 "Ce este stomacul?;Ce este plamanul?;Ce este inima?;Ce este rinichiul?",
 "Ce este stomacul?",
 "Biologie", 400),

("Substantele energetice principale",
 "Ce sunt glucidele?;Ce sunt proteinele?;Ce sunt lipidele?;Ce sunt vitaminele?",
 "Ce sunt glucidele?",
 "Biologie", 400),

("Procesul de sinteza a proteinelor",
 "Ce este sinteza proteinelor?;Ce este fotosinteza?;Ce este digestia?;Ce este respiratia?",
 "Ce este sinteza proteinelor?",
 "Biologie", 400),

("Sistemul care transporta sangele",
 "Ce este sistemul circulator?;Ce este sistemul nervos?;Ce este sistemul digestiv?;Ce este sistemul respirator?",
 "Ce este sistemul circulator?",
 "Biologie", 400),

("Celulele specializate pentru transmiterea impulsurilor",
 "Ce sunt neuronii?;Ce sunt globulele rosii?;Ce sunt leucocitele?;Ce sunt trombocitele?",
 "Ce sunt neuronii?",
 "Biologie", 400),

-- 500
("Procesul de replicare a ADN-ului",
 "Ce este replicarea ADN?;Ce este fotosinteza?;Ce este digestia?;Ce este respiratia?",
 "Ce este replicarea ADN?",
 "Biologie", 500),

("Organul principal al sistemului excretor",
 "Ce este rinichiul?;Ce este ficatul?;Ce este stomacul?;Ce este plamanul?",
 "Ce este rinichiul?",
 "Biologie", 500),

("Molecula responsabila de transmiterea informatiei genetice",
 "Ce este ADN-ul?;Ce este ARN-ul?;Ce este proteina?;Ce este lipida?",
 "Ce este ADN-ul?",
 "Biologie", 500),

("Procesul de transformare a energiei luminoase",
 "Ce este fotosinteza?;Ce este respiratia?;Ce este digestia?;Ce este osmoza?",
 "Ce este fotosinteza?",
 "Biologie", 500),

("Unitatea functionala a rinichiului",
 "Ce este nefronul?;Ce este neuronul?;Ce este alveola?;Ce este hepatocitul?",
 "Ce este nefronul?",
 "Biologie", 500),

("Procesul de eliminare a substantelor toxice",
 "Ce este excretia?;Ce este digestia?;Ce este respiratia?;Ce este fotosinteza?",
 "Ce este excretia?",
 "Biologie", 500),

("Componenta sangelui responsabila de imunitate",
 "Ce sunt leucocitele?;Ce sunt eritrocitele?;Ce sunt trombocitele?;Ce este plasma?",
 "Ce sunt leucocitele?",
 "Biologie", 500);
 
CREATE TABLE IF NOT EXISTS Scoruri (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Nume TEXT,
    Puncte INTEGER);