﻿using DearSanta.Interfaces;
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

            public WishListItem CreateWishListItem(WishListItem item)
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                    INSERT INTO [MealProduct] (ItemName, ItemDescription, ItemPrice, ItemImage,IsTopItem, IsPurchased )
                    OUTPUT INSERTED.ID
                    VALUES (@ItemName, @ItemDescription, @ItemPrice, @ItemImage, @IsTopItem, @IsPurchased);
                ";
                        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                        cmd.Parameters.AddWithValue("@ItemPrice", item.ItemPrice);
                        cmd.Parameters.AddWithValue("@ItemImage", item.ItemImage);
                        cmd.Parameters.AddWithValue("@IsTopItem", item.IsTopItem);
                        cmd.Parameters.AddWithValue("@IsPurchased", item.IsPurchased);                        

                        int id = (int)cmd.ExecuteScalar();

                        item.WishListItemId = id;

                        return item;

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
                        cmd.CommandText = $"{_baseSqlSelect} WHERE Id" +
                            $" = @id";

                        cmd.Parameters.AddWithValue("@id", id);

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



        public WishListItem? GetWishListItemByName(string name)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE ItemName" +
                        $" = @ItemName";

                    cmd.Parameters.AddWithValue("@ItemName", name);

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


        public void UpdateWishListItem(WishListItem item)
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
                                IsPurchased = @IsPurchased,
                                
                            WHERE WishListItemId = @id";

                        cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                        cmd.Parameters.AddWithValue("@ItemDescription", item.ItemDescription);
                        cmd.Parameters.AddWithValue("@ItemPrice", item.ItemPrice);
                        cmd.Parameters.AddWithValue("@ItemImage", item.ItemImage);
                        cmd.Parameters.AddWithValue("@IsTopItem", item.IsTopItem);
                        cmd.Parameters.AddWithValue("@IsPurchased", item.IsPurchased);                        
                        cmd.Parameters.AddWithValue("@id", item.WishListItemId);

                        cmd.ExecuteNonQuery();


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
                    WishListItemId = reader.GetInt32(reader.GetOrdinal("Id")),
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
