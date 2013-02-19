using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoKnock.Controllers.Models;

namespace TodoKnock.Controllers.Api
{
    public class TarefasController : ApiController
    {
        private TodoKnockContext db = new TodoKnockContext();

        // GET api/Tarefas
        public IEnumerable<Tarefa> GetTarefas()
        {
            return db.Tarefas.OrderBy(t => t.Terminada).AsEnumerable();
        }

        // GET api/Tarefas/5
        public Tarefa GetTarefa(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return tarefa;
        }

        // PUT api/Tarefas/5
        public HttpResponseMessage PutTarefa(int id, Tarefa tarefa)
        {
            if (ModelState.IsValid && id == tarefa.Id)
            {
                db.Entry(tarefa).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Tarefas
        public HttpResponseMessage PostTarefa(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                db.Tarefas.Add(tarefa);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tarefa);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = tarefa.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Tarefas/5
        public HttpResponseMessage DeleteTarefa(int id)
        {
            Tarefa tarefa = db.Tarefas.Find(id);
            if (tarefa == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Tarefas.Remove(tarefa);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, tarefa);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}