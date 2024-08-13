using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.EducationDtos
{
    public class EducationForEditDto
    {
        [RegularExpression("^[a-zA-Z ]{1,30}", ErrorMessage = "El título solamente puede contener hasta 30 caracteres, solamete se permiten letras.")]
        public string Title { get; set; }

        [RegularExpression("^[a-zA-Z0-9 ]{1,3000}$", ErrorMessage = "La descripción solamente puede contener hasta 3000 caracteres, solamete se permiten letras y números.")]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
