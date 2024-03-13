using Eshop.Data.Data;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Eshop.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        internal DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            context.Products.Include(u => u.category);

            this.context = context;
            dbset = context.Set<T>();

        }
        public void Add(T entity, ITempDataDictionary tempData=null)
        {
            try
            {
                dbset.Add(entity);

            }catch (Exception ex)
            {
                tempData["errorMessage"] = "An error occurred while adding the entity." +ex.Message;

            }

        }

        public void Delete(T entity,ITempDataDictionary tempData=null)
        {
            try
            {
                dbset.Remove(entity);

            }
            catch (Exception ex)
            {
                tempData["errorMessage"] = "An error occurred while removing the entity." + ex.Message;

            }

           
        }

        public void DeleteRange(IEnumerable<T> entities, ITempDataDictionary tempData = null)
        {

            try
            {
                dbset.RemoveRange(entities);

            }
            catch (Exception ex)
            {
                tempData["errorMessage"] = "An error occurred while removing the entities." + ex.Message;

            }

        }

 

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked=false, ITempDataDictionary tempData = null)
        {
            try
            {
                if (tracked)
                {
                    IQueryable<T> query = dbset;
                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            query = query.Include(includeprop);
                        }
                    }
                    query = query.Where(filter);
                    return query.FirstOrDefault();
                }
                else
                {
                    IQueryable<T> query = dbset.AsNoTracking();
                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            query = query.Include(includeprop);
                        }
                    }
                    query = query.Where(filter);
                    return query.FirstOrDefault();
                }
            }catch (Exception ex)
            {
                tempData["errorMessage"] = "An error occurred while getting the entity." + ex.Message;

                return null;
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, ITempDataDictionary tempData = null)
        {
            try
            {
                if (filter == null)
                {
                    IQueryable<T> query = dbset;

                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            query = query.Include(includeprop);
                        }
                    }
                    return query.ToList();
                }
                else
                {
                    IQueryable<T> query = dbset;
                    query = query.Where(filter);

                    if (!string.IsNullOrWhiteSpace(includeProperties))
                    {
                        foreach (var includeprop in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            query = query.Include(includeprop);
                        }
                    }
                    return query.ToList();
                }
            }catch(Exception ex)
            {
                tempData["errorMessage"] = "An error occurred while getting all the entities." + ex.Message;
                return Enumerable.Empty<T>(); 

            }

        }
    }
}
