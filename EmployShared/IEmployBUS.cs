using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployShared
{
    public interface IEmployBUS
    {
        List<Employee> GetAll();
        Employee GetEmployee(int ID);
        List<Employee> Search(string keyword);
        bool Add(Employee employee);
        bool Delete(int ID);
        bool Update(Employee employee);
    }
}
