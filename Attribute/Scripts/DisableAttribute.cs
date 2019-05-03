//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-05:04
//AnimationExpansion.Editor

using System;
using Icarus.IcAttribute.Core;

namespace Icarus.IcAttribute.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DisableAttribute : BaseAttribute
    {
        public bool IsShowArraySize { get; } = false;

        public DisableAttribute()
        {
        }

        public DisableAttribute(bool isShowArraySize)
        {
            IsShowArraySize = isShowArraySize;
        }
    }
}