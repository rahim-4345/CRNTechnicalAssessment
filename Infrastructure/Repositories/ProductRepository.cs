using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Entities;
//using Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace Infrastructure.Repositories
//{
//    public class ProductRepository 
//    {


//        private readonly ApplicationDbContext _context;

//        public ProductRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<Product>> GetAllAsync()
//        {
//            return await _context.Products
//                .AsNoTracking()
//                .ToListAsync();
//        }

//        public async Task<Product?> GetByIdAsync(int id)
//        {
//            return await _context.Products
//                .AsNoTracking()
//                .FirstOrDefaultAsync(x => x.Id == id);
//        }

//        public async Task<Product> CreateAsync(Product product)
//        {
//            await _context.Products.AddAsync(product);
//            await _context.SaveChangesAsync();

//            return product;
//        }

//        public async Task<Product> UpdateAsync(Product product)
//        {
//            _context.Products.Update(product);
//            await _context.SaveChangesAsync();

//            return product;
//        }

//        public async Task<bool> DeleteAsync(int id)
//        {
//            var product = await _context.Products
//                .FirstOrDefaultAsync(x => x.Id == id);

//            if (product == null)
//                return false;

//            _context.Products.Remove(product);
//            await _context.SaveChangesAsync();

//            return true;
//        }

//    }
//}
