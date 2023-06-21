using FINALEXAM.DAL;
using Microsoft.EntityFrameworkCore;

namespace FINALEXAM.Services
{
    public class LayoutService
    {
        private readonly AppDBContext _context;
        private readonly IHttpContextAccessor _http;

        public LayoutService(AppDBContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public async Task<Dictionary<string, string>> GetSettingsAsync()
        {
            var setting = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return setting;
        }
    }
}
