using DearSanta.Interfaces;
using DearSanta.Models;
using System.Data.SqlClient;

namespace DearSanta.Repositories
{
           public class WishListItemRepository : BaseRepository, IWishListItem
        {
            private readonly string _baseSqlSelect = @"SELECT WishListItemId,
                                                    ItemName,
                                                    ItemDescription,
                                                    ItemPrice,
                                                    ItemImage,
                                                    IsTopItem,
                                                    IsPurchased
                                                    
                                                   FROM WishListItem";

            public WishListItemRepository(IConfiguration config) : base(config) { }

            public List<WishListItem> GetAllWishListItems()
            {
                using (var conn = Connection)
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = _baseSqlSelect;

                        using (var reader = cmd.ExecuteReader())
                        {
                            var results = new List<WishListItem>();
                            while (reader.Read())
                            {
                                var product = LoadFromData(reader);

                                results.Add(product);
                            }

                            return results;
                        }
                    }
                }
            }

            public WishListItem CreateWishListItem(WishListItem item , int FamilyMemberId)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            INSERT INTO [WishListItem] (ItemName, ItemDescription, ItemPrice, ItemImage,IsTopItem, IsPurchased )
                            OUTPUT INSERTED.WishListItemId
                            VALUES (@ItemName, @ItemDescription, @ItemPrice, @ItemImage, @IsTopItem, @IsPurchased);
                        ";
                        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                        cmd.Parameters.AddWithValue("@ItemPrice", item.ItemPrice);
                        cmd.Parameters.AddWithValue("@ItemImage", item.ItemImage);
                        cmd.Parameters.AddWithValue("@IsTopItem", item.IsTopItem);
                        cmd.Parameters.AddWithValue("@IsPurchased", item.IsPurchased);
                        cmd.Parameters.AddWithValue("@FamilyMemberid", FamilyMemberId);

                        int id = (int)cmd.ExecuteScalar();

                        item.WishListItemId = id;

                        AddWishListItemToJoinTable(id, FamilyMemberId);

                        return item;

                    }
                }
            }

        public void AddWishListItemToJoinTable(int WishListItemId, int FamilyMemberId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO [FamilyMemberWishList] (FamilyMemberId, WishListItemId)
                        OUTPUT INSERTED.Id
                        VALUES (@FamilyMemberId, @WishListItemId);
                    ";
                    cmd.Parameters.AddWithValue("@WishListItemId", WishListItemId);
                    cmd.Parameters.AddWithValue("@FamilyMemberid", FamilyMemberId);

                    cmd.ExecuteScalar();
                }
            }
        }


        public WishListItem? GetWishListItemById(int id)

            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"{_baseSqlSelect} WHERE WishListItemId" +
                            $" = @WishListItemId";

                        cmd.Parameters.AddWithValue("@WishListItemId", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            WishListItem? result = null;
                            if (reader.Read())
                            {
                                return LoadFromData(reader);
                            }

                            return result;

                        }
                    }
                }
            }





        public WishListItem UpdateWishListItem(WishListItem item)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                    cmd.CommandText = @"
                            UPDATE WishListItem
                            SET
                                ItemName = @ItemName,
                                ItemDescription = @ItemDescription,
                                ItemPrice = @ItemPrice,
                                ItemImage = @ItemImage,
                                IsTopItem = @IsTopItem,
                                IsPurchased = @IsPurchased
                                
                            WHERE WishListItemId = @id";

                        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                        cmd.Parameters.AddWithValue("@ItemPrice", item.ItemPrice);
                        cmd.Parameters.AddWithValue("@ItemImage", item.ItemImage);
                        cmd.Parameters.AddWithValue("@IsTopItem", item.IsTopItem);
                        cmd.Parameters.AddWithValue("@IsPurchased", item.IsPurchased);                        
                        cmd.Parameters.AddWithValue("@id", item.WishListItemId);

                        cmd.ExecuteNonQuery();

                    return item;

                    }
                }
            }

            public void DeleteWishListItemById(int id)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DELETE FROM FamilyMemberWishList
                            WHERE WishListItemId = @id

                            DELETE FROM WishListItem
                            WHERE WishListItemId = @id
                        ";

                        cmd.Parameters.AddWithValue("@id", id);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            private WishListItem LoadFromData(SqlDataReader reader)
            {
                return new WishListItem
                {
                    WishListItemId = reader.GetInt32(reader.GetOrdinal("WishListItemId")),
                    ItemName = reader.GetString(reader.GetOrdinal("ItemName")),
                    ItemDescription = reader.GetString(reader.GetOrdinal("ItemDescription")),
                    ItemPrice = reader.GetInt32(reader.GetOrdinal("ItemPrice")),
                    ItemImage = reader.GetString(reader.GetOrdinal("ItemImage")),
                    IsTopItem = reader.GetBoolean(reader.GetOrdinal("IsTopItem")),
                    IsPurchased = reader.GetBoolean(reader.GetOrdinal("IsPurchased"))
                };
            }
        }

    }
