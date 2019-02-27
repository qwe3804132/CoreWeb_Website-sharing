﻿using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CoreWebUI.Areas.Admin.Controllers
{
    public class ListCategoryController : Controller
    {
        private AdminBs objBs;
        public ListCategoryController()
        {
            objBs = new AdminBs();
        }
        // GET: Admin/ListCateory
        public ActionResult Index(string SortOrder, string SortBy, string Page)
        {
            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            var categories = objBs.categoryBs.GetAll();

            switch (SortOrder)
            {
                case "Asc":
                    categories = categories.OrderBy(x => x.CategoryName).ToList();
                    break;
                case "Desc":
                    categories = categories.OrderByDescending(x => x.CategoryName).ToList();
                    break;
                default:
                    break;
            }




            ViewBag.TotalPages = Math.Ceiling(objBs.categoryBs.GetAll().Count() / 5.0);
            int page = int.Parse(Page == null ? "1" : Page);
            ViewBag.Page = page;
            categories = categories.Skip((page - 1) * 10).Take(10);//logic for showing pagely
            return View(categories);
        }
        public ActionResult Delete(int id)
        {
            try
            {
                objBs.categoryBs.Delete(id);
                TempData["Msg"] = "Deleted Successfully";
                return RedirectToAction("Index");
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Deleted Failed" + e1.Message;

                return RedirectToAction("Index");

            }

        }
    }
}