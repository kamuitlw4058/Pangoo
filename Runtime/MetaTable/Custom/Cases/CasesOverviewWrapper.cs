#if UNITY_EDITOR

using System;
using MetaTable;

namespace Pangoo.MetaTable
{
    [Serializable]
    public partial class CasesOverviewWrapper : MetaTableOverviewWrapper<CasesOverview, CasesDetailRowWrapper, CasesRowWrapper, CasesNewRowWrapper, UnityCasesRow>
    {

    }
}
#endif

