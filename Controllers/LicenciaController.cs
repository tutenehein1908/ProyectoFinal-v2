namespace Licencia___PF.Controllers
{
    using Licencia___PF.Model;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Licencias___PF.DTO;
    using AutoMapper;
    using Licencias___PF.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class LicenciaController : ControllerBase
    {
        private readonly LicenciaContext _context;
        private readonly LicenciaValidacion _validacionLicencia;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LicenciaController(LicenciaContext context, LicenciaValidacion validacionLicencia, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _validacionLicencia = validacionLicencia;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Devuelve una licencia específica según el ID proporcionado.
        /// </summary>
        /// <remarks>
        /// Por ejemplo:
        ///
        /// GET /licencias/5
        ///
        /// <br></br>
        /// La respuesta obtenida será del tipo:
        ///
        /// {
        ///     "id": Number,
        ///     "tipo": String,
        ///     "nombre": String,
        ///     "apellido": String,
        ///     "fechaFin": DateTime,
        ///     "fechaInicio": DateTime
        /// }
        ///
        /// </remarks>
        /// <response code="200">
        /// Respuesta exitosa.
        ///
        /// Código: 200  
        /// Mensaje: "Licencia encontrada"  
        /// Contenido: Objeto de tipo Licencia  
        /// </response>
        /// <response code="400">
        /// Se produjo un error al procesar la solicitud.
        ///
        /// Código: 400  
        /// Mensaje: "Ocurrió un error interno: [detalle del error]"  
        /// Contenido: null  
        /// </response>
        /// <response code="404">
        /// No se encontró una licencia con el ID proporcionado.
        ///
        /// Código: 404  
        /// Mensaje: "No se encontró una licencia con el ID: X"  
        /// Contenido: null  
        /// </response>
        /// <returns>
        /// Una licencia que coincide con el ID solicitado.
        /// </returns>


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var licencia = await _unitOfWork.Licencias.GetByIdAsync(id);

            if (licencia == null)
                return NotFound($"No se encontró una licencia con el ID: {id}");

            return Ok(licencia);
        }




        /// <summary>
        /// Obtiene el listado completo de licencias disponibles.
        /// </summary>
        /// <remarks>
        /// Por ejemplo:
        ///
        /// GET /licencias/listado
        ///
        /// <br></br>
        /// La respuesta obtenida será del tipo:
        ///
        /// {
        ///     "codigo": 200,
        ///     "mensaje": "Listado de licencias obtenido correctamente",
        ///     "contenido": [
        ///         {
        ///             "id": Number,
        ///             "tipo": String,
        ///             "nombre": String,
        ///             "apellido": String,
        ///             "fechaFin": DateTime,
        ///             "fechaInicio": DateTime
        ///         },
        ///         ...
        ///     ]
        /// }
        ///
        /// </remarks>
        /// <response code="200">
        /// Respuesta exitosa.
        ///
        /// Código: 200  
        /// Mensaje: "Listado de licencias obtenido correctamente"  
        /// Contenido: Lista de objetos Licencia  
        /// </response>
        /// <response code="400">
        /// Se produjo un error al procesar la solicitud.
        ///
        /// Código: 400  
        /// Mensaje: "Ocurrió un error interno: [detalle del error]"  
        /// Contenido: null  
        /// </response>
        /// <response code="500">
        /// Error en el servidor.
        ///
        /// Código: 500  
        /// Mensaje: "Ha ocurrido un error desconocido."  
        /// Contenido: null  
        /// </response>
        /// <returns>
        /// Una lista de todas las licencias almacenadas en el sistema.
        /// </returns>

        [HttpGet("listado")]
        public async Task<ActionResult> GetAll()
        {
            var licencias = await _unitOfWork.Licencias.GetAllAsync();
            return Ok(licencias);
        }





        /// <summary>
        /// Elimina una licencia existente según el ID proporcionado.
        /// </summary>
        /// <remarks>
        /// Por ejemplo:
        ///
        /// DELETE /licencias/5
        ///
        /// <br></br>
        /// La respuesta obtenida será del tipo:
        ///
        /// {
        ///     "filasAlteradas": 1,
        ///     "mensaje": "Licencia eliminada correctamente."
        /// }
        ///
        /// </remarks>
        /// <response code="200">
        /// Licencia eliminada exitosamente.
        ///
        /// Código: 200  
        /// Mensaje: "Licencia eliminada correctamente."  
        /// Contenido: Número de filas alteradas  
        /// </response>
        /// <response code="404">
        /// No se encontró ninguna licencia con el ID especificado.
        ///
        /// Código: 404  
        /// Mensaje: "Licencia no encontrada."  
        /// Contenido: null  
        /// </response>
        /// <response code="500">
        /// Error interno del servidor al intentar eliminar la licencia.
        ///
        /// Código: 500  
        /// Mensaje: "Ha ocurrido un error desconocido."  
        /// Contenido: null  
        /// </response>
        /// <returns>
        /// Devuelve el resultado de la operación de eliminación, incluyendo cuántas filas fueron afectadas.
        /// </returns>
        /// 
        
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var licencia = await _unitOfWork.Licencias.GetByIdAsync(id);

            if (licencia == null)
                return NotFound(new { mensaje = "Licencia no encontrada." });

            _unitOfWork.Licencias.Remove(licencia);
            await _unitOfWork.CommitAsync();

            return Ok(new { mensaje = "Licencia eliminada correctamente." });
        }
    


        /// <summary>
        /// Inserta una nueva licencia en la base de datos a partir de los datos proporcionados.
        /// </summary>
        /// <remarks>
        /// Por ejemplo:
        ///
        /// POST /licencias
        /// {
        ///     "nombre": "Juan",
        ///     "apellido": "Pérez",
        ///     "dni": "12345678",
        ///     "tipoDeLicencia": "B1",
        ///     "provincia": "Buenos Aires",
        ///     "localidad": "La Plata",
        ///     "direccion": "Calle Falsa 123",
        ///     "od": "OD-001",
        ///     "fechaInicio": "2025-01-01T00:00:00",
        ///     "fechaFin": "2026-01-01T00:00:00"
        /// }
        ///
        /// <br></br>
        /// La respuesta obtenida será del tipo:
        ///
        /// {
        ///     "mensaje": "Licencia agregada correctamente.",
        ///     "id": Number
        /// }
        ///
        /// </remarks>
        /// <response code="200">
        /// Licencia creada exitosamente.
        ///
        /// Código: 200  
        /// Mensaje: "Licencia agregada correctamente."  
        /// Contenido: ID de la nueva licencia  
        /// </response>
        /// <response code="400">
        /// Datos inválidos enviados en la solicitud.
        ///
        /// Código: 400  
        /// Mensaje: "ModelState con errores"  
        /// Contenido: Detalles del error de validación  
        /// </response>
        /// <response code="500">
        /// Error interno del servidor al intentar guardar la licencia.
        ///
        /// Código: 500  
        /// Mensaje: "Ha ocurrido un error desconocido."  
        /// Contenido: null  
        /// </response>
        /// <returns>
        /// Confirma la inserción de una nueva licencia y devuelve su ID.
        /// </returns>

        [HttpPost]
        public async Task<ActionResult> InsertarLicencia([FromBody] LicenciaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var nuevaLicencia = _mapper.Map<Licencia>(dto);

            await _unitOfWork.Licencias.AddAsync(nuevaLicencia);
            await _unitOfWork.CommitAsync();

            return Ok(new { mensaje = "Licencia agregada correctamente.", id = nuevaLicencia.Id });
        }

        /// <summary>
        /// Actualiza una licencia existente con los datos proporcionados.
        /// </summary>
        /// <remarks>
        /// Por ejemplo:
        ///
        /// PUT /licencias/5  
        /// {
        ///     "nombre": "Juan",
        ///     "apellido": "Pérez",
        ///     "dni": "12345678",
        ///     "tipoDeLicencia": "ordinaria",
        ///     "provincia": "Buenos Aires",
        ///     "localidad": "La Plata",
        ///     "direccion": "Calle Falsa 123",
        ///     "od": "OD00123",
        ///     "fechaInicio": "2025-01-01",
        ///     "fechaFin": "2025-12-31"
        /// }
        ///
        /// <br></br>
        /// La respuesta obtenida será del tipo:
        ///
        /// {
        ///     "mensaje": "Licencia actualizada correctamente."
        /// }
        /// </remarks>
        /// <response code="200">
        /// Licencia actualizada exitosamente.
        ///
        /// Código: 200  
        /// Mensaje: "Licencia actualizada correctamente."  
        /// Contenido: null  
        /// </response>
        /// <response code="400">
        /// Datos inválidos enviados en la solicitud.
        ///
        /// Código: 400  
        /// Mensaje: "Datos inválidos en la solicitud." o errores en ModelState  
        /// Contenido: Detalles del error  
        /// </response>
        /// <response code="404">
        /// No se encontró la licencia especificada.
        ///
        /// Código: 404  
        /// Mensaje: "Licencia no encontrada."  
        /// Contenido: null  
        /// </response>
        /// <response code="500">
        /// Error interno del servidor al intentar actualizar la licencia.
        ///
        /// Código: 500  
        /// Mensaje: "Ha ocurrido un error desconocido."  
        /// Contenido: null  
        /// </response>
        /// <returns>
        /// Confirma la actualización de la licencia especificada por su ID.
        /// </returns>

        [HttpPut("{id}")]
        public async Task<ActionResult> ActualizarLicencia(int id, [FromBody] LicenciaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!await _validacionLicencia.LicenciaExists(id))
                return NotFound(new { mensaje = "Licencia no encontrada." });

            var licencia = await _unitOfWork.Licencias.GetByIdAsync(id);

            _mapper.Map(dto, licencia);
            _unitOfWork.Licencias.Update(licencia);
            await _unitOfWork.CommitAsync();

            return Ok(new { mensaje = "Licencia actualizada correctamente." });
        }


    }
}
