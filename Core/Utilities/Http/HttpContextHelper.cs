﻿using Microsoft.AspNetCore.Http;

namespace Core.Utilities.Http
{
    public static class HttpContextHelper
    {
        private static IHttpContextAccessor? _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext? HttpContext => _httpContextAccessor?.HttpContext is not null ? _httpContextAccessor.HttpContext : null;
    }
}
