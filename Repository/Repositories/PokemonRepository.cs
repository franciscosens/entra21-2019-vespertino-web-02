using Model;
using Repository.Database;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        public bool Alterar(Pokemon pokemon)
        {
            throw new NotImplementedException();
        }

        public bool Apagar(int id)
        {
            throw new NotImplementedException();
        }

        public int Inserir(Pokemon pokemon)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"INSERT INTO pokemons 
(id_categoria, nome)
OUTPUT INSERTED.ID
VALUES (@ID_CATEGORIA, @NOME)";
            comando.Parameters.AddWithValue("@ID_CATEGORIA", 
                pokemon.IdCategoria);
            comando.Parameters.AddWithValue("@NOME", pokemon.Nome);
            int id = Convert.ToInt32(comando.ExecuteScalar());
            comando.Connection.Close();
            return id;
        }

        public Pokemon ObterPeloId(int id)
        {
            throw new NotImplementedException();
        }

        public List<Pokemon> ObterTodos(string pesquisa)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"SELECT 
categorias.id AS 'CategoriaId',
categorias.nome AS 'CategoriaNome', 
pokemons.id AS 'Id',
pokemons.nome AS 'Nome'
FROM pokemons 
INNER JOIN categorias ON(pokemons.id_categoria = categorias.id)";

            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();
            List<Pokemon> pokemons = new List<Pokemon>();
            foreach(DataRow linha in tabela.Rows){
                Pokemon pokemon = new Pokemon();
                pokemon.Id = Convert.ToInt32(linha["Id"]);
                pokemon.Nome = linha["Nome"].ToString();
                pokemon.IdCategoria = Convert.ToInt32(linha["CategoriaId"]);
                pokemon.Categoria = new Categoria();
                pokemon.Categoria.Id = Convert.ToInt32(linha["CategoriaId"]);
                pokemon.Categoria.Nome = linha["CategoriaNome"].ToString();
                pokemons.Add(pokemon);
            }
            return pokemons;
        }
    }
}
