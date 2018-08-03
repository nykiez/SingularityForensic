using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class EntityHelper {
        /// <summary>
        /// 从某个树(节点)中根据对应参数找到一个实体节点;
        /// (例如从路径参数中找到文件);
        /// </summary>
        /// <typeparam name="TEntity">节点类型</typeparam>
        /// <typeparam name="TParam">参数类型</typeparam>
        /// <param name="entity">树(节点)</param>
        /// <param name="splitParams">参数数组(例如路径参数数组)</param>
        /// <param name="getChildren">得到子节点的委托</param>
        /// <param name="predicate">判定对应的节点与某个参数是否匹配(例如文件名与路径参数名称是否相等)</param>
        /// <returns></returns>
        public static TEntity GetEntityFromParams<TEntity, TParam>(
           this TEntity entity,
           TParam[] splitParams,
           Func<TEntity, IEnumerable<TEntity>> getChildren,
           Func<TEntity, TParam, bool> predicate) {
            if (EqualityComparer<TEntity>.Default.Equals(entity)) {
                return default;
            }
            if (splitParams == null || splitParams.Length == 0) {
                return default;
            }
            if (getChildren == null) {
                throw new ArgumentNullException(nameof(getChildren));
            }
            if (predicate == null) {
                throw new ArgumentNullException(nameof(predicate));
            }

            TEntity entityNode = default;
            if (predicate(entity, splitParams[0])) {
                entityNode = entity;
            }
            else {
                return default;
            }

            if (splitParams.Length == 1) {
                return entityNode;
            }

            for (int index = 1; index < splitParams.Length - 1; index++) {
                entityNode = getChildren(entityNode).FirstOrDefault(p => predicate(p, splitParams[index]));
                if (EqualityComparer<TEntity>.Default.Equals(entityNode)) {
                    return default;
                }
            }

            return getChildren(entityNode).FirstOrDefault(p => predicate(p, splitParams[splitParams.Length - 1]));
        }


        /// <summary>
        /// 得到指定节点以上所有父节点;
        /// 若设定了截至节点,则到截至节点为止;
        /// </summary>
        /// <param name="stopEntity">截至节点</param>
        /// <param name="entity">节点</param>
        /// <param name="getParent">得到父节点</param>
        /// <param name="selfIncluded">是否包含自身</param>
        /// <returns></returns>
        public static IEnumerable<TEntity> GetParentEntities<TEntity>(
            this TEntity entity,
            TEntity stopEntity, 
            Func<TEntity, TEntity> getParent,
            bool selfIncluded = false
        ) where TEntity:class{

            if (stopEntity == null) {
                throw new ArgumentNullException(nameof(stopEntity));
            }
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }
            if(getParent == null) {
                throw new ArgumentNullException(nameof(getParent));
            }

            if (!CheckOwn(stopEntity, entity,getParent)) {
                throw new InvalidOperationException($"{nameof(stopEntity)} doesn't own the file {nameof(entity)}");
            }

            TEntity fileNode = selfIncluded ? entity : getParent(entity) ;
            while (fileNode != null) {
                yield return fileNode;
                if (fileNode == stopEntity) {
                    yield break;
                }
                fileNode = getParent(fileNode);
            }
        }

        /// <summary>
        /// 检查某个树形父节点中是否包含某个子节点;
        /// </summary>
        /// <param name="parentEntity">父节点</param>
        /// <param name="childEntity">子节点</param>
        /// <returns></returns>
        public static bool CheckOwn<TEntity>(this TEntity parentEntity, TEntity childEntity,Func<TEntity,TEntity> getParent) where TEntity : class {
            if (parentEntity == null) {
                throw new ArgumentNullException(nameof(parentEntity));
            }

            if (childEntity == null) {
                throw new ArgumentNullException(nameof(childEntity));
            }

            TEntity entityNode = childEntity;
            while (entityNode != null) {
                if (entityNode == parentEntity) {
                    break;
                }
                entityNode = getParent(entityNode);
            }

            return entityNode != null;
        }
    }
}
