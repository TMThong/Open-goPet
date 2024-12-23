
using Dapper;
using Gopet.Data.User;
using Gopet.Util;
using MySqlConnector;

public class TopPet : Top
{

    public static TopPet Instance = new TopPet();

    public TopPet() : base("top_pet")
    {

        base.name = "TOP Pet";
        base.desc = "Chỉ những thú cưng mạnh mẽ";
    }


    public override void Update()
    {
        try
        {
            lastDatas.Clear();
            lastDatas.AddRange(datas);
            datas.Clear();
            using (var conn = MYSQLManager.create())
            {
                conn.Execute("DELETE FROM `top_pet`;");
                conn.Execute("INSERT INTO `top_pet` (`ownerId`, `ownerName`,`petTemplateId`, `star`, `lvl`, `exp`, `maxHp`, `maxMp`, `def`, `atk`) SELECT `player`.`user_id` as `ownerId`, `player`.`name` as` ownerName`, JSON_VALUE(`player`.`pets`, '$[*].petIdTemplate') AS `petIdTemplate`, JSON_VALUE(`player`.`pets`, '$[*].star') AS `star`, JSON_VALUE(`player`.`pets`, '$[*].lvl') AS `lvl`, JSON_VALUE(`player`.`pets`, '$[*].exp') AS `exp`, JSON_VALUE(`player`.`pets`, '$[*].maxHp') AS `maxHp`, JSON_VALUE(`player`.`pets`, '$[*].maxMp') AS `maxMp`, COALESCE(JSON_VALUE(`player`.`pets`, '$[*].def'), 0) AS def, COALESCE(JSON_VALUE(player.pets, '$[*].atk'), 0 ) AS `atk` FROM `player` WHERE `player`.`pets` IS NOT NULL && JSON_VALUE(`player`.`pets`, '$[*].exp') IS NOT NULL;");
                conn.Execute("INSERT INTO `top_pet` (`ownerId`, `ownerName`,`petTemplateId`, `star`, `lvl`, `exp`, `maxHp`, `maxMp`, `def`, `atk`) SELECT `player`.`user_id` as `ownerId`, `player`.`name` as `ownerName`, JSON_VALUE(`player`.`petSelected`, '$.petIdTemplate') AS `petIdTemplate`, JSON_VALUE(`player`.`petSelected`, '$.star') AS `star`, JSON_VALUE(player.petSelected, '$.lvl') AS `lvl`, JSON_VALUE(player.petSelected, '$.exp') AS `exp`, JSON_VALUE(`player`.`petSelected`, '$.maxHp') AS `maxHp`, JSON_VALUE(`player`.`petSelected`, '$.maxMp') AS `maxMp`, COALESCE(JSON_VALUE(`player`.`petSelected`, '$.def'), 0) AS `def`, COALESCE(JSON_VALUE(`player`.`petSelected`, '$.atk'), 0) AS `atk` FROM `player` WHERE `player`.`petSelected` IS NOT NULL && NOT `player`.`petSelected` = 'null';");
                var topDataDynamic = conn.Query("SELECT * FROM `top_pet` ORDER BY lvl DESC, exp DESC LIMIT 30");
                int index = 1;
                foreach
                    (dynamic data in topDataDynamic)
                {
                    PetTemplate petTemplate = GopetManager.PETTEMPLATE_HASH_MAP.get(data.petTemplateId);
                    TopData topData = new TopData();
                    topData.id = data.ownerId;
                    topData.name = data.ownerName;
                    topData.imgPath = petTemplate.icon;
                    topData.title = getNameWithStar(data.star, petTemplate) + " của " + topData.name;
                    topData.desc = Utilities.Format("Hạng %s : Cấp %s  hiện có %s kinh nghiệm (hp) %s (mp) %s (def) %s (atk) %s",
                            index,
                            data.lvl,
                            Utilities.FormatNumber(data.exp),
                            data.maxHp,
                            data.maxMp,
                            data.def,
                            data.atk);
                    datas.Add(topData);
                    index++;
                }
            }
            updateSQLBXH();
        }
        catch (Exception e)
        {
            e.printStackTrace();
            
        }
    }

    public String getNameWithStar(int star, PetTemplate petTemplate)
    {
        String name = petTemplate.name + " ";
        for (int i = 0; i < star; i++)
        {
            name += "(sao)";
        }

        for (int i = 0; i < 5 - star; i++)
        {
            name += "(saoden)";
        }
        return name;
    }
}
