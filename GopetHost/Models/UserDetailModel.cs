
namespace GopetHost.Models
{
    public class UserDetailModel
    {
        public UserData User { get; set; }

        public ICollection<DongTienModel> DongTiens { get; set; }

        public UserDetailModel(UserData user, ICollection<DongTienModel> dongTiens)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            DongTiens = dongTiens ?? throw new ArgumentNullException(nameof(dongTiens));
        }
    }
}
