using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCrudOperation.Models;

namespace SimpleCrudOperation.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IEnumerable<Product> productsList { get; set; }
        public Product singleProduct { get; set; }

        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            productsList = await _db.Product.ToListAsync();

            return View(productsList);
        }

        public IActionResult Create(string data)
        {
            ViewBag.Success = data;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product productObject)
        {

            if (ModelState.IsValid)
            {
                await _db.AddAsync(productObject);
                await _db.SaveChangesAsync();

               return RedirectToAction("Create",new { data= "Successfully Saved" });
            }
            else
            {
                return View();
            }       
       
        }

        public async Task<IActionResult> Edit(int id)
        {
            singleProduct = await _db.Product.FindAsync(id);

            return View(singleProduct);
        }

  
        [HttpPost]
        public async Task<IActionResult> Edit(Product productObject)
        {
            if (ModelState.IsValid)
            {
                singleProduct = await _db.Product.FindAsync(productObject.Id);
                singleProduct.ProductName = productObject.ProductName;
                singleProduct.UnitPrice = productObject.UnitPrice;
                singleProduct.TotalQuantity = productObject.TotalQuantity;
                await _db.SaveChangesAsync();

                productsList = await _db.Product.ToListAsync();

                return View("Index",productsList);
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> Delete(int id)
        {

            singleProduct = await _db.Product.FindAsync(id);
            if(singleProduct == null)
            {
                return NotFound();
            }

            _db.Product.Remove(singleProduct);
            await _db.SaveChangesAsync();

            productsList = await _db.Product.ToListAsync();

            return View("Index",productsList);
        }


        //-----------------Json Return---------------------

        //url: /Product/GetAllDataApiJson
        [HttpGet]
        public async Task<IActionResult> GetAllDataApiJson()
        {
            return Json(new {data = await _db.Product.ToListAsync()});
        }
        //url: /Product/DeleteByDataApiJson
        [HttpDelete]
        public async Task<IActionResult> DeleteByDataApiJson(int id)
        {
            singleProduct = await _db.Product.FindAsync(id);
            if(singleProduct == null)
            {
                return Json(new { success = false, message = "Data Not Found!!!" }) ;
            }

           _db.Remove(singleProduct);
           await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Successfully Deleted!!!!" });
        }


    }
}