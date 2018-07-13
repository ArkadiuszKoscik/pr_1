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
    public partial class AutaForm : Form
    {
        ZleceniaController controller = new ZleceniaController();

        public AutaForm()
        {
            InitializeComponent();

            AutaListView.View = View.Details;
            AutaListView.GridLines = true;
            AutaListView.FullRowSelect = true;

            //Add column header
            AutaListView.Columns.Add("ID", 40);
            AutaListView.Columns.Add("Marka", 100);
            AutaListView.Columns.Add("Model", 100);
            AutaListView.Columns.Add("Rocznik", 70);
            AutaListView.Columns.Add("VIN", 100);

            this.odswiezAuta();

            ZleceniaListView.View = View.Details;
            ZleceniaListView.GridLines = true;
            ZleceniaListView.FullRowSelect = true;

            //Add column header
            ZleceniaListView.Columns.Add("ID", 40);
            ZleceniaListView.Columns.Add("Opis usterki", 100);
            ZleceniaListView.Columns.Add("Sposób naprawy", 100);
            ZleceniaListView.Columns.Add("Data", 70);
            ZleceniaListView.Columns.Add("Koszt", 40);
            ZleceniaListView.Columns.Add("Mechanik", 70);
        }

        private void odswiezAuta()
        {
            var auta = controller.pobierzWszystkieAuta();

            //Add items in the listview
            string[] arr = new string[5];
            ListViewItem itm;

            AutaListView.Items.Clear();
            //Add first item
            foreach (var auto in auta)
            {
                arr[0] = auto.id.ToString();
                arr[1] = auto.marka;
                arr[2] = auto.model;
                arr[3] = auto.rocznik.ToString();
                arr[4] = auto.vin;
                itm = new ListViewItem(arr);
                AutaListView.Items.Add(itm);
            }

            textBox1.Text = " ";
            textBox2.Text = " ";
            textBox3.Text = " ";
            textBox4.Text = " ";
            textBox5.Text = " ";
            textBox6.Text = " ";
            textBox7.Text = " ";
        }

        private void odswiezZlecenia()
        {
            try
            {
                int idAuta = Convert.ToInt32(AutaListView.FocusedItem.Text);
                Auto auto = controller.pobierzAuto(idAuta);

                ZleceniaListView.Items.Clear();
                foreach (var zlecenie in auto.zlecenia)
                {
                    //Add items in the listview
                    string[] arr = new string[6];
                    ListViewItem itm;


                    //Add first item
                    arr[0] = zlecenie.id.ToString();
                    arr[1] = zlecenie.opisUsterki;
                    arr[2] = zlecenie.opisNaprawy;
                    arr[3] = zlecenie.dataZlecenia.ToString();
                    arr[4] = zlecenie.koszt.ToString();
                    arr[5] = zlecenie.idMechanika.imie + " " + zlecenie.idMechanika.nazwisko;
                    itm = new ListViewItem(arr);
                    ZleceniaListView.Items.Add(itm);
                }
            }
            catch { }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idAuta = Convert.ToInt32(AutaListView.FocusedItem.Text);

            Auto auto = controller.pobierzAuto(idAuta);

            textBox1.Text = auto.idKlienta.imie + " " + auto.idKlienta.nazwisko;
            textBox2.Text = auto.idKlienta.telefon;
            textBox3.Text = auto.id.ToString();
            textBox4.Text = auto.marka;
            textBox5.Text = auto.model;
            textBox6.Text = auto.rocznik.ToString();
            textBox7.Text = auto.vin;

            odswiezZlecenia();
        }

        private void UsunAutoButton_Click(object sender, EventArgs e)
        {
            int idAuta;
            try
            {
                idAuta = Convert.ToInt32(AutaListView.FocusedItem.Text);
            }
            catch
            {
                MessageBox.Show("Nie zaznaczono żadnego auta", "Nie można usunąć auta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (DialogResult.No == MessageBox.Show("Czy na pewno chcesz usunąć auto o ID: " + idAuta, "Usuwanie auta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                return;
            try
            {
                controller.usunAuto(idAuta);
            }
            catch (Exception exc)
            {
                string mes = exc.Message;
                MessageBox.Show(mes, "Nie można usunąć auta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            odswiezAuta();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NoweAutoForm noweAutoOkno = new NoweAutoForm();
            noweAutoOkno.ShowDialog();
            odswiezAuta();

        }

        private void EdytujAutoButton_Click(object sender, EventArgs e)
        {
            int idAuta;
            try
            {
                idAuta = Convert.ToInt32(AutaListView.FocusedItem.Text);
            }
            catch
            {
                MessageBox.Show("Nie zaznaczono żadnego auta", "Nie można edytować auta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NoweAutoForm oknoEdycji = new NoweAutoForm(-1, idAuta);
            oknoEdycji.ShowDialog();
            controller = new ZleceniaController();
            odswiezAuta();
        }
    }
}
