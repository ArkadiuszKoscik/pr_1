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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void PokazZleceniabutton_Click(object sender, EventArgs e)
        {

        }

        private void PokazKlientowButton_Click(object sender, EventArgs e)
        {

        }

        private void PokazAutaButton_Click(object sender, EventArgs e)
        {
            AutaForm autaOkno = new AutaForm();
            autaOkno.Show();
        }

        private void PokazCzesciButton_Click(object sender, EventArgs e)
        {

        }

        private void PokazMagazynyButton_Click(object sender, EventArgs e)
        {
        }

        private void PokazMechanikowButton_Click(object sender, EventArgs e)
        {

        }

    }
}
