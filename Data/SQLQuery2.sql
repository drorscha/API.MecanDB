-- Inserir dados na tabela StatusPedidos
INSERT INTO StatusPedidos (StatusPedidoId, Descricao) VALUES
(1, 'Em processamento'),
(2, 'Aguardando pagamento'),
(3, 'Pago'),
(4, 'Enviado'),
(5, 'Entregue');

-- Inserir dados na tabela Categorias
INSERT INTO Categorias (CategoriaId, Nome, Descricao, Ativa) VALUES
(1, 'Eletrônicos', 'Produtos eletrônicos', 1),
(2, 'Roupas', 'Roupas masculinas e femininas', 1),
(3, 'Alimentos', 'Produtos alimentícios', 1);

-- Inserir dados na tabela Enderecos
INSERT INTO Enderecos (EnderecoId, Logradouro, Numero, Complemento, Bairro, Cidade, Estado, CEP) VALUES
(1, 'Rua A', '123', 'APTO 101', 'Centro', 'São Paulo', 'SP', '12345-678'),
(2, 'Avenida B', '456', '', 'Centro', 'Rio de Janeiro', 'RJ', '98765-432');

-- Inserir dados na tabela Clientes
INSERT INTO Clientes (ClienteId, Nome, CPF, Email, Senha, EnderecoId) VALUES
(1, 'João Silva', '123.456.789-00', 'joao@email.com', 'senha123', 1),
(2, 'Maria Souza', '987.654.321-00', 'maria@email.com', 'senha456', 2);

-- Inserir dados na tabela Vendedores
INSERT INTO Vendedores (VendedorId, RazaoSocial, NomeFantasia, Email, Senha, CNPJ, Comissao, EnderecoId) VALUES
(1, 'Empresa XYZ', 'XYZ', 'contato@xyz.com', 'senhaXYZ', '00.000.000/0000-00', 10.00, 1),
(2, 'Empresa ABC', 'ABC', 'contato@abc.com', 'senhaABC', '11.111.111/1111-11', 8.50, 2);

-- Inserir dados na tabela Produtos
INSERT INTO Produtos (ProdutoId, Descricao, Preco, Imagem, Status, VendedorId, CategoriaId) VALUES
(1, 'Smartphone', 1500.00, 'smartphone.jpg', 1, 1, 1),
(2, 'Camiseta', 35.00, 'camiseta.jpg', 1, 2, 2);

-- Inserir dados na tabela Carrinhos
INSERT INTO Carrinhos (CarrinhoId, DataPedido, ValorTotal, StatusPedidoId, ClienteId) VALUES
(1, '2023-11-01', 1500.00, 3, 1),
(2, '2023-11-05', 35.00, 3, 2);

-- Inserir dados na tabela Item_Carrinhos
INSERT INTO Item_Carrinhos (ItemCarrinhoId, Quantidade, Total, CarrinhoId, ProdutoId) VALUES
(1, 1, 1500.00, 1, 1),
(2, 2, 70.00, 2, 2);