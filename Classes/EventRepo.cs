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
    public class EventRepo
    {
        private DiaryEvents ConvertToEntity(DataRow dataRow)
        {
            var employee = new DiaryEvents();
            employee.empId = dataRow["empId"].ToString();
            employee.name = dataRow["name"].ToString();
            employee.designation = dataRow["designation"].ToString();
            employee.salary = float.Parse(dataRow["salary"].ToString());
            employee.bonus = Double.Parse(dataRow["bonus"].ToString());
            employee.rating = Double.Parse(dataRow["rating"].ToString());
            employee.overtime = Int32.Parse(dataRow["overtime"].ToString());
            employee.eligible = Boolean.Parse(dataRow["eligible"].ToString());
            employee.personId = Int32.Parse(dataRow["personId"].ToString());
            return employee;
        }

        internal bool DeleteEmployee(string id)
        {
            bool delete = false;
            try
            {
                string deleteQuery = "DELETE FROM Employee WHERE empId = '" + id + "';";
                var empDelete = DataAccess.ExecuteUpdateQuery(deleteQuery);
                if (empDelete == 1)
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
                MessageBox.Show(e.Message + "\n Emp Repo");
                return false;
            }
            return delete;
        }

        internal bool UpdateEmployee(DiaryEvents emp)
        {
            bool inserted = false;
            try
            {
                string updateQuery = @"UPDATE Employee SET designation = '" + emp.designation + "', salary = " + emp.salary + " , bonus = " + emp.bonus +
                                    ", rating = " + emp.rating + " WHERE empId = '" + emp.empId + "';";
                int row = DataAccess.ExecuteUpdateQuery(updateQuery);
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

        internal bool AddEmployee(DiaryEvents employee)
        {
            bool added = false;
            try
            {
                string query = null;
                var sql = "select * from Employee where empId = '" + employee.empId + "';";
                var dt = DataAccessUpdated.GetDataTable(sql);
                if (dt == null || dt.Rows.Count == 0)
                {
                    query = @"INSERT INTO Employee( empId, name,designation,salary,bonus,rating,overtime,eligible ,personId) " +
                        "VALUES ('" + employee.empId + "','" + employee.name + "','" + employee.designation + "'," + employee.salary + "," + employee.bonus + ", 0 , 0 ,'" + employee.eligible + "'," + employee.personId + ") ;";
                    int insRow = DataAccess.ExecuteUpdateQuery(query);
                    if (insRow == 1)
                    {
                        added = true;
                    }
                    else
                    {
                        added = false;
                    }
                }
                else
                {
                    this.UpdateEmployee(employee);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return added;
        }

        internal List<DiaryEvents> GetEmployees()
        {
            List<DiaryEvents> employees = new List<DiaryEvents>();
            try
            {
                var query = @"SELECT * FROM Employee; ";
                var dt = DataAccessUpdated.GetDataTable(query);
                int index = 0;
                while (index < dt.Rows.Count)
                {
                    DiaryEvents employee = new DiaryEvents();
                    employee = ConvertToEntity(dt.Rows[index]);
                    employees.Add(employee);
                    index++;
                }
            }
            catch
            {
                return null;
            }
            return employees;
        }
    }
}
