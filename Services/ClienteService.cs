using API.MecanDB.Models;
using API.MecanDB.Repositories;
using System;
using System.Collections.Generic;

namespace API.MecanDB.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository clienteRepository;

        public ClienteService(ClienteRepository clienteRepository)
        {
            this.clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        public List<Cliente> ObterTodosClientes()
        {
            try
            {
                var clientes = clienteRepository.ObterTodosClientes();

                if (clientes.Count == 0)
                {
                    throw new Exception("Nenhum cliente encontrado.");
                }

                return clientes;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter todos os clientes.", ex);
            }
        }

        public Cliente ObterClientePorId(int clienteId)
        {
            try
            {
                var cliente = clienteRepository.ObterClientePorId(clienteId);

                if (cliente == null)
                {
                    throw new Exception("Cliente não encontrado para o ID fornecido.");
                }

                return cliente;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter o cliente com ID {clienteId}.", ex);
            }
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                // Exemplo de validação simples: Verificar se o cliente já existe
                var clienteExistente = clienteRepository.ObterClientePorId(cliente.ID);
                if (clienteExistente != null)
                {
                    throw new Exception("Cliente já existe.");
                }

                clienteRepository.InserirCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir o cliente.", ex);
            }
        }

        public void AtualizarCliente(Cliente cliente)
        {
            try
            {
                // Exemplo de validação simples: Verificar se o cliente existe antes de atualizar
                var clienteExistente = clienteRepository.ObterClientePorId(cliente.ID);
                if (clienteExistente == null)
                {
                    throw new Exception("Cliente não encontrado para atualização.");
                }

                clienteRepository.AtualizarCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o cliente.", ex);
            }
        }

        public void ExcluirCliente(int clienteId)
        {
            try
            {
                // Exemplo de validação simples: Verificar se o cliente existe antes de excluir
                var clienteExistente = clienteRepository.ObterClientePorId(clienteId);
                if (clienteExistente == null)
                {
                    throw new Exception("Cliente não encontrado para exclusão.");
                }

                clienteRepository.ExcluirCliente(clienteId);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir o cliente.", ex);
            }
        }
    }
}
