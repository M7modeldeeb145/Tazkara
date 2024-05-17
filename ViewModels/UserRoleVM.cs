using System.ComponentModel.DataAnnotations;

namespace Tazkara.ViewModels
{
	public class UserRoleVM
	{
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
