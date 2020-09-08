using LibraryApi.Contracts;
using LibraryApi.Contracts.Request;
using LibraryApi.Contracts.Response;
using LibraryApi.Models;
using LibraryApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    /// <summary>
    /// Endpoint responsável por realizar operações de CRUD dos livros
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IUriService _uriService;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IUriService uriService,
          ILogger<BookController> logger, IMapper mapper)
        {
            this._bookService = bookService;
            this._uriService = uriService;
            this._logger = logger;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os livros cadastrados
        /// </summary>
        /// <response code="200">Êxito no retorno dos livros cadastrados</response>
        [HttpGet(Routes.Book.GetAll)]
        [ProducesResponseType(typeof(List<BookResponse>), 200)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Requisitando retorno de todos os livros...");

            var books = await _bookService
                .GetBooksAsync()
                .ConfigureAwait(false);

            _logger.LogInformation("Quantidade de livros retornados: {booksCount}", books.ToList().Count);
            return Ok(_mapper.Map<List<BookResponse>>(books));
        }

        /// <summary>
        /// Busca um livro com o ID especificado
        /// </summary>
        /// <param name="bookId">ID do livro</param>
        /// <response code="200">Êxito no retorno do livro pesquisado</response>
        /// <response code="404">Nenhum livro foi encontrado</response>
        [HttpGet(Routes.Book.Get)]
        [ProducesResponseType(typeof(BookResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get([FromRoute] Guid bookId)
        {
            _logger.LogInformation("Requisitando retorno do livro com id '{bookId}'...", bookId);

            var book = await _bookService
                .GetBookAsync(bookId)
                .ConfigureAwait(false);

            if (book is null)
            {
                _logger.LogInformation("Nenhum livro com id '{bookId}' foi encontrado!", bookId);
                return NotFound();
            }

            _logger.LogInformation("Livro com id '{bookId}' encontrado!", bookId);
            return Ok(_mapper.Map<BookResponse>(book));
        }

        /// <summary>
        /// Cadastra um novo livro
        /// </summary>
        /// <param name="req">Requisição com os dados do novo livro</param>
        /// <response code="201">Êxito no cadastro do livro</response>
        /// <response code="400">Erro ao tentar efetuar o cadastro do livro</response>
        [HttpPost(Routes.Book.Create)]
        [ProducesResponseType(typeof(BookResponse), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CreateBookRequest req)
        {
            _logger.LogInformation("Requisitando criação de livro...");
            _logger.LogInformation("Dados da requisição: {req}",
              JsonConvert.SerializeObject(req));

            var newBook = _mapper.Map<Book>(req);
            var result = await _bookService
                .AddBookAsync(newBook)
                .ConfigureAwait(false);

            if (!result)
            {
                _logger.LogWarning("Erro ao tentar inserir livro. " +
                  "Aplicação encerrou a requisição com o status {statusCode}",
                    HttpStatusCode.BadRequest);
                return BadRequest();
            }

            var uri = _uriService
              .GetBookUri(newBook.BookId.ToString());

            _logger.LogInformation("Novo livro inserido!");
            return Created(uri, _mapper.Map<BookResponse>(newBook));
        }

        /// <summary>
        /// Atualiza informações de livro cadastrado
        /// </summary>
        /// <param name="bookId">ID do livro que receberá as atualizações</param>
        /// <param name="req">Requisição com os dados de atualização do livro</param>
        /// <response code="200">Êxito na atualização do livro</response>
        /// <response code="404">Nenhum livro foi encontrado</response>
        /// <response code="400">Erro ao tentar atualizar o livro</response>
        [HttpPut(Routes.Book.Update)]
        [ProducesResponseType(typeof(BookResponse), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromRoute] Guid bookId, [FromBody] UpdateBookRequest req)
        {
            _logger.LogInformation("Requisitando atualização de livro com id {bookId}...", bookId);
            _logger.LogInformation("Dados da requisição: {req}",
              JsonConvert.SerializeObject(req));

            var updatedBook = _mapper.Map<Book>(req);
            var result = await _bookService
              .UpdateBookAsync(bookId, updatedBook)
              .ConfigureAwait(false);

            if (!result)
            {
                _logger.LogWarning("Erro ao tentar atualizar livro. " +
                    "Aplicação encerrou a requisição com o status {statusCode}",
                      HttpStatusCode.BadRequest);
                return BadRequest();
            }

            _logger.LogInformation("Livro com id {id} foi atualizado!", bookId);
            return Ok(_mapper.Map<BookResponse>(updatedBook));
        }

        /// <summary>
        /// Deleta um livro
        /// </summary>
        /// <param name="bookId">ID do livro que será deletado</param>
        /// <response code="204">Êxito ao deletar um livro</response>
        /// <response code="404">Nenhum livro foi encontrado</response>
        [HttpDelete(Routes.Book.Delete)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute] Guid bookId)
        {
            _logger.LogInformation("Requisitando deletar livro com id '{id}'...", bookId);

            var deleted = await _bookService
                .DeleteBookAsync(bookId)
                .ConfigureAwait(false);

            if (!deleted)
            {
                _logger.LogInformation("Nenhum livro com id '{id}' foi encontrado!", bookId);
                return NotFound();
            }

            _logger.LogInformation("Livro com id {id} foi deletado!", bookId);
            return NoContent();
        }
    }
}
