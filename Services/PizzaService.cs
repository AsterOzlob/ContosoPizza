using ContosoPizza.Data;
using ContosoPizza.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoPizza.Services
{
    public class PizzaService
    {
        private readonly PizzaContext _context;
        public PizzaService(PizzaContext context)
        {
            _context = context;
        }

        public List<Pizza> GetAll() => _context.Pizzas.ToList();

        public Pizza? Get(int id) => _context.Pizzas.Find(id);

        public void Add(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var pizza = _context.Pizzas.Find(id);
            if (pizza is null)
                return;

            _context.Pizzas.Remove(pizza);
            _context.SaveChanges();
        }

        public void Update(Pizza pizza)
        {
            _context.Pizzas.Update(pizza);
            _context.SaveChanges();
        }
    }
}
