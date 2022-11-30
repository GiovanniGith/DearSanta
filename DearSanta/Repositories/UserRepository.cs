using DearSanta.Interfaces;
using DearSanta.Models;
using System.Data.SqlClient;

namespace DearSanta.Repositories
{
    public class UserRepository : BaseRepository, IUser
    {
        private readonly string _baseSqlSelect = @"SELECT UserId,
                                                    FirebaseUserId,
                                                    UserName,
                                                    UserEmail,
                                                    FamilyId,
                                                    IsAdmin
                                                    
                                                   FROM [User]";

        public UserRepository(IConfiguration config) : base(config) { }

        public List<User> GetAllUsers()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = _baseSqlSelect;

                    using (var reader = cmd.ExecuteReader())
                    {
                        var results = new List<User>();
                        while (reader.Read())
                        {
                            var user = LoadFromData(reader);

                            results.Add(user);
                        }

                        return results;
                    }
                }
            }
        }



        public User CreateUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO [User] (FirebaseUserId, UserName, UserEmail, FamilyId, IsAdmin)
                    OUTPUT INSERTED.ID
                    VALUES (@FirebaseId, @UserName, @UserEmail, @FamilyId, @IsAdmin);
                ";
                    cmd.Parameters.AddWithValue("@firebaseId", user.FirebaseId);
                    cmd.Parameters.AddWithValue("@userName", user.UserName);
                    cmd.Parameters.AddWithValue("@email", user.UserEmail);
                    cmd.Parameters.AddWithValue("@address", user.FamilyId);
                    cmd.Parameters.AddWithValue("@image", user.IsAdmin);                    

                    int id = (int)cmd.ExecuteScalar();

                    user.UserId = id;
                    return user;
                }
            }
        }


        public User? GetUserById(int id)
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
                        User? result = null;
                        if (reader.Read())
                        {
                            return LoadFromData(reader);
                        }

                        return result;

                    }
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            UPDATE [User]
                            SET
                                UserName = @UserName,
                                UserEmail = @UserEmail,
                                FamilyId = @FamilyId,
                                IsAdmin = @IsAdmin,                                 
                            WHERE UserId = @id";

                    cmd.Parameters.AddWithValue("@UserName", user.UserName);
                    cmd.Parameters.AddWithValue("@UserEmail", user.UserEmail);
                    cmd.Parameters.AddWithValue("@FamilyId", user.FamilyId);
                    cmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);          

                    cmd.ExecuteNonQuery();
                }
            }

        }

        private User LoadFromData(SqlDataReader reader)
        {
            return new User
            {
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                FirebaseId = reader.GetString(reader.GetOrdinal("FirebaseUserId")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                UserEmail = reader.GetString(reader.GetOrdinal("UserEmail")),
                FamilyId = reader.GetInt32(reader.GetOrdinal("FamilyId")),                
                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin"))
               
            };
        }


    }
}
