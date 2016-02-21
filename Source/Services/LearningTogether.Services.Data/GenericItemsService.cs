namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Data.Common;
    using LearningTogether.Data.Models;

    public class GenericItemsService<T> : IGenericItemsService<T>
        where T : BaseItem
    {
        private readonly IDbRepository<T> items;

        public GenericItemsService(IDbRepository<T> items)
        {
            this.items = items;
        }

        public IQueryable<T> GetTop(int count)
        {
            return this.items.All().OrderByDescending(x => x.Ratings.Sum(y => y.Value)).Take(count);
        }

        public IQueryable<T> All()
        {
            return this.items.All().OrderByDescending(x => x.Ratings.Sum(y => y.Value));
        }

        public T GetById(int id)
        {
            return this.items.GetById(id);
        }

        public void Create(T item)
        {
            this.items.Add(item);
            this.items.SaveChanges();
        }

        public void Update(T item)
        {
            this.items.Update(item);
            this.items.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var item = this.items.GetById(id);
            this.items.Delete(item);
            this.items.SaveChanges();
        }
    }
}