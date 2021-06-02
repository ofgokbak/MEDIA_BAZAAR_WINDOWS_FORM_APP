using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormDesktopApp.Objects.Departments
{
    public class Department
    {
        private int department_id;
        private string department_name;

        public Department()
        {
        }

        public int Department_id { get => department_id; set => department_id = value; }
        public string Department_name { get => department_name; set => department_name = value; }


        public override string ToString()
        {
            return $" ID:{this.Department_id} - Name: {this.Department_name}";
        }
        
        
    }
}
