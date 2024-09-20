using Gopet.Data.GopetItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.User
{
    public class AdminSelectItemData
    {
        public int Count { get; }

        public Item Item { get; }

        public AdminSelectItemData(int count, Item item)
        {
            Count = count;
            Item = item;
        }
    }

    public static class AdminSelectItemDataExtension
    {
        public static bool TryGet(IEnumerable<AdminSelectItemData> adminSelectItemDatas, Item item, out AdminSelectItemData adminSelectItemData)
        {
            adminSelectItemData = adminSelectItemDatas.Where(t => t.Item == item).FirstOrDefault();
            return adminSelectItemData != null;
        }

        public static bool TryAdd(ICollection<AdminSelectItemData> adminSelectItemDatas, Item item, int count)
        {
            if (TryGet(adminSelectItemDatas, item, out AdminSelectItemData adminSelectItemData))
            {
                return false;
            }
            adminSelectItemDatas.Add(new AdminSelectItemData(count, item));
            return true;
        }
    }
}
