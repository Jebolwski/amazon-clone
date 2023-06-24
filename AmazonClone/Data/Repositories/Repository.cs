using AmazonClone.Data.Context;
using AmazonClone.Domain.Entities;
using AmazonClone.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AmazonClone.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Entity
    {
        protected readonly BaseContext db;
        protected readonly DbSet<T> dbset;

        public Repository(BaseContext db)
        {
            this.db = db;
            this.dbset = db.Set<T>();
        }



        public T add(T entity)
        {
            dbset.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public bool delete(Guid id)
        {
            T entity = get(id);
            if (entity != null)
            {
                dbset.Remove(entity);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public T get(Guid id)
        {
            return dbset.FirstOrDefault(p => p.id == id);
        }

        public T update(T entity)
        {
            dbset.Update(entity);
            db.SaveChanges();
            return entity;
        }

        public IQueryable<T> getAll()
        {
            return dbset;
        }
    }
}
