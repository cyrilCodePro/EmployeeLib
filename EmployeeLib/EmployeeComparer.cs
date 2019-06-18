using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeLib
{
    //Comparer class to remove duplicates
    class EmployeeComparer : IEqualityComparer<Employees>
    {
        public bool Equals(Employees x, Employees y)
        {
            return x.Id.Trim().Equals(y.Id)||(x.Manger_Id.Trim().Equals("")&&y.Manger_Id.Trim().Equals(""));
        }
       public int GetHashCode(Employees obj)
        {
            return obj.Id.GetHashCode();
        }


    }
}
