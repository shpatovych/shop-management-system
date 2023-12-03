using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shop_Management_System
{
    public partial class UserForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DbConnect dbcon = new DbConnect();
        SqlDataReader dr;
        string title = "Hike Shop Management System"; 
        public UserForm()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.Connection());
            LoadUser();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            UserModule module = new UserModule(this);
            module.ShowDialog();
        }
        #region Method
        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cm = new SqlCommand("SELECT * FROM tbUser WHERE CONCAT(name,address,phone,dob,role) LIKE '%" + txtSearch.Text + "%'", cn);
            //cm = new SqlCommand("SELECT * FROM tbUser WHERE CONCAT(name,address,phone,dob,role) LIKE @searchPattern", cn);
            //cm.Parameters.AddWithValue("@searchPattern", "%" + txtSearch.Text + "%");

            cn.Open();
            dr = cm.ExecuteReader();
            while (dr.Read()) 
            { 
                i++;
                dgvUser.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
            }
            dr.Close();
            cn.Close();
        }

        #endregion Method
    }
}
