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
    public class EventRepo
    {
        private DiaryEvents ConvertToEntity(DataRow dataRow)
        {
            var diary_event = new DiaryEvents();
            diary_event.event_id = dataRow["event_id"].ToString();
            diary_event.title = dataRow["title"].ToString();
            diary_event.description = dataRow["description"].ToString();
            diary_event.post_date = DateTime.Parse(dataRow["post_date"].ToString());
            if (String.IsNullOrEmpty(dataRow["last_modify_date"].ToString()) != true)
            {
                diary_event.last_modify_date = DateTime.Parse(dataRow["last_modify_date"].ToString());
            }
            diary_event.priority = dataRow["priority"].ToString();
            diary_event.image = dataRow["image"].ToString();
            diary_event.user_FK = Int32.Parse(dataRow["user_FK"].ToString());

            return diary_event;
        }

        internal bool DeleteDiaryEvent(string id)
        {
            bool delete = false;
            try
            {
                string deleteQuery = "DELETE FROM diary_events WHERE event_id = '" + id + "';";
                var eventDelete = DataAccess.ExecuteQuery(deleteQuery);
                if (eventDelete == 1)
                {
                    delete = true;
                }
                else
                {
                    delete = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return delete;
        }

        internal bool UpdateDiaryEvent(DiaryEvents diaryEvent)
        {
            bool inserted = false;
            try
            {
                string updateQuery = @"UPDATE [dbo].[diary_events]
                                       SET [title] = '" + diaryEvent.title +
                                          "',[description] = '" + diaryEvent.description +
                                          "',[last_modify_date] = '" + DateTime.Now +
                                          "',[priority] = '" + diaryEvent.priority +
                                          "',[image] = '" + diaryEvent.image +
                                     "' WHERE event_id = '" + diaryEvent.event_id + "'";
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

        internal bool AddDiaryEvent(DiaryEvents diaryEvent)
        {
            bool added = false;
            try
            {
                var query = @"INSERT INTO diary_events( title,description,post_date,last_modify_date,priority,user_FK) " +
                        "VALUES ('" + diaryEvent.title + "','" + diaryEvent.description + "','" + diaryEvent.post_date + "','" + diaryEvent.last_modify_date + "','" + diaryEvent.priority + "','" + diaryEvent.user_FK + "') ;";
                int insRow = DataAccess.ExecuteQuery(query);
                if (insRow == 1)
                {
                    added = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return added;
        }

        internal List<DiaryEvents> GetDiaryEvents(int user_FK)
        {
            List<DiaryEvents> diaryEvents = new List<DiaryEvents>();
            try
            {
                var query = @"SELECT * FROM diary_events where user_FK = " + user_FK + ";";
                var dt = DataAccess.GetDataTable(query);
                int index = 0;
                while (index < dt.Rows.Count)
                {
                    DiaryEvents diaryEv = new DiaryEvents();
                    diaryEv = ConvertToEntity(dt.Rows[index]);
                    diaryEvents.Add(diaryEv);
                    index++;
                }
            }
            catch
            {
                MessageBox.Show("Data Not Loaded \n Data :: NULL");
                return null;
            }
            return diaryEvents;
        }
    
        internal DiaryEvents Search(int event_id, int user_FK)
        {
            DiaryEvents diaryEvent = new DiaryEvents();
            try
            {
                var query = @"SELECT * FROM diary_events where event_id = '" + event_id + "' AND user_FK = " + user_FK + ";";
                var dt = DataAccess.GetDataTable(query);
                int index = 0;
                while (index < dt.Rows.Count)
                {
                    diaryEvent = ConvertToEntity(dt.Rows[index]);
                    index++;
                }
            }
            catch
            {
                MessageBox.Show("Data Not Loaded \n Data :: NULL");
                return null;
            }
            return diaryEvent;
        }
    }
}
