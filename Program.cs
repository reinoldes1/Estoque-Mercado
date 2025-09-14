using System;
using System.Text.RegularExpressions;
using Mercado.Models;
using Mercado.Data;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            DataContextDapper dapper = new DataContextDapper(config);

            Produto myProduto = new Produto
            {
                Nome = "Queijo",
                Preco = 150.99m,
                Quantidade = 250,
                Categoria = "Alimentos"
            };

            string sql = @"INSERT INTO EstoqueSchema.Estoque(Nome, Preco, Quantidade, Categoria) 
            VALUES('" + myProduto.Nome
            + "','" + myProduto.Preco
            + "','" + myProduto.Quantidade
            + "','" + myProduto.Categoria
            + "')";

            string sqlSelect = @"SELECT
                Estoque.ProdutoId,
                Estoque.Nome,
                Estoque.Preco,
                Estoque.Quantidade,
                Estoque.Categoria 
            FROM EstoqueSchema.Estoque";

            int result = dapper.ExecuteSqlWithRowCount(sql);

            IEnumerable<Produto> produtos = dapper.LoadData<Produto>(sqlSelect);

            Console.WriteLine("|ID | Nome | Preco | Quantidade | Categoria");
            foreach (Produto singleProduto in produtos)
            {
                Console.WriteLine("|" + singleProduto.ProdutoId
                + "|" + singleProduto.Nome
                + "|" + singleProduto.Preco
                + "|" + singleProduto.Quantidade
                + "|" + singleProduto.Categoria
                + "|");
            }

            

        }
    }
}