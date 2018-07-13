using bazadanych.Model;
using bazadanych.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bazadanych
{
    public partial class NoweAutoForm : Form
    {
        //private int idWlascicela;
        ZleceniaController controller = new ZleceniaController();
        int idAuta;

        public NoweAutoForm(int idKlienta = -1, int idAuta = -1)
        {
            InitializeComponent();
            
            //wypełnianie pól w przypadku edycji
            this.idAuta = idAuta; 
            if(idAuta >= 0)
            {
                Auto auto = controller.pobierzAuto(idAuta);
                idKlienta = auto.idKlienta.id;
                textBox2.Text = auto.marka;
                textBox3.Text = auto.model;
                textBox4.Text = auto.rocznik.ToString();
                textBox5.Text = auto.vin;
            }

            //wypełnienie listy klientów i w razie czego zaznaczenie klienta domyślnego
            IEnumerable< Klient> klienci = controller.pobierzWszystkichKlientow();
            int index = 0;

            foreach(var klient in klienci)
            {
                KlienciComboBox.Items.Add(klient.imie + " " + klient.nazwisko);
                if (klient.id == idKlienta)
                    KlienciComboBox.SelectedIndex = index;
                index++;
            }
        }

        private void ZapiszAutoButton_Click(object sender, EventArgs e)
        {
            string marka = textBox2.Text;
            string model = textBox3.Text;
            int rocznik =  Convert.ToInt32(textBox4.Text);
            string vin = textBox5.Text;

            int indeks = KlienciComboBox.SelectedIndex;
            if (indeks < 0)
            {
                MessageBox.Show("Nie zaznaczono żadnego klienta", "Nie można dodać zlecenia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var klienci = controller.pobierzWszystkichKlientow();
            int idKlienta = klienci.ElementAt(indeks).id;

            if(idAuta < 0)
                controller.dodajAuto(idKlienta, marka, model, rocznik, vin);
            else
                controller.edytujAuto(idAuta, idKlienta, marka, model, rocznik, vin);
            
            this.Close();
        }

        private void AnulujButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
