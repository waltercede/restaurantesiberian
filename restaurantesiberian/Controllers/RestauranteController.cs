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
            Console.WriteLine(nombreCiudad);
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {
                    List<Restaurante> restauranteModels = new();
                    string StoredProc = "exec Sp_Restaurantes @tipofuncion,@ciudad,@idciudad,@idrestaurante,@nombrerestaurant,@numeroAforo,@telefono";
 
                    List<SqlParameter> parms = new List<SqlParameter>
                {
    
                new SqlParameter { ParameterName = "@tipofuncion", Value = 1 },
                new SqlParameter { ParameterName = "@ciudad", Value = nombreCiudad },
                new SqlParameter { ParameterName = "@idciudad", Value = 1 },
                new SqlParameter { ParameterName = "@idrestaurante", Value = 1 },
                new SqlParameter { ParameterName = "@nombrerestaurant", Value = "" },
                new SqlParameter { ParameterName = "@numeroAforo", Value = 1 },
                new SqlParameter { ParameterName = "@telefono", Value = "" },
                };

                    restauranteModels = await _context.Restaurantes.FromSqlRaw<Restaurante>(StoredProc, parms.ToArray()).ToListAsync();
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
            Console.WriteLine(id);
            try
            {
                using (SiberianDBContext _context = new SiberianDBContext())
                {
                    List<Restaurante> restauranteModels = new();
                    string StoredProc = "exec Sp_Restaurantes @tipofuncion,@ciudad,@idciudad,@idrestaurante,@nombrerestaurant,@numeroAforo,@telefono";

                    List<SqlParameter> parms = new List<SqlParameter>
                {

                new SqlParameter { ParameterName = "@tipofuncion", Value = 2 },
                new SqlParameter { ParameterName = "@ciudad", Value = "" },
                new SqlParameter { ParameterName = "@idciudad", Value = id },
                new SqlParameter { ParameterName = "@idrestaurante", Value = 1 },
                new SqlParameter { ParameterName = "@nombrerestaurant", Value = "" },
                new SqlParameter { ParameterName = "@numeroAforo", Value = 1 },
                new SqlParameter { ParameterName = "@telefono", Value = "" },
                };

                    restauranteModels = await _context.Restaurantes.FromSqlRaw<Restaurante>(StoredProc, parms.ToArray()).ToListAsync(); // restauranteModels = await _context.Restaurantes.FromSqlRaw(StoredProc).ToListAsync();
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

        // POST api/<RestauranteController>4664
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
                                              Value = values.Idciudad
                                            },  
                                               new SqlParameter("@idrestaurante",   SqlDbType.Int)
                                            {
                                              Direction = ParameterDirection.Input,
                                              Value = 1
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
                                              Value =values.Telefono
                                            }
                                            };

                    if (values.Telefono.Length == 10)
                    {
                        await _context.Database.ExecuteSqlRawAsync("exec Sp_Restaurantes  @tipofuncion,@ciudad,@idciudad,@idrestaurante,@nombrerestaurante,@numeroAforo,@telefono", parameters: parameters);
                        respuesta.Status = 1;
                        respuesta.Mensaje = "Restaurante Guardado";
                    }
                    else
                    {
                        respuesta.Status = 0;
                        respuesta.Mensaje = "El telefono  debe contener 10 digitos ";
                    }

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
                                              new SqlParameter("@ciudad", SqlDbType.VarChar)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = ""
                                              },
                                               new SqlParameter("@idciudad", SqlDbType.Int)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = values.Idciudad
                                               },  
                                               new SqlParameter("@idrestaurante", SqlDbType.Int)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = values.Idrestaurante
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

                await _context.Database.ExecuteSqlRawAsync("exec Sp_Restaurantes @tipofuncion,@ciudad,@idciudad, @idrestaurante,@nombrerestaurante,@numeroAforo,@telefono", parameters: parameters);
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

                    var parameters = new[] {
                                              new SqlParameter("@tipofuncion", SqlDbType.Int)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = 5
                                              },
                                              new SqlParameter("@ciudad", SqlDbType.VarChar)
                                              {
                                                  Direction = ParameterDirection.Input,
                                                  Value = ""
                                              },
                                               new SqlParameter("@idciudad", SqlDbType.Int)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = 1
                                               },
                                               new SqlParameter("@idrestaurante", SqlDbType.Int)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = id
                                               },
                                               new SqlParameter("@nombrerestaurante", SqlDbType.VarChar)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = ""
                                               },
                                               new SqlParameter("@numeroAforo", SqlDbType.Int)
                                               {
                                                   Direction = ParameterDirection.Input,
                                                   Value = 1
                                               },
                                                 new SqlParameter("@telefono", SqlDbType.VarChar)
                                                 {
                                                     Direction = ParameterDirection.Input,
                                                     Value = ""
                                                 }
                                            };

                    await _context.Database.ExecuteSqlRawAsync("exec Sp_Restaurantes @tipofuncion,@ciudad,@idciudad, @idrestaurante,@nombrerestaurante,@numeroAforo,@telefono", parameters: parameters);

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
