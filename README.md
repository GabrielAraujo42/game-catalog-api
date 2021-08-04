# game-catalog-API
API de catálogo de jogos.
Criado seguindo o projeto fornecido pelo especialista Thiago Campos de Oliveira na Digital Innovation One.

# Funcionalidades desenvolvidas
### Contempladas no projeto

- Controller para receber as requests http;
- Models para determinar valores de Input e Output nas funções da controller;
- Interface de classe de serviço referenciada pela controller;
- Classe implementando as funções da interface de serviço para lidar com as requests;
- Interface de classe de repositório referenciada pela classe de serviço;
- Classe modelando estrutura da tabela de jogos no banco de dados;
- Classes implementando a interface de repositório (com memória local e utilizando Sql Server);
- Classes de exceção e Middleware para lidar com exceções não tratadas;
- Comentários com detalhes extras para página Swagger.

### Extras

- Verificação que impede update de registro caso já exista jogo com o Nome e Desenvolvedora digitados (uma vez que inclusão de registros já tinha essa verificação);
- Função extra na classe GameSqlServerRepository abstraindo linhas recorrentes de conexão e execução de comando em banco;
- Inclusão de detalhes em todas as funções da classe GameController para o Swagger;
- Toda a aplicação desenvolvida em inglês.
