-- Tabela STATUS_PEDIDO
CREATE TABLE StatusPedidos
(
    StatusPedidoId INTEGER PRIMARY KEY,
    Descricao VARCHAR(255)
);

-- Tabela CATEGORIA
CREATE TABLE Categorias
(
    CategoriaId INTEGER PRIMARY KEY,
    Nome VARCHAR(50),
    Descricao VARCHAR(255),
    Ativa BIT
);

-- Tabela ENDERECO
CREATE TABLE Enderecos
(
    EnderecoId INTEGER PRIMARY KEY,
    Logradouro VARCHAR(100),
    Numero VARCHAR(10),
    Complemento VARCHAR(50),
    Bairro VARCHAR(50),
    Cidade VARCHAR(50),
    Estado VARCHAR(50),
    CEP VARCHAR(10)
);

-- Tabela CLIENTE
CREATE TABLE Clientes
(
    ClienteId INTEGER PRIMARY KEY,
    Nome VARCHAR(255),
    CPF VARCHAR(15),
    Email VARCHAR(70),
    Senha VARCHAR(25),
    EnderecoId INTEGER,
    FOREIGN KEY (EnderecoId) REFERENCES Enderecos(EnderecoId)
);

-- Tabela VENDEDOR
CREATE TABLE Vendedores
(
    VendedorId INTEGER PRIMARY KEY,
    RazaoSocial VARCHAR(100),
    NomeFantasia VARCHAR(70),
    Email VARCHAR(70),
    Senha VARCHAR(25),
    CNPJ VARCHAR(18),
    Comissao DECIMAL(5,2),
    EnderecoId INTEGER,
    FOREIGN KEY (EnderecoId) REFERENCES Enderecos(EnderecoId)
);

-- Tabela PRODUTO
CREATE TABLE Produtos
(
    ProdutoId INTEGER PRIMARY KEY,
    Descricao VARCHAR(255),
    Preco DECIMAL(7,2),
    Imagem VARCHAR(200),
    Status BIT,
    VendedorId INTEGER,
    CategoriaId INTEGER,
    FOREIGN KEY (VendedorId) REFERENCES Vendedores(VendedorId),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(CategoriaId)
);

-- Tabela CARRINHO
CREATE TABLE Carrinhos
(
    CarrinhoId INTEGER PRIMARY KEY,
    DataPedido DATE,
    ValorTotal DECIMAL(7,2),
    StatusPedidoId INTEGER,
    ClienteId INTEGER,
    FOREIGN KEY (StatusPedidoId) REFERENCES StatusPedidos(StatusPedidoId),
    FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

-- Tabela ITEM_CARRINHO
CREATE TABLE Item_Carrinhos
(
    ItemCarrinhoId INTEGER PRIMARY KEY,
    Quantidade INTEGER,
    Total DECIMAL(7,2),
    CarrinhoId INTEGER,
    ProdutoId INTEGER,
    FOREIGN KEY (CarrinhoId) REFERENCES Carrinhos(CarrinhoId),
    FOREIGN KEY (ProdutoId) REFERENCES Produtos(ProdutoId)
);