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
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = @"UPDATE pokemons SET
nome = @NOME,
id_categoria = @ID_CATEGORIA
WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", pokemon.Id);
            comando.Parameters.AddWithValue("@NOME", pokemon.Nome);
            comando.Parameters.AddWithValue("@ID_CATEGORIA",
                pokemon.IdCategoria);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;

        }

        public bool Apagar(int id)
        {
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "DELETE FROM pokemons WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            int quantidade = comando.ExecuteNonQuery();
            comando.Connection.Close();
            return quantidade == 1;
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
            SqlCommand comando = Conexao.AbrirConexao();
            comando.CommandText = "SELECT * FROM pokemons WHERE id = @ID";
            comando.Parameters.AddWithValue("@ID", id);
            DataTable tabela = new DataTable();
            tabela.Load(comando.ExecuteReader());
            comando.Connection.Close();

            if(tabela.Rows.Count == 0)
            {
                return null;
            }

            DataRow linha = tabela.Rows[0];
            Pokemon pokemon = new Pokemon();
            pokemon.Id = Convert.ToInt32(linha["id"]);
            pokemon.IdCategoria = Convert.ToInt32(linha["id_categoria"]);
            pokemon.Nome = linha["nome"].ToString();
            return pokemon;
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
