using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookAPP.Core;
using BookAPP.Api.Utils;
using BookAPP.Core.DTOs;
using BookAPP.Core.Commands;
using BookAPP.Core.Exceptions;
using System;

namespace BookApp.API.Controllers
{
    [ApiController]
    public class BooksController : BaseController
    {
        private readonly ILogger<BooksController> _logger;
        private readonly Message _messages;
        public BooksController(ILogger<BooksController> logger, Message messages)
        {
            _logger=logger;
            _messages=messages;
        }


        /// <summary
        /// Add Book
        /// </summary>

        [HttpPost("BookAPP-API/Books")]
        [ProducesResponseType(typeof(Envelope), 200)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddBook([FromBody] AddBookDTO addBookDTO)
        {
            if(ModelState.IsValid)
            {
                AddBookCommand addBookCommand = new AddBookCommand(addBookDTO.Title, addBookDTO.AuthorId,
                addBookDTO.ReleaseYear, "Fiction");
                try
                {
                    var result = _messages.Dispatch(addBookCommand);
                    return Ok(result);
                }
                catch(DomainException ex)
                {
                    _logger.LogError(ex.Message);
                    return Error(ex.Message);
                }
                catch(Exception ex)
                {
                    _logger.LogCritical(ex.Message);
                    return StatusCode(500);
                }

            }
            return BadRequest();
        }

    }
}