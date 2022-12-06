using DearSanta.Interfaces;
using DearSanta.Models;
using System.Data.SqlClient;

namespace DearSanta.Repositories
{
    public class FamilyRepository : BaseRepository, IFamily
    {
        private readonly string _baseSqlSelect = @"SELECT FamilyId,
                                                          FamilyName
                                                   
                                                    
                                                   FROM [Family]";

        public FamilyRepository(IConfiguration config) : base(config) { }

        
        public List<Family> GetAllFamilies()
        {

            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<Family>();
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

           

        public Family CreateFamily(Family newFam)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [Family] (FamilyName)
                    OUTPUT INSERTED.FamilyId
                    VALUES (@FamilyName);
                ";
                    cmd.Parameters.AddWithValue("@FamilyName", newFam.FamilyName);



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
                    cmd.CommandText = $"{_baseSqlSelect} WHERE FamilyId" +
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
                            UPDATE Family
                            SET
                                FamilyName = @FamilyName
                               
                                
                            WHERE FamilyId = @FamilyId";

                    cmd.Parameters.AddWithValue("@FamilyId", famUpdate.FamilyId);
                    cmd.Parameters.AddWithValue("@FamilyName", famUpdate.FamilyName);
                    



                    cmd.ExecuteNonQuery();


                }
            }
        }

        public List<MembersInAFamily>? GetMembersInAFamilyByFamilyId(int id)

        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @$"Select mif.id, f.FamilyId, f.FamilyName, fm.FamilyMemberId, fm.FamilyMemberName, fm.FamilyMemberAge, fm.FamilyMemberGender  from ((MembersInAFamily mif 
                                         Join Family f ON  mif.FamilyId = f.FamilyId)
                                         Join FamilyMember fm ON mif.FamilyMemberId = fm.FamilyMemberId)
                                         WHERE f.FamilyId = @FamilyId";

                    cmd.Parameters.AddWithValue("@FamilyId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        List<MembersInAFamily>? newFamilyList = new List<MembersInAFamily>();
                        while (reader.Read())
                        {
                            MembersInAFamily fm = LoadFromData2(reader);

                            newFamilyList.Add(fm);
                        }

                        return newFamilyList;
                    }
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


        private MembersInAFamily LoadFromData2(SqlDataReader reader)
        {
            return new MembersInAFamily
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FamilyId = reader.GetInt32(reader.GetOrdinal("FamilyId")),
                FamilyName = reader.GetString(reader.GetOrdinal("FamilyName")),
                FamilyMemberId = reader.GetInt32(reader.GetOrdinal("FamilyMemberId")),
                FamilyMemberName = reader.GetString(reader.GetOrdinal("FamilyMemberName")),
                FamilyMemberAge = reader.GetInt32(reader.GetOrdinal("FamilyMemberAge")),
                FamilyMemberGender = reader.GetString(reader.GetOrdinal("FamilyMemberGender"))

            };

        }




    }
   }
 
