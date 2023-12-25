// Created by EgeSuperMine.
// Do not abuse/exploit this Program in any way.
// Program can only be executed by the ones that have Permission.

using System;

namespace Raise_an_EgeSuperMine
{
  internal class Program
  {
    static void Main(string[] args)
    {
      if (Environment.UserName == HiddenClass.TargetUsername)
      {
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        System.Windows.Forms.Application.Run(new Program());
        return;
      } else { System.Diagnostics.Process.GetCurrentProcess().Kill(); return; }
    }

    public Program() { InitClass(true, new byte[1]); }

    private void GrantAccessTopSecret_Click(object sender, EventArgs e)
    {
      if (Environment.UserName == HiddenClass.TargetUserName && HiddenClass.Permission >= 4)
      {
        ShowDocumentary("Top_Secret_Raise_an_EgeSuperMine", true, HiddenClass.Permission);
      }
    }

    private void ShowDocumentary(string Documentary, bool View, byte permission)
    {
      if (!View) { MessageBox.Show($"{HiddenClass.TopSecrets}", " ", MessageBoxButtons.OK, MessageBoxIcon.None); }
      else { System.Windows.Forms.Application.Run(new DocumentaryWindow());
    }
  }
}
