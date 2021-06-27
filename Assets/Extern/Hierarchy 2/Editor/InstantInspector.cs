using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace Hierarchy2
{
	public class InstantInspector : EditorWindow
	{
		public enum FillMode
		{
			Default,
			Add
		}

		private ScrollView scrollView;
		private List<Editor> editors = new List<Editor>();
		private Color objectNameColor = new Color32(58, 121, 187, 255);
		private List<Object> components = new List<Object>();
		private List<Foldout> folouts = new List<Foldout>();

		public static InstantInspector OpenEditor()
		{
			InstantInspector window = GetWindow<InstantInspector>("Instant Inspector");
			window.titleContent.image = EditorGUIUtility.IconContent("UnityEditor.InspectorWindow").image;
			return window;
		}

		private void OnEnable()
		{
			if(rootVisualElement.childCount == 0)
			{
				scrollView = new ScrollView(ScrollViewMode.Vertical);
				rootVisualElement.Add(scrollView);
			}
		}

		private void OnDisable()
		{
			Dispose();
		}

		private void Dispose()
		{
			components.Clear();

			while(scrollView.childCount > 0)
			{
				scrollView[0].RemoveFromHierarchy();
			}

			while(editors.Count > 0)
			{
				DestroyImmediate(editors[0]);
				editors.RemoveAt(0);
			}
		}

		public void Fill(List<Object> objects, FillMode fillMode = FillMode.Default)
		{
			objects = new List<Object>(objects);

			if(fillMode == FillMode.Add)
			{
				components.RemoveAll(item => item == null);
				foreach(Object component in components)
				{
					if(!objects.Contains(component))
					{
						objects.Add(component);
					}
				}
			}

			Dispose();
			components = objects;
			folouts.Clear();

			foreach(Object component in components)
			{
				Foldout folout = new Foldout(string.Format("{0}", component.GetType().Name))
				{
					Value = components.Count == 1 ? true : false
				};
				folout.name = folout.Title;
				folouts.Add(folout);

				folout.imageElement.image = EditorGUIUtility.ObjectContent(component, component.GetType()).image;
				folout.headerElement.RegisterCallback<MouseUpEvent>((evt) =>
				{
					if(evt.button == 1)
					{
						Rect rect = new Rect(folout.headerElement.layout)
						{
							position = evt.mousePosition
						};
						HierarchyEditor.DisplayObjectContextMenu(rect, component, 0);
						evt.StopPropagation();
					}
				});


				Label objectName = new Label(string.Format(" [{0}]", component.name));
				objectName.StyleTextColor(objectNameColor);
				objectName.RegisterCallback<MouseUpEvent>((evt) =>
				{
					if(evt.button == 0)
					{
						EditorGUIUtility.PingObject(component);
						Selection.activeObject = component;
						evt.StopPropagation();
					}
				});
				folout.headerElement.Add(objectName);

				Image remove = new Image
				{
					image = EditorGUIUtility.IconContent("winbtn_win_close").image
				};
				remove.StyleSize(13, 13);
				remove.StylePosition(Position.Absolute);
				remove.StyleRight(8);
				remove.StyleAlignSelf(Align.Center);
				remove.RegisterCallback<MouseUpEvent>((evt) =>
				{
					if(evt.button == 0)
					{
						if(component != null)
						{
							components.Remove(component);
						}
						else
						{
							components.RemoveAll(item => item == null);
						}

						Fill(new List<Object>(components));
						evt.StopPropagation();
					}
				});
				folout.headerElement.Add(remove);

				bool isMat = component is Material;

				Editor editor = null;

				if(isMat)
				{
					editor = MaterialEditor.CreateEditor(component) as MaterialEditor;
				}
				else
				{
					editor = Editor.CreateEditor(component);
				}

				VisualElement inspector = editor.CreateInspectorGUI();

				if(inspector == null)
				{
					inspector = new IMGUIContainer(() =>
					{
						bool tempState = EditorGUIUtility.wideMode;
						float tempWidth = EditorGUIUtility.labelWidth;

						EditorGUIUtility.wideMode = true;

						if(component is Transform)
						{
							EditorGUIUtility.labelWidth = 64;
						}

						if(editor.target != null)
						{
							if(isMat)
							{
								MaterialEditor maEditor = editor as MaterialEditor;

								EditorGUILayout.BeginVertical();
								if(maEditor.PropertiesGUI())
								{
									maEditor.PropertiesChanged();
								}

								EditorGUILayout.EndVertical();
							}
							else
							{
								editor.OnInspectorGUI();
							}

							objectName.StyleTextColor(objectNameColor);
						}
						else
						{
							objectName.StyleTextColor(Color.red);
							EditorGUILayout.HelpBox("Reference not found.", MessageType.Info);
						}

						EditorGUIUtility.wideMode = tempState;
						EditorGUIUtility.labelWidth = tempWidth;
					});
				}

				inspector.style.marginLeft = 16;
				inspector.style.marginRight = 2;
				inspector.style.marginTop = 4;

				folout.Add(inspector);
				editors.Add(editor);
				scrollView.Add(folout);

				if(isMat)
				{
					IMGUIContainer preview = new IMGUIContainer(() =>
					{
						editor.DrawPreview(new Rect(0, 0, inspector.layout.size.x,
							Mathf.Clamp(inspector.layout.width / 2, 64, 200)));
					});
					preview.StyleMarginTop(4);
					preview.StretchToParentWidth();
					inspector.RegisterCallback<GeometryChangedEvent>((callback) =>
					{
						preview.StyleHeight(Mathf.Clamp(inspector.layout.width / 2, 64, 200));
					});
					preview.StylePosition(Position.Relative);
					preview.name = "Material Preview";
					folout.Add(preview);
				}
			}

			Repaint();
		}
	}
}