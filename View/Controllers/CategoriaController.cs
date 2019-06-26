using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private CategoriaRepository repository;

        public CategoriaController()
        {
            repository = new CategoriaRepository();
        }

        // GET: Categoria
        public ActionResult Index()
        {
            List<Categoria> categorias = repository.ObterTodos();
            ViewBag.Categorias = categorias;
            return View();
        }
    }
}