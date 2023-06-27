﻿using BookRentalSystem.DTO;
using BookRentalSystem.Models;
using BookRentalSystem.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using BookRentalSystem.Models.ErrorHandling;

namespace BookRentalSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _service;

       public AuthorsController(IAuthorsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Author\" is null." });
            }
            try
            {
                return Ok(await _service.GetAllItems());
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
           
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Author\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                return Ok(await _service.GetItem(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = ex.Message });
            }
            

        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor([FromBody] AuthorDTO authorDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "The entity set \"Author\" is null." });
            }

            try
            {
                var author = await _service.AddAuthor(authorDTO);

                return CreatedAtAction("GetAuthor", new { id = author.AuthorID }, authorDTO);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Author failed to create. {ex.Message}" });
            }
            
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorDTO authorDTO)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "The entity set \"Author\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.UpdateAuthor(id, authorDTO);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Updated Successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Author failed to update. {ex.Message}"});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!_service.IfTableExists())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "The entity set \"Author\" is null." });
            }

            if (!await _service.IfExists(id))
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "No record exists with that ID." });
            }

            try
            {
                await _service.Delete(id);

                return StatusCode(StatusCodes.Status200OK,
                    new Response { Status = "Success", Message = "Deleted Successfully." });
            }
            catch( Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = ex.Message });
            }
            
        }
    }
}
