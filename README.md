# Projeto Microserviço

## Descrição

Este projeto é um microserviço desenvolvido com .NET, utilizando RabbitMQ para comunicação entre serviços. A aplicação é organizada seguindo os princípios de DDD, Clean Code e SOLID.

## Configuração do RabbitMQ

Para que o microserviço funcione corretamente, é necessário configurar um usuário e senha para o RabbitMQ. Siga os passos abaixo para criar um usuário no RabbitMQ e configurar as credenciais no arquivo `appsettings.json`.

### Passos para criar um usuário no RabbitMQ

1. **Acesse o RabbitMQ Management Plugin**:
   - Normalmente, o RabbitMQ Management Plugin está disponível no endereço: `http://localhost:15672/`
   - Faça login com as credenciais padrão (`username: guest`, `password: guest`).

2. **Crie um novo usuário**:
   - Navegue até a aba `Admin`.
   - Em `Add a user`, preencha os campos `Username`, `Password` e `Tags` (deixe como `management` para conceder permissões administrativas).
   - Clique em `Add user`.

3. **Configure permissões para o novo usuário**:
   - Após adicionar o usuário, vá para a seção `Permissions`.
   - Selecione o novo usuário na lista.
   - Configure as permissões de `Configure`, `Write` e `Read` conforme necessário para o seu projeto.
   - Clique em `Set permission`.

### Configurando o `appsettings.json`

Depois de criar o usuário no RabbitMQ, você precisa configurar as credenciais no arquivo `appsettings.json` do seu projeto.

1. **Abra o arquivo `appsettings.json`** no seu projeto.

2. **Adicione as informações de configuração** do RabbitMQ como mostrado abaixo:

```json
{
  "RabbitMq": {
    "HostName": "localhost",
    "UserName": "test",
    "Password": "test"
  }
}
```

Certifique-se de substituir `"test"` e `"test"` pelas credenciais que você criou.

## Executando a Aplicação

Após configurar o RabbitMQ e o `appsettings.json`, você pode executar a aplicação via Docker Compose no Visual Studio.

### Executando com Docker Compose no Visual Studio

1. **Abra o Visual Studio** e carregue sua solução.

2. **Configure o projeto para usar Docker Compose**:
   - Clique com o botão direito do mouse na solução no Solution Explorer.
   - Selecione `Add` > `Container Orchestration Support`.
   - Escolha `Docker Compose` e clique em `OK`.

3. **Verifique o arquivo `docker-compose.yml`**:
   - Certifique-se de que o arquivo `docker-compose.yml` está configurado corretamente na raiz do seu projeto.

4. **Defina o Docker Compose como projeto de inicialização**:
   - No Solution Explorer, clique com o botão direito no projeto `docker-compose`.
   - Selecione `Set as Startup Project`.

5. **Execute a aplicação**:
   - Clique em `Start` ou pressione `F5` para iniciar a aplicação.

O Visual Studio irá construir as imagens Docker e iniciar os contêineres definidos no arquivo `docker-compose.yml`.
