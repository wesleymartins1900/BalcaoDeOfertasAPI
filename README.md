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
Implementar logs de eventos para facilitar o monitoramento e análise da API.
Não vejo muito o que podemos melhorar nesse momento além de ampliar as regras de negócio, implementar novos endpoints e a partir daí, pensar em melhoria conforme necessidade. Mas, pra fechar, a curto prazo, acredito que seria bacana implementar Testes automatizados dentro do possível. Agregaria bastante valor à API e diminuiria o tempo que levamos para testar algumas regras mais simples.

Agora, a longo prazo, podemos utilizar algumas ferramentas para monitorar e analisar o desempenho da API a fim de identificar gargalos, problemas de desempenho, identificar quais endpoint são mais utilizados pelos usuários em produção, etc. 
Também, manter uma documentação atualizada, é sempre importante para APIs.

4) Observações:

    A listagem no app acontecerá por paginação ou scroll. O endpoint precisa atender a qualquer um dos 2.

Por mais que eu trabalhe a tempo com .NET e APIs, nunca precisei implementar lógica com "scroll". Sempre fiz somente por paginação. Criei uma lógica que acredito que funcionará corretamente onde o front passa o último ID e capturo as Ofertas seguintes a partir deste ID. O ideal, embora esteja funcionando, seria testar integrando com um projeto front-end para entender se esta lógica faz sentido ou precisa ser ajustada.

    Criar ofertas - opção para criar uma nova oferta. Como a criação da oferta depende do saldo de uma moeda em uma carteira que se quer vender, a oferta está diretamente atrelada a uma carteira específica do usuário.

"a oferta está diretamente atrelada a uma carteira específica do usuário" para este caso, fiquei com dúvidas sobre a forma correta de implementar. Se, literalmente, eu atrelar a oferta à carteira, entendo que só haveria um tipo de moeda por carteira, certo? A minha implementação não faz exatamente este vínculo. Da forma que estruturei, existe a possibilidade de uma carteira possuir várias moedas distintas e um usuário possuir várias carteiras. Com esta minha implementação, se realmente a carteira não puder possuir várias moedas, seria simples alterar a condição para bloquear. Para casos assim, logo no início da demanda seria ideal uma conversa breve com PO para definir a estrutura correta.


5) Links:

Repositório: https://github.com/wesleymartins1900/BalcaoDeOfertasAPI.git 

LinkedIn: https://www.linkedin.com/in/wesley19/ 

E-mail: wesley.martins1900@gmail.com 
