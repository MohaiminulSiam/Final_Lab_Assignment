using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Lab_Assignment;
using Final_Lab_Assignment.Classes;

namespace Final_Lab_Assignment.Classes
{
    public class UserRepo
    {
        internal static User GetUserDetail(string email , string password )
        {
            User user = new User();
            try
            {
                var query = @"SELECT * FROM users where email = '" + email + "' and password = '" + password + "';";
                var dt = DataAccess.GetDataTable(query);
                int index = 0;
                while (index < dt.Rows.Count)
                {
                    user = ConvertToEntity(dt.Rows[index]);
                    index++;
                }
            }
            catch
            {
                MessageBox.Show("Data Not Loaded \n Data :: NULL");
                return null;
            }
            return user;
        }

        private static User ConvertToEntity(DataRow dataRow)
        {
            var user = new User();
            user.user_id = Int32.Parse(dataRow["user_id"].ToString());
            user.name = dataRow["name"].ToString();
            user.email = dataRow["email"].ToString();
            user.join_date = DateTime.Parse(dataRow["join_date"].ToString());
            user.password = dataRow["password"].ToString();
            user.phone = dataRow["phone"].ToString();
            user.address = dataRow["address"].ToString();
            user.image = dataRow["image"].ToString();

            return user;
        }

        internal static bool UpdateUser(User user)
        {
            bool inserted = false;
            try
            {
                string updateQuery = @"UPDATE [dbo].[users]
                                       SET [name] = '" + user.name +
                                          "',[email] = '" + user.email +
                                          "',[password] = '" + user.password +
                                          "',[phone] = '" + user.phone +
                                          "',[address] = '" + user.address +
                                          "',[image] = '" + user.image +
                                     "WHERE user_id = '" +  user.user_id + "'";
                int row = DataAccess.ExecuteQuery(updateQuery);
                if (row == 1)
                {
                    inserted = true;
                }
            }
            catch
            {
                return false;
            }
            return inserted;
        }
    
        internal static bool CheckUserExist(string email)
        {
            try
            {
                var query = @"SELECT * FROM users where email LIKE '%" + email + "%';";
                var dt = DataAccess.GetDataTable(query);
                if (dt.Rows.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    
        internal static bool InsertUser(User user)
        {
            bool inserted = false;
            try
            {
                string query = @"INSERT INTO [dbo].[users]([name],[email],[password],[phone],[address],[image],[join_date])
                                VALUES( '"+user.name+ "' ,'" + user.email + "','" + user.password + "','" + user.phone + "','" + user.address + 
                                "','" + user.image + "','" + user.join_date + "')";
                int row = DataAccess.ExecuteQuery(query);
                if (row == 1)
                {
                    inserted = true;
                }
            }
            catch
            {
                return false;
            }
            return inserted;
        }
    }
}
