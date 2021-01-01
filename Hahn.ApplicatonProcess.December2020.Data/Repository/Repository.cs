using Hahn.ApplicatonProcess.December2020.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository
{
    /// <summary>
    ///    Implementation of <see cref="Repository"/>
    /// </summary>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDBContext context;
        private DbSet<T> entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="ApplicationDBContext"/> to use.</param>
        public Repository(ApplicationDBContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        /// <inheritdoc />
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        /// <inheritdoc />
         public T GetById(int id) => entities.Find(id);

        /// <inheritdoc />
        public void Insert(T entity)
        {
            entities.Add(entity);
            context.SaveChanges();
        }

        /// <inheritdoc />
        public void Update(T entity)
        {
            entities.Update(entity);
            context.SaveChanges();
        }

        /// <inheritdoc />
        public void Delete(T entity)
        {

            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
