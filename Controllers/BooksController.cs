using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookAPP.Core;
using BookAPP.Api.Utils;
using BookAPP.Core.DTOs;
using BookAPP.Core.Commands;
using BookAPP.Core.Exceptions;
using System;
using BookAPP.Core.Queries;

namespace BookApp.API.Controllers
{
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly ILogger<BooksController> _logger;
        private readonly Message _messages;
        public BooksController(ILogger<BooksController> logger, Message messages)
        {
            _logger = logger;
            _messages = messages;
        }


        /// <summary
        /// Add Book
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPost("BookApp/Books")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddBook([FromBody] AddBookDTO addBookDTO)
        {
            if (ModelState.IsValid)
            {
                AddBookCommand addBookCommand = new AddBookCommand(addBookDTO.Title, addBookDTO.AuthorId,
                addBookDTO.ReleaseYear);
                try
                {
                    var result = _messages.Dispatch(addBookCommand);
                    return FromResult(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

        /// <summary
        /// Get all Books
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("BookApp/Books")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetBooks()
        {
            if (ModelState.IsValid)
            {
                GetAllBooksQuery getAllAuthorsQuery = new GetAllBooksQuery();
                try
                {
                    var result = _messages.Dispatch(getAllAuthorsQuery);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }


        /// <summary
        /// Update Book
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPut("BookApp/Books/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult UpdateBook(Guid id, [FromBody] UpdateBookDTO updateBookDTO)
        {
            if (ModelState.IsValid)
            {
                UpdateBookCommand updateBookCommand = new UpdateBookCommand(updateBookDTO.Title, updateBookDTO.Genre, updateBookDTO.AuthorId, id, updateBookDTO.ReleaseYear);
                try
                {
                    var result = _messages.Dispatch(updateBookCommand);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }


        /// <summary
        /// Get Book
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("BookApp/Books/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetBook(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetBookQuery getBookQuery = new GetBookQuery(id);
                try
                {
                    var result = _messages.Dispatch(getBookQuery);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }


        /// <summary
        /// Update Book
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpDelete("BookApp/Books/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult DeleteBook(Guid id)
        {
            if (ModelState.IsValid)
            {
                DeleteBookCommand deleteBookCommand = new DeleteBookCommand(id);
                try
                {
                    var result = _messages.Dispatch(deleteBookCommand);
                    return Ok(result);
                }
                catch (DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

    }
}