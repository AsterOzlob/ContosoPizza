using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;
using SQLitePCL;

namespace ContosoPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        PizzaService _service;

        public PizzaController(PizzaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Pizza> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Pizza> GetById(int id)
        {
            var pizza = _service.GetById(id);

            if (pizza is null)
            {
                return NotFound();
            }

            return pizza;
        }

        [HttpPost]
        public IActionResult Create(Pizza newPizza)
        {
            var pizza = _service.Create(newPizza);
            return CreatedAtAction(nameof(GetById), new { id = pizza!.Id }, pizza);
        }

        [HttpPost("{id}/addtopping")]
        public IActionResult AddToping(int id, int toppingId)
        {
            var pizzaToUpdate = _service.GetById(id);

            if(pizzaToUpdate is null)
            {
                return NotFound();
            }

            _service.AddTopping(id, toppingId);
            return NoContent();
        }

        [HttpPost("{id}/updatesauce")]
        public IActionResult UpdateSauce(int id, int sauceId)
        {
            var pizzaToUpdate = _service.GetById(id);

            if (pizzaToUpdate is null)
            {
                return NotFound();
            }

            _service.updateSauce(id, sauceId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _service.GetById(id);

            if (pizza is null)
            {
                return NotFound();
            }

            _service.DeleteById(id);
            return Ok();
        }
    }
}
