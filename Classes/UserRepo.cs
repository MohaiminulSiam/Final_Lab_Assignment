using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Lab_Assignment;
using Final_Lab_Assignment_code.Classes;
namespace Final_Lab_Assignment_code.Classes
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

        internal static bool UpdateDiaryEvent(DiaryEvents diaryEvent)
        {
            bool inserted = false;
            try
            {
                string updateQuery = @"UPDATE diary_events SET title = '" + diaryEvent.title + "', description = " + diaryEvent.description + " , last_modify_date = '" + DateTime.Now.ToString("yyyy-MM-dd") +
                                    "', priority = '" + diaryEvent.priority + "', image = '" + diaryEvent.image + "' WHERE event_id = '" + diaryEvent.event_id + "';";
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
    }
}
