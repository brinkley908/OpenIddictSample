using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ResourceService.Repository
{
    public class DataRepositoryBase<TEntity> : IDataRepositoryBase<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public DataRepositoryBase( DbContext context )
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        //public virtual IEnumerable<TEntity> Get( string query,
        //    params object[] parameters )
        //{
        //    return null dbSet..SqlQuery( query, parameters ).ToList();
        //}

        public IQueryable<TEntity> GetAll( string include = null )
        {
            if ( string.IsNullOrEmpty( include ) )
                return _dbSet;

            else
                return _dbSet.Include( include );
        }

        public virtual IQueryable<TEntity> Query(
           Expression<Func<TEntity, bool>>
           filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "" )
        {
            IQueryable<TEntity> query = _dbSet;

            if ( filter != null )
            {
                query = query.Where( filter );
            }

            if ( includeProperties != null )
            {
                foreach ( var includeProperty in includeProperties.Split
                ( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries ) )
                {
                    query = query.Include( includeProperty );
                }
            }


            if ( orderBy != null )
            {
                return orderBy( query );
            }
            else
            {
                return query;
            }

        }


        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "" )
        {
            IQueryable<TEntity> query = _dbSet;

            if ( filter != null )
            {
                query = query.Where( filter );
            }

            if ( includeProperties != null )
            {
                foreach ( var includeProperty in includeProperties.Split
                ( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries ) )
                {
                    query = query.Include( includeProperty );
                }
            }


            if ( orderBy != null )
            {
                return orderBy( query ).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID( object id )
        {
            return _dbSet.Find( id );
        }

        public virtual void Insert( TEntity entity )
        {
            _dbSet.Add( entity );
        }

        public virtual void Delete( object id )
        {
            TEntity entityToDelete = _dbSet.Find( id );
            Delete( entityToDelete );
        }

        public virtual void Delete( TEntity entityToDelete )
        {
            if ( _context.Entry( entityToDelete ).State == EntityState.Detached )
            {
                _dbSet.Attach( entityToDelete );
            }
            _dbSet.Remove( entityToDelete );
        }

        public virtual void Update( TEntity entityToUpdate )
        {
            _dbSet.Attach( entityToUpdate );
            _context.Entry( entityToUpdate ).State = EntityState.Modified;
        }
    }
}
