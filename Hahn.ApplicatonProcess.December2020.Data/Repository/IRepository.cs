using Hahn.ApplicatonProcess.December2020.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repository
{
    /// <summary>
    ///     Defines repository actions.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IRepository<T> where T: BaseEntity
    {
        /// <summary>
        ///     Get all the entitys.
        /// </summary>
        /// <returns>All the entitys.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        ///     Get an entity by id.
        /// </summary>
        /// <param name="id">Id of the entity to get.</param>
        /// <returns>The matching entity or null.</returns>
        T GetById(int id);

        /// <summary>
        ///     Insert a entity.
        ///     Should not be able to update an existing entity.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        void Insert(T entity);

        /// <summary>
        ///      Updates a entity based on the id supplied.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        ///     Deletes a entity by id.
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        void Delete(T entity);
    }
}