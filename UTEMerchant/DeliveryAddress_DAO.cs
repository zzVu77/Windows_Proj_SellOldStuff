using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTEMerchant
{
    public class DeliveryAddress_DAO : DAO<DeliveryAddress>
    {
        public DeliveryAddress_DAO() { }

        public override List<DeliveryAddress> Load()
        {
            return db.LoadData<DeliveryAddress>("SELECT * FROM [dbo].[DeliveryAddress]");
        }

        public List<DeliveryAddress> GetDeliveryAddressByUserID(int userID)
        {
            return db.LoadData<DeliveryAddress>($"SELECT * FROM [dbo].[DeliveryAddress] WHERE UserID = '{userID}'");
        }

        public void AddDeliveryAddress(string recipientName, string streetAddress, string recipientPhone, string city, string district, string ward, int userID, bool defaultAddress)
        {
            string sqlStr = "INSERT INTO [dbo].[DeliveryAddress] (RecipientName, StreetAddress, RecipientPhone, City, District, Ward, UserID, DefaultAddress) VALUES (@RecipientName, @StreetAddress, @RecipientPhone, @City, @District, @Ward, @UserID, @DefaultAddress)";
            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@RecipientName", recipientName),
                new SqlParameter("@StreetAddress", streetAddress),
                new SqlParameter("@RecipientPhone", recipientPhone),
                new SqlParameter("@City", city),
                new SqlParameter("@District", district),
                new SqlParameter("@Ward", ward),
                new SqlParameter("@UserID", userID),
                new SqlParameter("@DefaultAddress", defaultAddress));
        }

        public void RemoveDeliveryAddress(int deliveryAddressID)
        {
            string sqlStr = "DELETE FROM [dbo].[DeliveryAddress] WHERE ID = @ID";
            db.ExecuteNonQuery(sqlStr, new SqlParameter("@ID", deliveryAddressID));
        }

        public void UpdateDeliveryAddress(DeliveryAddress deliveryAddress)
        {
            string sqlStr = "UPDATE [dbo].[DeliveryAddress] SET RecipientName = @RecipientName, StreetAddress = @StreetAddress, RecipientPhone = @RecipientPhone, City = @City, District = @District, Ward = @Ward, DefaultAddress = @DefaultAddress WHERE ID = @ID";
            db.ExecuteNonQuery(sqlStr,
                new SqlParameter("@RecipientName", deliveryAddress.RecipientName),
                new SqlParameter("@StreetAddress", deliveryAddress.StreetAddress),
                new SqlParameter("@RecipientPhone", deliveryAddress.RecipientPhone),
                new SqlParameter("@City", deliveryAddress.City),
                new SqlParameter("@District", deliveryAddress.District),
                new SqlParameter("@Ward", deliveryAddress.Ward),
                new SqlParameter("@DefaultAddress", deliveryAddress.DefaultAddress),
                new SqlParameter("@ID", deliveryAddress.ID));
        }

        public DeliveryAddress GetUserDefaultAddress(int userID)
        {
            string sqlStr = "SELECT * FROM [dbo].[DeliveryAddress] WHERE UserID = @UserID AND DefaultAddress = 1";
            return db.LoadData<DeliveryAddress>(sqlStr, new SqlParameter("@UserID", userID)).FirstOrDefault();
        }
    }
}
