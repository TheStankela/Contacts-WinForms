using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WFAppKolokvijum.Interface;
using WFAppKolokvijum.Model;
using WFAppKolokvijum.Repository;

namespace WFAppKolokvijum
{
    public partial class Form1 : Form
    {
        IContactRepository contactRepository = new ContactRepository();
        public Form1()
        {
            InitializeComponent();
            InputEnabled(false);
            RefreshList();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                txtId.Text = item.SubItems[0].Text;
                txtFirstName.Text = item.SubItems[1].Text;
                txtLastName.Text = item.SubItems[2].Text;
                txtEmail.Text = item.SubItems[3].Text;
                txtPhone.Text = item.SubItems[4].Text;

            }
            else
            {
                txtId.Text = string.Empty;
                txtFirstName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPhone.Text = string.Empty;
            }


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshList();
        }

        private void RefreshList()
        {
            listView1.Items.Clear();
            var kontakti = contactRepository.GetContacts();
            foreach (var item in kontakti)
            {
                ListViewItem l = new ListViewItem(item.Id.ToString());
                l.SubItems.Add(item.FirstName);
                l.SubItems.Add(item.LastName);
                l.SubItems.Add(item.Email);
                l.SubItems.Add(item.Phone);

                listView1.Items.Add(l);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchBox == null)
            {
                RefreshList();
                MessageBox.Show("Search polje je prazno.");
                
            }
            else
            {
                listView1.Items.Clear();
                var name = txtSearchBox.Text.ToString();
                var contacts = contactRepository.GetContact(name);
                foreach (var item in contacts)
                {
                    ListViewItem l = new ListViewItem(item.Id.ToString());
                    l.SubItems.Add(item.FirstName);
                    l.SubItems.Add(item.LastName);
                    l.SubItems.Add(item.Email);
                    l.SubItems.Add(item.Phone);

                    listView1.Items.Add(l);
                }
            }
            
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            InputEnabled(true);
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            txtId.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();   
        }

        private void InputEnabled(bool value)
        {
            txtFirstName.Enabled = value;
            txtLastName.Enabled = value;
            txtEmail.Enabled = value;
            txtPhone.Enabled = value;
        }

        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            var kontakti = contactRepository.GetContacts();
            var kontakt = new Contact();
            kontakt.FirstName = txtFirstName.Text;
            kontakt.LastName = txtLastName.Text;
            kontakt.Email = txtEmail.Text;
            kontakt.Phone = txtPhone.Text;


            if (!kontakti.Any(c => c.Email == kontakt.Email))
            {

                contactRepository.AddContact(kontakt);
                RefreshList();
                InputEnabled(false);
                MessageBox.Show("Kontakt napravljen.");
            }
            else
            {
                kontakt.Id = int.Parse(txtId.Text);
                contactRepository.UpdateContact(kontakt);
                RefreshList();
                InputEnabled(false);
                MessageBox.Show("Kontakt izmenjen.");
            }
            

        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            InputEnabled(true);
        }
        private void btnObrisi_Click(object sender, EventArgs e)
        {
          
            if (txtId.Text == null)
            {
                MessageBox.Show("Morate selektovati kontakt koji zelite da obrisete!");
            }
            else
            {
                contactRepository.DeleteContact(int.Parse(txtId.Text));
                MessageBox.Show("Uspesno obrisan kontakt.");
            }
            RefreshList();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var odgovor = MessageBox.Show("Zelite li da izadjete?", "Application closing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (odgovor == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else 
            {
                e.Cancel = true;
            }
            
            
        }

    }
}