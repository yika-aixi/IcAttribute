# IcAttribute
Unity字段使用的特性绘制

简单易懂的代码,简单的编写,较好的库分配. 目前只支持Filed的操作,函数的调用暂时不支持.

如何使用:`克隆该项目到Unity工程就可以.`

编写一个自己的Filed特性,只需要继承`BaseAttribute`

`DisableAttribute.cs`
```
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
```

编写特性的处理类,只需要继承`DrawerHandle`然后在类上打上`[Handle(typeof(BaseAttribute))]`即可

`Disable`特性的处理类`DisableAttributeDrawer`:

```
[Handle(typeof(DisableAttribute))]
    public class DisableAttributeDrawer : DrawerHandle
    {
        public override bool Handle(BaseAttribute attribute, SerializedProperty serialized)
        {
            GUI.enabled = false;
            {
                if (serialized.isArray)
                {
                    EditorGUILayout.PropertyField(serialized);
                    if (serialized.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        {
                            if (((DisableAttribute)attribute).IsShowArraySize)   
                            {
                                EditorGUILayout.PropertyField(serialized.FindPropertyRelative("Array.size"));
                            }

                            for (var i = 0; i < serialized.arraySize; i++)
                            {
                                var element = serialized.GetArrayElementAtIndex(i);
                                EditorGUILayout.PropertyField(element,true);
                            }
                        }
                        EditorGUI.indentLevel--;
                    }

                }
                else
                {
                    EditorGUILayout.PropertyField(serialized);
                }
            }
            GUI.enabled = true;

            return true;
        }
    }
```
