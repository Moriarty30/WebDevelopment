﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SttudentsMVC.Models;

namespace SttudentsMVC.Controllers
{
    public class StudentsController : Controller
    {
        //Global Variables
        private static List<Student> studentsList = LoadStudents();
        
        // GET: StudentsController
        public ActionResult Index()
        {
            return View(studentsList);
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            var student = studentsList.FirstOrDefault(x => x.Id == id);
            return View(student);
        }

        // GET: StudentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Edit/5
        public ActionResult Edit(int id)
        {
            var student = studentsList.FirstOrDefault(x => x.Id == id);
            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            var student = studentsList.FirstOrDefault(x => x.Id == id);
            return View(student);
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #region Private-Methods

        private static List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();

            students.Add(new Student() { Id = 1, FirstName = "Jhon", LastName = "pedro", DateOfBirth = new DateTime(1980, 10, 10), Sex = 'M' });
            students.Add(new Student() { Id = 2, FirstName = "Jhon", LastName = "pedro", DateOfBirth = new DateTime(1980, 10, 10), Sex = 'M' });
            students.Add(new Student() { Id = 3, FirstName = "Jhon", LastName = "pedro", DateOfBirth = new DateTime(1980, 10, 10), Sex = 'F' });

            return students;

        }

        #endregion Private-Methods

    }
}
