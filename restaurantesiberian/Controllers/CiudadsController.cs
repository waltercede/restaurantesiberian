using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expiry.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurantesiberian.Models;

namespace restaurantesiberian.Controllers
{
    [ApiController]
    public class CiudadsController : ControllerBase
    {
        private readonly SiberianDBContext _context;
        private readonly IJwtAuthenticationService _authService;

       
        public CiudadsController(SiberianDBContext context, IJwtAuthenticationService authService)
        {
            _context = context;
            _authService = authService;
        }
        [HttpPost]
        [Route("api/GrabaCiudad")]
        public async Task<IActionResult> GrabaCiudad(InputCiudad ciudad)
        {
            Ciudad ciudadModels = new();
            RespuestaModels respuesta = new();
            try
            {
              ciudadModels.NombreCiudad = ciudad.NombreCiudad;
                ciudadModels.FechaCreacion = DateTime.Now;
                _context.Ciudads.Add(ciudadModels);
            await _context.SaveChangesAsync();

            respuesta.Status = 1;
        
            }
            catch (Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje = e.Message;
                // return Ok(respuesta);
            }
             return Ok(respuesta);
        }

        [HttpPost]
        [Route("api/ActualizaCiudad")]
        public async Task<IActionResult> ActualizaCiudad(InputCiudad ciudad)
        {

            RespuestaModels respuesta = new();
            try
            {
                Ciudad ciudadModels = _context.Ciudads.Find(ciudad.Idciudad);
               // ciudadModels.Idciudad = ciudad.Idciudad;
                ciudadModels.NombreCiudad = ciudad.NombreCiudad;
                _context.Entry(ciudadModels).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _context.SaveChangesAsync();
                respuesta.Status = 1;

            }
            catch (Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje = e.Message;
                // return Ok(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("api/Listaciudades")]
        public async Task<IActionResult> Listaciudades()
        {
            RespuestaModels respuesta = new();
       
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {
                    List<Ciudad> ciudadModels = new();
                    ciudadModels = await _context.Ciudads.ToListAsync();
                    respuesta.Status = 1;
                    respuesta.DatosJson = ciudadModels;
                }
            }
            catch (Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje = e.Message;
                // return Ok(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpGet("api/authenticate")]
        public IActionResult Authenticate()
        {
            var token = _authService.Authenticate("Siberian", "12345");

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
