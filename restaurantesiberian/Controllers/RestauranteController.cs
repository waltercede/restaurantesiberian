using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using restaurantesiberian.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace restaurantesiberian.Controllers
{
   // [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        // GET: api/<RestauranteController>
        [HttpGet]
        [Route("api/ListaRestaurantexnom/{nombreCiudad}")]
        public async Task<IActionResult> ListaRestaurantexnom( string nombreCiudad)
        {
            RespuestaModels respuesta = new();
            List<Restaurante> restauranteModels = new();
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {

                    string StoredProc = "exec Sp_Restaurantes" +
                     "@tipofuncion= 1, @ciudad ='gu'" ;
                    restauranteModels = await _context.Restaurantes.FromSqlRaw(StoredProc).ToListAsync();
                    respuesta.Status = 1;
                    respuesta.DatosJson = restauranteModels;
                }
            }
            catch(Exception e)
            {
                respuesta.Status = 0;
                respuesta.Mensaje =e.Message;
               // return Ok(respuesta);
            }
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("api/MotrarRestaurantexid/{id}")]
        public async Task<IActionResult> MotrarRestaurantexid(int id)
        {
            RespuestaModels respuesta = new();
            List<Restaurante> restauranteModels = new();
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {

                    string StoredProc = "exec Sp_Restaurantes" +
                     "@tipofuncion ="+ 2  +"," +
                     "@iderestaurante = " + id + "";
                    restauranteModels = await _context.Restaurantes.FromSqlRaw(StoredProc).ToListAsync();
                    respuesta.Status = 1;
                    respuesta.DatosJson = restauranteModels;
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

        // POST api/<RestauranteController>
        [HttpPost]
        [Route("api/GrabaRestaurante")]
        public async Task<IActionResult> GrabaRestaurante(InputRestaurante  values)
        {
            RespuestaModels respuesta = new();
            try
            {
                using (SiberianDBContext _context = new())
                {

                    var parameters = new[] {
                                               new SqlParameter("@tipofuncion", SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 3
                                            },
                                              new SqlParameter("@ciudad",   SqlDbType.VarChar)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = ""
                                            },
                                               new SqlParameter("@idciudad",   SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 1//values.Idciudad
                                            },  
                                               new SqlParameter("@idrestaurante",   SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 1
                                            },
                                               new SqlParameter("@nombrerestaurante", SqlDbType.VarChar)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = "cajun"//values.NombreRestaurante
                                            },
                                               new SqlParameter("@numeroAforo", SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 10//values.NumeroAforo
                                            },
                                                 new SqlParameter("@telefono", SqlDbType.VarChar)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value ="0951362110" //values.Telefono
                                            }
                                            };
                    await _context.Database.ExecuteSqlRawAsync("exec Sp_Restaurantes  @tipofuncion,@ciudad,@idciudad,@idrestaurante,@nombrerestaurante,@numeroAforo,@telefono", parameters: parameters);
                    respuesta.Status = 1;
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

        [HttpPost]
        [Route("api/ActualizarRestaurante")]
        public async Task<IActionResult> ActualizarRestaurante(InputRestaurante values)
        {
            RespuestaModels respuesta = new();
            try
            {
                using (SiberianDBContext _context = new())
                {

                    var parameters = new[] {
                                               new SqlParameter("@tipofuncion", SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 4
                                            },
                                                  new SqlParameter("@idrestaurante",   SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = values.Idrestaurante
                                            },
                                               new SqlParameter("@idciudad",   SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = values.Idciudad
                                            },
                                               new SqlParameter("@nombrerestaurante", SqlDbType.VarChar)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = values.NombreRestaurante
                                            },
                                               new SqlParameter("@numeroAforo", SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = values.NumeroAforo
                                            },   
                                               new SqlParameter("@telefono", SqlDbType.VarChar)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = values.Telefono
                                            }
                                            };
                    await _context.Database.ExecuteSqlRawAsync("exec Sp_Restaurantes @tipofuncion,@idciudad, @idrestaurante,@nombrerestaurante,@numeroAforo,@telefono", parameters: parameters);
                    respuesta.Status = 1;
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

        // DELETE api/<RestauranteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminaRestaurante(int id)
        {

            RespuestaModels respuesta = new();
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {

                    string StoredProc = "exec Sp_Restaurantes" +
                     "@tipofuncion =" + 5 + "," +
                     "@iderestaurante = " + id + "";
                    await _context.Database.ExecuteSqlRawAsync(StoredProc);
                    respuesta.Status = 1;
                    //respuesta.DatosJson = restauranteModels;
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
    }
}
