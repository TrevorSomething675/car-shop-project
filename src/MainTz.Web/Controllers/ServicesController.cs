using MainTz.Web.ViewModels.ManufacturerViewModels;
using MainTz.Web.ViewModels.ServicesViewModels;
using MainTz.Web.ViewModels.BrandViewModels;
using MainTz.Application.Repositories;
using MainTz.Application.Models;
using Microsoft.AspNetCore.Mvc;
using MainTz.Web.Attributes;
using AutoMapper;

namespace MainTz.Web.Controllers
{
    [CustomAuthorizeAttribute("Admin", "Manager")]
    public class ServicesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        public ServicesController(IMapper mapper, IBrandRepository brandRepository, IManufacturerRepository manufacturerRepository) 
        {
            _manufacturerRepository = manufacturerRepository;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var serviceViewModel = new ServicesResponse();
            var manufacturers = await _manufacturerRepository.GetManufacturersAsync();
            var brands = await _brandRepository.GetBrandsAsync();
            serviceViewModel.Manufacturers = _mapper.Map<List<ManufacturerResponse>>(manufacturers);
            serviceViewModel.Brands = _mapper.Map<List<BrandResponse>>(brands);

            return View(serviceViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            var result = await _brandRepository.CreateAsync(brand);
            return RedirectToAction("Index", "Services");
        }
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var result = await _brandRepository.DeleteByIdAsync(id);
            return RedirectToAction("Index", "Services");
        }
        public async Task<IActionResult> DeleteBrandByName([FromForm] string name)
        {
            try
            {
                var result = await _brandRepository.DeleteBrandByName(name);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Services");
        }
        [HttpPost]
        public async Task<IActionResult> CreateManufacturer(Manufacturer manufacturer)
        {
            var result = await _manufacturerRepository.CreateManufacturer(manufacturer);
            return RedirectToAction("Index", "Services");
        }
        public async Task<IActionResult> DeleteManufacturer(int id)
        {
            var result = await _manufacturerRepository.DeleteById(id);
            return RedirectToAction("Index", "Services");
        }
        public async Task<IActionResult> DeleteManufacturerByName([FromForm] string name)
        {
            try
            {
                var result = await _manufacturerRepository.DeleteByName(name);
            }
            catch(Exception ex)
            {
                
            }
            return RedirectToAction("Index", "Services");
        }
    }
}
