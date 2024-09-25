using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            // Devuelve la lista de todos los clientes
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/clientes/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            // Busca un cliente por su ID
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el cliente
            }

            return cliente; // Retorna el cliente encontrado
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            // Agrega nuevo cliente a la db
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
            // Retorna 201 Created y la ubicación del nuevo recurso
        }

        // PUT: api/clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(); // Retorna 400 si los IDs no coinciden
            }

            // Marca el cliente como modificado
            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync(); // Intenta guardar los cambios
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound(); // Retorna 404 si no existe el cliente
                }
                else
                {
                    throw; // Lanza la excepción si ocurre un error distinto
                }
            }

            return NoContent(); // Retorna 204 No Content si la actualización es exitosa
        }

        // DELETE: api/clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            // Busca el cliente a eliminar
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound(); // Retorna 404 si no se encuentra el cliente
            }

            _context.Clientes.Remove(cliente); // Elimina el cliente
            await _context.SaveChangesAsync(); // Guarda los cambios

            return NoContent(); // Retorna 204 No Content si la eliminación es exitosa
        }

        // Verifica si el cliente existe
        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id); // Retorna true si existe
        }
    }
}
