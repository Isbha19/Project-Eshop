using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Data.Repository
{
    public interface IRepository<T> where T : class
    { 
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null, ITempDataDictionary tempData = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null,bool tracked=false, ITempDataDictionary tempData = null);
        void Add(T entity, ITempDataDictionary tempData = null);
        
        void Delete(T entity,ITempDataDictionary tempData=null);
        void DeleteRange(IEnumerable<T> entities, ITempDataDictionary tempData = null);
    }
}
