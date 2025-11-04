using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Dtos.Auth
{
    public record RegisterDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; init; }
        [Required]
        public string? Password { get; init; }
        [Required]
        public string? ConfirmPassword { get; init; }
        [Required]
        public string? FirstName { get; init; }
        [Required]
        public string? LastName { get; init; }
    }
}