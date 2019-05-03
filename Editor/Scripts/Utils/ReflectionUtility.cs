﻿//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-10:40
//AnimationExpansion.Editor

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Icarus.IcAttribute.Utils
{
    public static class ReflectionUtility
    {
        public static IEnumerable<FieldInfo> GetAllFields(this object self, Func<FieldInfo, bool> predicate)
        {
            List<Type> types = new List<Type>()
            {
                self.GetType()
            };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<FieldInfo> fieldInfos = types[i]
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(this object self, Func<PropertyInfo, bool> predicate)
        {
            List<Type> types = new List<Type>()
            {
                self.GetType()
            };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<PropertyInfo> propertyInfos = types[i]
                    .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (var propertyInfo in propertyInfos)
                {
                    yield return propertyInfo;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetAllMethods(this object self, Func<MethodInfo, bool> predicate)
        {
            IEnumerable<MethodInfo> methodInfos = self.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(predicate);

            return methodInfos;
        }

        public static FieldInfo GetField(this object self, string fieldName)
        {
            return GetAllFields(self, f => f.Name.Equals(fieldName, StringComparison.InvariantCulture)).FirstOrDefault();
        }

        public static PropertyInfo GetProperty(this object self, string propertyName)
        {
            return GetAllProperties(self, p => p.Name.Equals(propertyName, StringComparison.InvariantCulture)).FirstOrDefault();
        }

        public static MethodInfo GetMethod(this object self, string methodName)
        {
            return GetAllMethods(self, m => m.Name.Equals(methodName, StringComparison.InvariantCulture)).FirstOrDefault();
        }
    }
}
