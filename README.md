

# Auxiliar Contábil - EM ANDAMENTO - [![Deploy Application](https://github.com/RodrigoSousa19/AuxiliarContabil/actions/workflows/deploy.yml/badge.svg)](https://github.com/RodrigoSousa19/AuxiliarContabil/actions/workflows/deploy.yml)
## Objetivo
Esta api tem o objetivo de auxiliar com cálculos básicos para qualquer pessoa que tenha ME, para facilitar os cálculos de custos com impostos como DAS e GPS, além de também informar o Pró Labore e uma estimativa de salário liquido após todos os cálculos.

# Banco de Dados e Variáveis de Ambiente
## Banco de Dados
Para a base de dados, deixarei os scripts para todas as tabelas necessárias para que a aplicação funcione corretamente na pasta Scripts.

## Variáveis de ambiente
Para que tudo funcione corretamente, você precisará criar uma variável de ambiente chamada: MSSQL_CONNECTION_STRING
esta variável será responsável por armazenar a string de conexão com o seu banco de dados, para criar esta variável é simples.
basta seguir estes passos:

No windows:
Abra o powershell como administrador e cole o seguinte comando: 

```sh
setx MSSQL_CONNECTION_STRING SUA_STRING_DE_CONEXAO
```

No Linux:
No CMD faça os seguintes comandos:

```sh
sudo nano /etc/environment
MSSQL_CONNECTION_STRING="SUA_STRING_DE_CONEXAO"
source /etc/environment
```
Verifique com o comando para saber se a string de conexão está sendo retornada corretamente.
```sh
echo $MSSQL_CONNECTION_STRING 
```
## Criando o container

Após criar toda a estrutura necessária para a API, basta abrir o CMD na raiz do projeto, onde o arquivo compose.yaml está e digitar o seguinte comando:

```sh
docker-compose up --build -d
```
e pronto, a api vai estar disponível na porta 5000 do seu host, pronta para uso.

# Controllers

## ComposicaoSalarialController

### Descrição
A `ComposicaoSalarialController` gerencia as operações relacionadas à composição salarial, permitindo a criação, leitura, atualização e exclusão de registros relacionados ao faturamento da empresa.

### Endpoints

#### 1. `GET api/composicaosalarial`
- **Descrição**: Retorna todas as composições salariais.
- **Retorno**: 
  - **200 OK**: Uma lista de `ComposicaoSalarioDto`.

#### 2. `GET api/composicaosalarial/{id}`
- **Descrição**: Retorna uma composição salarial específica pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID da composição salarial.
- **Retorno**: 
  - **200 OK**: Um objeto `ComposicaoSalarioDto` se encontrado.
  - **404 Not Found**: Se a composição salarial não existir.

#### 3. `POST api/composicaosalarial`
- **Descrição**: Cria uma nova composição salarial.
- **Corpo da Requisição**: Um objeto `ComposicaoSalarioDto`.
- **Retorno**: 
  - **201 Created**: O objeto criado, com o local para acessar a nova composição.

#### 4. `PUT api/composicaosalarial/{id}`
- **Descrição**: Atualiza uma composição salarial existente.
- **Parâmetros**: 
  - `id` (int): O ID da composição salarial.
- **Corpo da Requisição**: Um objeto `ComposicaoSalarioDto`.
- **Retorno**: 
  - **204 No Content**: Se a atualização for bem-sucedida.
  - **400 Bad Request**: Se o ID não corresponder ao ID do objeto no corpo da requisição.

#### 5. `DELETE api/composicaosalarial/{id}`
- **Descrição**: Exclui uma composição salarial pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID da composição salarial.
- **Retorno**: 
  - **204 No Content**: Se a exclusão for bem-sucedida.

#### 6. `PUT api/composicaosalarial/TrocarComposicaoAtual/{id}`
- **Descrição**: Atualiza a composição atual para a composição salarial especificada pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID da composição salarial.
- **Retorno**: 
  - **204 No Content**: Se a atualização for bem-sucedida.

---

## DasController

### Descrição
A `DasController` gerencia as operações relacionadas ao Documento de Arrecadação do Simples Nacional (DAS), no momento apenas controla as Faixas para este documento.

### Endpoints

#### 1. `GET api/das`
- **Descrição**: Retorna todos os registros de DAS.
- **Retorno**: 
  - **200 OK**: Uma lista de `DasDto`.

#### 2. `GET api/das/{id}`
- **Descrição**: Retorna um registro de DAS específico pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID do DAS.
- **Retorno**: 
  - **200 OK**: Um objeto `DasDto` se encontrado.
  - **404 Not Found**: Se o DAS não existir.

#### 3. `POST api/das`
- **Descrição**: Cria um novo registro de DAS.
- **Corpo da Requisição**: Um objeto `DasDto`.
- **Retorno**: 
  - **201 Created**: O objeto criado, com o local para acessar o novo DAS.

#### 4. `PUT api/das/{id}`
- **Descrição**: Atualiza um registro de DAS existente.
- **Parâmetros**: 
  - `id` (int): O ID do DAS.
- **Corpo da Requisição**: Um objeto `DasDto`.
- **Retorno**: 
  - **204 No Content**: Se a atualização for bem-sucedida.

#### 5. `DELETE api/das/{id}`
- **Descrição**: Exclui um registro de DAS pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID do DAS.
- **Retorno**: 
  - **204 No Content**: Se a exclusão for bem-sucedida.

---

## ExtratoBancarioController

### Descrição
A `ExtratoBancarioController` gerencia as operações relacionadas ao extrato bancário da conta PJ da empresa, esta controller ainda está em construção e futuramente poderá fazer o processamento de arquivos OFX (semelhante a um XML)

### Endpoints

#### 1. `GET api/extratobancario`
- **Descrição**: Retorna todos os extratos bancários.
- **Retorno**: 
  - **200 OK**: Uma lista de `ExtratoBancarioPessoaJuridicaDTO`.

#### 2. `GET api/extratobancario/{id}`
- **Descrição**: Retorna um extrato bancário específico pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID do extrato bancário.
- **Retorno**: 
  - **200 OK**: Um objeto `ExtratoBancarioPessoaJuridicaDTO` se encontrado.
  - **404 Not Found**: Se o extrato bancário não existir.

#### 3. `POST api/extratobancario`
- **Descrição**: Cria um novo extrato bancário.
- **Corpo da Requisição**: Um objeto `ExtratoBancarioPessoaJuridicaDTO`.
- **Retorno**: 
  - **201 Created**: O objeto criado, com o local para acessar o novo extrato.

#### 4. `POST api/extratobancario/inserirlista`
- **Descrição**: Cria uma lista de extratos bancários.
- **Corpo da Requisição**: Uma lista de `ExtratoBancarioPessoaJuridicaDTO`.
- **Retorno**: 
  - **201 Created**: Se a lista for criada com sucesso.

#### 5. `PUT api/extratobancario/{id}`
- **Descrição**: Atualiza um extrato bancário existente.
- **Parâmetros**: 
  - `id` (int): O ID do extrato bancário.
- **Corpo da Requisição**: Um objeto `ExtratoBancarioPessoaJuridicaDTO`.
- **Retorno**: 
  - **204 No Content**: Se a atualização for bem-sucedida.

#### 6. `DELETE api/extratobancario/{id}`
- **Descrição**: Exclui um extrato bancário pelo ID.
- **Parâmetros**: 
  - `id` (int): O ID do extrato bancário.
- **Retorno**: 
  - **204 No Content**: Se a exclusão for bem-sucedida.
---
