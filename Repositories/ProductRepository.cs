using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Data;
using ProductsApi.Models;

namespace ProductsApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDataContext _context;

        //Constructor
        public ProductRepository(IDataContext context) // Injection IDataContext
        {
            _context = context;
        }

        //GetProducts
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
        //GetProduct
        public async Task<Product> Get(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        //CreateProduct
        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        //UpdateProduct
        public async Task Update(Product product)
        {
            var itemToUpdate = await _context.Products.FindAsync(product.ProductId);
            if (itemToUpdate == null)
            {
                throw new NullReferenceException();
            }

            //Update continue if found 
            itemToUpdate.Name = product.Name;
            itemToUpdate.Price = product.Price;
            await _context.SaveChangesAsync();

        }
        //DeleteProduct
        public async Task Delete(int id)
        {
            var itemToDelete = await _context.Products.FindAsync(id);
            if (itemToDelete == null)
            {
                throw new NullReferenceException();
            }
            //If found ToDelete
            _context.Products.Remove(itemToDelete);
            await _context.SaveChangesAsync(); // Await above command to be finish before excute this line
        }

    }
}