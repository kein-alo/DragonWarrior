// PHẦN D - Mô hình một bộ dữ liệu; tên thuộc tính PHẢI khớp tên khóa trong users.json
namespace Lab5_SOF3091.Data;

public class UserData
{
	public string Username { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public bool ThanhCong { get; set; }
	public string GhiChu { get; set; } = string.Empty;
}