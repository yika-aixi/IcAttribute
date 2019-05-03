//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-09:20
//AnimationExpansion.Editor

using Icarus.IcAttribute.Core;
using UnityEditor;
using UnityEngine;

namespace Icarus.IcAttribute
{
    public class DrawerHandle
    {
        /// <summary>
        /// 进行处理 ,第一优先级
        /// </summary>
        /// <param name="attribute">标记的特性</param>
        /// <param name="serialized">需要处理的目标</param>
        /// <returns>如果处理了返回true,否则false</returns>
        public virtual bool Handle(BaseAttribute attribute,SerializedProperty serialized)
        {
            return false;
        }

        /// <summary>
        /// 进行处理, Handle(SerializedProperty serialized) 返回false的话使用改函数
        /// </summary>
        /// <param name="position">坐标</param>
        /// <param name="serialized">需要处理的目标</param>
        /// <param name="attribute">标记的特性</param>
        public virtual void Handle(Rect position, BaseAttribute attribute, SerializedProperty serialized)
        {
        }
    }
}