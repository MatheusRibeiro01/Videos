using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Filme.Data;
using Filmes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Videos.Controllers
{
    [Route("v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> GetCategoria(
            [FromServices] DataContext context)
        {
            var categorias = await context.Categorias.AsNoTracking().ToArrayAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoriaById(
            [FromBody] int id,
            [FromServices] DataContext context)
        {
            var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CriarCategoria(
            [FromBody] Categoria model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível criar a categoria"});
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> AtualizarCategoria(
            int id,
            [FromBody] Categoria model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Categoria>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new {message = "Esse registro já foi atualizado"});
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível atualizar esse registro"});
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Categoria>> ApagarCategoria(
            int id,
            [FromBody] Categoria model,
            [FromServices] DataContext context)
        {
            var categoria = await context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (categoria == null)
                return NotFound(new {message = "Categoria não encontrada"});

            try
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
                return Ok(new {message = "Categoria removida com sucesso"});
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível remover a categoria"});
            }
        }

    }

}