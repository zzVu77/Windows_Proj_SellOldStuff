using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    internal class ItemSearch_DAO : DAO<ItemSearch>
    {
        public override List<ItemSearch> Load()
        {
            return db.LoadData<ItemSearch>("Select * from [dbo].[UserProductSearchs] ");
        }
        public List<ItemSearch> LoadbyUser(int Id_user)
        {
            return db.LoadData<ItemSearch>("Select * from [dbo].[UserProductSearchs] where Id_user = @Id_user"
                , new SqlParameter("@Id_user", Id_user));
        }
        public override void Add(ItemSearch obj)
        {
            db.ExecuteNonQuery("INSERT INTO [dbo].[UserProductSearchs] ([Id_user], [Item_Id]) SELECT @Id_user, @Item_Id WHERE NOT EXISTS (  SELECT 1 FROM [dbo].[UserProductSearchs]   WHERE [Id_user] = @Id_user AND [Item_Id] = @Item_Id )",
                new System.Data.SqlClient.SqlParameter("@Id_user", obj.Id_user),
                new System.Data.SqlClient.SqlParameter("@Item_Id", obj.Item_Id)
                );
        }
        public void UpdateSearch(int Item_id, int Id_user)
        {
            db.ExecuteNonQuery(@"
                MERGE INTO [dbo].[UserProductSearchs] AS target
                USING (VALUES (@Id_user, @Item_Id)) AS source ([Id_user], [Item_Id])
                ON target.[Id_user] = source.[Id_user] AND target.[Item_Id] = source.[Item_Id]
                WHEN MATCHED THEN
                    UPDATE SET target.[Search_Count] = target.[Search_Count] + 1
                WHEN NOT MATCHED THEN
                    INSERT ([Id_user], [Item_Id], [Search_Count])
                    VALUES (source.[Id_user], source.[Item_Id], 1);",
                    new SqlParameter("@Item_Id", Item_id),
                    new SqlParameter("@Id_user", Id_user)
                );
        }

    }
}
