# AuthorsAndBooksAPI

##Objetivo do Projeto
A AuthorsAndBooksAPI é uma API RESTful para o cadastro e gerenciamento de autores e livros. Ela permite a realização de operações CRUD (Create, Read, Update, Delete) e inclui autenticação e autorização utilizando JWT.

## Descrição do Projeto
<p>ASP.NET Core</p>
<p>.Net 6</p>
<p>Entity Framework Core</p>
<p>JWT - Autenticação e autorização dos usuários</p>
<p>SQL Server</p>
<p>Entity Framework</p>
<p>Swagger UI</p>

## Organização do Projeto

O projeto está organizado em três camadas:
- API
- Domain
- Infrastructure


##Como Testar?
Clonar o repositorio em uma pasta local.
Iniciar o Projeto.
Editar a ConnectionStrings de acordo com a sua máquina.
Executar o comando: dotnet ef database update (Comando do Entity Framework para persistir as entidades no banco de dados).

==========================================================================================

LOGIN:
POST
EndPoit -> https://localhost:7212/api/Auth/login
Body:
{
  "username": "admin",
  "password": "password"
}
==========================================================================================

Author

TODOS OS AUTORES
>GetAll: Ok
EndPoint->https://localhost:7212/api/Authors

AUTOR POR ID
>Get: Ok
EndPoint-> https://localhost:7212/api/Authors/{idAuthor}

BUSCAR TODOS OS LIVROS DE UM AUTOR
>Get: Ok
EndPoint-> https://localhost:7212/api/Authors/books/{idAuthor}


CRIAR AUTOR
>Post: OK
EndPoint -> https://localhost:7212/api/Authors
Body Author:
{
  "id": 0,
  "name": "Nome Autor"
}

Body Author com Book:
{
  "id": 0,
  "name": "Nome Autor",
  "books": [
    {
      "id": 0,
      "title": "Nome Livro"
	}
  ]
}


EDITAR AUTOR
>Put:Ok
EndPoint -> https://localhost:7212/api/Authors/{idAuthor}
Body:
{
  "id": {idAuthor},
  "name": "Nome Author modificado"
}



DELETAR AUTOR
>Delete: Ok
EndPoint -> https://localhost:7212/api/Authors/{idAuthor}
Body:


==========================================================================================
***Books:

TODOS OS LIVROS
>Get: OK
EndPoint -> https://localhost:7212/api/Books

LIVRO POR ID
>Get: Ok
EndPoint -> https://localhost:7212/api/Books/{id}

CRIAR NOVO LIVRO
>Post: Ok
EndPoint -> https://localhost:7212/api/Books
Com autor existente
Body:
{
  "id": 0,
  "title": "Nome Livro",
  "authorId": {IdAutor},
  "author": {
    "id": 0,
    "name": ""
  }
}

Com Novo Autor
Body:
{
  "id": 0,
  "title": "Nome Livro",
  "authorId": 0,
  "author": {
    "id": 0,
    "name": "Novo Autor"
  }
}

ATUALIZAR LIVRO
>Put: Ok
EndPoint -> https://localhost:7212/api/Books/{id}

Body:
{
  "id": {IdLivro},
  "title": "Novo Nome do Livro",
  "authorId": 0
}


Deletar LIVRO
>Put: Ok
EndPoint -> https://localhost:7212/api/Books/{id}
