using MainTz.Application.Models;

namespace MainTz.Application.Repositories
{
	public interface IManufacturerRepository
	{
		public Task<Manufacturer> CreateManufacturer(Manufacturer manufacturer);
		public Task<Manufacturer> DeleteById(int id);
		public Task<Manufacturer> DeleteByName(string name);
		public Task<List<Manufacturer>> GetManufacturersAsync();
	}
}
