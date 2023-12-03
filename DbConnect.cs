using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Shop_Management_System
{
    internal class DbConnect
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cd = new SqlCommand();
        private string con;

        public string Connection() 
        {
            con = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Programs\CsharpProjects\repos\Shop Management System\dbHikeShop.mdf;Integrated Security=True; Connect Timeout = 30";
            return con;
        }
    }
}
