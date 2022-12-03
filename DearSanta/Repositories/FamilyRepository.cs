using DearSanta.Interfaces;
using DearSanta.Models;
using System.Data.SqlClient;

namespace DearSanta.Repositories
{
    public class FamilyRepository : BaseRepository, IFamily
    {
        private readonly string _baseSqlSelect = @"SELECT FamilyId,
                                                          Family Name
                                                   
                                                    
                                                   FROM [Family]";

        public FamilyRepository(IConfiguration config) : base(config) { }

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
                            var product = LoadFromData2(reader);

                            results.Add(product);
                        }

                        return results;
                    }
                }
            }
        }

        public Family CreateFamily(Family newFam)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Family] (FamilyName)
                    OUTPUT INSERTED.ID
                    VALUES (@FamilyName);
                ";
                    cmd.Parameters.AddWithValue("@ItemName", newFam.FamilyName);



                    int id = (int)cmd.ExecuteScalar();

                    newFam.FamilyId = id;

                    return newFam;

                }
            }
        }


        public Family? GetFamilyById(int id)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"{_baseSqlSelect} WHERE Id" +
                        $" = @FamilyId";

                    cmd.Parameters.AddWithValue("@FamilyId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Family? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }






        public void UpdateFamily(Family famUpdate)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE WishListItem
                            SET
                                FamilyName = @FamilyName,
                               
                                
                            WHERE FamilyId = @FamilyId";

                    cmd.Parameters.AddWithValue("@FamilyName", famUpdate.FamilyName);



                    cmd.ExecuteNonQuery();


                }
            }
        }



        private Family LoadFromData(SqlDataReader reader)
        {
            return new Family
            {
                FamilyId = reader.GetInt32(reader.GetOrdinal("FamilyId")),
                FamilyName = reader.GetString(reader.GetOrdinal("FamilyName")),

            };

        }

        private FamilyMember LoadFromData2(SqlDataReader reader)
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
    }
   }
 
