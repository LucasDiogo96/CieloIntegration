using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.BaseEntities
{
    public class AuthenticatioModel
    {
        [Required]
        public string AppName { get; set; }
        [Required]
        public string AppKey { get; set; }
    }
}
