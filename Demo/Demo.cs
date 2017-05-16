using IndexedCollections;
using IndexedCollections.Attributes;
using IndexedCollections.DataStructures;
using IndexedCollections.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    

    public partial class Demo : Form
    {
        private IndexedDictionary<int,Person> people;
        private int idToAdd;
        private int idToFind;

        public Demo()
        {
            InitializeComponent();
            people = new IndexedDictionary<int, Person>();
            SetupPeople();
        }
        private void SetupPeople()
        {
            people.Add(BuildPerson(1,"Bolik","Lolik"));
            people.Add(BuildPerson(2, "Bolik", "Tolik"));
            people.Add(BuildPerson(3, "Krolik", "Johnson"));
            people.Add(BuildPerson(4, "Krolik", "Trump"));
        }
        private Person BuildPerson(int id,string firstName,string lastName)
        {
            Person person = new Person(id,firstName,lastName);
            return person;
        }
        private void Demo_Load(object sender, EventArgs e)
        {
            btnFetch_Click(null, null);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (idToAdd != 0)
            {
                Person newPerson = new Person(idToAdd,  txtAddFirstName.Text,txtAddLastName.Text );
                try
                {
                    people.Add(newPerson);
                    txtAddFirstName.Text = "";
                    txtAddLastName.Text = "";
                    txtAddId.Text = "0";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Id cannot be 0 or null !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFetch_Click(object sender, EventArgs e)
        {
            Person template = new Person(idToFind);
            LogicOperator lOperator = LogicOperator.OR;
            if (!string.IsNullOrWhiteSpace(txtFetchFirstName.Text))
            {
                template.FirstName = txtFetchFirstName.Text;
            }
            else
            {
                template.FirstName = null;
            }

            if (!string.IsNullOrWhiteSpace(txtFetchLastName.Text))
            {
                template.LastName = txtFetchLastName.Text;
            }
            else
            {
                template.LastName = null;
            }


            Person[] result = null;
            lOperator = LogicOperator.AND_IGNORE_NULLS;
            if (idToFind != 0)
            {
                result = people.GetByTemplate(template, lOperator, true);
            }
            else
            {
                result = people.GetByTemplate(template, lOperator, false);
            }
            
            lstResult.Items.Clear();
            lstResult.Items.AddRange(result);


        }

        private void txtAddId_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if(!int.TryParse(txtAddId.Text,out temp))
            {
                txtAddId.Text = idToAdd.ToString();
            }
            else
            {
                idToAdd = temp;
            }
        }

        private void txtFetchId_TextChanged(object sender, EventArgs e)
        {
            int temp;
            if (!int.TryParse(txtFetchId.Text, out temp))
            {
                txtFetchId.Text = idToFind.ToString();
            }
            else
            {
                idToFind = temp;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Person person = lstResult.SelectedItem as Person;
            if (person != null)
            {
                people.RemoveByKey(person.Id);
                lstResult.Items.Remove(person);
            }
            else
            {
                MessageBox.Show("Please select item to delete ...");
            }
        }
    }
}
