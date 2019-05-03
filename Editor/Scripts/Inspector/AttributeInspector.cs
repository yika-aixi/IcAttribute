//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2019年05月02日-09:05
//AnimationExpansion.Editor

using System;
using System.Collections.Generic;
using System.Reflection;
using Icarus.IcAttribute.Core;
using Icarus.IcAttribute.Utils;
using UnityEditor;
using UnityEngine;

namespace Icarus.IcAttribute.Inspector
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class AttributeInspector: UnityEditor.Editor
    {
        private SerializedProperty script;

        private IEnumerable<FieldInfo> fields;

        private Dictionary<string, SerializedProperty> serializedPropertiesByFieldName = new Dictionary<string, SerializedProperty>();

        private Dictionary<HandleAttribute, Type> _attHandles = new Dictionary<HandleAttribute, Type>();
        Dictionary<Type,DrawerHandle> _handles = new Dictionary<Type, DrawerHandle>();
        private void OnEnable()
        {
            this.script = this.serializedObject.FindProperty("m_Script");

            // Cache serialized fields
            this.fields = ReflectionUtility.GetAllFields(this.target, f => this.serializedObject.FindProperty(f.Name) != null);

            // Cache serialized properties by field name
            this.serializedPropertiesByFieldName = new Dictionary<string, SerializedProperty>();
            foreach (var field in this.fields)
            {
                this.serializedPropertiesByFieldName[field.Name] = this.serializedObject.FindProperty(field.Name);
            }


            AttributeUtil.GetFilterSystemAssemblyAllTAttributeType(_attHandles);

            foreach (var handle in _attHandles)
            {
                if (!_handles.ContainsKey(handle.Key.GetType()))
                {
                    _handles.Add(handle.Key.HandleType,(DrawerHandle) Activator.CreateInstance(handle.Value));
                }
            }
        }

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            if (this.script != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(this.script);
                GUI.enabled = true;
            }

            foreach (var serializedProperty in serializedPropertiesByFieldName)
            {
                var attributes = serializedProperty.Value.GetAttributes<BaseAttribute>();

                if (attributes.Length == 0)
                {
                    EditorGUILayout.PropertyField(serializedProperty.Value,true);
                }

                foreach (var attribute in attributes)
                {
                    DrawerHandle handle = null;

                    _handles.TryGetValue(attribute.GetType(), out handle);

                    if (handle != null)
                    {
                        if (handle.Handle(attribute, serializedProperty.Value))
                        {
                            handle.Handle(GUILayoutUtility.GetLastRect(), attribute, serializedProperty.Value);
                        }
                    }
                }
            }

            this.serializedObject.ApplyModifiedProperties();
        }
       
    }
}