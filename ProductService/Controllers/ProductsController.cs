using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Entities;
using ProductService.Repository;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/Products
        [HttpGet]
        public async Task <ActionResult> Getproducts()
        {
            try
            {
                return Ok(await _productRepository.GetProducts());
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error Reteriving Data from DataBase");
            }            
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
           
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromBody]Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var createdProduct = await _productRepository.InsertProduct(product);

                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new product record");
            }
                        
        }

        //DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productToDelete = await _productRepository.GetProductById(id);

            if (productToDelete == null)
            {
                return NotFound($"Product with Id = {id} not found");
            }

            _productRepository.DeleteProduct(id);          

            return NoContent();
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            try
            {
                var product = await _productRepository.GetProductByName(name);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }

        }

        //private bool ProductExists(int id)
        //{
        //    return _context.products.Any(e => e.Id == id);
        //}
    }
}
