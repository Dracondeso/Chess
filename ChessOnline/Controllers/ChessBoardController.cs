using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using ChessOnline.Models;
using ChessOnline.Models.Board;
using ChessOnline.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ChessOnline.Controllers
{
    public class ChessBoardController : Controller
    {
        public Socket SocketClient;
        public static List<User> Users;
        public static User SetUser(User user)
        {
            if (user.Side == Side.White)
            {
                if (Users.Count == 0)
                    Users.Add(user);
                else
                    Users[0] = user;
                return Users[0];
            }
            if (user.Side == Side.Black)
            {
                if (Users.Count == 1)
                    Users.Add(user);
                else
                    Users[1] = user;
                return Users[1];
            }
            return user;
        }
        // GET: ChessBoard
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChessBoard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChessBoard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChessBoard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChessBoard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChessBoard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ChessBoard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChessBoard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}