using LibraryApi.Contracts.Response;
using LibraryApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace LibraryApi.Examples.Response
{
    public class BookResponseExample : IExamplesProvider<BookResponse>
    {
        public BookResponse GetExamples()
        {
            return new BookResponse
            {
                Id = Guid.NewGuid(),
                Title = "Desenvolvimento de Software. Aplicativo Comercial com C# e Camadas",
                Description = "Foi dado um enfoque principal em questões de padrões de desenvolvimento - Design Patterns e conceitos OOP – Programação Orientada a Objetos. Visando aproveitar ao máximo o conceito de Orientação a Objetos, também foi abordado o Mapeamento Objeto-Relacional (ORM), que consiste em um framework que tem por objetivo suprir as disparidades entre o paradigma orientado a objetos e o modelo entidade-relacional, criando uma ponte (mapeamento) entre o modelo relacional e o modelo orientado a objetos. Ou seja, ao trabalhar com essa abordagem, é possível a construção de sistemas utilizando o paradigma orientado a objetos, cujo os objetos são persistidos em um banco de dados relacional. Também o conceito de programação em camadas foi tratado nesse livro com a construção de parte de um Aplicativo Comercial, focando no desenvolvimento de formulários de Cadastros Básicos. O material possui atividades que são incluídas como atividades para o estudante, fazendo com que seja aplicado todo o conhecimento adquirido.",
                Category = "Tecnologia",
                Year = 2018,
                PublishedIn = "28/06/2018 00:00:00",
                Author = new Author
                {
                    Id = Guid.NewGuid(),
                    Name = "Alexandre Dutra de Oliveira",
                    Email = "alexandre.oliveira@email.com"
                }
            };
        }
    }
}
