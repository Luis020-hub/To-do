# To-do List

## Descrição

Este projeto é dividido em duas partes: back em ASP.NET Core (C#) e um front em Vue.js.

## Configuração do Backend (ASP.NET Core)

### Requisitos
- SDK do .NET Core instalado ([Download SDK](https://dotnet.microsoft.com/download))

### Passos para Executar
1. Abra um terminal ou prompt de comando e navegue até o diretório do projeto backend.
2. Execute o comando `dotnet restore` para restaurar as dependências do projeto.
3. Após a restauração dos pacotes, execute o comando `dotnet run` para iniciar o servidor local.
4. O servidor ASP.NET Core será iniciado na url http://localhost:5151.

### Observações
- Certifique-se de que as dependências listadas no arquivo `back.csproj` estão instaladas corretamente.

## Configuração do Frontend (Vue.js)

### Requisitos
- Node.js e npm instalados ([Download Node.js](https://nodejs.org))

### Passos para Executar
1. Abra um terminal ou prompt de comando e navegue até o diretório do projeto frontend.
2. Instale o Axios `npm install axios`.
3. Execute o comando `npm install` para instalar todas as dependências listadas no arquivo `package.json`.
4. Após a instalação das dependências, execute o comando `npm run serve` para iniciar o servidor de desenvolvimento.
5. O servidor normalmente inicia em http://localhost:8080 (Será mostrado no console).

### Observações
- Certifique-se de que as versões das dependências listadas no arquivo `package.json` correspondem às especificadas para evitar problemas de compatibilidade.

## Acessando o Projeto
Após seguir os passos acima, você poderá acessar o projeto localmente:
- Backend ASP.NET Core: [http://localhost:5151](http://localhost:5151)
- Frontend Vue.js: [http://localhost:8080](http://localhost:8080)
