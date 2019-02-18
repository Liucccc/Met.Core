using Met.Core.IRepository;
using Met.Core.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Met.Core.Repository.SqlServer
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<TEntity> entityDB;

        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        internal SimpleClient<TEntity> EntityDB
        {
            get { return entityDB; }
            private set { entityDB = value; }
        }
        public BaseRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<TEntity>(db);
        }



        public async Task<TEntity> QueryByID(object objId)
        {
            return await Task.Run(() => db.Queryable<TEntity>().InSingle(objId));
        }
        /// <summary>
        /// 功能描述:根据ID查询一条数据
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="objId">id（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns>数据实体</returns>
        public async Task<TEntity> QueryByID(object objId, bool blnUseCache = false)
        {
            return await Task.Run(() => db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingle(objId));
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="predicate">条件表达式树</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByClause(Expression<Func<TEntity, bool>> predicate)
        {
            return await Task.Run(() => db.Queryable<TEntity>().First(predicate));
        }

        /// <summary>
        /// 根据条件查询数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<TEntity> QueryByClause(string strWhere)
        {
            return await Task.Run(() => db.Queryable<TEntity>().Where(strWhere).First());
        }

        /// <summary>
        /// 功能描述:根据ID查询数据
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="lstIds">id列表（必须指定主键特性 [SugarColumn(IsPrimaryKey=true)]），如果是联合主键，请使用Where条件</param>
        /// <returns>数据实体列表</returns>
        public async Task<List<TEntity>> QueryByIDs(object[] lstIds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().In(lstIds).ToList());
        }

        /// <summary>
        /// 写入实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<int> Add(TEntity entity)
        {
            var i = await Task.Run(() => db.Insertable(entity).ExecuteReturnBigIdentity());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return (int)i;
        }

        /// <summary>
        /// 批量写入实体数据
        /// </summary>
        /// <param name="entity">列表</param>
        /// <returns></returns>
        public async Task<bool> Add(List<TEntity> list)
        {
            var i = await Task.Run(() => db.Insertable(list.ToArray()).ExecuteCommand());
            //返回的i是long类型,这里你可以根据你的业务需要进行处理
            return i > 0;
        }

        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> Update(TEntity entity)
        {
            //这种方式会以主键为条件
            var i = await Task.Run(() => db.Updateable(entity).ExecuteCommand());
            return i > 0;
        }

        public async Task<bool> Update(TEntity entity, string strWhere)
        {
            return await Task.Run(() => db.Updateable(entity).Where(strWhere).ExecuteCommand() > 0);
        }

        public async Task<bool> Update(string strSql, SugarParameter[] parameters = null)
        {
            return await Task.Run(() => db.Ado.ExecuteCommand(strSql, parameters) > 0);
        }

        public async Task<bool> Update(
          TEntity entity,
          List<string> lstColumns = null,
          List<string> lstIgnoreColumns = null,
          string strWhere = ""
            )
        {
            IUpdateable<TEntity> up = await Task.Run(() => db.Updateable(entity));
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                up = await Task.Run(() => up.IgnoreColumns(it => lstIgnoreColumns.Contains(it)));
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                up = await Task.Run(() => up.UpdateColumns(it => lstColumns.Contains(it)));
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                up = await Task.Run(() => up.Where(strWhere));
            }
            return await Task.Run(() => up.ExecuteCommand()) > 0;
        }

        /// <summary>
        /// 根据实体删除一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public async Task<bool> Delete(TEntity entity)
        {
            var i = await Task.Run(() => db.Deleteable(entity).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID的数据
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        public async Task<bool> DeleteById(object id)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>(id).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定ID集合的数据(批量删除)
        /// </summary>
        /// <param name="ids">主键ID集合</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIds(object[] ids)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>().In(ids).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定条件表达式的数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>().Where(whereExpression).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<bool> Delete(string strWhere)
        {
            var i = await Task.Run(() => db.Deleteable<TEntity>().Where(strWhere).ExecuteCommand());
            return i > 0;
        }

        /// <summary>
        /// 功能描述:查询所有数据
        /// 作　　者:Met.Core
        /// </summary>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query()
        {
            return await Task.Run(() => entityDB.GetList());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return await Task.Run(() => db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }

        /// <summary>
        /// 功能描述:查询数据列表
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="whereExpression">whereExpression</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => entityDB.GetList(whereExpression));
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToList());
        }
        /// <summary>
        /// 功能描述:查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc).WhereIF(whereExpression != null, whereExpression).ToList());
        }

        /// <summary>
        /// 功能描述:查询一个列表
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToList());
        }


        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Met.Core
        /// </summary>
        /// <param name="whereExpression">条件表达式</param>
        /// <param name="intTop">前N条</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns>数据列表</returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression,
            int intTop,
            string strOrderByFileds)
        {
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).Take(intTop).ToList());
        }

        /// <summary>
        /// 功能描述:查询前N条数据
        /// 作　　者:Met.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop).ToList());
        }



        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Met.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(whereExpression != null, whereExpression).ToPageList(intPageIndex, intPageSize));
        }

        /// <summary>
        /// 功能描述:分页查询
        /// 作　　者:Met.Core
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
            return await Task.Run(() => db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).ToPageList(intPageIndex, intPageSize));
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
            var totalCount = 0;
            var page = await Task.Run(() => db.Queryable<TEntity>()
            .WhereIF(whereExpression != null, whereExpression)
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .ToPageList(pageIndex, pageSize, ref totalCount));
            var list = new PageData<TEntity>(page, pageIndex, pageSize, totalCount);
            return list;
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
            var totalCount = 0;
            var page = await Task.Run(() => db.Queryable<TEntity>().
            WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .ToPageList(pageIndex, pageSize, ref totalCount));
            var list = new PageData<TEntity>(page, pageIndex, pageSize, totalCount);
            return list;
        }


    }
}
