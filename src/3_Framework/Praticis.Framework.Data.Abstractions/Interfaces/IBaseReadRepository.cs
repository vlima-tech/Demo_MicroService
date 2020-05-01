
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Praticis.Framework.Layers.Domain.Abstractions;

namespace Praticis.Framework.Layers.Data.Abstractions
{
    public interface IBaseReadRepository<TModel> : IDisposable
        where TModel : class, IModel
    {
        /// <summary>
        /// Verify if a model exists by identification key.
        /// </summary>
        /// <param name="id">Identification key of model.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns 'True' if model exists or 'False' does not exist.
        /// </returns>
        bool Exists(Guid id, bool track = false);

        /// <summary>
        /// Verify if a model exists by main identification properties. Per default, is the key.
        /// </summary>
        /// <param name="model">The model to find.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns 'True' if model exists or 'False' does not exist.
        /// </returns>
        bool Exists(TModel model, bool track = false);

        /// <summary>
        /// Find a model by identification key.
        /// Use var result = await SearchByIdAsync(id);
        /// </summary>
        /// <param name="id">The identification key of model.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a model if found. Null will be returned if entity not found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<TModel> SearchByIdAsync(Guid id, bool track = false);

        /// <summary>
        /// Find a model by main identification properties. Per default, is the key.
        /// Use var result = await FindAsync(model);
        /// </summary>
        /// <param name="model">The model to find.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a model if found. Null will be returned if entity not found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<TModel> FindAsync(TModel model, bool track = false);

        /// <summary>
        /// Filters a sequence of models based on a predicate.
        /// </summary>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a model collection that match with predicate. An empty list will be returned if nothing found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, bool track = false);

        /// <summary>
        /// Filter and order a sequence of models based on a predicate.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="orderBy">A function to sort elements of a sequence.</param>
        /// <param name="descendingOrder">Set 'True' to sort in descending order or 'False' to sort in ascending order.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a ordered model collection that match with predicate. An empty list will be returned if nothing found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> FindAsync<TKey>(Expression<Func<TModel, bool>> predicate, Func<TModel, TKey> orderBy, 
            bool descendingOrder = false, bool track = false);

        /// <summary>
        /// Filters a sequence of Model based on a predicate. Use PageIndex and PageSize parameters to limit search length.
        /// </summary>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="pageIndex">The start page list</param>
        /// <param name="pageSize">The length of page list</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a model collection that match with predicate. An empty list will be returned if nothing found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> FindAsync(Expression<Func<TModel, bool>> predicate, int pageIndex, int pageSize, bool track = false);

        /// <summary>
        /// Filter and order a sequence of models based on a predicate.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="orderBy">A function to sort elements of a sequence.</param>
        /// <param name="pageNumber">The start page list</param>
        /// <param name="pageLength">The length of page list</param>
        /// <param name="descendingOrder">Set 'True' to sort in descending order or 'False' to sort in ascending order.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns>
        /// Returns a ordered model collection that match with predicate. An empty list will be returned if nothing found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> FindAsync<TKey>(Expression<Func<TModel, bool>> predicate, Func<TModel, TKey> orderBy, int pageNumber, 
            int pageLength, bool descendingOrder = false, bool track = false);

        /// <summary>
        /// Make query on model repository. The search is executed when call finally methods (ToList, Count, etc)
        /// </summary>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns></returns>
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, bool track = false);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate">A function to test each model for a condition.</param>
        /// <param name="pageIndex">The start page list</param>
        /// <param name="pageSize">The length of page list</param>
        /// <param name="track">Enable or desable the EF Core Change Tracker</param>
        /// <returns></returns>
        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate, int pageIndex, int pageSize, bool track = false);

        /// <summary>
        /// Load all entities.
        /// </summary>
        /// <returns>
        /// Returns a task with loading action running. Empty list will be returned if not existis entities.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> GetAllAsync();

        /// <summary>
        /// Load all entities. Filters a sequence of Model based on a predicate. Use PageIndex and PageSize parameters to limit reeturn length.
        /// </summary>
        /// <param name="pageIndex">The start page list</param>
        /// <param name="pageSize">The length of page list</param>
        /// <returns>
        /// Returns a model collection. An empty list will be returned if nothing found.
        /// See errors and notifications in service bus to verify if there was any problem.
        /// </returns>
        Task<IList<TModel>> GetAllAsync(int pageIndex, int pageSize);

        /// <summary>
        /// Obtains a number of entities existing in database.
        /// </summary>
        /// <returns>Returns a number of entities existing in database.</returns>
        Task<long> CountAllAsync();

        /// <summary>
        /// Obtains a number of entities existing in database by Filters.
        /// </summary>
        /// <returns>Returns a number of entities existing in database.</returns>
        Task<long> CountAllAsync(Expression<Func<TModel, bool>> predicate, bool track = false);
    }
}