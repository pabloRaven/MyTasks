﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Core.Models;
using MyTasks.Core.Models.Domeins;
using MyTasks.Core.ViewModels;
using MyTasks.Persistance;
using MyTasks.Persistence.Extensions;
using MyTasks.Persistence.Repositories;
using System;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private TaskRepository _taskRepository;

        public TaskController(ApplicationDbContext context)
        {
            _taskRepository = new TaskRepository(context);
        }

        public IActionResult Tasks()
        {
            var userId = User.GetUserId();

            var vm = new TasksViewModel
            {
                FilterTasks = new FilterTasks(),
                Task = _taskRepository.Get(userId),
                Categories = _taskRepository.GetCategories()

            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Tasks(TasksViewModel viewModel)
        {
            var userId = User.GetUserId();

            var tasks = _taskRepository.Get(userId,
                viewModel.FilterTasks.IsExecuted,
                viewModel.FilterTasks.CategoryId,
                viewModel.FilterTasks.Title);


            return PartialView("_TasksTable", tasks);
        }

        public IActionResult Task(int id = 0)
        {
            var userId = User.GetUserId();

            var task = id == 0 ?
                new Task { Id = 0, UserId = userId, Term = DateTime.Today } :
                   _taskRepository.Get(id, userId);

            var vm = new TaskViewModel
            {
                Task = task,
                Heading = id == 0 ?
                "Dodawanie nowego zadania" : "Edytowanie zadania",
                Categories = _taskRepository.GetCategories()
            };
            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Task(Task task)
        {
            var userId = User.GetUserId();
            task.UserId = userId;

            if (!ModelState.IsValid)
            {
                var vm = new TaskViewModel
                {
                    Task = task,
                    Heading = task.Id == 0 ?
                "Dodawanie nowegozadania" : "Edytowanie zadania",
                    Categories = _taskRepository.GetCategories()
                };

                return View("Task", vm);
            }
            if (task.Id == 0)
                _taskRepository.Add(task);
            else
                _taskRepository.Update(task);

            return RedirectToAction("Tasks");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {

            try
            {
                var userId = User.GetUserId();
                _taskRepository.Delete(id, userId);

            }
            catch (Exception ex)
            {
                //logowanie do pliku             
                return Json(new { succes = false, message = ex.Message });
            }
            return Json(new { succes = true });
        }

        [HttpPost]
        public IActionResult Finish(int id)
        {

            try
            {
                var userId = User.GetUserId();
                _taskRepository.Finish(id, userId);

            }
            catch (Exception ex)
            {
                //logowanie do pliku             
                return Json(new { succes = false, message = ex.Message });
            }
            return Json(new { succes = true });
        }

    }
}
