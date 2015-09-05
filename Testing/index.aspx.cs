using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Testing
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDataToGridView();
        }

        public void GetDataToGridView()
        {
            List<User> users = GetAllData();
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("User ID", typeof (Int32));
            dataTable.Columns.Add("User Name", typeof(string));
            dataTable.Columns.Add("Password", typeof(string));
            dataTable.Columns.Add("Full Name", typeof(string));
            dataTable.Columns.Add("Staff Code", typeof(string));
            dataTable.Columns.Add("Group ID", typeof(string));
            dataTable.Columns.Add("Location ID", typeof(string));
            dataTable.Columns.Add("Active ID", typeof(string));
            foreach (User user in users)
            {
                dataTable.Rows.Add(user.Userid, user.Username, user.Password, user.Fullname, user.Stafcode, user.GroupId,
                    user.Locationid, user.Activeid);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        public List<User> GetAllData()
        {
            string query = String.Format("Select * from UserInfo");
            List<User> users = new List<User>();


            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings[1].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(query,connection))
                {
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                       User user = new User();
                        user.Userid = Convert.ToInt32(rdr[0]);
                        user.Username = rdr[1].ToString();
                        user.Password = rdr[2].ToString();
                        user.Fullname = rdr[3].ToString();
                        user.Stafcode = rdr[4].ToString();
                        user.GroupId = rdr[5].ToString();
                        user.Locationid = rdr[6].ToString();
                        user.Activeid = rdr[6].ToString();
                        users.Add(user);

                    }
                    connection.Close();
                }
            }
            return users;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}