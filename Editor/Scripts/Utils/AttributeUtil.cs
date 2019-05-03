//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-10:11
//AnimationExpansion.Editor

using System;
using System.Collections.Generic;

namespace Icarus.IcAttribute.Utils
{
    public class AttributeUtil
    {
        /// <summary>
        /// 获取当前域,排除System程序集外其他程序集所有的被指定类型的特性标记的类型
        /// </summary>
        /// <typeparam name="T">特性类型</typeparam>
        /// <param name="outResult">key:特性,value:所标记的脚本类型</param>
        public static void GetFilterSystemAssemblyAllTAttributeType<T>(Dictionary<T,Type> outResult) where T : System.Attribute
        {
            outResult.Clear();
            List<System.Type> types = new List<Type>();
            Util.Type.GetFilterSystemAssemblyAllType(types);

            foreach (var type in types)
            {
                var customAttributes = System.Attribute.GetCustomAttributes(type, typeof(T));
                
                foreach (var attribute in customAttributes)
                {
                    outResult.Add((T) attribute,type);
                }
            }
        }
    }
}