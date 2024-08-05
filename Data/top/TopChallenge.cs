using Dapper;
using Gopet.Data.User;
using Gopet.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gopet.Data.top
{
    internal class TopChallenge : Top
    {

        public static readonly TopChallenge Instance = new TopChallenge();

        public TopChallenge() : base("top.challenge")
        {
            this.name = "Top vượt ải";
            this.desc = string.Empty;
        }

        public override void Update()
        {
            try
            {
                lastDatas.Clear();
                lastDatas.AddRange(datas);
                datas.Clear();
                try
                {

                    using (var conn = MYSQLManager.create())
                    {
                        var topChData = conn.Query<TopChallengeData>("SELECT * FROM `top_challenge` WHERE `Type` = 0 ORDER BY `Turn` DESC , `Time` DESC LIMIT 50;");
                        int index = 1;
                        foreach
                            (TopChallengeData data in topChData)
                        {
                            TopData topData = new TopData();
                            topData.id = data.Id;
                            topData.name = "Ải";
                            topData.imgPath = "npcs/kiss.png";
                            topData.title = string.Format("Đội: {0}", string.Join(" , " , data.Name));
                            topData.desc = string.Format("Vượt ải đến ải {0} thời gian là: {1}", data.Turn, data.Time.ToString());
                            datas.Add(topData);
                            index++;
                        }
                    }
                }
                catch (Exception e)
                {
                    e.printStackTrace();
                }
                updateSQLBXH();
            }
            catch (Exception e)
            {
                e.printStackTrace();
            }
        }
    }
}
