using DatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseCore.Interfaces
{
    public interface IDatabaseContext : IDisposable
    {
        /// <summary>
        /// Users
        /// </summary>
        public DbSet<UserModel> Users { get; set; }

        public DbSet<AccountModel> Accounts { get; set; }

        public DbSet<TransactionModel> Transactions { get; set; }

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns>int</returns>
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///     Add new entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>

        EntityEntry Add([NotNull] object entity);

        /// <summary>
        ///     Add new generic entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Add<TEntity>([NotNull] TEntity entity) where TEntity : class;

        /// <summary>
        ///     Update entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry Update([NotNullAttribute] object entity);

        /// <summary>
        ///     Update generic entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Update<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class;
    }
}
