﻿using Commerce.Core.DataTransferObjects;

namespace Commerce.Core.Repositories.Interfaces;

public interface ICatalogRepository
{
    Task<List<ProductCatalogDTO>> GetAll();
    Task<ProductCatalogDTO> GetById(string id);
    Task<List<ProductCatalogDTO>> GetLogById(Guid id);
    Task Insert(ProductCatalogDTO catalog);
    Task InsertLog(ProductCatalogDTO catalog);
    Task Update(ProductCatalogDTO catalog);
    Task Delete(string catalog);
}