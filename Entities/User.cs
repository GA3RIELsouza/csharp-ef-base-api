using Base_API.DTO;
using Base_API.Infrastructure.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Base_API.Entities
{
    [Table("user")]
    public class User : EntityBase
    {
        [Required]
        [Column("name")]
        [MinLength(2)]
        [MaxLength(64)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [Column("email")]
        [MinLength(3)]
        [MaxLength(254)]
        public string Email { get; set; } = String.Empty;

        [Required]
        [Column("password_hash")]
        [MinLength(32)]
        [MaxLength(32)]
        public byte[] PasswordHash { get; set; } = [32];

        [Required]
        [Column("password_salt_hash")]
        [MinLength(16)]
        [MaxLength(16)]
        public byte[] PasswordSaltHash { get; set; } = [16];

        [Required]
        [Column("is_admin")]
        public bool IsAdmin { get; set; } = false;

        public static User FromRegisterDto(RegisterDto dto)
        {
            (var passwordHash, var passwordSaltHash) = dto.Password.HashPassword();

            return new()
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = passwordHash,
                PasswordSaltHash = passwordSaltHash
            };
        }

        public void UpdateFromDto(UpdateUserDto dto)
        {
            this.Name = String.IsNullOrWhiteSpace(dto.Name) ? this.Name : dto.Name;
        }
    }
}
