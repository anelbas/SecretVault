﻿//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace SecretVaultAPI.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class PostsController : Controller
//    {
//        // GET: Posts
//        public ActionResult Index()
//        {
//            return View();
//        }

//        // GET: Posts/Details/5
//        [HttpGet]
//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        // GET: Posts/Create
//        [HttpPost("post/{id}")]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Posts/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Posts/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }

//        // POST: Posts/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: Posts/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: Posts/Delete/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id, IFormCollection collection)
//        {
//            try
//            {
//                return RedirectToAction(nameof(Index));
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
