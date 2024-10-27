using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GopetHost.Data;
using GopetHost.Models;
using GopetHost.Ulti;

namespace GopetHost.Controllers
{
    public class UserController : HelperController
    {
        private readonly AppDatabaseContext _context;

        public UserController(AppDatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("user_id,username,password,email,phone")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ShowMessage("Đăng nhập thất bại", "Thông tin tài khoản hoặc mật khẩu không chính xác", "is-danger");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }
    }
}
