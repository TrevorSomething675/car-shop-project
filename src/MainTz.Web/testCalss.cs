/*
using System;
using AutoMapper;
using KKDTS.Core.Extensions;
using KKDTS.Dal.Entities.ControlTechnicalPoints;
using KKDTS.Server.Service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KKDTS.Core.Specifications;
using KKDTS.Core.Specifications.Pagination;
using KKDTS.Http.Models.Configurations;
using KKDTS.Server.DL;
using KKDTS.Server.Service.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Internal;

namespace KKDTS.Infrastructure.Repositories
{
	public class ControlTechnicalPointRepository : RepositoryBase<ControlTechnicalPoint, ControlTechnicalPointEntity, int>, IControlTechnicalPointRepository
	{
		public ControlTechnicalPointRepository(IDbContextFactory<KKDTSDbContext> dbContextFactory, IMapper mapper) : base(dbContextFactory, mapper)
		{

		}

		public override async Task<ControlTechnicalPoint?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
		{
			await using var context = _dbContextFactory.CreateDbContext();
			var ctp = await context.ControlTechnicalPoints
				.Include(ctp => ctp.TrafficLanes)
					.ThenInclude(tl => tl.Devices)
				.Include(ctp => ctp.IntegrationSystems)
				.FirstOrDefaultAsync(ctp => ctp.Id == id, cancellationToken);
			return _mapper.Map<ControlTechnicalPoint>(ctp);
		}

		// NOTE: метод переопределен, т.к. есть проблемы в ProjectTo для json полей
		public override async Task<Page<ControlTechnicalPoint>> GetPageAsync(IPagingSpecification specification, CancellationToken cancellationToken = default)
		{
			await using var context = _dbContextFactory.CreateDbContext();
			var page = await context.ControlTechnicalPoints
				.ApplySpecification(specification as ISpecification<ControlTechnicalPointEntity>)
				.ToPageAsync(specification, cancellationToken);

			return _mapper.Map<Page<ControlTechnicalPoint>>(page);
		}

		// NOTE: Метод переопределен, т.к. в ином случае прилетают ошибки о трекинге сущностей в БД
		public override async Task<ControlTechnicalPoint> UpdateAsync(ControlTechnicalPoint modelToUpdate, CancellationToken cancellationToken = default)
		{
			await using var context = _dbContextFactory.CreateDbContext();

			var entity = _mapper.Map<ControlTechnicalPointEntity>(modelToUpdate);

			var mainEntity = await context.ControlTechnicalPoints
				.Include(ctp => ctp.TrafficLanes)
				.Include(ctp => ctp.IntegrationSystems)
				.Include(ctp => ctp.Users)
				.Where(ctp => ctp.Id == modelToUpdate.Id)
				.FirstOrDefaultAsync(cancellationToken);

			mainEntity.IsInspectionComplex = entity.IsInspectionComplex;
			mainEntity.Parameters = entity.Parameters;
			mainEntity.Access = entity.Access;
			mainEntity.Address = entity.Address;
			mainEntity.Name = entity.Name;
			mainEntity.IsDeleted = entity.IsDeleted;
			mainEntity.ObjectId = entity.ObjectId;

			entity = context.ControlTechnicalPoints.Update(mainEntity).Entity;
			await context.SaveChangesAsync(cancellationToken);
			return _mapper.Map<ControlTechnicalPoint>(entity);
		}

		public async Task<List<ControlTechnicalPoint>> GetRangeAsync(IEnumerable<int> ids, CancellationToken token = default)
		{
			await using var context = _dbContextFactory.CreateDbContext();
			var ctps = await context.ControlTechnicalPoints
				.Include(ctp => ctp.TrafficLanes)
				.Where(ctp => ids.Contains(ctp.Id))
				.ToListAsync(token);

			return _mapper.Map<List<ControlTechnicalPoint>>(ctps);
		}
		*/