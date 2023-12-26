// Future Plan: Money Pop
// Version planned for feature to be added: BETA 1.2
// Type: Click GFX (Visual Effect)

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Raise_an_EgeSuperMine
{
  public partial class Game : Form
  {
    // Stuff for this Feature
    private Label game_gfx_click1;
    private Label game_gfx_click2;
    private Label game_gfx_click3;
    private Label game_gfx_click4;
    private Label game_gfx_click5;
    private int[] gfx_click_num = { -1, 0, 0, 0, 0, 0 }; // int[0] Unused, int[1] - int[5] used for game_gfx_click[].
    private int nextGFXClick = 1;
    Random rnd = new Random();
    public static int MoneyGain = default;
    // Stuff for this Feature

    public Game() { InitializeComponent(); }
    
    private void InitializeComponent()
    {
      // ...
      game_gfx_click1.BackColor = Color.Transparent;
      game_gfx_click2.BackColor = Color.Transparent;
      game_gfx_click3.BackColor = Color.Transparent;
      game_gfx_click4.BackColor = Color.Transparent;
      game_gfx_click5.BackColor = Color.Transparent;
      // ...
    }

    private void game_esm_Click(object sender, EventArgs e)
    {
      // ...

      // Critical System: Raise_an_EgeSuperMine/idea_rae_critical.cs
      Thread GFXClick = new Thread(t =>
      {
        if (nextGFXClick == 1)
        {
          game_gfx_click1.Location = new Point(game_gfx_click1.Left+rnd.Next(0, 50), game_gfx_click1.Top+rnd.Next(0, 50);
          if (CriticalClick) { gfx_click_num[1] = (MoneyGain * ModMulti) * 2; } else { gfx_click_num[1] = MoneyGain * ModMulti; }
          if (CriticalClick) { game_gfx_click1.ForeColor = Color.Red; } else { game_gfx_click1.ForeColor = Color.White; }
          if (CriticalClick) { game_gfx_click1.Font = new Font("Microsoft Sans Serif", 24F); }
          else { game_gfx_click1.Font = new Font("Microsoft Sans Serif", 12F); }
          game_gfx_click1.Text = gfx_click_num[1].ToString();
          game_gfx_click1.Enabled = true; game_gfx_click1.Visible = true;
          nextGFXClick = 2;
          while (game_gfx_click1.Top < this.Top+100)
          {
            game_gfx_click1.Location = new Point(game_gfx_click1.Left-2, game_gfx_click1.Top+2); Thread.Sleep(1);
          }
          game_gfx_click1.Enabled = false; game_gfx_click1.Visible = false;
          game_gfx_click1.Location = new Point(0, 0);
          nextGFXClick = 1;
          return;
        }
        // ...
      }); GFXClick.Start();
    }
  }
}
