using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessOnline.Models;
using ChessOnline.Networking;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using ChessOnline.Models.Board;

namespace ChessOnline.Controllers
{
    public class HomeController : Controller
    {
        private string DbPsw1 = "1234";
        private string DbUser1 = "User1";
        private string DbPsw2 = "1234";
        private string DbUser2 = "User2";
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }//LoginPage for insert Username and Password
        //Index Page who call LogInControl 
        public bool LogInControl(User user)
        {
            if ((user.Password == DbPsw1 && user.UserName == DbUser1) || (user.Password == DbPsw2 && user.UserName == DbUser2))
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.UtcNow.AddHours(1);
                options.IsEssential = true;
                string json = JsonConvert.SerializeObject(user);
                HttpContext.Response.Cookies.Append("AuthCookie", json, options);
                return true;
            }
            return false;
        }

        public IActionResult WaitingPage(User user)
        {
            if (LogInControl(user))
            {
                SynchronousSocketClient.StartClient(JsonConvert.SerializeObject(user));
                return View();
            }
            else
            {
                return View("LogIn", user);
            }
        }
        public IActionResult ChessBoard(User user)
        {
            if (user.Side== Models.Enum.Side.NotAssigned)
            {
                return View ("WaitingPage",user);
            }
            return View();
        }//ChessBoard for admitted player
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public void ToServerSoket(string toServer)
        {
            SynchronousSocketClient.StartClient(toServer);


        }

    }

}
