using System.ComponentModel.DataAnnotations;
// Keep the System.Data namespace for dynamic variable usage
namespace StudentDatabase.Models
{
    public class User
    {
        public User(dynamic dataRow)
        {
            if (int.TryParse(dataRow["UserID"].ToString(), out int userId)) UserId = userId;
            Username = (string)dataRow["Username"] ?? "";
            UserPassword = (string)dataRow["UserPassword"] ?? "";
            Email = dataRow["Email"] == DBNull.Value ? "" : (string)dataRow["Email"];
        }
        public User() { }
        public int UserId { get; set; }
        [Required, MinLength(2)]
        public string Username { get; set; } = String.Empty;
        [Required, MinLength(2)]
        public string UserPassword { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
    }
}
