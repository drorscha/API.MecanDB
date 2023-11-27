using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.MecanDB.Models;
using API.MecanDB.Repositories;

namespace API.MecanDB.Services
{
    public class CarrinhoService
    {
        private readonly CarrinhoRepository carrinhoRepository;

        public CarrinhoService(CarrinhoRepository carrinhoRepository)
        {
            this.carrinhoRepository = carrinhoRepository ?? throw new ArgumentNullException(nameof(carrinhoRepository));
        }

        public void AdicionarCarrinho(Carrinho carrinho)
        {
            try
            {
                carrinhoRepository.Adicionar(carrinho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar carrinho: {ex.Message}");
                throw;
            }
        }

        public void Excluir(Carrinho carrinho)
        {
            try
            {
                carrinhoRepository.Excluir(carrinho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir carrinho: {ex.Message}");
                throw;
            }
        }

        public void Atualizar(Carrinho carrinho)
        {
            try
            {
                carrinhoRepository.Atualizar(carrinho);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar carrinho: {ex.Message}");
                throw;
            }
        }

        public Carrinho ObterCarrinhoPorId(int id)
        {
            try
            {
                return carrinhoRepository.ObterPorId(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter carrinho por ID: {ex.Message}");
                throw;
            }
        }

        public List<Carrinho> ObterTodosCarrinhos()
        {
            try
            {
                return carrinhoRepository.ObterTodos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter todos os carrinhos: {ex.Message}");
                throw;
            }
        }
    }
}
