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
        internal static DataModel DataClient = new DataModel();
        private string DbPsw1 = "1234";
        private string DbUser1 = "User1";
        private string DbPsw2 = "1234";
        private string DbUser2 = "User2";
        public IActionResult Index(User user)
        {
            if (LogInControl(user))
            {
                return View();
            }
            else
            {
                return View("LogIn", user);
            }
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
                options.SameSite = SameSiteMode.Lax;
                DataClient.User = user;
                DataClient.serverOperation = Models.Enum.ServerOperationType.LogInOperation;
                string json = JsonConvert.SerializeObject(DataClient);
                HttpContext.Response.Cookies.Append("AuthCookie", json, options);

                return true;
            }
            return false;
        }
        public IActionResult WaitingPage()
        {
            ClientServer clientServer = new ClientServer();
            clientServer.SetUp();
            clientServer.ClientSendData();
          //  HttpContext.Response.Cookies.Append("AuthCookie", stringFromServer);
           // DataClient = JsonConvert.DeserializeObject<DataModel>(stringFromServer);
            return View();
        }
        public IActionResult ChessBoard()
        {
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


    }

}
