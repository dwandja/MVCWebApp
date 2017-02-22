using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcWebApp.Models;
using AutoMapper;

namespace MvcWebApp.ViewModels
{

    public class Repo_Student : Repository
    {

        public IEnumerable<StudentFull> getStudentFull()
        {

            var objs = context.Students.Include("Communication").OrderBy(n => n.StudentId);

            List<StudentFull> list = new List<StudentFull>();

            foreach (var item in objs)
            {

                StudentFull student = new StudentFull();
                student.StudentId = item.StudentId;
                student.SenecaId = item.SenecaId;
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;
                student.Communication = item.Communication;

                list.Add(student);

            }

            return list;

        }

        public StudentFull getStudentFull(int? id)
        {

            var obj = context.Students.Include("Communication").FirstOrDefault(n => n.StudentId == id);

            return Mapper.Map<StudentFull>(obj);

            /*
            StudentFull student = new StudentFull();
            student.studentId = obj.studentId;
            student.senecaId = obj.senecaId;
            student.firstName = obj.firstName;
            student.lastName = obj.lastName;
            student.communication = obj.communication;
            return student;
            */

        }

        public IEnumerable<StudentList> getStudentList()
        {

            var all = context.Students.OrderBy(n => n.StudentId);

            List<StudentList> list = new List<StudentList>();

            foreach (var item in all)
            {

                StudentList student = new StudentList();
                student.StudentId = item.StudentId;
                student.SenecaId = item.SenecaId;
                student.FirstName = item.FirstName;
                student.LastName = item.LastName;

                list.Add(student);

            }

            return list;

        }

    }

}