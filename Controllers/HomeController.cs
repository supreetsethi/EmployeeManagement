using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using EmployeeManagement.Model;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    
    public class HomeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public IWebHostEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                              IWebHostEnvironment hostingEnvironment)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        public ViewResult Index()
        {
            //throw new Exception("Exception occur");

            return View(_employeeRepository.GetAllEmployee());
        }

        public ViewResult Details(int? Id)
        {
            var employee = _employeeRepository.GetEmployee(Id.Value);
            if (employee == null)
            {
                Response.StatusCode = (int)HttpStatusCode.NotFound;
                return View("EmployeeNotFound", Id.Value);
            }
            return View(_employeeRepository.GetEmployee(Id ?? 1));
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await UploadFiles(model);
                Employee employee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(employee);
                return RedirectToAction("details", new { Id = employee.Id });
            }
            return View();
        }

        [HttpGet]
        public ViewResult Edit(int Id)
        {
            var employee = _employeeRepository.GetEmployee(Id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Department = employee.Department,
                Email = employee.Email,
                ExistingPhotoPath = employee.PhotoPath,
                Name = employee.Name
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepository.GetEmployee(model.Id);

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.Name = model.Name;
                if (model.Photos != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        var filePath = Path.Combine(_hostingEnvironment.WebRootPath + "images" + model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    string uniqueFileName = await UploadFiles(model);
                    employee.PhotoPath = uniqueFileName;
                }

                _employeeRepository.Update(employee);
                return RedirectToAction("details", new { Id = employee.Id });
            }
            return View();
        }

        private async Task<string> UploadFiles(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photos != null && model.Photos.Count > 0)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath + @"\images\");
                for (int i = 0; i < model.Photos.Count; i++)
                {
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos[i].FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photos[i].CopyTo(fileStream);
                    }
                    await UploadFileToS3(model.Photos[i], uniqueFileName);
                }
            }

            return uniqueFileName;
        }

        public async Task UploadFileToS3(IFormFile file, string FileName)
        {

            using (var client = new AmazonS3Client("AKIAUGXQFT4MAE4EIOCH", "E/7YgFb1j+BmgpyAWzlDXdCi/o5v9TZvp2ebajvS", RegionEndpoint.APSouth1))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var appendfilename = Guid.NewGuid();


                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = FileName,
                        BucketName = "employee-image-bucket",
                        CannedACL = S3CannedACL.PublicRead
                    };

                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
        }
    }
}
