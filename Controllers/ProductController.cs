using EmployeeManagement.Model;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IWebHostEnvironment hostingEnvironment;

        public ProductController(IProductRepository productRepository, IWebHostEnvironment hostingEnvironment)
        {
            this.productRepository = productRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ViewResult Index()
        {
            return View(productRepository.GetAllProduct());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ViewResult Details(int? Id)
        {
            var employee = productRepository.GetProduct(Id.Value);
            if (employee == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("ProductNotFound", Id.Value);
            }
            return View(productRepository.GetProduct(Id ?? 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Create()
        {
            ProductCreateViewModel productCreateViewModel = new ProductCreateViewModel();
            productCreateViewModel.lstBrands = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "Samsung" },
                new SelectListItem { Value = "1", Text = "LG" },
                new SelectListItem { Value = "3", Text = "Nokia"  },
            };
            return View(productCreateViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await UploadFiles(model);
                Product product = new Product()
                {
                    ProductName = model.ProductName,
                    ImagePath = uniqueFileName,
                    Quantity = model.Quantity,
                    ProductPrice = model.ProductPrice,
                    IsActive = true,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "dbo",
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = "dbo"
                };
                productRepository.Add(product);
                return RedirectToAction("details", new { Id = product.Id });
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var product = productRepository.GetProduct(Id);
            ProductEditViewModel productEditViewModel = new ProductEditViewModel()
            {
                ProductName = product.ProductName,
                ExistingPhotoPath = product.ImagePath,
                Quantity = product.Quantity,
                ProductPrice = product.ProductPrice,
                IsActive = product.IsActive,

            };
            return View(productEditViewModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = productRepository.GetProduct(model.Id);
                product.Id = model.Id;
                product.ProductName = model.ProductName;
                product.ProductPrice = model.ProductPrice;
                product.Quantity = model.Quantity;
                product.UpdatedDate = DateTime.Now;
                product.UpdatedBy = "dbo";
                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var filePath = Path.Combine(hostingEnvironment.WebRootPath + "images" + model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    string uniqueFileName = await UploadFiles(model);
                    product.ImagePath = uniqueFileName;
                }

                productRepository.Update(product);
                return RedirectToAction("details", new { Id = product.Id });
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<string> UploadFiles(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath + @"\images\");
                for (int i = 0; i < model.Photos.Count; i++)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos[i].FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photos[i].CopyTo(fileStream);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}
