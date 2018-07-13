using bazadanych.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Controller
{
    public class ZleceniaController : BaseController
    {
        //klienci

        public void dodajKlienta(string imie, string nazwisko, string telefon)
        {
            var nowyKlient = new Klient()
            {
                imie = imie,
                nazwisko = nazwisko,
                telefon = telefon
            };
            db.Klienci.Add(nowyKlient);
            db.SaveChanges();
        }

        public void edytujKlienta(int id, string imie, string nazwisko, string telefon)
        {
            var edytowanyKlient = pobierzKlienta(id);
            edytowanyKlient.imie = imie;
            edytowanyKlient.nazwisko = nazwisko;
            edytowanyKlient.telefon = telefon;
            db.SaveChanges();
        }

        public void usunKlienta(int id)
        {
            if (db.Auta.Any(auto => auto.idKlienta.id == id && auto.zlecenia.Count() > 0))
            {
                zglosBlad("Klient figuruje na przynajmniej jednym zleceniu");
                return;
            }
            var usuwanyKlient = db.Klienci.SingleOrDefault(klient => klient.id == id);

            while (usuwanyKlient.pojazdy.Count > 0)
            {
                var usuwaneAuto = usuwanyKlient.pojazdy.First();
                db.Auta.Remove(usuwaneAuto);
            }
            db.Klienci.Remove(usuwanyKlient);
            db.SaveChanges();
        }

        public IEnumerable<Klient> pobierzWszystkichKlientow()
        {
            return db.Klienci.ToList();
        }

        public Klient pobierzKlienta(int id)
        {
            return db.Klienci.SingleOrDefault(zlecene => zlecene.id == id);
        }

        //auta

        public IEnumerable<Auto> pobierzAutaKlienta(int idKlienta)
        {
            return db.Auta.Where(auto => auto.idKlienta.id == idKlienta);
        }

        public IEnumerable<Auto> pobierzWszystkieAuta()
        {
            return db.Auta.ToList();
        }

        public Auto pobierzAuto(int idAuta)
        {
            return db.Auta.SingleOrDefault(auto => auto.id == idAuta);
        }

        public void dodajAuto(int idKlienta, string marka, string model, int rocznik, string vin)
        {
            Klient klient = pobierzKlienta(idKlienta);

            var noweAuto = new Auto()
            {
                idKlienta = klient,
                marka = marka,
                model = model,
                rocznik = rocznik,
                vin = vin,
            };
            //db.Auta.Add(noweAuto);
            klient.pojazdy.Add(noweAuto);
            db.SaveChanges();
        }

        public void edytujAuto(int id, int idKlienta, string marka, string model, int rocznik, string vin)
        {
            Klient klient = pobierzKlienta(idKlienta);

            var auto = pobierzAuto(id);
            auto.idKlienta = klient;
            auto.marka = marka;
            auto.model = model;
            auto.rocznik = rocznik;
            auto.vin = vin;
            db.SaveChanges();
        }

        public void usunAuto(int id)
        {
            var usuwaneAuto = db.Auta.SingleOrDefault(auto => auto.id == id);
            if (usuwaneAuto.zlecenia.Count() > 0)
            {
                zglosBlad("Auto figuruje na przynajmniej jednym zleceniu");
                return;
            }
            db.Auta.Remove(usuwaneAuto);
            db.SaveChanges();
        }

        //mechanicy

        public void usunMechanika(int id)
        {
            var usuwanyMechanik = db.Mechanicy.SingleOrDefault(mechanik => mechanik.id == id);
            if (usuwanyMechanik.zlecenia.Count() > 0)
            {
                zglosBlad("Mechanik figuruje na przynajmniej jednym zleceniu");
                return;
            }
            db.Mechanicy.Remove(usuwanyMechanik);
            db.SaveChanges();
        }

        public IEnumerable<Mechanik> pobierzWszystkichMechanikow()
        {
            return db.Mechanicy.ToList();
        }

        public Mechanik pobierzMechanika(int id)
        {
            return db.Mechanicy.SingleOrDefault(mechanik => mechanik.id == id);
        }

        public void dodajMechanika(string imie, string nazwisko, decimal pensja)
        {
            var nowyMechanik = new Mechanik()
            {
                imie = imie,
                nazwisko = nazwisko,
                pensja = pensja
            };
            db.Mechanicy.Add(nowyMechanik);
            db.SaveChanges();
        }

        public void edytujMechanika(int id, string imie, string nazwisko, decimal pensja)
        {
            var mechanik = pobierzMechanika(id);
            mechanik.imie = imie;
            mechanik.nazwisko = nazwisko;
            mechanik.pensja = pensja;
            db.SaveChanges();
        }

        //zlecenia i części
        
        public IEnumerable<Zlecenie> pobierzWszystkieZlecenia()
        {
            return db.Zlecenia.ToList();
        }

        public Zlecenie pobierzZlecenie(int id)
        {
            return db.Zlecenia.SingleOrDefault(zlecene => zlecene.id == id);
        }

        public void dodajZlecenie(int idAuta, int idMechanika, string opisUsterki, string opisNaprawy, decimal koszt)
        {
            //dodawanie zlecenia dla mechanika
            var mechanik = this.pobierzMechanika(idMechanika);
            //var klient = this.pobierzKlienta(idKlienta);
            var auto = this.pobierzAuto(idAuta);
            if (mechanik == null)
                zglosBlad("Mechanik o identyfikatorze {0} nie istnieje", idMechanika);
            var zlecenie = new Zlecenie()
            {
                dataZlecenia = DateTime.Now,
                opisUsterki = opisUsterki,
                idAuta = auto,
                idMechanika = mechanik,
                opisNaprawy = opisNaprawy,
                koszt = koszt

            };
            db.Zlecenia.Add(zlecenie);
            //mechanik.zlecenia.Add(zlecenie);
            db.SaveChanges();
        }

        public void edytujZlecenie(int idZlecenia, int idAuta, int idMechanika, string opisUsterki, string opisNaprawy, decimal koszt)
        {
            //dodawanie zlecenia dla mechanika
            var mechanik = this.pobierzMechanika(idMechanika);
            //var klient = this.pobierzKlienta(idKlienta);
            var auto = this.pobierzAuto(idAuta);
            if (mechanik == null)
                zglosBlad("Mechanik o identyfikatorze {0} nie istnieje", idMechanika);
            var zlecenie = pobierzZlecenie(idZlecenia);
            zlecenie.opisUsterki = opisUsterki;
            zlecenie.idAuta = auto;
            zlecenie.idMechanika = mechanik;
            zlecenie.opisNaprawy = opisNaprawy;
            zlecenie.koszt = koszt;
            db.SaveChanges();
        }

        public void dodajCzescDoZlecenia(int idCzesci, int idZlecenia, int iloscCzesci)
        {
            //sprawdzenie czy w ktorymkolwiek magazynie znajduje sie wystarczajaca ilosc czesci
            //if (db.Magazyny.Any(magazyn => magazyn.czesci.Where(czesc => czesc.id == idCzesci).Count() >= iloscCzesci && ))
            if (db.DostepneCzesci.Any(czesc => czesc.idCzesci.id == idCzesci && czesc.ilosc >= iloscCzesci))
            {
                //pobranie magazynu - mozna dodac metoda pobierajaca magazyn gdzie jest taka i taka ilosc czesci o takim i takim identyfkatorze.
                //var oMagazyn = db.Magazyny.Single(magazyn => magazyn.czesci.Where(czesc => czesc.id == idCzesci).Count() >= iloscCzesci);
                //pobranie interesujacej nas czesci
                //var oCzesc = oMagazyn.czesci.SingleOrDefault(czesc => czesc.id == idCzesci);

                var oCzesc = db.DostepneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == idCzesci && czesc.ilosc >= iloscCzesci);

                var oMagazyn = oCzesc.idMagazynu;

                //pobranie zlecenia
                var oZlecenie = db.Zlecenia.SingleOrDefault(zlecenie => zlecenie.id == idZlecenia);
                //sprawdzenie czy zlecenie sie znajduje w bazie (gdyby ktos chcial hackowac i podal zly id) i w razie czego zglaszamy wyjatek
                if (oZlecenie == null)
                    zglosBlad("Brak zlecenia o identyfikatorze {0}", idZlecenia);

                //sprawdzenie czy ta część już jest na zleceniu
                var potrzebneCzesci = db.PotrzebneCzesci.ToList();
                var numQuery =
                    from rekord in potrzebneCzesci
                    where rekord.idZlecenia.id == idZlecenia && rekord.idCzesci.id == idCzesci
                    select rekord;

                //dodanie do zlecenia ilosc czesci do naprawy
                if (numQuery.Count() == 0)
                {
                    oZlecenie.czesciDoNaprawy.Add(new PotrzebnaCzesc() { idCzesci = oCzesc.idCzesci, idZlecenia = oZlecenie, ilosc = iloscCzesci });
                }
                else
                {
                    var edytowanaCzesc = numQuery.First();

                    edytowanaCzesc.ilosc += iloscCzesci;
                }
                //odebranie ilosci czesci
                oCzesc.ilosc -= iloscCzesci;
                if (oCzesc.ilosc == 0)
                {
                    db.DostepneCzesci.Remove(oCzesc);
                }
                //zapisanie zmian
                db.SaveChanges();
            }
            else
            {
                zglosBlad("Żaden z magazynów nie ma potrzebnej ilości części");
            }
        }

        public void edytujCzesciNaZleceniu(int idCzesciNaZleceniu, int iloscCzesci)
        {
            var pCzesc = db.PotrzebneCzesci.SingleOrDefault(czesc => czesc.id == idCzesciNaZleceniu);
            int roznica = iloscCzesci - pCzesc.ilosc;
            if (roznica > 0)
            {
                //sprawdzenie czy w ktorymkolwiek magazynie znajduje sie wystarczajaca ilosc czesci
                if (db.DostepneCzesci.Any(czesc => czesc.idCzesci.id == pCzesc.idCzesci.id && czesc.ilosc >= roznica))
                {
                    //pobranie magazynu - mozna dodac metoda pobierajaca magazyn gdzie jest taka i taka ilosc czesci o takim i takim identyfkatorze.
                    //var oMagazyn = db.Magazyny.Single(magazyn => magazyn.czesci.Where(czesc => czesc.id == idCzesci).Count() >= iloscCzesci);
                    //pobranie interesujacej nas czesci
                    //var oCzesc = oMagazyn.czesci.SingleOrDefault(czesc => czesc.id == idCzesci);

                    var dCzesc = db.DostepneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == pCzesc.idCzesci.id && czesc.ilosc >= iloscCzesci);

                    var magazyn = dCzesc.idMagazynu;

                    ////pobranie zlecenia
                    //var oZlecenie = db.Zlecenia.SingleOrDefault(zlecenie => zlecenie.id == idZlecenia);
                    ////sprawdzenie czy zlecenie sie znajduje w bazie (gdyby ktos chcial hackowac i podal zly id) i w razie czego zglaszamy wyjatek
                    //if (oZlecenie == null)
                    //    zglosBlad("Brak zlecenia o identyfikatorze {0}", idZlecenia);

                    ////sprawdzenie czy ta część już jest na zleceniu
                    //var potrzebneCzesci = db.PotrzebneCzesci.ToList();
                    //var numQuery =
                    //    from rekord in potrzebneCzesci
                    //    where rekord.idZlecenia.id == idZlecenia && rekord.idCzesci.id == idCzesci
                    //    select rekord;

                    ////dodanie do zlecenia ilosc czesci do naprawy
                    //if (numQuery.Count() == 0)
                    //{
                    //    oZlecenie.czesciDoNaprawy.Add(new PotrzebnaCzesc() { idCzesci = oCzesc.idCzesci, idZlecenia = oZlecenie, ilosc = iloscCzesci });
                    //}
                    //else
                    //{
                    //    var edytowanaCzesc = numQuery.First();
                    //    edytowanaCzesc.ilosc += roznica;
                    //}

                    pCzesc.ilosc += roznica;
                    //odebranie ilosci czesci
                    dCzesc.ilosc -= roznica;
                    if (dCzesc.ilosc == 0)
                    {
                        db.DostepneCzesci.Remove(dCzesc);
                    }
                }
                else
                {
                    zglosBlad("Żaden z magazynów nie ma potrzebnej ilości części");
                    return;
                }
            }
            else
            {
                pCzesc.ilosc = iloscCzesci;
            }
            db.SaveChanges();
        }

        public void usunZlecenie(int id)
        {
            var usuwaneZlecenie = db.Zlecenia.SingleOrDefault(zlecenie => zlecenie.id == id);
            while (usuwaneZlecenie.czesciDoNaprawy.Count > 0)
            {
                var usuwaneCzesci = usuwaneZlecenie.czesciDoNaprawy.First();
                db.PotrzebneCzesci.Remove(usuwaneCzesci);
            }
            db.Zlecenia.Remove(usuwaneZlecenie);
            db.SaveChanges();
        }

        public PotrzebnaCzesc pobierzCzesciZeZlecenia(int idCzesci, int idZlecenia)
        {
            return db.PotrzebneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == idCzesci && czesc.idZlecenia.id == idZlecenia);
        }

        public void usunCzesciZeZlecenia(int idCzesci, int idZlecenia)
        {
            //var usuwanaCzesc = db.PotrzebneCzesci.First(czesc => czesc.idCzesci.id == idCzesci && czesc.idZlecenia.id == idZlecenia);
            var usuwanaCzesc = db.PotrzebneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == idCzesci && czesc.idZlecenia.id == idZlecenia);
            db.PotrzebneCzesci.Remove(usuwanaCzesc);
            db.SaveChanges();
        }

        public IEnumerable<Czesc> pobierzWszystkieCzesci()
        {
            return db.Czesci.ToList();
        }
    }
}
