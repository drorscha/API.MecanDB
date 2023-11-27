using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using API.MecanDB.Models;

namespace API.MecanDB.Repositories
{
    public class CarrinhoRepository
    {
        private readonly string connectionString;

        public CarrinhoRepository(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public SqlConnection CreateConnection() 
        {
            return new SqlConnection(connectionString);
        }

        public void AddParameters(SqlCommand command, Carrinho carrinho) 
        {
            command.Parameters.AddWithValue("@ID", carrinho.ID);
            command.Parameters.AddWithValue("@Data_Pedido", carrinho.Data_Pedido);
            command.Parameters.AddWithValue("@Valor_Total", carrinho.Valor_Total);
            
            command.Parameters.AddWithValue("@Status_Pedido_ID", carrinho.Status_Pedido_ID);
            command.Parameters.AddWithValue("@Cliente_ID", carrinho.Cliente_ID);
        }

        public void Adicionar(Carrinho carrinho)
        {
            using (SqlConnection connection = CreateConnection())
            {
                try
                {
                    connection.Open();

                    string query = "INSERT INTO Carrinho (ID, Data_Pedido, Valor_Total, Status_Pedido_ID, Cliente_ID) " +
                                   "VALUES (@ID, @Data_Pedido, @Valor_Total, @Status_Pedido_ID, @Cliente_ID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adicione os parâmetros com os tipos de dados corretos
                        command.Parameters.AddWithValue("@ID", carrinho.ID);
                        command.Parameters.AddWithValue("@Data_Pedido", carrinho.Data_Pedido);
                        command.Parameters.AddWithValue("@Valor_Total", carrinho.Valor_Total);
                        command.Parameters.AddWithValue("@Status_Pedido_ID", carrinho.Status_Pedido_ID);
                        command.Parameters.AddWithValue("@Cliente_ID", carrinho.Cliente_ID);

                        command.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Erro SQL: {ex.Message}");
                    // Aqui você pode adicionar mais lógica de tratamento para exceções específicas do SQL Server
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    // Aqui você pode adicionar um tratamento genérico para outras exceções
                }
            }
        }


        public void Atualizar(Carrinho carrinho)
        {
            using (SqlConnection connection = CreateConnection())
            {
                string query = "UPDATE Carrinho SET Data_Pedido = @Data_Pedido, Valor_Total = @Valor_Total, Status_Pedido_ID = @Status_Pedido_ID, Cliente_ID = @Cliente_ID WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    AddParameters(command, carrinho);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Erro SQL ao atualizar carrinho: {ex.Message}");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao atualizar carrinho: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public void Excluir(Carrinho carrinho)
        {
            using (SqlConnection connection = CreateConnection())
            {
                // Verificar se há itens relacionados na tabela ItemCarrinho antes de excluir o carrinho
                bool hasRelatedItems = CheckForRelatedItems(carrinho.ID);

                if (hasRelatedItems)
                {
                    // Aqui você pode optar por excluir os itens relacionados, atualizá-los ou lidar de outra forma
                    // Por exemplo:
                    // DeleteRelatedItems(carrinho.ID);
                    // Ou
                    // UpdateRelatedItems(carrinho.ID);
                    // ...
                    // Ou lançar uma exceção indicando que não é possível excluir o carrinho neste momento

                    Console.WriteLine("Não é possível excluir o carrinho pois existem itens relacionados na tabela ItemCarrinho.");
                    return;
                }

                string query = "DELETE FROM Carrinho WHERE ID = @ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", carrinho.ID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Erro SQL ao excluir carrinho: {ex.Message}");
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao excluir carrinho: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        private bool CheckForRelatedItems(int carrinhoId)
        {
            using (SqlConnection connection = CreateConnection())
            {
                string query = "SELECT COUNT(*) FROM ItemCarrinho WHERE Carrinho_ID = @Carrinho_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Carrinho_ID", carrinhoId);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }


        private Carrinho MapDataReaderToCarrinho(SqlDataReader reader)
        {
            return new Carrinho
            {
                ID = Convert.ToInt32(reader["ID"]),
                Data_Pedido = Convert.ToDateTime(reader["Data_Pedido"]),
                Valor_Total = Convert.ToDecimal(reader["Valor_Total"]),
                
                Status_Pedido_ID = Convert.ToInt32(reader["Status_Pedido_ID"]),
                Cliente_ID = Convert.ToInt32(reader["Cliente_ID"])
            };
        }

        
        private List<Carrinho> ObterCarrinhos(string query, Dictionary<string, object> parameters = null)
        {
            List<Carrinho> carrinhos = new List<Carrinho>();

            using (SqlConnection connection = CreateConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Carrinho carrinho = MapDataReaderToCarrinho(reader);
                                carrinhos.Add(carrinho);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Erro SQL: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro: {ex.Message}");
                    }
                }
            }

            return carrinhos;
        }

        public Carrinho ObterPorId(int id)
        {
            string query = "SELECT * FROM Carrinho WHERE ID = @ID";
            var parameters = new Dictionary<string, object> { { "@ID", id } };
            List<Carrinho> carrinhos = ObterCarrinhos(query, parameters);

            return carrinhos.FirstOrDefault();
        }

        public List<Carrinho> ObterTodos()
        {
            string query = "SELECT * FROM Carrinho";
            return ObterCarrinhos(query);
        }
    }
}
