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

        public ActionResult Index()
        {
            List<Pokemon> pokemons = repositorio.ObterTodos("");
            ViewBag.Pokemons = pokemons;
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

        public ActionResult Apagar(int id)
        {
            repositorio.Apagar(id);
            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id)
        {
            Pokemon pokemon = repositorio.ObterPeloId(id);
            ViewBag.Pokemon = pokemon;

            CategoriaRepository categoriaRepository = new CategoriaRepository();
            List<Categoria> categorias = categoriaRepository.ObterTodos();
            ViewBag.Categorias = categorias;

            return View();
        }

        public ActionResult Update(int id, string nome, int idCategoria)
        {
            Pokemon pokemon = new Pokemon();
            pokemon.Id = id;
            pokemon.Nome = nome;
            pokemon.IdCategoria = idCategoria;
            repositorio.Alterar(pokemon);

            return RedirectToAction("Index");
        }

    }

}