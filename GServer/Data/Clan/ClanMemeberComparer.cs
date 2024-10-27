
using Gopet.Data.GopetClan;

sealed class ClanMemeberComparer : IComparer<ClanMember>
{
    public int Compare(ClanMember? o1, ClanMember? o2)
    {
        return o1.user_id - o2.user_id;
    }
}

