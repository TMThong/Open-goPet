using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopetHost.Models
{
	[Table("web_config")]
	public class WebConfigModel
	{
		public const string ACTIVE_USER_PRICE = "active.user.price";
		public const string TỈ_LỆ_NẠP = "napthe.ti.le";
		public const string NỘI_DUNG_NẠP = "noidung.nap.tien";
		public const string MÃ_ĐỐI_TÁC = "partner.id.card";
		public const string KEY_ĐỐI_TÁC = "partner.key.card";
		public const string SỐ_TRANG_MÀ_DIỄN_ĐÀN_HIỂN_THỊ = "num.page.show";
		public const string URL_FILE_JAR = "file.jar";
		public const string URL_FILE_IOS = "file.ios";
		public const string URL_FILE_APK = "file.apk";



		[Key]
		[Display]
		[Required]
		public string Key { get; set; }
		[Required]
		public string Value { get; set; } = string.Empty;
		[Range(0, 5)]
		[Required]
		public TypeValue Type { get; set; } = TypeValue.String;

		[Required]
		public string Comment { get; set; } = string.Empty;

		public enum TypeValue
		{
			String = 0,
			Boolean = 1,
			Int32 = 2,
			Int64 = 3,
			Float = 4,
			Double = 5
		}

		public object ObjectAsValue
		{
			get
			{
				switch (Type)
				{
					case TypeValue.Boolean:
						return bool.Parse(Value);
					case TypeValue.Int32: return int.Parse(Value);
					case TypeValue.Int64: return long.Parse(Value);
					case TypeValue.Float: return float.Parse(Value);
					case TypeValue.Double: return double.Parse(Value);
					default:
						return Value;
				}
			}
		}
	}
}
