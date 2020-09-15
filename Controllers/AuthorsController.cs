using System;
using BookApp.API.Controllers;
using BookAPP.Api.Utils;
using BookAPP.Core;
using BookAPP.Core.Commands;
using BookAPP.Core.DTOs;
using BookAPP.Core.Exceptions;
using BookAPP.Core.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookAPP.API.Controllers
{
    public class AuthorsController : BaseController
    {
        private readonly ILogger<AuthorsController> _logger;
        private readonly Message _messages;
        public AuthorsController(ILogger<AuthorsController> logger, Message messages)
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

        [HttpPost("BookApp/Authors")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult AddAuthor([FromBody] AddAuthorDTO addAuthorDTO)
        {
            if (ModelState.IsValid)
            {
                AddAuthorCommand addBookCommand = new AddAuthorCommand(addAuthorDTO.Firstname, addAuthorDTO.Lastname);
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
        /// Get all Authors
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpGet("BookApp/Authors")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetAuthors()
        {
            if (ModelState.IsValid)
            {
                GetAllAuthorsQuery getAllAuthorsQuery = new GetAllAuthorsQuery();
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
        /// Get Author
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPut("BookApp/Authors/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult GetAuthor(Guid id)
        {
            if (ModelState.IsValid)
            {
                GetAuthorQuery getAuthor = new GetAuthorQuery(id);
                try
                {
                    var result = _messages.Dispatch(getAuthor);
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
        /// Update Author
        /// </summary>
        /// <response code="400">Bad request</response> 
        /// <response code="401">Unknown Identity</response>
        /// <response code="403">Unauthorized</response>

        [HttpPut("BookApp/Authors/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult UpdateAuthor(Guid id, [FromBody] UpdateAuthorDTO updateAuthorDTO)
        {
            if (ModelState.IsValid)
            {
                UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(updateAuthorDTO.Firstname, updateAuthorDTO.Lastname, id);
                try
                {
                    var result = _messages.Dispatch(updateAuthorCommand);
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

        [HttpPut("BookApp/Author/{id}")]
        [ProducesResponseType(typeof(Envelope), 201)]
        [ProducesResponseType(typeof(Envelope), 400)]
        public IActionResult DeleteAuthor(Guid id)
        {
            if (ModelState.IsValid)
            {
                DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(id);
                try
                {
                    var result = _messages.Dispatch(deleteAuthorCommand);
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