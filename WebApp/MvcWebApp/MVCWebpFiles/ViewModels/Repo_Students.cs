using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using INT422Project.Models;

namespace INT422Project.ViewModels
{

    public class Repo_Students : Repository
    {

        public IEnumerable<StudentFull> getStudentFull()
        {

            var objs = context.Students.Include("communication").OrderBy(n => n.studentId);

            List<StudentFull> list = new List<StudentFull>();

            foreach (var item in objs)
            {

                StudentFull student = new StudentFull();
                student.studentId = item.studentId;
                student.senecaId = item.senecaId;
                student.firstName = item.firstName;
                student.lastName = item.lastName;
                student.communication = item.communication;

                list.Add(student);

            }

            return list;

        }

        public StudentFull getStudentFull(int? id)
        {

            var obj = context.Students.Include("communication").FirstOrDefault(n => n.studentId == id);

            StudentFull student = new StudentFull();
            student.studentId = obj.studentId;
            student.senecaId = obj.senecaId;
            student.firstName = obj.firstName;
            student.lastName = obj.lastName;
            student.communication = obj.communication;

            return student;

        }

        public IEnumerable<StudentList> getStudentList()
        {

            var all = context.Students.OrderBy(n => n.studentId);

            List<StudentList> list = new List<StudentList>();

            foreach (var item in all)
            {

                StudentList student = new StudentList();
                student.studentId = item.studentId;
                student.senecaId = item.senecaId;
                student.firstName = item.firstName;
                student.lastName = item.lastName;

                list.Add(student);

            }

            return list;

        }

    }

}