using System;
using System.Windows.Forms;

namespace ssl_aimbot
{
	// Token: 0x02000005 RID: 5
	internal static class Program
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000031F5 File Offset: 0x000013F5
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}
