using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CagedItems
{
    /// <summary>
    /// Interface which represents items that can be caged.
    /// </summary>
    public interface ICageable
    {
        /// <summary>
        /// Gets the Size of item to be displayed.
        /// </summary>
        double DisplaySize { get; }

        /// <summary>
        /// Gets the Key which indicates what resource to obtain.
        /// </summary>
        string ResourceKey { get; }

        /// <summary>
        /// Gets the X position of the item.
        /// </summary>
        int XPosition { get; }

        /// <summary>
        /// Gets the Y position of the item.
        /// </summary>
        int YPosition { get; }

        /// <summary>
        /// Gets the X direction of the item.
        /// </summary>
        HorizontalDirection XDirection { get; }

        /// <summary>
        /// Gets the Y direction of the item.
        /// </summary>
        VerticalDirection YDirection { get; }

        HungerState HungerState { get; }

        Action<ICageable> OnImageUpdate { get; set; }

        bool IsActive { get; }
    }
}
