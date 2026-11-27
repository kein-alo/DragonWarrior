// PHẦN D - Đọc Data/users.json (static, dùng cho TestCaseSource)
using System.Text.Json;
using Lab5_SOF3091.Data;

namespace Lab5_SOF3091.Utilities;

public static class JsonDataReader
{
	public static List<UserData> DocUsers()
	{
		string path = Path.Combine(AppContext.BaseDirectory, "Data", "users.json");

		if (!File.Exists(path))
			throw new FileNotFoundException($"Không tìm thấy file dữ liệu: {path}", path);

		var users = JsonSerializer.Deserialize<List<UserData>>(File.ReadAllText(path));

		if (users == null || users.Count == 0)
			throw new InvalidDataException("users.json rỗng hoặc sai định dạng.");

		return users;
	}
}