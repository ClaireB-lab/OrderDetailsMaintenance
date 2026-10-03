using System;
using System.Windows.Forms;
using OrderDetailsMaintenance.Models.DataLayer;

namespace OrderDetailsMaintenance
{
    public partial class frmCustomerMaintenance : Form
    {
        private NorthwindContext _context = new NorthwindContext();
        private Customer selectedCustomer;

        //Claire Battelle
        public frmCustomerMaintenance()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string id = txtCustomerId.Text.Trim();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("Enter a customer ID", "Input Error");
                return;
            }

            selectedCustomer = _context.Customers.Find(id);

            if (selectedCustomer != null)
            {
                txtContact.Text = selectedCustomer.ContactName;
                txtAddress.Text = selectedCustomer.Address;
                txtCity.Text = selectedCustomer.City;
                txtCountry.Text = selectedCustomer.Country;
            }
            else
            {
                MessageBox.Show("Customer not found", "Not found");
                ClearTextBoxes();
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Please find a customer before saving.", "Error");
                return;
            }

            selectedCustomer.ContactName = txtContact.Text;
            selectedCustomer.Address = txtAddress.Text;
            selectedCustomer.City = txtCity.Text;
            selectedCustomer.Country = txtCountry.Text;

            try
            {
                _context.Customers.Update(selectedCustomer);
                _context.SaveChanges();
                MessageBox.Show("Customer updated successfully", "Success");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failure saving changes: {ex.Message}", "Database Error");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ClearTextBoxes()
        {
            txtContact.Clear();
            txtCustomerId.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtCountry.Clear();
        }
    }
}