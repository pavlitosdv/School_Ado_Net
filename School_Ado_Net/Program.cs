using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School_Ado_Net
{
    class Program
    {
        static void Main(string[] args)
        {
            School_DB_insert_data db = new School_DB_insert_data();


            //db.AddTodo("python", "ok","full", new DateTime(2019, 10, 05), new DateTime(2019, 10, 05));


            //db.GetAllStudents();
            //db.GetAllTrainers();
            //db.GetAllCourses();
            //db.GetAllAssignments();
            //db.GetAllStudentsPerCourse("Java","Part Time");
            //db.GetAllAssignmentsPerCourse();
            //db.GetAllTrainersPerCourse();
            //db.GetAllAssignmentsPerCoursePerStudent();

            //db.Student_InMore_Than_One_Course();


            db.RunSchool();





            Console.ReadKey();

        }
    }
}
