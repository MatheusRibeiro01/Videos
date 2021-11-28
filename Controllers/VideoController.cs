using System;
using System.Collections.Generic;
using Filme.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Filmes.Models
{
    [Route("v1/videos")]
    public class VideoController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Video>>> ListarVideos(
            [FromServices] DataContext context)
        {
            var videos = await context.Videos.AsNoTracking().ToArrayAsync();
            return Ok(videos);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> ListarVideosPeloId( 
            int id,
            [FromServices] DataContext context)
        {
            var video = await context.Videos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return Ok(video);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Video>> SalvarVideo(
            [FromBody] Video model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Videos.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível criar o video"});
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Video>> AtualizarVideo(
            int id, 
            [FromBody] Video model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Video>(model).State = EntityState.Modified;
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

        // DELETE api/<ValuesController>/5
        [HttpDelete]
        [Route("{id:int}")]
        public async  Task<ActionResult<Video>> ApagarFilme(
            int id,
            [FromServices] DataContext context)
        {
            var video = await context.Videos.FirstOrDefaultAsync(x => x.Id == id);
            if (video == null)
                return NotFound(new {message = "Vídeo não encontrado"});

            try
            {
                context.Videos.Remove(video);
                await context.SaveChangesAsync();
                return Ok(new {message = "Vídeo removido com sucesso"});
            }
            catch (System.Exception)
            {
                return BadRequest(new {message = "Não foi possível remover o vídeo"});
            }
        }
    }
}
