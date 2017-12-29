using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact_Form_Test_WF
{
    public partial class ContactForm : Form
    {

        string cleanName;
        string cleanLastName;
        string cleanPhone;
        string cleanFind;
        Contact contact;

        public ContactForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtName.Focus();

            string count = DataAccess.GetCount();
            txtOutput.Text = $"Contact Count: {count}";

        }


        //private bool IsTextboxValid(string textboxInput)
        //{
        //    if (textboxInput.Length > 0)
        //        return true;
        //    else
        //        return false;
        //}

        private bool IsTextboxValid(TextBox textbox)
        {
            if (textbox.Text.Length > 0)
            {                
                return true;
            }
            else
            {               
                return false;
            }
        }


        private bool IsPhoneValid(TextBox textbox)
        {
            try
            {
                

                string phoneToCheck = textbox.Text.Trim();

                if (string.IsNullOrEmpty(phoneToCheck))
                    return false;

                
                    var r = new Regex(@"^\(?([0-9]{3})\)?[-.●]?([0-9]{3})[-.●]?([0-9]{4})$");

                    return r.IsMatch(phoneToCheck);
                
               

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool IsFormValid()
        {
            bool isValid = true;

            if (!IsTextboxValid(txtName))
                ErrorIndicator(false, txtName);

            if (!IsTextboxValid(txtLastName))
                ErrorIndicator(false, txtLastName);

            if (!IsPhoneValid(txtPhone))
                ErrorIndicator(false, txtPhone);

            return isValid;           
        }

        private void ErrorIndicator(bool validationRule, Control textbox)
        {
            if (validationRule)
            {
                textbox.BackColor = System.Drawing.SystemColors.Window;
            }
            else
            {
                textbox.BackColor = Color.LightPink;
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            //remove unwanted whitespace / punctuation
            cleanName = CapitalizeString(txtName.Text.Trim().Replace(" ", ""));
            cleanLastName = CapitalizeString(txtLastName.Text.Trim().Replace(" ", ""));
            cleanPhone = Regex.Replace(txtPhone.Text, @"[^0-9.]", "");


            if (IsFormValid())
            {
                DataAccess.AddContact(cleanName, cleanLastName, cleanPhone);
                string count = DataAccess.GetCount();

                txtOutput.Text = "Contact Successfully Added!\n";
                txtOutput.Text += $"Contact Count: {count}";

                RestoreTextboxColor();
                ClearAllTextboxes();
                ClearAllLabels();
                txtName.Focus();
            }
            //else
            //{
            //    ErrorIndicator(IsTextboxValid(cleanName), txtName);
            //    ErrorIndicator(IsTextboxValid(cleanLastName), txtLastName);
            //    ErrorIndicator(IsPhoneValid(cleanPhone), txtPhone);
            //}
        }


        private void RestoreTextboxColor()
        {
            txtName.BackColor = System.Drawing.SystemColors.Window;
            txtLastName.BackColor = System.Drawing.SystemColors.Window;
            txtPhone.BackColor = System.Drawing.SystemColors.Window;
        }


        //private void txtFind_TextChanged(object sender, EventArgs e)
        //{
        //    string tempCleanFind = CapitalizeString(txtFind.Text.Trim().Replace(" ", ""));
        //    cleanFind = Regex.Replace(tempCleanFind, @"[^\w\s\d]", "");

        //    if (cmbFind.Text == "Name")
        //    {
        //        //FindContactByFirstName();
        //        Contact contact = DataAccess.FindByFirstName(cleanFind);

        //        if(contact != null)
        //        {
        //            txtOutput.Text = "Contact Found!";
        //            txtName.Text = contact.FirstName.ToString();
        //            txtLastName.Text = contact.LastName.ToString();
        //            txtPhone.Text = contact.PhoneNumber.ToString();

        //            RestoreTextboxColor();
        //        }
        //        else
        //        {
        //            txtOutput.Text = "Contact does not exist.";
        //        }

        //    }
        //    else if (cmbFind.Text == "Last Name")
        //    {
        //        //FindContactByLastName();
        //        Contact contact = DataAccess.FindByLastName(cleanFind);

        //        if (contact != null)
        //        {
        //            txtOutput.Text = "Contact Found!";
        //            txtName.Text = contact.FirstName.ToString();
        //            txtLastName.Text = contact.LastName.ToString();
        //            txtPhone.Text = contact.PhoneNumber.ToString();

        //            RestoreTextboxColor();
        //        }
        //        else
        //        {
        //            txtOutput.Text = "Contact does not exist.";
        //        }

        //    }
        //    else if (cmbFind.Text == "Phone Number")
        //    {
        //        //FindContactByPhoneNumber();
        //        Contact contact = DataAccess.FindByPhoneNumber(cleanFind);

        //        if (contact != null)
        //        {
        //            txtOutput.Text = "Contact Found!";
        //            txtName.Text = contact.FirstName.ToString();
        //            txtLastName.Text = contact.LastName.ToString();
        //            txtPhone.Text = contact.PhoneNumber.ToString();

        //            RestoreTextboxColor();
        //        }
        //        else
        //        {
        //            txtOutput.Text = "Contact does not exist.";
        //        }
        //    }
        //}


        private void btnListAll_Click(object sender, EventArgs e)
        {
            txtOutput.Text = "";
            ////sorts list alphabetically
            //var sortedList = contactList.OrderBy(x => x.FirstName).ToList();
            List<Contact> contacts = DataAccess.GetAllContacts();

            if (contacts != null)
            {
                foreach (Contact c in contacts)
                {
                    txtOutput.Text += $"{c.FirstName} {c.LastName} {c.PhoneNumber}\n";
                }
            }
            else
            {
                txtOutput.Text = "There are 0 contacts";
            }
        }
       
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //if(cmbFind.Text == "Name")
            //{
            //    DataAccess.DeleteByFirstName(cleanFind);
            //    txtOutput.Text = "Contact Deleted!";
            //    ClearAllTextboxes();
            //}
            //else if(cmbFind.Text == "Last Name")
            //{
            //    DataAccess.DeleteByLastName(cleanFind);
            //    txtOutput.Text = "Contact Deleted!";
            //    ClearAllTextboxes();
            //}
            //else if(cmbFind.Text == "Phone Number")
            //{
            //    DataAccess.DeleteByPhoneNumber(cleanFind);
            //    txtOutput.Text = "Contact Deleted!";
            //    ClearAllTextboxes();
            //}

            if (contact != null)
            {            

                DataAccess.DeleteContact(contact);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearAllTextboxes();
            ClearAllLabels();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        string CapitalizeString(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            return Char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }

        void ClearAllTextboxes()
        {
            txtName.Text = "";
            txtLastName.Text = "";
            txtPhone.Text = "";
            txtFind.Text = "";
        }

        void ClearAllLabels()
        {
            lblNameRequired.Text = "";
            lblLNameRequired.Text = "";
            lblPhoneError.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string tempCleanFind = CapitalizeString(txtFind.Text.Trim().Replace(" ", ""));
            cleanFind = Regex.Replace(tempCleanFind, @"[^\w\s\d]", "");

            if (cmbFind.Text == "Name")
            {
                //FindContactByFirstName();
                contact = DataAccess.FindByFirstName(cleanFind);

                if (contact != null)
                {
                    txtOutput.Text = "Contact Found!";
                    txtName.Text = contact.FirstName.ToString();
                    txtLastName.Text = contact.LastName.ToString();
                    txtPhone.Text = contact.PhoneNumber.ToString();

                    RestoreTextboxColor();
                }
                else
                {
                    txtOutput.Text = "Contact does not exist.";
                }

            }
            else if (cmbFind.Text == "Last Name")
            {
                //FindContactByLastName();
                Contact contact = DataAccess.FindByLastName(cleanFind);

                if (contact != null)
                {
                    txtOutput.Text = "Contact Found!";
                    txtName.Text = contact.FirstName.ToString();
                    txtLastName.Text = contact.LastName.ToString();
                    txtPhone.Text = contact.PhoneNumber.ToString();

                    RestoreTextboxColor();
                }
                else
                {
                    txtOutput.Text = "Contact does not exist.";
                }

            }
            else if (cmbFind.Text == "Phone Number")
            {
                //FindContactByPhoneNumber();
                Contact contact = DataAccess.FindByPhoneNumber(cleanFind);

                if (contact != null)
                {
                    txtOutput.Text = "Contact Found!";
                    txtName.Text = contact.FirstName.ToString();
                    txtLastName.Text = contact.LastName.ToString();
                    txtPhone.Text = contact.PhoneNumber.ToString();

                    RestoreTextboxColor();
                }
                else
                {
                    txtOutput.Text = "Contact does not exist.";
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(contact != null)
            {
                contact.FirstName = txtName.Text.Trim();
                contact.LastName = txtLastName.Text.Trim();
                contact.PhoneNumber = txtPhone.Text.Trim();

                DataAccess.UpdateContact(contact);
            }
        }
    }
}
