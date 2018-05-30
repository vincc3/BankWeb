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
    public class CustomersController : ApiController
    {
        private MyDatabaseEntities db = new MyDatabaseEntities();

        // GET: api/Customers
        [ResponseType(typeof(string))]
        public IHttpActionResult GetCustomers()
        {
            List<CustomerJson> listData = new List<CustomerJson>();
            foreach (Customer customer in db.Customers)
            {
                CustomerJson j = new CustomerJson();
                j.Id = customer.Id;
                j.AccountNumber = customer.AccountNumber;
                j.AccountBalance = customer.AccountBalance.ToString();
                j.AccountName = customer.AccountName;
                j.ContactInformation = customer.ContactInformation;
                j.FirstName = customer.FirstName;
                j.LastName = customer.LastName;
                listData.Add(j);
            }

            return Ok(JsonConvert.SerializeObject(listData));
        }

        // GET: api/Customers/5
        [ResponseType(typeof(string))]
        public IHttpActionResult GetCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            CustomerJson j = new CustomerJson();
            j.Id = customer.Id;
            j.AccountNumber = customer.AccountNumber;
            j.AccountBalance = customer.AccountBalance.ToString();
            j.AccountName = customer.AccountName;
            j.ContactInformation = customer.ContactInformation;
            j.FirstName = customer.FirstName;
            j.LastName = customer.LastName;


            return Ok(JsonConvert.SerializeObject(j));
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomer(int id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            db.Entry(customer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customer))]
        public IHttpActionResult PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customer))]
        public IHttpActionResult DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customer);
            db.SaveChanges();

            return Ok(customer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}