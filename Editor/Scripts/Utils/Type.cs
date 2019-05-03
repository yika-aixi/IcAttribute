//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年12月18日03:15:44
//AnimationExpansion.Editor

using System;
using System.Collections.Generic;
using System.Linq;

namespace Icarus.IcAttribute.Utils
{
    public static partial class Util
    {
        public static class Type
        {
            /// <summary>
            /// 获取当前域,排除System程序集外其他程序集所有的类型
            /// </summary>
            public static void GetFilterSystemAssemblyAllType(List<System.Type> outTypes)
            {
                outTypes.Clear();
                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var result = assemblies.Where(x => !x.GetName().FullName.Contains("System"))
                    .Select(x => x.GetTypes());

                foreach (var types in result)
                {
                    outTypes.AddRange(types);
                }
            }

            /// <summary>
            /// 获取当前域,排除System程序集外其他程序集基类的实现的AssemblyQualifiedNames
            /// </summary>
            /// <param name="typeBase"></param>
            /// <returns></returns>
            public static string[] GetFilterSystemAssemblyQualifiedNames(System.Type typeBase)
            {
                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var result = assemblies.Where(x => !x.GetName().FullName.Contains("System"))
                    .Select(x => x.GetName().FullName).ToArray();

                return GetAssemblyNames(typeBase, result,2);
            }

            /// <summary>
            /// 获取当前域,排除System程序集外其他程序集基类的实现TypeFullNames
            /// </summary>
            /// <param name="typeBase"></param>
            /// <returns></returns>
            public static string[] GetFilterSystemAssemblyTypeFullNames(System.Type typeBase)
            {
                System.Reflection.Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var result = assemblies.Where(x => !x.GetName().FullName.Contains("System"))
                    .Select(x => x.GetName().FullName).ToArray();

                return GetAssemblyNames(typeBase, result,1);
            }

            /// <summary>
            /// 获取某基类所有的类型
            /// </summary>
            /// <param name="typeBase">基类类型</param>
            /// <param name="assemblyNames">程序集集合</param>
            /// <param name="getNameType">
            ///    1: FullName
            ///    2：AssemblyQualifiedName
            ///    3：Name
            /// </param>
            /// <returns></returns>
            private static string[] GetAssemblyNames(System.Type typeBase, string[] assemblyNames, int getNameType)
            {
                List<string> typeNames = new List<string>();
                List<System.Type> types = new List<System.Type>();
                GetAssemblyNames(typeBase, assemblyNames, types);

                foreach (System.Type type in types)
                {
                    if (type.IsClass && !type.IsAbstract && typeBase.IsAssignableFrom(type))
                    {
                        switch (getNameType)
                        {
                            case 1:
                                typeNames.Add(type.FullName);
                                continue;
                            case 2:
                                typeNames.Add(type.AssemblyQualifiedName);
                                continue;
                            case 3:
                                typeNames.Add(type.Name);
                                continue;
                        }
                    }
                }

                typeNames.Sort();
                return typeNames.ToArray();
            }

            /// <summary>
            /// 获取某基类所有的类型
            /// </summary>
            /// <param name="typeBase">基类类型</param>
            /// <param name="assemblyNames">程序集集合</param>
            /// <param name="getNameType">
            ///    1: FullName
            ///    2：AssemblyQualifiedName
            ///    3：Name
            /// </param>
            /// <returns></returns>
            private static void GetAssemblyNames(System.Type typeBase, string[] assemblyNames,List<System.Type> outResult)
            {
                outResult.Clear();

                foreach (string assemblyName in assemblyNames)
                {
                    System.Reflection.Assembly assembly = null;
                    try
                    {
                        assembly = System.Reflection.Assembly.Load(assemblyName);
                    }
                    catch
                    {
                        continue;
                    }

                    if (assembly == null)
                    {
                        continue;
                    }

                    outResult.AddRange(assembly.GetTypes());
                }
            }
        }
    }
}