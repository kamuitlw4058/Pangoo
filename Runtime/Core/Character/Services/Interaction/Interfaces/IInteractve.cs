using UnityEngine;
using Pangoo.Core.Common;

namespace Pangoo.Core.Characters
{
    public interface IInteractive : ISpatialHash
    {

        /// <summary>
        /// The scene object that this interface belongs to
        /// </summary>
        GameObject Instance { get; set; }

        /// <summary>
        /// Returns the scene object instance id that this interface belongs to
        /// </summary>
        int InstanceID { get; }

        /// <summary>
        /// Whether this Interactive object is being interacted. Useful to hide any interaction
        /// tooltips while it is running
        /// </summary>
        bool IsInteracting { get; }

        // METHODS: -------------------------------------------------------------------------------

        /// <summary>
        /// Executed when a character attempts to interact with this interface
        /// </summary>
        /// <param name="character"></param>
        void Interact(Character character);

        /// <summary>
        /// Executed when the interaction finishes
        /// </summary>
        void Stop();

        bool InteractEnable { get; set; }

        bool InteractCanBan { get; set; }

        bool InteractTriggerEnter { get; set; }

        bool InteractDisabled { get; }

        float InteractRadius { get; set; }


        Vector3 InteractOffset { get; set; }


        float InteractRadian { get; set; }
    }
}