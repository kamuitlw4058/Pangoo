using GameFramework.Debugger;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Linq;

namespace Pangoo
{
    public sealed class DebugWindowCharacter : DebuggerComponent.ScrollableDebuggerWindowBase
    {
        protected override void OnDrawScrollableWindow()
        {
            var main = PangooEntry.Service.mainService;
            if (main == null || (main != null && main.StaticScene == null))
            {
                return;
            }
            var character = main.CharacterService;

            GUILayout.Label("<b>脚步声</b>");
            GUILayout.BeginVertical("box");
            DrawItem("脚步声 材质配置数量", character.Player?.character?.FootstepsService?.FootstepConfig?.footsteps?.Length.ToString());
            DrawItem("脚步声HitNum", character.Player?.character?.FootstepsService?.HitNum.ToString());
            DrawItem("脚步声Hit Target", character.Player?.character?.FootstepsService?.Hit.ToString());
            DrawItem("脚步声Hit collider", character.Player?.character?.FootstepsService?.Hit.collider?.ToString());
            DrawItem("脚步声Hit collider Name", character.Player?.character?.FootstepsService?.Hit.collider?.gameObject.name);
            DrawItem("脚步声Hit collider Name[0]", character.Player?.character?.FootstepsService?.Hits[0].collider?.gameObject.name);
            DrawItem("脚步声Hit collider Name[1]", character.Player?.character?.FootstepsService?.Hits[1].collider?.gameObject.name);
            DrawItem("脚步声Hit collider Name[2]", character.Player?.character?.FootstepsService?.Hits[2].collider?.gameObject.name);
            DrawItem("脚步声Hit collider Name[3]", character.Player?.character?.FootstepsService?.Hits[3].collider?.gameObject.name);
            DrawItem("脚步声Hit collider Name[4]", character.Player?.character?.FootstepsService?.Hits[4].collider?.gameObject.name);


            try
            {
                DrawItem("脚步声Hit Renderer", character.Player?.character?.FootstepsService?.Renderer?.name);
            }
            catch
            {

            }



            GUILayout.EndVertical();



        }

        private string GetBatteryLevelString(float batteryLevel)
        {
            if (batteryLevel < 0f)
            {
                return "Unavailable";
            }

            return batteryLevel.ToString("P0");
        }
    }
}