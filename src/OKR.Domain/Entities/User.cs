using OKR.Domain.Enums;

namespace OKR.Domain.Entities;

public class User
{
  public Guid Id { get; init; }
  public required string Name { get; init; }
  public required string Email { get; init; }
  public required string Password { get; set; }
  public required string Role { get; init; } = Roles.Staff;
}
