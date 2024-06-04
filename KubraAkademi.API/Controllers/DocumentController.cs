using KubraAkademi.API.Models;
using KubraAkademi.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KubraAkademi.API.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DocumentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/documents
        [HttpGet]
        public ActionResult<IEnumerable<DocumentDto>> GetDocuments()
        {
            var documentDtos = _context.Documents.Select(d => new DocumentDto
            {
                Id = d.Id,
                Name = d.Name,
                DocUrl = d.DocUrl,
                Description = d.Description,
                CreatedDate = d.CreateDate,
            }).ToList();

            return Ok(documentDtos);
        }

        // GET: api/documents/{id}
        [HttpGet("{id}")]
        public ActionResult<DocumentDto> GetDocument(Guid id)
        {
            var document = _context.Documents.FirstOrDefault(d => d.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            var documentDto = new DocumentDto
            {
                Id = document.Id,
                Name = document.Name,
                DocUrl = document.DocUrl,
                Description = document.Description,
                CreatedDate = document.CreateDate,
            };

            return Ok(documentDto);
        }

        // POST: api/documents
        [HttpPost]
        public async Task<ActionResult<DocumentDto>> CreateDocument(DocumentDto documentDto)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Name = documentDto.Name,
                DocUrl = documentDto.DocUrl,
                Description = documentDto.Description,
                CreateDate = DateTime.Now,
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            var createdDocumentDto = new DocumentDto
            {
                Id = document.Id,
                Name = document.Name,
                DocUrl = document.DocUrl,
                Description = document.Description,
                CreatedDate = document.CreateDate,
            };

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, createdDocumentDto);
        }

        // DELETE: api/documents/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(document);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
