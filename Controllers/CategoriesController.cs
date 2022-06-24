using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.DTOs;
using Movies.Models;
using Movies.Repository;

namespace Movies.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoriesController(IUnitOfWork _unitOfWork,IMapper _mapper)
        {
            mapper = _mapper;
            unitOfWork = _unitOfWork;
        }
        [HttpGet]
        public async  Task<IActionResult> GetCategories()
        {
            try
            {
                var Categories = await unitOfWork.Categories.GetAll();
                if(Categories==null)
                    return NotFound();
                else
                    return Ok(Categories);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var Category = await unitOfWork.Categories.GetById(id);
                if (Category == null)
                    return NotFound($"Can't Find Category With Id {id}");
                else
                    return Ok(Category);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest();
            Category category=mapper.Map<Category>(categoryDTO);
            await unitOfWork.Categories.Add(category);
            await unitOfWork.complete();
            return Ok(category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory(int id,[FromBody] CategoryDTO categoryDTO)
        {
            var category=await unitOfWork.Categories.GetById(id);
            if (category == null)
                return NotFound($"Can't Find Category With Id {id}");
            mapper.Map<CategoryDTO,Category>(categoryDTO,category);
            unitOfWork.Categories.Update(category);
            await unitOfWork.complete();
            return Ok(category);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category=await unitOfWork.Categories.GetById(id);
            if (category == null)
                return NotFound($"Can't Find Category With Id{id}");
            unitOfWork.Categories.Delete(category);
            await unitOfWork.complete();
            return Ok("Deleted");
        }
    }
}
 