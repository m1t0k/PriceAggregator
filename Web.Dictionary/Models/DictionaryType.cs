using System.ComponentModel.DataAnnotations;

namespace Web.Dictionary.Models
{
    public class DictionaryType
    {
        [Key]
        public string Name { get; set; }
    }
}