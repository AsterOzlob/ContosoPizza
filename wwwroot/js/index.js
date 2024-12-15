const API_URL = "https://localhost:7050/Pizza";

// DOM elements
const addPizzaForm = document.getElementById("addPizzaForm");
const pizzaNameInput = document.getElementById("pizzaName");
const isGlutenFreeCheckbox = document.getElementById("isGlutenFree");
const pizzaTableBody = document.getElementById("pizzaTableBody");

// Get all Pizzas
async function fetchPizzas() {
    try {
        const response = await fetch(API_URL);
        if (response.ok) {
            const pizzas = await response.json();
            renderPizzaTable(pizzas);
        } else {
            pizzaTableBody.innerHTML = "<tr><td colspan='3'>No pizzas available</td></tr>";
        }
    } catch (error) {
        console.error("Error fetching pizzas:", error);
    }
}

// Render Pizzas in Table
function renderPizzaTable(pizzas) {
    pizzaTableBody.innerHTML = ""; // Clear the table
    pizzas.forEach((pizza) => {
        const row = document.createElement("tr");

        row.innerHTML = `
            <td>${pizza.name}</td>
            <td>${pizza.isGlutenFree ? "Yes" : "No"}</td>
            <td>
                <button class="btn btn-danger btn-sm" onclick="deletePizza(${pizza.id})">Delete</button>
            </td>
        `;

        pizzaTableBody.appendChild(row);
    });
}

// Add a new Pizza
async function addPizza(event) {
    event.preventDefault();

    const pizza = {
        name: pizzaNameInput.value.trim(),
        isGlutenFree: isGlutenFreeCheckbox.checked,
    };

    try {
        const response = await fetch(API_URL, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(pizza),
        });

        if (response.ok) {
            fetchPizzas(); // Refresh the table
            addPizzaForm.reset();
        } else {
            const error = await response.json();
            alert(`Error: ${error.message}`);
        }
    } catch (error) {
        console.error("Error adding pizza:", error);
    }
}

// Delete a Pizza
async function deletePizza(id) {
    if (!confirm("Are you sure you want to delete this pizza?")) return;

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: "DELETE",
        });

        if (response.ok) {
            fetchPizzas(); // Refresh the table
        } else {
            const error = await response.json();
            alert(`Error: ${error.message}`);
        }
    } catch (error) {
        console.error("Error deleting pizza:", error);
    }
}

addPizzaForm.addEventListener("submit", addPizza);

fetchPizzas(); // Load pizzas on page load
