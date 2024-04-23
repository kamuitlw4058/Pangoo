
using System.Collections.Generic;

namespace Pangoo.Core.VisualScripting
{
    public interface INodeSlot
    {
        INode Parent { get; set; }

        INode Child { get; set; }
    }
}