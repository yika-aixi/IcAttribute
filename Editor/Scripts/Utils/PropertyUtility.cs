//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-10:30
//AnimationExpansion.Editor

using UnityEditor;
using System.Reflection;

namespace Icarus.IcAttribute.Utils
{
    public static class PropertyUtility
    {
        public static T GetAttribute<T>(this SerializedProperty self) where T : System.Attribute
        {
            T[] attributes = GetAttributes<T>(self);
            return attributes.Length > 0 ? attributes[0] : null;
        }

        public static T[] GetAttributes<T>(this SerializedProperty self) where T : System.Attribute
        {
            FieldInfo fieldInfo = ReflectionUtility.GetField(GetTargetObject(self), self.name);

            return (T[])fieldInfo.GetCustomAttributes(typeof(T), true);
        }

        public static UnityEngine.Object GetTargetObject(SerializedProperty property)
        {
            return property.serializedObject.targetObject;
        }
    }
}
