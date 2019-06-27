using Model;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace View.Controllers
{
    public class PokemonController : Controller
    {
        private PokemonRepository repositorio;

        public PokemonController()
        {
            repositorio = new PokemonRepository();
            repositorio.ObterTodos("");
        }

        // GET: Pokemon
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastro()
        {
            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Store(int idCategoria, string nome)
        {
            Pokemon pokemon = new Pokemon();
            pokemon.IdCategoria = idCategoria;
            pokemon.Nome = nome;
            repositorio.Inserir(pokemon);
            return RedirectToAction("Index");
        }

    }

}