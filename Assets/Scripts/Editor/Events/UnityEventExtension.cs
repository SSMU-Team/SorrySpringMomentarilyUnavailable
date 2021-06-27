using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using UnityEditor;
using UnityEditor.Events;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

using Utility.Event;

namespace Utility
{
	public static class UnityEventExtension
	{
		/// <summary>
		/// Auto register functions from the same game object on a Unity event.
		/// </summary>
		/// <param name="e">The event to register functions in.</param>
		/// <param name="caller">The caller Monobahaviour, the functions with the nameEvent will be discarded.</param>
		/// <param name="nameEvent">The name of the event, add "On" in script to this name for register.</param>
		public static void AutoReferenceUnityEvent(this UnityEvent e, MonoBehaviour caller, string nameEvent)
		{
			List<MethodInfo> method_infos = caller.gameObject.GetMethods(typeof(void),
				new[] { typeof(void) },
				ReflectionUtilities.publicFlags,
				new[] { caller });

			RegisterPersistentListeners(e, caller, method_infos, "On" + nameEvent);
			EditorUtility.SetDirty(caller);
		}

		/// <summary>
		/// Auto register functions from the same game object on a Unity event.
		/// Special case with one argument in callback.
		/// </summary>
		/// <typeparam name="T"> The generic type of the Unity Event</typeparam>
		/// <param name="e">The event to register functions in.</param>
		/// <param name="caller">The caller Monobahaviour, the functions with the nameEvent will be discarded.</param>
		/// <param name="nameEvent">The name of the event, add "On" in script to this name for register.</param>
		public static void AutoReferenceUnityEvent<T>(this UnityEvent<T> e, MonoBehaviour caller, string nameEvent)
		{
			List<MethodInfo> method_infos = caller.gameObject.GetMethods(
				typeof(void),
				new[] { typeof(T) },
				ReflectionUtilities.publicFlags,
				new[] { caller });
			RegisterPersistentListeners(e, caller, method_infos, "On" + nameEvent);
			EditorUtility.SetDirty(caller);
		}

		/// <summary>
		/// Search and register events for EventScriptTrigger script.
		/// Use it in context menu.
		/// </summary>
		/// <param name="menuCommand">Argument for context menu.</param>
		[MenuItem("CONTEXT/EventCollisionFilteredTrigger/Auto-reference events reflection")]
		public static void AutoReferenceEventCollisionFilteredTrigger(MenuCommand menuCommand)
		{
			EventCollisionFilteredTrigger script = menuCommand.context as EventCollisionFilteredTrigger;

			//Search functions with CallbackContext as arguments (used only in input event).
			List<MethodInfo> method_infos = script.gameObject.GetMethods(typeof(void),
				new[] { typeof(GameObject) },
				ReflectionUtilities.publicFlags,
				new[] { script });

			//Filter and register events
			RegisterPersistentListeners(script.TriggerFilteredEnter, script, method_infos, "OnTriggerFilteredEnter");
			RegisterPersistentListeners(script.TriggerFilteredStay, script, method_infos, "OnTriggerFilteredStay");
			RegisterPersistentListeners(script.TriggerFilteredExit, script, method_infos, "OnTriggerFilteredExit");

			EditorUtility.SetDirty(script);
		}

		private static void RegisterPersistentListeners(UnityEvent unityEvent, MonoBehaviour caller, List<MethodInfo> method_infos, string nameFunc)
		{
			List<MethodInfo> methods = method_infos.Where(x => x.Name == nameFunc).ToList();
			if(methods.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("Registrations " + nameFunc + " from " + caller.gameObject + ".").Append("\n");
				foreach(MethodInfo info in methods)
				{
					UnityAction method_delegate = System.Delegate.CreateDelegate(typeof(UnityAction), caller.gameObject.GetComponent(info.DeclaringType), info) as UnityAction;
					UnityEventTools.RemovePersistentListener(unityEvent, method_delegate);
					UnityEventTools.AddPersistentListener(unityEvent, method_delegate);
					sb.Append(info.DeclaringType + "/" + info.Name + " function registred.").Append("\n");
				}
				Debug.Log(sb.ToString());
			}
			else
			{
				Debug.LogWarning("No functions " + nameFunc + " in " + caller.gameObject);
			}
		}

		private static void RegisterPersistentListeners<T>(UnityEvent<T> unityEvent, MonoBehaviour caller, List<MethodInfo> method_infos, string nameFunc)
		{
			List<MethodInfo> methods = method_infos.Where(x => x.Name.Equals(nameFunc)).ToList();
			if(methods.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.Append("Registrations " + nameFunc + " from " + caller.gameObject + ".").Append("\n");
				foreach(MethodInfo info in methods)
				{
					UnityAction<T> method_delegate = System.Delegate.CreateDelegate(typeof(UnityAction<T>), caller.gameObject.GetComponent(info.DeclaringType), info) as UnityAction<T>;
					UnityEventTools.RemovePersistentListener<T>(unityEvent, method_delegate);
					UnityEventTools.AddPersistentListener<T>(unityEvent, method_delegate);
					sb.Append(info.DeclaringType + "/" + info.Name + " function registred.").Append("\n");
				}
				Debug.Log(sb.ToString());
			}
			else
			{
				Debug.LogWarning("No functions " + nameFunc + " in " + caller.gameObject);
			}
		}

		/// <summary>
		/// Search and register events for UnityInput script.
		/// Use it in context menu.
		/// </summary>
		/// <param name="menuCommand">Argument for context menu.</param>
		[MenuItem("CONTEXT/PlayerInput/Auto-reference events reflection")]
		public static void AutoReferenceUnityInput(MenuCommand menuCommand)
		{
			PlayerInput playerinput = menuCommand.context as PlayerInput;

			//Search functions with CallbackContext as arguments (used only in input event).
			List<MethodInfo> method_infos = playerinput.gameObject.GetMethods(typeof(void),
				new[] { typeof(InputAction.CallbackContext) },
				ReflectionUtilities.publicFlags,
				new[] { playerinput });

			//Filter and register foreach input action the corresponding methods
			foreach(InputActionMap actionmap in playerinput.actions.actionMaps)
			{
				foreach(InputAction action in actionmap.actions)
				{
					List<MethodInfo> methods = method_infos.Where(x => x.Name == ("On" + action.name)).ToList();

					foreach(MethodInfo info in methods)
					{
						UnityAction<InputAction.CallbackContext> method_delegate = System.Delegate.CreateDelegate(typeof(UnityAction<InputAction.CallbackContext>), playerinput.gameObject.GetComponent(info.DeclaringType), info) as UnityAction<InputAction.CallbackContext>;
						PlayerInput.ActionEvent playeraction = playerinput.actionEvents.First(x => x.actionName.Contains(action.name));
						UnityEventTools.RemovePersistentListener<InputAction.CallbackContext>(playeraction, method_delegate);
						UnityEventTools.AddPersistentListener<InputAction.CallbackContext>(playeraction, method_delegate);
					}
				}
			}

			EditorUtility.SetDirty(playerinput);

			//Debug a message report
			StringBuilder sb = new StringBuilder();
			sb.Append("Registration events from " + playerinput.gameObject + ".").Append("\n");
			foreach(PlayerInput.ActionEvent playeraction in playerinput.actionEvents)
			{
				for(int i = 0; i < playeraction.GetPersistentEventCount(); i++)
				{
					sb.Append(playeraction.GetPersistentTarget(i).GetType() + "/" + playeraction.GetPersistentMethodName(i) + " function registred.").Append("\n");
				}
			}
			Debug.Log(sb.ToString());
		}
	}
}

