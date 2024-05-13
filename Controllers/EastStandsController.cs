﻿using Microsoft.AspNetCore.Mvc;
using Tazaker.Models;
using Tazkara.IRepository;
using Tazkara.ViewModels;

namespace Tazkara.Controllers
{
    public class EastStandsController : Controller
    {
        IEastStands repository;
        public EastStandsController(IEastStands repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Seets = repository.GetAll();
            return View(Seets);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Stadiums"]=repository.GetAllStadiums();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EastStandsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var ESVM = new EastStands()
                {
                    Capacity = viewModel.Capacity,
                    Cost = viewModel.Cost,
                    Name = viewModel.Name,
                    StadiumId = viewModel.StadiumId,
                };
                repository.Create(ESVM);
                return RedirectToAction("Index");
            }
            return View("Create");
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var st = repository.GetById(id);
            if (st != null)
            {
                var stvm = new EastStandsViewModel()
                {
                    Capacity = st.Capacity,
                    Cost = st.Cost,
                    Name = st.Name,
                    StadiumId = st.StadiumId,
                };
                return View(stvm);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(EastStandsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var ESVM = repository.GetById(viewModel.Id);
                if (ESVM != null)
                {
                    repository.Update(ESVM);
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            return View("Update");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var st = repository.GetById(id);
            if (st != null)
            {
                repository.Delete(id); return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
