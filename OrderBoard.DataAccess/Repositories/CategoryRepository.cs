﻿using OrderBoard.AppServices.Categories.Repositories;
using OrderBoard.Infrastructure.Repository;
using OrderBoard.Contracts.Categories;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using OrderBoard.Domain.Entities;
using OrderBoard.AppServices.Other.Specifications;


namespace OrderBoard.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category, OrderBoardDbContext> _repository;
        private readonly IMapper _mapper;

        public CategoryRepository(IRepository<Category, OrderBoardDbContext> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid?> AddAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public async Task<List<CategoryInfoModel>> GetBySpecificationWithPaginationAsync(
    ISpecification<Category> specification, int take, int? skip, CancellationToken cancellationToken)
        {
            var query = _repository
                .GetAll()
                .OrderBy(item => item.Id)
                .Where(specification.PredicateExpression);

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            return await query
                .Take(take)
                .ProjectTo<CategoryInfoModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public async Task<CategoryInfoModel> GetBySpecificationAsync(ISpecification<Category> specification, CancellationToken cancellationToken)
        {
            return await _repository
                .GetByPredicate(specification.PredicateExpression)
                .ProjectTo<CategoryInfoModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public Task<List<CategoryDataModel>> GetAllChildDataByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.ParentId == id)
                .ProjectTo<CategoryDataModel>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
        public Task<CategoryDataModel> GetDataByIdAsync(Guid? id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(s => s.Id == id)
                .ProjectTo<CategoryDataModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
        public async Task UpdateAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(model, cancellationToken);
            return;
        }
        public async Task DeleteByIdAsync(Category model, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(model, cancellationToken);
            return;
        }
    }
}
