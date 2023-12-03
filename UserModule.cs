using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Shop_Management_System
{
    public partial class UserModule : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        string title = "Hike Shop Management System";

        bool check = false;

        UserForm userForm;
        public UserModule(UserForm user)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
            userForm = user;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CheckField();
                if (check)
                {
                    if (MessageBox.Show("Are you sure you want to register this user?", "User registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cm = new SqlCommand("INSERT INTO tbUser(name,address,phone,role,dob,password)VALUES(@name,@address,@phone,@role,@dob,@password)", cn);
                        cm.Parameters.AddWithValue("@name", txtName.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.Parameters.AddWithValue("@phone", txtPhone.Text);
                        cm.Parameters.AddWithValue("@role", cbRole.Text);
                        cm.Parameters.AddWithValue("@dob", dtDob.Value);
                        cm.Parameters.AddWithValue("@password", txtPassword.Text);

                        cn.Open();
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("User has been successfully registred!", title);
                        Clear();
                        userForm.LoadUser();
                    }
                }
                
            }
            catch (Exception ex)
            { 
                MessageBox.Show(ex.Message, title);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRole.Text == "Employee")
            {
                this.Height = 490 - 52;
                lblPassword.Visible = false;
                txtPassword.Visible = false;
            }
            else
            {
                this.Height = 490;
                lblPassword.Visible = true;
                txtPassword.Visible = true;
            }
        }
        #region Methods

        public void Clear()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            cbRole.SelectedIndex = 0;
            txtPassword.Clear();
            dtDob.Value = DateTime.Now;
        }

        public void CheckField()
        {
            if (txtName.Text == "" | txtAddress.Text == "") 
            {
                MessageBox.Show("Required data field", "Warning");
                return;
            }

            if (CheckAge(dtDob.Value) < 18)
            {
                MessageBox.Show("User is child worker! Under 18 year", "Warning");
                return;
            }
            check = true;
        }

        //to calculate age under 18
        public static int CheckAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;
            return age;
        }
        #endregion Methods
    }
}
