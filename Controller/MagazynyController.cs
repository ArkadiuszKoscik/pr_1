using bazadanych.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bazadanych.Controller
{
    class MagazynyController : BaseController
    {
        //magazyny

        public void dodajMagazyn(string adres, string nazwa)
        {
            var nowyMagazyn = new Magazyn()
            {
                adres = adres,
                nazwa = nazwa
            };
            db.Magazyny.Add(nowyMagazyn);
            db.SaveChanges();
        }

        public void edytujMagazyn(int id, string adres, string nazwa)
        {
            var magazyn = pobierzMagazyn(id);
            magazyn.nazwa = nazwa;
            magazyn.adres = adres;
            db.SaveChanges();
        }

        public IEnumerable<Magazyn> pobierzWszystkieMagazyny()
        {
            return db.Magazyny.ToList();
        }

        public Magazyn pobierzMagazyn(int id)
        {
            return db.Magazyny.SingleOrDefault(magazyn => magazyn.id == id);
        }
        
        public void usunMagazyn(int id)
        {
            var usuwanyMagazyn = db.Magazyny.SingleOrDefault(magazyn => magazyn.id == id);

            while (usuwanyMagazyn.czesci.Count() > 0)
            {
                var usuwanaCzesc = usuwanyMagazyn.czesci.First();
                db.DostepneCzesci.Remove(usuwanaCzesc);
            }
            db.Magazyny.Remove(usuwanyMagazyn);
            db.SaveChanges();
        }

        //części

        public IEnumerable<Czesc> pobierzWszystkieCzesci()
        {
            return db.Czesci.ToList();
        }

        public Czesc pobierzCzesc(int id)
        {
            return db.Czesci.SingleOrDefault(czesc => czesc.id == id);
        }

        public void dodajCzescDoMagazynu(int idCzesci, int idMagazynu, int ilosc)
        {
            var czesc = pobierzCzesc(idCzesci);
            var magazyn = pobierzMagazyn(idMagazynu);

            var dostepneCzesci = db.DostepneCzesci.ToList();

            var numQuery =
                from rekord in dostepneCzesci
                where rekord.idMagazynu.id == idMagazynu && rekord.idCzesci.id == idCzesci
                select rekord;

            if(numQuery.Count() == 0)
            {
                var nowaCzesc = new DostepneCzesci()
                {
                    idCzesci = czesc,
                    idMagazynu = magazyn,
                    ilosc = ilosc
                };
                db.DostepneCzesci.Add(nowaCzesc);
            }
            else
            {
                var edytowanaCzesc = numQuery.First();

                edytowanaCzesc.ilosc += ilosc;
            }
            db.SaveChanges();
        }

        public void edyujCzescWMagazynie(int idRekordu, int ilosc)
        {
            var edytowanaCzesc = db.DostepneCzesci.Single(czesc => czesc.id == idRekordu);
            edytowanaCzesc.ilosc = ilosc;
            db.SaveChanges();
        }

        public void usunCzesciZMagazynu(int idCzesci, int idMagazynu)
        {
            var usuwanaCzesc = db.DostepneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == idCzesci && czesc.idMagazynu.id == idMagazynu);
            db.DostepneCzesci.Remove(usuwanaCzesc);
            db.SaveChanges();
        }

        public void dodajCzesc(string nazwa, string opis, decimal cena)
        {
            var nowaCzesc = new Czesc()
            {
                nazwa = nazwa,
                opis = opis,
                cenaCzesci = cena
            };
            db.Czesci.Add(nowaCzesc);
            db.SaveChanges();
        }

        public void edytujCzesc(int id, string nazwa, string opis, decimal cena)
        {
            var czesc = pobierzCzesc(id);
            czesc.nazwa = nazwa;
            czesc.opis = opis;
            czesc.cenaCzesci = cena;
            db.SaveChanges();
        }

        public int pobierzIlosc(int id)
        {
            Czesc czesc = pobierzCzesc(id);
            int ilosc = 0;

            foreach (var mag in czesc.gdzieDostepne)
            {
                ilosc += mag.ilosc;
            }
            return ilosc;
        }

        //public void dodajCzescDoZlecenia(int idCzesci, int idZlecenia, int iloscCzesci)
        //{
        //    //sprawdzenie czy w ktorymkolwiek magazynie znajduje sie wystarczajaca ilosc czesci
        //    //if (db.Magazyny.Any(magazyn => magazyn.czesci.Where(czesc => czesc.id == idCzesci).Count() >= iloscCzesci && ))
        //    if (db.DostepneCzesci.Any(czesc => czesc.idCzesci.id == idCzesci && czesc.ilosc >= iloscCzesci))
        //    {
        //        //pobranie magazynu - mozna dodac metoda pobierajaca magazyn gdzie jest taka i taka ilosc czesci o takim i takim identyfkatorze.
        //        //var oMagazyn = db.Magazyny.Single(magazyn => magazyn.czesci.Where(czesc => czesc.id == idCzesci).Count() >= iloscCzesci);
        //        //pobranie interesujacej nas czesci
        //        //var oCzesc = oMagazyn.czesci.SingleOrDefault(czesc => czesc.id == idCzesci);

        //        var oCzesc = db.DostepneCzesci.SingleOrDefault(czesc => czesc.idCzesci.id == idCzesci && czesc.ilosc >= iloscCzesci);

        //        var oMagazyn = oCzesc.idMagazynu;

        //        //pobranie zlecenia
        //        var oZlecenie = db.Zlecenia.SingleOrDefault(zlecenie => zlecenie.id == id);
        //        //sprawdzenie czy zlecenie sie znajduje w bazie (gdyby ktos chcial hackowac i podal zly id) i w razie czego zglaszamy wyjatek
        //        if (oZlecenie == null)
        //            zglosBlad("Brak zlecenia o identyfikatorze {0}", idZlecenia);

        //        //sprawdzenie czy ta część już jest na zleceniu
        //        var potrzebneCzesci = db.PotrzebneCzesci.ToList();
        //        var numQuery =
        //            from rekord in potrzebneCzesci
        //            where rekord.idZlecenia.id == idZlecenia && rekord.idCzesci.id == idCzesci
        //            select rekord;

        //        //dodanie do zlecenia ilosc czesci do naprawy
        //        if (numQuery.Count() == 0)
        //        {
        //            oZlecenie.czesciDoNaprawy.Add(new PotrzebnaCzesc() { idCzesci = oCzesc.idCzesci, idZlecenia = oZlecenie, ilosc = iloscCzesci });
        //        }
        //        else
        //        {
        //            var edytowanaCzesc = numQuery.First();

        //            edytowanaCzesc.ilosc += iloscCzesci;
        //        }
        //        //odebranie ilosci czesci
        //        oCzesc.ilosc -= iloscCzesci;
        //        if (oCzesc.ilosc == 0)
        //        {
        //            db.DostepneCzesci.Remove(oCzesc);
        //        }
        //        //zapisanie zmian
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        zglosBlad("Żaden z magazynów nie ma potrzebnej ilości części");
        //    }
        //}

        public DostepneCzesci pobierzDostepnoscCzesci(int idCzesci, int idMagazynu)
        {
            return db.DostepneCzesci.SingleOrDefault(dCzesc => dCzesc.idCzesci.id == idCzesci && dCzesc.idMagazynu.id == idMagazynu);
        }

        public void usunCzesc(int id)
        {
            var usuwanaCzesc = db.Czesci.SingleOrDefault(czesc => czesc.id == id);
            if (usuwanaCzesc.gdziePotrzebne.Count() > 0)
            {
                zglosBlad("Czesc figuruje na przynajmniej jednym zleceniu");
                return;
            }
            //foreach(var dost in usuwanaCzesc.gdzieDostepne)
            while (usuwanaCzesc.gdzieDostepne.Count() > 0)
            {
                db.DostepneCzesci.Remove(usuwanaCzesc.gdzieDostepne.First());
            }
            db.Czesci.Remove(usuwanaCzesc);
            db.SaveChanges();
        }
    }
}
