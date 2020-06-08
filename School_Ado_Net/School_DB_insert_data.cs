using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;

namespace School_Ado_Net
{
    class School_DB_insert_data
    {
        private string connection_string = @"Data Source=OLE\SQLEXPRESS;Initial Catalog=School1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region Region Adding Elements
        //Add Course
        public void AddCourse(string title, string stream, string type, DateTime start_date, DateTime end_date)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Courses(Title, Stream, Type, [Start Date], [End Date]) " +
                    "VALUES (@title, @stream, @type, @start_date, @end_date)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@title", title));
                    cmd.Parameters.Add(new SqlParameter("@stream", stream));
                    cmd.Parameters.Add(new SqlParameter("@type", type));
                    cmd.Parameters.Add(new SqlParameter("@start_date", start_date));
                    cmd.Parameters.Add(new SqlParameter("@end_date", end_date));

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //Add Student
        public void AddStudent(string first_name, string last_name, DateTime date_of_birth, decimal tuition_fees)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Students([First name],[Last Name],[Date of Birth], [tuition fees]) " +
                    "VALUES (@first_name, @last_name, @date_of_birth, @tuition_fees)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                    cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                    cmd.Parameters.Add(new SqlParameter("@date_of_birth", date_of_birth));
                    cmd.Parameters.Add(new SqlParameter("tuition_fees", tuition_fees));

                    cmd.ExecuteNonQuery();
                }

            }
        }

        //Add Trainer
        public void AddTrainer(string first_name, string last_name, int Course_id_fk, string subject)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Trainers([First Name], [Last Name], Course_id_fk, Subject)" +
                    "VALUES (@first_name, @last_name, @Course_id_fk, @subject)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                    cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                    cmd.Parameters.Add(new SqlParameter("@Course_id_fk", Course_id_fk));
                    cmd.Parameters.Add(new SqlParameter("@subject", subject));

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //Add Assignment
        public void AddAssignment(string title, string description, DateTime submission_Date_time)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Assignment(Title,Description,[submission Date time]) " +
                    "VALUES(@title,@description,@submission_Date_time)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@title", title));
                    cmd.Parameters.Add(new SqlParameter("@description", description));
                    cmd.Parameters.Add(new SqlParameter("@submission_Date_time", submission_Date_time));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        //Add Grades
        public void AddGrades(int Student_id_fk, int Assignment_id_fk, decimal Oral_Mark, decimal Total_Mark)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Grades_and_Middle_table(Student_id_fk, Assignment_id_fk, [Oral Mark],[Total Mark])" +
                    "VALUES(@Student_id_fk, @Assignment_id_fk, @Oral_Mark,@Total_Mark)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@Student_id_fk", Student_id_fk));
                    cmd.Parameters.Add(new SqlParameter("@Assignment_id_fk", Assignment_id_fk));
                    cmd.Parameters.Add(new SqlParameter("@Oral_Mark", Oral_Mark));
                    cmd.Parameters.Add(new SqlParameter("@Total_Mark", Total_Mark));

                    cmd.ExecuteNonQuery();

                }
            }
        }

        //Add Student Per Course
        public void AddStudentPerCourse(int course_id, int student_id)
        {
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO Courses_Students_List_Middle_table(Course_id, Student_id) " +
                    "VALUES (@Course_id, @Student_id)", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@Course_id", course_id));
                    cmd.Parameters.Add(new SqlParameter("Student_id", student_id));

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
        #endregion

        #region Region Print Data
        public void GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                student_id = (int)reader["StudentID"],
                                first_name = (string)reader["First name"],
                                last_name = (string)reader["Last Name"],
                                date_of_birth = (DateTime)reader["Date of Birth"],
                                tuition_fees = (int)reader["tuition fees"]
                            });
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -10} | {2, -14} | {3, -7} | {4, -5}", "Student ID", "First Name", "Last Name", "Date of Birth", "Tuition Fees");
            Console.WriteLine("---------------------------------------------------------------------");
            foreach (Student st in students)
            {
                Console.WriteLine("{0, -10} | {1, -10} | {2, -14} | {3, -8} | {4, -5}", st.student_id, st.first_name, st.last_name, st.date_of_birth.ToString("MMM d, yyyy"), st.tuition_fees);
            }
            Console.WriteLine();
        }

        public void GetAllTrainers()
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Trainers", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainers.Add(new Trainer
                            {
                                trainer_id = (int)reader["TrainerID"],
                                Course_id_fk = (int)reader["Course_id_fk"],
                                first_name = (string)reader["First Name"],
                                last_name = (string)reader["Last Name"],
                                subject = (string)reader["Subject"]
                            });
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -10} | {2, -14} | {3, -12} | {4, -5}", "Trainer ID", "First Name", "Last Name", "Subject", "Course FK");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Trainer tr in trainers)
            {
                Console.WriteLine("{0, -10} | {1, -10} | {2, -14} | {3, -12} | {4, -5}", tr.trainer_id, tr.first_name, tr.last_name, tr.subject, tr.Course_id_fk);
            }
            Console.WriteLine();
        }

        public void GetAllCourses()
        {
            List<Course> courses = new List<Course>();

            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                course_id = (int)reader["CourseID"],
                                title = (string)reader["Title"],
                                stream = (string)reader["Stream"],
                                type = (string)reader["Type"],
                                start_date = (DateTime)reader["Start Date"],
                                end_date = (DateTime)reader["End Date"]
                            });
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -8} | {2, -8} | {3, -10} | {4, -12} | {5, -2}", "Course ID", "Title", "Stream", "Type", "Start Date", "End Date");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Course cr in courses)
            {
                Console.WriteLine("{0, -9} | {1, -8} | {2, -8} | {3, -10} | {4, -10} | {5, -2}", cr.course_id, cr.title, cr.stream, cr.type, cr.start_date.ToString("MMM d, yyyy"), cr.end_date.ToString("MMM d, yyyy"));
            }
            Console.WriteLine();
        }
        public void GetAllAssignments()
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Assignment", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                assignment_id = (int)reader["AssignmentID"],
                                title = (string)reader["Title"],
                                description = (string)reader["Description"],
                                sub_date = (DateTime)reader["submission Date time"]
                            });
                        }
                    }
                }
            }

            Console.WriteLine("{0, -2} | {1, -5} | {2, -12} | {3, -5}", "Assignment ID", "Title", "Description", "Submission Date");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Assignment assign in assignments)
            {
                Console.WriteLine("{0, -13} | {1, -5} | {2, -12} | {3, -5}", assign.assignment_id, assign.title, assign.description, assign.sub_date.ToString("MMM d, yyyy"));
            }
            Console.WriteLine();
        }

        public void GetAllStudentsPerCourse(int course)
        {
            List<Student> students = new List<Student>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses_Students_List_Middle_table " +
                    "INNER JOIN Courses ON Courses_Students_List_Middle_table.Course_id = Courses.CourseID " +
                    "INNER JOIN Students ON Courses_Students_List_Middle_table.Student_id = Students.StudentID " +
                    "WHERE Courses.CourseID = @course", connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@course";
                    param.Value = course;
                    cmd.Parameters.Add(param);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                student_id = (int)reader["StudentID"],
                                first_name = (string)reader["First name"],
                                last_name = (string)reader["Last Name"],
                                date_of_birth = (DateTime)reader["Date of Birth"],
                                tuition_fees = (int)reader["tuition fees"]
                            });
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -10} | {2, -14} | {3, -7} | {4, -5}", "Student ID", "First Name", "Last Name", "Date Of Birth", "Tuition Fees");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Student st in students)
            {
                Console.WriteLine("{0, -10} | {1, -10} | {2, -14} | {3, -8} | {4, -5}", st.student_id, st.first_name, st.last_name, st.date_of_birth, st.tuition_fees);
            }
            Console.WriteLine();
        }

        public void GetAllTrainersPerCourse(int course)
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DISTINCT Trainers.[First Name],Trainers.[Last Name]," +
                                                    "Trainers.TrainerID,Trainers.Subject,Trainers.Course_id_fk " +
                                                    "FROM Courses " +
                                                    "INNER JOIN Trainers ON Courses.CourseID = Trainers.Course_id_fk " +
                                                    "WHERE Courses.CourseID = @course", connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@course";
                    param.Value = course;
                    cmd.Parameters.Add(param);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            trainers.Add(new Trainer
                            {
                                trainer_id = (int)reader["TrainerID"],
                                Course_id_fk = (int)reader["Course_id_fk"],
                                first_name = (string)reader["First Name"],
                                last_name = (string)reader["Last Name"],
                                subject = (string)reader["Subject"]
                            });
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -10} | {2, -14}", "Trainer ID", "First Name", "Last Name");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Trainer tr in trainers)
            {
                Console.WriteLine("{0, -10} | {1, -10} | {2, -14}", tr.trainer_id, tr.first_name, tr.last_name);
            }
            Console.WriteLine();
        }

        public void GetAllAssignmentsPerCourse(int course)
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Middle_Course_Assignment " +
                    "INNER JOIN Assignment ON Middle_Course_Assignment.Assignment_id = Assignment.AssignmentID " +
                    "INNER JOIN Courses ON Middle_Course_Assignment.Course_id = Courses.CourseID " +
                    "WHERE Courses.CourseID = @course", connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@course";
                    param.Value = course;
                    cmd.Parameters.Add(param);
                

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                assignment_id = (int)reader["AssignmentID"],
                                title = (string)reader["Title"],
                                description = (string)reader["Description"],
                                sub_date = (DateTime)reader["submission Date time"]
                            });
                        }
                    }

                }
                Console.WriteLine("{0, -2} | {1, -6} | {2, -15} | {3, -5}", "Assignment ID", "Title", "Description", "Submission Date");
                Console.WriteLine("---------------------------------------------------------------------");

                foreach (Assignment assignment in assignments)
                {
                    Console.WriteLine("{0, -13} | {1, -5} | {2, -12} | {3, -5}", assignment.assignment_id, assignment.title, assignment.description, assignment.sub_date.ToString("MMM d, yyyy"));
                }
                Console.WriteLine();
            }

        }

        public void Student_InMore_Than_One_Course()
        {
            string query = "SELECT Students.StudentID, Students.[First name], Students.[Last Name],Students.[Date of Birth],Students.[tuition fees], COUNT(DISTINCT Courses_Students_List_Middle_table.Course_id ) AS Number_of_Different_Courses FROM Courses_Students_List_Middle_table INNER JOIN Students ON Students.StudentID=Courses_Students_List_Middle_table.Student_id GROUP BY Courses_Students_List_Middle_table.Student_id,Students.[First name],Students.[Last Name] ,Students.StudentID, Students.[Date of Birth],Students.[tuition fees] HAVING COUNT(DISTINCT Courses_Students_List_Middle_table.Course_id )>1";

            List<Student> students = new List<Student>();
            using(SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                {
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                students.Add(new Student
                                {
                                    student_id = (int)reader["StudentID"],
                                    first_name = (string)reader["First name"],
                                    last_name = (string)reader["Last Name"],
                                    date_of_birth = (DateTime)reader["Date of Birth"],
                                    tuition_fees = (int)reader["tuition fees"]
                                });
                            }
                        }
                    }
                }
            }
            Console.WriteLine("{0, -8} | {1, -10} | {2, -14} | {3, -7} | {4, -5}", "Student ID", "First Name", "Last Name", "Date of Birth", "Tuition Fees");
            Console.WriteLine("-----------|------------|----------------|---------------|---------");

            foreach (Student st in students)
            {
                Console.WriteLine("{0, -10} | {1, -10} | {2, -14} | {3, -8} | {4, -5}", st.student_id, st.first_name, st.last_name, st.date_of_birth.ToString("MMM d, yyyy"), st.tuition_fees);
            }
            Console.WriteLine();
        }


        //All the assignments per course per student Not Functional
        public void GetAllAssignmentsPerCoursePerStudent(int c, int s)
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection connection = new SqlConnection(connection_string))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Assignment " +
                    "INNER JOIN Courses_Students_List_Middle_table ON Courses_Students_List_Middle_table.Student_id = Students.StudentID " +
                    "INNER JOIN Courses ON Courses_Students_List_Middle_table.Course_id = Courses.CourseID " +
                    "INNER JOIN Middle_Course_Assignment ON Middle_Course_Assignment.Course_id  = Courses.CourseID " +
                    "INNER JOIN Assignment ON Assignment.AssignmentID = Middle_Course_Assignment.Assignment_id " +
                    "GROUP BY Assignment.Description, Students.[First name], Students.[Last Name], Courses.Title, Courses.Type " +
                    "ORDER BY Students.[Last Name], Courses.Title", connection))
                {
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@c";
                    param.Value = c;
                    cmd.Parameters.Add(param);

                    SqlParameter param1 = new SqlParameter();
                    param1.ParameterName = "@s";
                    param1.Value = s;
                    cmd.Parameters.Add(param1);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            assignments.Add(new Assignment
                            {
                                assignment_id = (int)reader["AssignmentID"],
                                title = (string)reader["Title"],
                                description = (string)reader["Description"],
                                sub_date = (DateTime)reader["submission Date time"]
                            });
                        }
                    }
                }
            }

            Console.WriteLine("{0, -2} | {1, -5} | {2, -12} | {3, -5}", "Assignment ID", "Title", "Description", "Submission Date");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (Assignment assign in assignments)
            {
                Console.WriteLine("{0, -13} | {1, -5} | {2, -12} | {3, -5}", assign.assignment_id, assign.title, assign.description, assign.sub_date.ToString("MMM d, yyyy"));
            }
            Console.WriteLine();
        }

        #endregion

        #region Region Menu code
        public void ShowInitialMenu()
        {
            Console.WriteLine("Hello Welcome to School");
            Console.WriteLine("Press the following Indexes for:");
            Console.WriteLine("1. To Print Data");    
            Console.WriteLine("2. To Add Data");
            Console.WriteLine("0. To Exit");
        }

        public void MenuPrintData()
        {
            Console.WriteLine("1. To Get All Courses");
            Console.WriteLine("2. To Get All Students");
            Console.WriteLine("3. To Get All Trainers");
            Console.WriteLine("4. To Get All Assignment");
            Console.WriteLine("5. To Get All Students Per Course");
            Console.WriteLine("6. To Get All Trainers Per Course");
            Console.WriteLine("7. To Get All Assignments Per Course");
            Console.WriteLine("8. To Get All Assignments Per Course Per Student (Not implemented)");
            Console.WriteLine("9. A list of students that belong to more than one courses");
        }

        public void MenuAddData()
        {
            Console.WriteLine("1. Add Course");
            Console.WriteLine("2. Add Student");
            Console.WriteLine("3. Add Trainer and assgin in a course");
            Console.WriteLine("4. Add Assignment");
            Console.WriteLine("5. Add Grades");
            Console.WriteLine("6. Add Student In Course");
        }
        #endregion

       

        public void RunSchool()
        {
            while (true)
            {
                ShowInitialMenu();
                string input=Console.ReadLine();
                switch(input)
                {
                    case "1":
                        Thread.Sleep(400);
                        Console.Clear();
                        MenuPrintData();
                        string input1 = Console.ReadLine();
                        switch (input1)
                        {
                            case "1":
                                Thread.Sleep(400);
                                Console.Clear();
                                GetAllCourses();
                                Console.WriteLine();
                                break;
                            case "2":
                                Thread.Sleep(400);
                                Console.Clear();
                                GetAllStudents();
                                break;
                            case "3":
                                Thread.Sleep(400);
                                Console.Clear();
                                GetAllTrainers();
                                break;
                                case "4":
                                Thread.Sleep(400);
                                Console.Clear();
                                GetAllAssignments();
                                break;
                            case "5":
                                Thread.Sleep(400);
                                Console.Clear();
                                Console.WriteLine("For which course do you want to display the students");
                                GetAllCourses();
                                Console.WriteLine();
                                Console.WriteLine("Type the Course ID");
                                int input2 =Convert.ToInt32(Console.ReadLine());
                                GetAllStudentsPerCourse(input2);
                                Console.WriteLine();
                                break;
                            case "6":
                                Thread.Sleep(400);
                                Console.Clear();
                                Console.WriteLine("For which course do you want to display the Trainers");
                                GetAllCourses();
                                Console.WriteLine();
                                Console.WriteLine("Enter the course ID");
                                Console.WriteLine();
                                int input4 =Convert.ToInt32(Console.ReadLine());
                                GetAllTrainersPerCourse(input4);
                                Console.WriteLine();
                                break;
                            case "7":
                                Thread.Sleep(400);
                                Console.Clear();
                                Console.WriteLine("For which course do you want to display the Assignments");
                                GetAllCourses();
                                Console.WriteLine();
                                Console.WriteLine("Please mind that input is Case Sensitive");
                                Console.WriteLine("Type the course ID");
                                Console.WriteLine();
                                int input6 =Convert.ToInt32(Console.ReadLine());                                
                                Thread.Sleep(200);
                                Console.Clear();
                                GetAllAssignmentsPerCourse(input6);
                                Console.WriteLine();
                                break;
                            //case "8":
                            //    Console.WriteLine("For Which Course do you want to display the Students?");
                            //    GetAllCourses();
                            //    int c = Convert.ToInt32(Console.ReadLine());
                            //    GetAllStudentsPerCourse(c);
                            //    Console.WriteLine("For Which Student do you want to display the Assignments?");
                            //    int s = Convert.ToInt32(Console.ReadLine());
                            //    GetAllAssignmentsPerCoursePerStudent(c, s);
                            //    break;
                            case "9":
                                Thread.Sleep(400);
                                Console.Clear();
                                Student_InMore_Than_One_Course();
                                Console.WriteLine();
                                break;
                        }
                        break;
                    case "2":
                        Thread.Sleep(400);
                        Console.Clear();
                        MenuAddData();
                        string input21 = Console.ReadLine();
                        switch (input21)
                        {
                            case "1":
                                Console.WriteLine("Please Type the Title of the Course");
                                string title = Console.ReadLine();
                                Console.WriteLine("Please Type the Stream");
                                string stream = Console.ReadLine();
                                Console.WriteLine("Please Type the Type of the Course (Full Time/Part Time");
                                string type = Console.ReadLine();
                                Console.WriteLine("Please Type the Starting Date of the Course as dd/mm/yyyy");
                                DateTime start_date = Convert.ToDateTime(Console.ReadLine());
                                Console.WriteLine("Please Type the End Date of the Course as dd/mm/yyyy");
                                DateTime end_date = Convert.ToDateTime(Console.ReadLine());
                                Console.WriteLine();

                                AddCourse(title, stream, type, start_date, end_date);
                                Console.WriteLine();
                                break;
                            case "2":
                                Console.WriteLine("Type the First Name of the Student");
                                string first_name = Console.ReadLine();
                                Console.WriteLine("Type the Last Name of the Student");
                                string last_name = Console.ReadLine();
                                Console.WriteLine("Type the Date of Birth as dd/mm/yyyy");
                                DateTime date_of_birth = Convert.ToDateTime(Console.ReadLine());
                                Console.WriteLine("How much is the Tuition Fees?");
                                decimal fees = Convert.ToDecimal(Console.ReadLine());
                                Console.WriteLine();

                                AddStudent(first_name, last_name, date_of_birth, fees);
                                Console.WriteLine();
                                break;
                            case "3":
                                Console.WriteLine("Insert the First Name of the Trainer");
                                string trainer_first_name = Console.ReadLine();
                                Console.WriteLine("Insert the Last Name");
                                string trainer_last_name = Console.ReadLine();
                                Console.WriteLine("What subject(s) does he/she taughts?");
                                string subject = Console.ReadLine();
                                Console.WriteLine("In which Course to you want to add the Trainer?");
                                Console.WriteLine("Press the index of the Course");
                                Console.WriteLine();
                                GetAllCourses();
                                int inputCourse = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine();

                                AddTrainer(trainer_first_name, trainer_last_name, inputCourse, subject);
                                Console.WriteLine();
                                break;
                            case "4":
                                Console.WriteLine("Type the Title of the Assignent");
                                string assignment_title = Console.ReadLine();
                                Console.WriteLine("Type the Description");
                                string description = Console.ReadLine();
                                Console.WriteLine("What is the Submission Date ? type it as as dd/mm/yyyy");
                                DateTime sub_date = Convert.ToDateTime(Console.ReadLine());

                                AddAssignment(assignment_title, description, sub_date);
                                Console.WriteLine();
                                break;
                            case "5":
                                Console.WriteLine("Press the Index of the Student who want to add a grade");
                                GetAllStudents();
                                int student_index = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Press the Index of the Assignment");
                                GetAllAssignments();
                                int course_index = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter the Oral Mark");
                                decimal oral_mark = Convert.ToDecimal(Console.ReadLine());
                                Console.WriteLine("Enter the Total Mark");
                                decimal total_mark = Convert.ToDecimal(Console.ReadLine());

                                AddGrades(student_index, course_index, oral_mark, total_mark);
                                Console.WriteLine();
                                break;
                            case "6":
                                Console.WriteLine("Press the Index of the Student");
                                GetAllStudents();
                                int st_index = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Press the Index of the Course to assign the Student");
                                GetAllCourses();
                                int c_index = Convert.ToInt32(Console.ReadLine());

                                AddStudentPerCourse(st_index, c_index);
                                Console.WriteLine();
                                break;                           
                        }
                        break;
                    case "0":
                        Environment.Exit(0);
                        break;

                }


            }

        }
    }

}

