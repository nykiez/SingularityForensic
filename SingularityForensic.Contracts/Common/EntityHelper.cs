using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingularityForensic.Contracts.Common {
    public static class EntityHelper {
        /// <summary>
        /// 从某个类似树形实体中根据对应参数找到一个实体节点;
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <param name="entity"></param>
        /// <param name="splitParams"></param>
        /// <param name="getChildren"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static TEntity GetEntityFromParams<TEntity, TParam>(
           TEntity entity,
           TParam[] splitParams,
           Func<TEntity, IEnumerable<TEntity>> getChildren,
           Func<TEntity, TParam, bool> predicate) {
            if (EqualityComparer<TEntity>.Default.Equals(entity)) {
                return default(TEntity);
            }
            if (splitParams == null || splitParams.Length == 0) {
                return default(TEntity);
            }
            if (getChildren == null) {
                throw new ArgumentNullException(nameof(getChildren));
            }
            if (predicate == null) {
                throw new ArgumentNullException(nameof(predicate));
            }

            TEntity entityNode = default(TEntity);
            if (predicate(entity, splitParams[0])) {
                entityNode = entity;
            }
            else {
                return default(TEntity);
            }

            if (splitParams.Length == 1) {
                return entityNode;
            }

            for (int index = 1; index < splitParams.Length - 1; index++) {
                entityNode = getChildren(entityNode).FirstOrDefault(p => predicate(p, splitParams[index]));
                if (EqualityComparer<TEntity>.Default.Equals(entityNode)) {
                    return default(TEntity);
                }
            }

            return getChildren(entityNode).FirstOrDefault(p => predicate(p, splitParams[splitParams.Length - 1]));
        }


        /// <summary>
        /// 得到指定节点以上所有父节点;
        /// </summary>
        /// <param name="ownerEntity"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> GetParentEntities<TEntity>(
            this TEntity ownerEntity, 
            TEntity entity, 
            Func<TEntity, TEntity> getParent,
            bool selfIncluded = false
        ) where TEntity:class{

            if (ownerEntity == null) {
                throw new ArgumentNullException(nameof(ownerEntity));
            }
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }
            if(getParent == null) {
                throw new ArgumentNullException(nameof(getParent));
            }

            if (!CheckOwn(ownerEntity, entity,getParent)) {
                throw new InvalidOperationException($"{nameof(ownerEntity)} doesn't own the file {nameof(entity)}");
            }

            TEntity fileNode = selfIncluded ? entity : getParent(entity) ;
            while (fileNode != null) {
                yield return fileNode;
                if (fileNode == ownerEntity) {
                    yield break;
                }
                fileNode = getParent(fileNode);
            }
        }

        /// <summary>
        /// 检查某个集合中是否包含某个节点;
        /// </summary>
        /// <param name="parentEntity"></param>
        /// <param name="childEntity"></param>
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
