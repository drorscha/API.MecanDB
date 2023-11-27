using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using API.MecanDB.Models;

namespace API.MecanDB.Repositories
{
    public class ClienteRepository
    {
        private readonly string connectionString;

        public ClienteRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }

        public Cliente ObterClientePorId(int clienteId)
        {
            Cliente cliente = null;

            using (SqlConnection connection = CreateConnection())
            {
                string query = "SELECT * FROM Cliente WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", clienteId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cliente = new Cliente
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                CPF = Convert.ToInt64(reader["CPF"]),
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Endereco_ID = Convert.ToInt32(reader["Endereco_ID"])
                            };
                        }
                    }
                }
            }

            return cliente;
        }

        public List<Cliente> ObterTodosClientes()
        {
            List<Cliente> clientes = new List<Cliente>();

            using (SqlConnection connection = CreateConnection())
            {
                string query = "SELECT * FROM Cliente";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clientes.Add(new Cliente
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                CPF = Convert.ToInt64(reader["CPF"]), // CPF como long
                                Email = reader["Email"].ToString(),
                                Senha = reader["Senha"].ToString(),
                                Endereco_ID = Convert.ToInt32(reader["Endereco_ID"])
                            });
                        }
                    }
                }
            }

            return clientes;
        }

        public void InserirCliente(Cliente cliente)
        {
            try
            {
                string query = "INSERT INTO Cliente (Nome, CPF, Email, Senha, Endereco_ID) VALUES (@Nome, @CPF, @Email, @Senha, @Endereco_ID)";

                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", cliente.Nome);
                        command.Parameters.AddWithValue("@CPF", cliente.CPF);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Senha", cliente.Senha);
                        command.Parameters.AddWithValue("@Endereco_ID", cliente.Endereco_ID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
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
                string query = "UPDATE Cliente SET Nome = @Nome, CPF = @CPF, Email = @Email, Senha = @Senha, Endereco_ID = @NovoEndereco_ID WHERE ID = @ClienteID";

                using (SqlConnection connection = CreateConnection())
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Nome", cliente.Nome);
                        command.Parameters.AddWithValue("@CPF", cliente.CPF);
                        command.Parameters.AddWithValue("@Email", cliente.Email);
                        command.Parameters.AddWithValue("@Senha", cliente.Senha);
                        command.Parameters.AddWithValue("@NovoEndereco_ID", cliente.Endereco_ID);
                        command.Parameters.AddWithValue("@ClienteID", cliente.ID);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o cliente.", ex);
            }
        }


        public void ExcluirCliente(int clienteId)
        {
            using (SqlConnection connection = CreateConnection())
            {
                string query = "DELETE FROM Cliente WHERE ID = @ClienteId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClienteId", clienteId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
