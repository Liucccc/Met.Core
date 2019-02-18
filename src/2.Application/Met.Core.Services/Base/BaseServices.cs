using Met.Core.IRepository;
using Met.Core.IServices;
using Met.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Met.Core.Services
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> baseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public async Task<TEntity> QueryByID(object objId)
        {
            return await baseDal.QueryByID(objId);
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await baseDal.QueryByID(objId, blnUseCache);
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await baseDal.QueryByIDs(lstIds);
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByClause(Expression<Func<TEntity, bool>> predicate)
        {
            return await baseDal.QueryByClause(predicate);
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByClause(string strWhere)
        {
            return await baseDal.QueryByClause(strWhere);
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Add(TEntity entity)
        {
            return await baseDal.Add(entity) > 0 ? true : false;
        }

        /// <summary>
        /// 批量写入实体数据
        /// </summary>
        /// <param name="entity">列表</param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> list)
        {
            return await baseDal.Add(list);
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            return await baseDal.Update(entity);
        }
        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await baseDal.Update(entity, strWhere);
        }

        public async Task<bool> Update(
         TEntity entity,
         List<string> lstColumns = null,
         List<string> lstIgnoreColumns = null,
         string strWhere = ""
            )
        {
            return await baseDal.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }


        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">博文实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            return await baseDal.Delete(entity);
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            return await baseDal.DeleteById(id);
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            return await baseDal.DeleteByIds(ids);
        }

        /// <summary>
        /// 删除指定条件表达式的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Delete(whereExpression);
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string strWhere)
        {
            return await baseDal.Delete(strWhere);
        }

        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await baseDal.Query();
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await baseDal.Query(strWhere);
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await baseDal.Query(whereExpression);
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await baseDal.Query(whereExpression, orderByExpression, isAsc);
        }

        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await baseDal.Query(whereExpression, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            string strWhere,
            int intTop,
            string strOrderByFileds)
        {
            return await baseDal.Query(strWhere, intTop, strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intPageIndex,
            int intPageSize,
            string strOrderByFileds)
        {
            return await baseDal.Query(
              whereExpression,
              intPageIndex,
              intPageSize,
              strOrderByFileds);
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:AZLinli.Blog.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码（下标0）</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="intTotalCount">数据总量</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
          string strWhere,
          int intPageIndex,
          int intPageSize,
          string strOrderByFileds)
        {
            return await baseDal.Query(
            strWhere,
            intPageIndex,
            intPageSize,
            strOrderByFileds);
        }

        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        public async Task<PageData<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int pageIndex = 1, int pageSize = 20, string orderBy = "")
        {
            return await baseDal.QueryPage(whereExpression, pageIndex, pageSize, orderBy);
        }

        /// <summary>
        /// 根据条件查询分页数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex">当前页面索引</param>
        /// <param name="pageSize">分布大小</param>
        /// <returns></returns>
        public async Task<PageData<TEntity>> QueryPage(string strWhere = "", int pageIndex = 1, int pageSize = 20, string orderBy = "")
        {
            return await baseDal.QueryPage(strWhere, pageIndex, pageSize, orderBy);
        }
    }
}
