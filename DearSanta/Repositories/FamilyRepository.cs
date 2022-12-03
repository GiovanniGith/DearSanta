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



        private Family LoadFromData(SqlDataReader reader)
        {
            return new Family
            {
                FamilyId = reader.GetInt32(reader.GetOrdinal("FamilyId")),
                FamilyName = reader.GetString(reader.GetOrdinal("FamilyName")),

            };

        }

    }
   }
 
