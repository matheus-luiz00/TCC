using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using LocacaoGaragens.Models;

namespace LocacaoGaragens.Controllers
{
    public class ModelosController : ApiController
    {
        private ContextDB db = new ContextDB();

        // GET: api/Modelos
        public IQueryable<Modelo> GetModelos()
        {
            return db.Modelos;
        }

        // GET: api/Modelos/5
        [ResponseType(typeof(Modelo))]
        public async Task<IHttpActionResult> GetModelo(int id)
        {
            Modelo modelo = await db.Modelos.FindAsync(id);
            if (modelo == null)
            {
                return NotFound();
            }

            return Ok(modelo);
        }

        [Route("Api/Modelos/{codigo}/marca")]
        [HttpGet]
        public IQueryable<Modelo> GetModelos(int codigo)
        {
            return db.Modelos.Where(x => x.MarcaTable.Codigo == codigo) ;
        }

        // PUT: api/Modelos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutModelo(int id, Modelo modelo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modelo.Id)
            {
                return BadRequest();
            }

            db.Entry(modelo).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModeloExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Modelos
        [ResponseType(typeof(Modelo))]
        public async Task<IHttpActionResult> PostModelo(Modelo modelo)
        {
            var marca = db.Marcas.Find(modelo.MarcaTable.Id);
            modelo.MarcaTable = marca;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Modelos.Add(modelo);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = modelo.Id }, modelo);
        }

        // DELETE: api/Modelos/5
        [ResponseType(typeof(Modelo))]
        public async Task<IHttpActionResult> DeleteModelo(int id)
        {
            Modelo modelo = await db.Modelos.FindAsync(id);
            if (modelo == null)
            {
                return NotFound();
            }

            db.Modelos.Remove(modelo);
            await db.SaveChangesAsync();

            return Ok(modelo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModeloExists(int id)
        {
            return db.Modelos.Count(e => e.Id == id) > 0;
        }
    }
}