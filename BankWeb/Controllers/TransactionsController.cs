using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Script.Serialization;
using BankWeb.Models;
using Newtonsoft.Json;

namespace BankWeb.Controllers
{
    public class TransactionsController : ApiController
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: api/Transactions
        [ResponseType(typeof(string))]
        public IHttpActionResult GetTransactions()
        {
            List<TransactionJson> listData = new List<TransactionJson>();
            foreach (var transaction in db.Transactions.OrderBy(a => a.Date))
            {
                TransactionJson j = new TransactionJson();
                j.AccountNumber = transaction.Customer.AccountNumber;
                j.Date = transaction.Date.ToString("dd/MM/yyyy HH:mm:ss");
                j.Value = transaction.Value.ToString();
                j.DebitCredit = transaction.DebitCredit;
                listData.Add(j);
            }
            return Ok(JsonConvert.SerializeObject(listData));
        }

        // GET: api/Transactions/5
        [ResponseType(typeof(string))]
        public IHttpActionResult GetTransaction(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            TransactionJson j = new TransactionJson();
            j.AccountNumber = transaction.Customer.AccountNumber;
            j.Date = transaction.Date.ToString("dd/MM/yyyy HH:mm:ss");
            j.Value = transaction.Value.ToString();
            j.DebitCredit = transaction.DebitCredit;

            return Ok(JsonConvert.SerializeObject(j));
        }

        // PUT: api/Transactions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTransaction(int id, Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != transaction.Id)
            {
                return BadRequest();
            }

            db.Entry(transaction).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult PostTransaction(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Transactions.Add(transaction);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = transaction.Id }, transaction);
        }

        // DELETE: api/Transactions/5
        [ResponseType(typeof(Transaction))]
        public IHttpActionResult DeleteTransaction(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }

            db.Transactions.Remove(transaction);
            db.SaveChanges();

            return Ok(transaction);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TransactionExists(int id)
        {
            return db.Transactions.Count(e => e.Id == id) > 0;
        }
    }
}