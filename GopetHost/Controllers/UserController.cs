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
        public async Task<IActionResult> Create([Bind("username,password,repassword,email,phone")] UserData userData)
        {
            if (ModelState.IsValid)
            {
                if(userData.password != userData.repassword)
                {
                    ShowMessage("Đăng ký thất bại", "Do mật khẩu xác nhận không chính xác", "is-danger");
                    goto TO_HOME;
                }
                if (!Ulti.Ulti.UserPassRegex.IsMatch(userData.username) || !Ulti.Ulti.UserPassRegex.IsMatch(userData.password))
                {
                    ShowMessage("Đăng ký thất bại", "Do tên tài khoản hoặc mật khẩu chứa ký tự đặc biệt!. Chỉ chấp nhận độ dài tài khoản mật khẩu từ 5-20 ký tự và không chứa ký tự đặc biệt", "is-danger");
                    goto TO_HOME;
                }
                if (_context.Users.Any(x => x.username == userData.username))
                {
                    ShowMessage("Đăng ký thất bại", "Do tên tài khoản đã tồn tại", "is-danger");
                    goto TO_HOME;
                }
                _context.Add(userData);
                await _context.SaveChangesAsync();
                ShowMessage("Đăng ký thành công", "Đăng ký thành công mời bạn vào game.Nhớ phải bảo mật tài khoản kỷ càng tránh lộ thông tin trước người lạ.", "is-success is-light");
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            ShowMessage("Đăng ký thất bại", "Do bạn điền không đủ các trường thông tin", "is-danger");
        TO_HOME:
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private bool UserDataExists(int id)
        {
            return _context.Users.Any(e => e.user_id == id);
        }
    }
}
