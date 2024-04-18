﻿namespace LMS.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T? GetById(int id);
        IQueryable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}