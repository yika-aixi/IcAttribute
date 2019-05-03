//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-09:10
//AnimationExpansion.Editor

using System;

namespace Icarus.IcAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class HandleAttribute:Attribute
    {
        public Type HandleType { get; }

        /// <summary>
        /// 处理的类型
        /// </summary>
        /// <param name="handleType"></param>
        public HandleAttribute(Type handleType)
        {
            this.HandleType = handleType;
        }
    }
}