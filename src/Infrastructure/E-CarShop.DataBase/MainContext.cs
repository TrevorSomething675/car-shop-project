using E_CarShop.Core.ConfigurationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace E_CarShop.DataBase
{
    public class MainContext(IOptions<DataBaseOptions> options) : DbContext
    {
        private readonly DataBaseOptions _options = options.Value;


    }
}