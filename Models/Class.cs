using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.MecanDB.Models
{
    public class Endereco
    {
        public int ID { get; set; }
        // Outras propriedades do endereço, como Rua, Cidade, etc.
    }

    public class Categoria
    {
        public int ID { get; set; }
        // Outras propriedades da categoria, se houver
    }

    public class Vendedor
    {
        public int ID { get; set; }
        public string Razao_Social { get; set; }
        public string Nome_Fantasia { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CNPJ { get; set; }
        public decimal Comissao { get; set; }
        public int Endereco_ID { get; set; }
    }

    public class Produto
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public string Imagem { get; set; }
        public string Status { get; set; }
        public int Vendedor_ID { get; set; }
        public int Categoria_ID { get; set; }
    }

    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public long CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int Endereco_ID { get; set; }
    }

    public class Carrinho
    {
        public int ID { get; set; }
        public DateTime Data_Pedido { get; set; }
        public decimal Valor_Total { get; set; }
        public int Status_Pedido_ID { get; set; }
        public int Cliente_ID { get; set; }
    }

    public class ItemCarrinho
    {
        public int ID { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get; set; }
        public int Carrinho_ID { get; set; }
        public int Produto_ID { get; set; }
    }

}

