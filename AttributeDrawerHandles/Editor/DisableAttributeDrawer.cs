//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-05:04
//AnimationExpansion.Editor

using Icarus.IcAttribute.Attributes;
using Icarus.IcAttribute.Core;
using UnityEditor;
using UnityEngine;

namespace Icarus.IcAttribute.Drawer
{
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
}