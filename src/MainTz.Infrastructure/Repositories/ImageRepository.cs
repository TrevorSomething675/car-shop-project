using MainTz.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using MainTz.Application.Models;
using MainTa.Database.Context;
using AutoMapper;

namespace MainTz.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;
        public ImageRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper) 
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }
        public async Task<Image> GetImageByIdAsync(int? id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var imageEntity = context.Images.FirstOrDefault(i => i.Id == id);
                var image = _mapper.Map<Image>(imageEntity);
                return image;
            }
        }
        public async Task<List<Image>> GetImagesAsync()
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                var imageEntities = await context.Images.ToListAsync();
                var images = _mapper.Map<List<Image>>(imageEntities);
                return images;
            }
        }
    }
}