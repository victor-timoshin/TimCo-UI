using System.ComponentModel;

namespace TimCo_UI.Components.Hyperlink
{
	public enum MetaPolicyTypes
	{
		[Description("")]
		None = 0,

		[Description("follow")]
		Follow = 1,

		[Description("nofollow")]
		NoFollow = 2
	}
}