using System.ComponentModel.DataAnnotations;

namespace trex.Models.Dto.Lobby;
public class CreateIn
{
    [Required]
    [RegularExpression("\\w{3,15}")]
    public string Nickname { get; set; }
}
