# BalcaoDeOfertasAPI

1) Detalhes e Primeira Execução:

Para configurar a API, basta executar o script "EstruturaInicial" alterando a variável @Path para o caminho que deseja armazenar o banco de dados.

Há também, o script "CargaDeDadosParaTestes.sql" onde deixei pronto uma carga de dados para testes.

Pode ser utilizado qualquer conexão do SQL Server, sem problemas, só precisa alterar a ConnectionString no appsettings.json conforme necessário mantendo 'Initial Catalog=BalcaoDB;'.

    "DefaultConnection": "Data Source=localhost;Initial Catalog=BalcaoDB;Integrated Security=True;TrustServerCertificate=true;"

Eu, particularmente, não gosto de manter uma estrutura "5 - Database" com os scripts e o banco de dados direto no projeto, porém, como o intuito deste projeto seja avaliar a qualidade de código e testar a API, preferi manter para ficar mais organizado e com fácil acesso para o avaliador.

2) DTOs utilizados no projeto:

Um ponto de atenção: Eu, em diversos casos, gosto de criar um projeto separado com os DTOs utilizados na API/Projeto. Neste caso específico, eu apenas referenciei o projeto mas, o ideal (quando necessário) seria subir como pacote NuGet e então referenciar em qualquer outro projeto que faça requisições para esta API.

3) Melhorias e Novas Implementações:

Inicialmente, talvez o mais fácil de visualizar, seja o endpoint "GetBalcaoDeOfertas" que pode ter um gargalo devido ao número de requisições e a cada página no front que gera mais requisições. Eu recomendaria criar uma estrutura de cache com Redis que diminua este impacto.
Não vejo muito o que podemos melhorar nesse momento além de ampliar as regras de negócio, implementar novos endpoints e a partir daí, pensar em melhoria conforme necessidade. Mas, pra fechar, acredito que seria bacana implementar Testes automatizados dentro do possível. Agregaria bastante valor à API e diminuiria o tempo que levamos para testar algumas regras mais simples.

Repositório: https://github.com/wesleymartins1900/BalcaoDeOfertasAPI.git
