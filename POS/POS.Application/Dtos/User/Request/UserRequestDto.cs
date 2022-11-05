using Microsoft.AspNetCore.Http;
using POS.Application.Validators.Generic;
using POS.Application.Validators.TipoArchivoValidacion;

namespace POS.Application.Dtos.User.Request
{
    public class UserRequestDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile? Image { get; set; }
        public int State { get; set; }

    }
}
