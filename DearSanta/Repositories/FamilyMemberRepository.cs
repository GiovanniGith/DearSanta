using DearSanta.Interfaces;
using DearSanta.Models;
using System.Data.SqlClient;

namespace DearSanta.Repositories
{
    public class FamilyMemberRepository : BaseRepository, IFamilyMember
    {
        private readonly string _baseSqlSelect = @"SELECT FamilyMemberId,
                                                          FamilyMemberName,
                                                          FamilyMemberAge,
                                                          FamilyMemberGender,
                                                          FamilyId

                                                   
                                                    
                                                   FROM [FamilyMember]";

        public FamilyMemberRepository(IConfiguration config) : base(config) { }

        public List<FamilyMember> GetAllFamilyMembers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<FamilyMember>();
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

        public FamilyMember CreateFamilyMember(FamilyMember newFamMember)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [FamilyMember] (FamilyMemberName, FamilyMemberAge, FamilyMemberGender, FamilyId )
                    OUTPUT INSERTED.FamilyMemberId
                    VALUES (@FamilyMemberName, @FamilyMemberAge, @FamilyMemberGender, @FamilyId)";

                    cmd.Parameters.AddWithValue("@FamilyMemberId", newFamMember.FamilyMemberId);
                    cmd.Parameters.AddWithValue("@FamilyMemberName", newFamMember.FamilyMemberName);
                    cmd.Parameters.AddWithValue("@FamilyMemberAge", newFamMember.FamilyMemberAge);
                    cmd.Parameters.AddWithValue("@FamilyMemberGender", newFamMember.FamilyMemberGender);
                    cmd.Parameters.AddWithValue("@FamilyId", newFamMember.FamilyId);

                    int id = (int)cmd.ExecuteScalar();

                    newFamMember.FamilyMemberId = id;

                    AddFamilyMemberToFamily(newFamMember.FamilyId, newFamMember.FamilyMemberId);

                    return newFamMember;

                }
            }
        }

        public void AddFamilyMemberToFamily(int familyId, int familyMemberId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [MembersInAFamily] (FamilyId, FamilyMemberId)
                    OUTPUT INSERTED.Id
                    VALUES (@FamilyId, @FamilyMemberId)";

                    cmd.Parameters.AddWithValue("@FamilyMemberId", familyMemberId);
                    cmd.Parameters.AddWithValue("@FamilyId", familyId);

                    int id = (int)cmd.ExecuteScalar();
                }
            }
        }


        public FamilyMember? GetFamilyMemberById(int id)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE FamilyMemberId" +
                        $" = @FamilyMemberId";

                    cmd.Parameters.AddWithValue("@FamilyMemberId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        FamilyMember? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }



       

        public FamilyMember UpdateFamilyMember(FamilyMember memberUpdate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE FamilyMember
                            SET
                                FamilyMemberName = @FamilyMemberName,
                                FamilyMemberAge = @FamilyMemberAge,
                                FamilyMemberGender = @FamilyMemberGender,
                                FamilyId = @FamilyId
                               
                                
                            WHERE FamilyMemberId = @FamilyMemberId";

                    cmd.Parameters.AddWithValue("@FamilyMemberName", memberUpdate.FamilyMemberName);
                    cmd.Parameters.AddWithValue("@FamilyMemberAge", memberUpdate.FamilyMemberAge);
                    cmd.Parameters.AddWithValue("@FamilyMemberGender", memberUpdate.FamilyMemberGender);
                    cmd.Parameters.AddWithValue("@FamilyId", memberUpdate.FamilyId);
                    cmd.Parameters.AddWithValue("@FamilyMemberId", memberUpdate.FamilyMemberId);


                    cmd.ExecuteNonQuery();

                    return memberUpdate;
                }
            }
        }

        public void DeleteFamilyMember(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM MembersInAFamily
                            WHERE FamilyMemberId = @id

                            DELETE FROM FamilyMemberWishList
                            WHERE FamilyMemberId = @id
                            
                            DELETE FROM FamilyMember
                            WHERE FamilyMemberId = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }



        public List<FamilyMemberWishList>? GetWishListByFamilyMemberId(int id)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"SELECT fml.id, fm.FamilyMemberId, fm.FamilyMemberName, fm.FamilyMemberAge, fm.FamilyMemberGender,wi.WishListItemId,wi.ItemName, wi.ItemDescription,wi.ItemPrice,wi.ItemImage,wi.IsTopItem,wi.IsPurchased from ((FamilyMemberWishList fml 
Join WishListItem wi ON  fml.WishListItemId = wi.WishListItemId)
Join FamilyMember fm ON fml.FamilyMemberId = fm.FamilyMemberId)
                                         WHERE fm.FamilyMemberId = @FamilyMemberId";

                    cmd.Parameters.AddWithValue("@FamilyMemberId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<FamilyMemberWishList>? newWishList = new List<FamilyMemberWishList>();
                        while (reader.Read())
                        {
                            FamilyMemberWishList newItem = LoadFromData2(reader);

                            newWishList.Add(newItem);
                        }

                        return newWishList;
                    }
                }
            }
        }



        private FamilyMember LoadFromData(SqlDataReader reader)
        {
            return new FamilyMember
            {
                FamilyMemberId = reader.GetInt32(reader.GetOrdinal("FamilyMemberId")),
                FamilyMemberName = reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                FamilyMemberAge = reader.GetInt32(reader.GetOrdinal("FamilyMemberAge")),
                FamilyMemberGender = reader.GetString(reader.GetOrdinal("FamilyMemberGender")),
                FamilyId = reader.GetInt32(reader.GetOrdinal("FamilyId")),


            };

        }
        private FamilyMemberWishList LoadFromData2(SqlDataReader reader)
        {
            return new FamilyMemberWishList
            {
                Id= reader.GetInt32(reader.GetOrdinal("Id")),
                FamilyMemberId = reader.GetInt32(reader.GetOrdinal("FamilyMemberId")),
                FamilyMemberName = reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                FamilyMemberAge = reader.GetInt32(reader.GetOrdinal("FamilyMemberAge")),
                FamilyMemberGender = reader.GetString(reader.GetOrdinal("FamilyMemberGender")),
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
