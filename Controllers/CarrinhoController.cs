using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.MecanDB.Models;
using API.MecanDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.MecanDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService carrinhoService;

        public CarrinhoController(CarrinhoService carrinhoService)
        {
            this.carrinhoService = carrinhoService ?? throw new ArgumentNullException(nameof(carrinhoService));
        }

        [HttpPost]
        public IActionResult AdicionarCarrinho(Carrinho carrinho)
        {
            try
            {
                carrinhoService.AdicionarCarrinho(carrinho);
                return Ok("Carrinho adicionado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao adicionar carrinho: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarCarrinho(int id, Carrinho carrinho)
        {
            try
            {
                var carrinhoExistente = carrinhoService.ObterCarrinhoPorId(id);
                if (carrinhoExistente == null)
                {
                    return NotFound("Carrinho não encontrado");
                }

                carrinho.ID = id;
                carrinhoService.Atualizar(carrinho);
                return Ok("Carrinho atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar carrinho: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCarrinho(int id)
        {
            try
            {
                var carrinhoExistente = carrinhoService.ObterCarrinhoPorId(id);
                if (carrinhoExistente == null)
                {
                    return NotFound("Carrinho não encontrado");
                }

                carrinhoService.Excluir(carrinhoExistente);
                return Ok("Carrinho excluído com sucesso!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao excluir carrinho: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterCarrinhoPorId(int id)
        {
            try
            {
                var carrinho = carrinhoService.ObterCarrinhoPorId(id);
                if (carrinho == null)
                {
                    return NotFound("Carrinho não encontrado");
                }

                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter carrinho por ID: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult ObterTodosCarrinhos()
        {
            try
            {
                var carrinhos = carrinhoService.ObterTodosCarrinhos();
                return Ok(carrinhos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter todos os carrinhos: {ex.Message}");
            }
        }
    }
}
