using Met.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Met.Core.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity> QueryByID(object objId);
        Task<TEntity> QueryByID(object objId, bool blnUseCache = false);
        Task<TEntity> QueryByClause(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> QueryByClause(string strWhere);
        Task<List<TEntity>> QueryByIDs(object[] lstIds);
        Task<int> Add(TEntity model);
        Task<bool> Add(List<TEntity> list);
        Task<bool> DeleteById(object id);
        Task<bool> Delete(TEntity model);
        Task<bool> DeleteByIds(object[] ids);
        Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression);
        Task<bool> Delete(string strWhere);
        Task<bool> Update(TEntity model);
        Task<bool> Update(TEntity entity, string strWhere);
        Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");
        Task<List<TEntity>> Query();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intPageIndex, int intPageSize, string strOrderByFileds);
        Task<PageData<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string orderBy = "");
        Task<PageData<TEntity>> QueryPage(string strWhere = "", int pageIndex = 1, int pageSize = 20, string orderBy = "");
    }
}
