namespace LearningTogether.Services.Data
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;

    using LearningTogether.Data.Common.Models;
    using LearningTogether.Data.Models;

    public interface IGenericItemsService<T>
        where T : BaseModel<int>
    {
        IQueryable<T> GetTop(int count);

        IQueryable<T> All();

        T GetById(int id);

        void Create(T item);

        void Update(T item);

        void DeleteById(int id);
    }
}