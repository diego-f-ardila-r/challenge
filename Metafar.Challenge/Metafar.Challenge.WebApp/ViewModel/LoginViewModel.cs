using System.ComponentModel.DataAnnotations;

namespace Metafar.Challenge.WebApp.ViewModel;

public class LoginModelViewModel
{
    [Required(ErrorMessage= "El number de tarjeta es requerido")]
    [Range(10000000, 99999999, ErrorMessage = "El numero de tarjeta debe tener al menos 8 digitos")]
    public int CardNumber { get; set; }

    [Required]
    [Range(1000, 9999, ErrorMessage = "El pin debe tener al menos 4 digitos")]
    public int Pin { get; set; }
}