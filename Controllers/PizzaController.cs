using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContosoPizza.Models;
using ContosoPizza.Services;

namespace ContosoPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly PizzaService _pizzaService;

        public PizzaController(PizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        // GET: api/Pizza
        [HttpGet]
        public ActionResult<List<Pizza>> GetAll()
        {
            var pizzas = _pizzaService.GetAll();
            if (pizzas == null || pizzas.Count == 0)
                return NoContent(); // Возвращает статус 204, если список пуст

            return Ok(pizzas);
        }

        // GET: api/Pizza/{id}
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza == null)
                return NotFound(new { message = $"Pizza with ID {id} not found." });

            return Ok(pizza);
        }

        // POST: api/Pizza
        [HttpPost]
        public IActionResult Create([FromBody] Pizza pizza)
        {
            if (pizza == null || string.IsNullOrEmpty(pizza.Name))
            {
                return BadRequest(new { message = "Invalid pizza data. Ensure all fields are filled correctly." });
            }

            _pizzaService.Add(pizza);
            return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
        }

        // PUT: api/Pizza/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest(new { message = "Pizza ID mismatch." });

            var existingPizza = _pizzaService.Get(id);
            if (existingPizza == null)
                return NotFound(new { message = $"Pizza with ID {id} not found." });

            _pizzaService.Update(pizza);
            return NoContent();
        }

        // DELETE: api/Pizza/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = _pizzaService.Get(id);

            if (pizza == null)
                return NotFound(new { message = $"Pizza with ID {id} not found." });

            _pizzaService.Delete(id);
            return Ok(new { message = $"Pizza with ID {id} deleted successfully." });
        }
    }
}
