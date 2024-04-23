
using System.Collections.Generic;

namespace Pangoo.Core.VisualScripting
{
    public interface INode
    {
        INodeSlot ParentSlot { get; set; }

        List<INodeSlot> ChildernSlot { get; set; }

        int ChildernCount { get; }

        object Data { get; set; }

    }
}