﻿using Microsoft.AspNetCore.Mvc;
using WebDev.Application.Models;
using WebDev.Application.Config;
using WebDev.Services;
using Microsoft.Extensions.Options;
using WebDev.Services.Entities;

namespace WebDev.Application.Controllers
{
    public class UsersController : Controller
    {
        private static List<User> _userList;
        private static int numUsers;
        private readonly ApiConfiguration _apiConfiguration;
        private UsersService usersService;

        public UsersController(IOptions<ApiConfiguration> apiConfiguration)
        {
            // Mock User List
            if (_userList is null)
            {
                _userList = new List<User>()
                {
                    new User{Id=1, Email="Julio.Robles@email.com", Name="Julio Robles", Username="jrobles", Password="Password"},
                    new User{Id=2, Email="Pilar.Lopez@email.com", Name="Pilar Lopez", Username="plopez", Password="Password"},
                    new User{Id=3, Email="Felipe.Daza@email.com", Name="Felipe Daza", Username="fdaza", Password="Password"},
                };
                numUsers = _userList.Count;
            }
            _apiConfiguration = apiConfiguration.Value;
            usersService = new UsersService(_apiConfiguration.ApiUsersUrl);
        }
        // GET: UsersController
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            // Set Object Model
            return View(_userList);
        }

        // GET: UsersController/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(int id)
        {
            var userFound = _userList.FirstOrDefault(u => u.Id == id);

            if (userFound == null)
            {
                return NotFound();
            }

            return View(userFound);
        }

        // GET: UsersController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Id = ++numUsers;
                    _userList.Add(user);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var userFound = _userList.FirstOrDefault(u => u.Id == id);

            if (userFound == null)
            {
                return NotFound();
            }

            return View(userFound);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userFound = _userList.FirstOrDefault(u => u.Id == user.Id);
                    userFound.Email = user.Email;
                    userFound.Name = user.Name;
                    userFound.Username = user.Username;
                    userFound.Password = user.Password;

                    return RedirectToAction(nameof(Index));
                }
                return View(user);
            }
            catch
            {
                return View();
            }
        }


        // GET: UsersController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var userFound = _userList.FirstOrDefault(u => u.Id == id);

            if (userFound == null)
            {
                return NotFound();
            }

            return View(userFound);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(User user)
        {
            try
            {
                var userFound = _userList.FirstOrDefault(u => u.Id == user.Id);

                if (userFound == null)
                {
                    return View();
                }

                _userList.Remove(userFound);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private User MapperToUser(UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Name = userDto.Name,
                Username = userDto.Username,
                Password = userDto.Password
            };
        }

        private UserDto MapperToUserDto(User user)
        {
            return UserDto.Build(
              id: user.Id,
              email: user.Email,
              name: user.Name,
              username: user.Username,
              password: user.Password
            );
        }

    }
}