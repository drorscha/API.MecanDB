using System;
using System.Collections.Generic;
using API.MecanDB.Models;
using API.MecanDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.MecanDB.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService clienteService;

        public ClienteController(ClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet("{clienteId}")]
        public ActionResult<Cliente> ObterClientePorId(int clienteId)
        {
            try
            {
                var cliente = clienteService.ObterClientePorId(clienteId);

                if (cliente == null)
                {
                    return NotFound(); // Retorna 404 se o cliente não for encontrado
                }

                return Ok(cliente); // Retorna 200 com o cliente se encontrado
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro interno
            }
        }

        [HttpGet]
        public ActionResult<List<Cliente>> ObterTodosClientes()
        {
            try
            {
                var clientes = clienteService.ObterTodosClientes();
                return Ok(clientes); // Retorna 200 com a lista de clientes
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro interno
            }
        }

        [HttpPost]
        public ActionResult InserirCliente([FromBody] Cliente cliente)
        {
            try
            {
                clienteService.InserirCliente(cliente);
                return Ok(); // Retorna 200 se o cliente for inserido com sucesso
            }
            catch (Exception ex)
            {
                // Retornar a mensagem completa da exceção para análise
                return StatusCode(500, $"Erro interno: {ex.ToString()}"); // Retorna 500 em caso de erro interno
            }
        }

        [HttpPut("{clienteId}")]
        public ActionResult AtualizarCliente(int clienteId, [FromBody] Cliente cliente)
        {
            try
            {
                if (clienteId != cliente.ID) // Ajuste aqui para utilizar o atributo correto do modelo
                {
                    return BadRequest("IDs de cliente inconsistentes"); // Retorna 400 se os IDs forem inconsistentes
                }

                clienteService.AtualizarCliente(cliente);
                return Ok(); // Retorna 200 se o cliente for atualizado com sucesso
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.ToString()}"); // Retorna 500 em caso de erro interno
            }
        }

        [HttpDelete("{clienteId}")]
        public ActionResult ExcluirCliente(int clienteId)
        {
            try
            {
                clienteService.ExcluirCliente(clienteId);
                return Ok(); // Retorna 200 se o cliente for excluído com sucesso
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}"); // Retorna 500 em caso de erro interno
            }
        }
    }
}
