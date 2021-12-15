using System;

namespace HashtableImpl {
    class Program {
        static void Main(string[] args) {
        }
    }

    public class Element {
        /// <summary>
        /// 
        /// </summary>
        public string Id;
        public string Name;
    }


    public interface IHashtable {

        /// <summary>
        /// Element in der Hashtabelle einfügen
        /// </summary>
        /// <param name="e">einzufügendes Element</param>
        /// <returns>
        /// true: Element wurde eingefügt;
        /// false: Hashtabelle voll; Element nicht eingefügt
        /// </returns>
        bool Put(Element e);


        /// <summary>
        /// Element in der Hashtabelle suchen
        /// </summary>
        /// <param name="id">Schlüssel des zu suchenden Elementes</param>
        /// <returns>
        /// gesuchtes Element; 
        /// null-> Element nicht gefunden
        /// </returns>
        Element Get(string id);

        /// <summary>
        /// Element in der Hashtabelle löschen
        /// </summary>
        /// <param name="id">Schlüssel des zu löschenden Elementes</param>
        /// <returns>
        /// true: Element wurde gelöscht; 
        /// false: Element nicht gefunden
        /// </returns>
        bool Delete(string id);
    }

    public enum Zustand {
        Frei,
        Besetzt,
        Geloescht
    }

    public class HashTabElement {
        public Element Element { get; set; }
        public Zustand Zustand { get; set; }

        public HashTabElement() {
            Zustand = Zustand.Frei;
        }

        public HashTabElement(Element e, Zustand newZustand) {
            Zustand = newZustand;
            Element = e;
        }
    }

    public class Hashtable : IHashtable {

        /**************************************************************************
         * ATTRIBUTE
         **************************************************************************/

        private const int MAX = 29;
        private readonly HashTabElement[] tab = new HashTabElement[MAX];


        /**************************************************************************
         * PRIVATE METHODEN
         **************************************************************************/

        /**
         * Hashfunktion: berechnet aus dem Schlüssel den Primärindex
         * @param S Schlüssel
         * @return Primärindex 
         */
         
        private int Hash(string s) {
            var value = s.Length * (s[0] + s[s.Length - 1]) % MAX;
            return value;
        }

        /**
         * Element anhand des Schlüssel in der Hashtabelle suchen
         * @param id Schlüssel des ELementes
         * @return Position des Elementes in der Hashtabelle; -1: Element nicht gefunden
         */
        private int Suchen(string id) {
            var pindex = Hash(id);

            // Test ob Element an Position Primärindex
            if (this.tab[pindex].Zustand == Zustand.Besetzt && this.tab[pindex].Element.Id == id) {
                return pindex;
            }

            // Tabelle an dieser Stelle nicht gefunden -> sondieren
            var sindex = (pindex + 1) % MAX;
            while (sindex != pindex && this.tab[sindex].Zustand != Zustand.Frei) {
                if (this.tab[pindex].Element.Id == id) {
                    // Element gefunden
                    return pindex;
                }

                sindex = (sindex + 1) % MAX;
            }

            // Element nicht gefunden voll
            return -1;

        }


        /**************************************************************************
         * PUBLIC METHODEN
         **************************************************************************/

        /**
         * Defaultkonstruktor
         */
        public Hashtable() {
            for (var i = 0; i < MAX; i++) {
                tab[i] = new HashTabElement();
            }
        }

        /**
         * Element in der Hashtabelle einfügen
         * @param e einzufügendes Element
         * @return true: Element wurde eingefügt; 
         * 		   false: Hashtabelle voll; Element nicht eingefügt
         */
        public bool Put(Element e) {
            var pindex = Hash(e.Id);
            Console.WriteLine("Primärindex =" + pindex + " Schlüssel=" + e.Id);

            // Test ob Tabelle an Position index frei ist
            if (tab[pindex].Zustand == Zustand.Frei) {
                tab[pindex].Zustand = Zustand.Besetzt;
                tab[pindex].Element = e;
                return true;
            }

            // Element bereits vorhanden an Position Primärindex
            if (e.Id == this.tab[pindex].Element.Id) {
                // Element bereits in Liste vorhanden
                Console.WriteLine(" Doppelt Primär");
                return false;
            }

            // Tabelle an dieser Stelle besetzt -> sondieren
            var sindex = (pindex + 1) % MAX;
            while (sindex != pindex && this.tab[sindex].Zustand == Zustand.Besetzt && this.tab[sindex].Element.Id != e.Id) {
                sindex = (sindex + 1) % MAX;
            }

            Console.WriteLine(" Element" + e.Id);
            Console.WriteLine(" index" + sindex);
            Console.WriteLine(" Zustand" + this.tab[sindex].Zustand);

            // Element bereits vorhanden an Position Sekundärindex
            if (this.tab[sindex].Zustand == Zustand.Besetzt && e.Id == this.tab[sindex].Element.Id) {
                // Element bereits in Liste vorhanden
                Console.WriteLine(" Doppelt Sekundär");
                return false;
            }

            if (this.tab[sindex].Zustand != Zustand.Besetzt) {
                this.tab[sindex].Zustand = Zustand.Besetzt;
                this.tab[sindex].Element = e;
                return true;
            }

            // Tabelle voll
            return false;
        }

        /**
         * Element in der Hashtabelle suchen
         * @param id Schlüssel des zu suchenden Elementes
         * @return gesuchtes Element; null-> Element nicht gefunden 
         */
        public Element Get(string id) {
            var index = Suchen(id);

            if (index != -1) {
                // Element gefunden
                return tab[index].Element;
            }

            return null;
        }

        /**
         * Element in der Hashtabelle löschen
         * @param id Schlüssel des zu löschenden Elementes
         * @return true: Element wurde gelöscht; 
         * 		   false: Element nicht gefunden
         */
        public bool Delete(string id) {
            var index = Suchen(id);

            if (index != -1) {
                // Element gefunden
                tab[index].Element = null;
                tab[index].Zustand = Zustand.Geloescht;
                return true;
            }

            return false;
        }
    }
}
