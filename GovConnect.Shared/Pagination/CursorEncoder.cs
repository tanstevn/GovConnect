using System.Text;

namespace GovConnect.Shared.Pagination {
    public static class CursorEncoder {
        public static string Encode(string value) {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(value));
        }

        public static string Decode(string value) {
            return Encoding.UTF8.GetString(Convert.FromBase64String(value));
        }
    }
}
