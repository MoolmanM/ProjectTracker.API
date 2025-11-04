using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NuGet.Common;

namespace TaskManager.Dtos.Auth
{
    public record AuthResponseDto
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
        public DateTime ExpiresAt { get; init; }
        public string UserId { get; init; } = null!;
        public string Email { get; init; } = null!;
    }
}