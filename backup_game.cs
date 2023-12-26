using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Configuration;
using System.Drawing.Text;
using System.Media;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Policy;
using System.Configuration;
using System.Linq.Expressions;
using System.Windows.Markup;
using DiscordRpcDemo;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Raise_an_EgeSuperMine
{
    public partial class Game : Form
    {
        readonly bool TrueStatement = true;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.DoubleBuffered = true;
            base.OnPaint(e);
        }

        public static string SaveFileLocation; // Never used this yet it still made it to F3 Menu ;-;

        readonly Random rnd = new Random();
        private DiscordRpc.EventHandlers handlers;
        private DiscordRpc.RichPresence presence;
        // readonly DateTime Time = DateTime.Now;
        DateTime Save1_CreatedTime = DateTime.Now;
        DateTime Save1_UpdatedTime = DateTime.Now;

        readonly Keys MuteKey = Keys.M;
        readonly Keys SaveKey = Keys.S;

        public static decimal Money = 0;
        public static int[] Other = { 0, 12, 1, 1, 0, 1, 1 }; // int[0] = Unused, int[1] = Fontsize (Auto=12),
        public static int CPS;                          // int[2] = Auto-Feeder Toggle (Auto=1), int[3] = Auto-Healer Toggle (Auto=1),
        public static int FPS;                          // int[4] = F4 Toggle, int[5] = Auto-Feeder Option, int[6] = Auto-Healer Option
        public static decimal SavedMoney;
        public static int Hunger = 100;
        public static int MaxHunger = 100;
        public static int MoneyGain = 1;
        public static int Health = 250;
        public static int AntiDeaths = 0;
        public static string Host = null;
        public static bool OverworldIsUnlocked = false;
        public static int Gamble_PuttenMoney = 1000;
        public static bool AllowCrashing = false;
        public static bool OHIO_MODE_ACTIVATED = false;
        public static bool GameIsLocked = false;
        public static bool GameIsRunning = true;
        public static bool ShopIsOpen = false;
        public static int GameIsMuted = 0;
        public static string GameDifficulty = null;
        public static bool mods_fastesm = false; static byte debug_mods_fastesm = 0;
        public static bool mods_InfAntiDeath = false; // static byte debug_mods_InfAntiDeath = 0;
        public static bool MoneyTestMode = false;
        public static bool HealthTestMode = false;
        public static bool HungerTestMode = false;
        sbyte ESM_Reverse = 0;
        sbyte GF_Reverse = 0;
        bool F12 = false;
        public static int ModMulti = 1;
        public static int CheatCodeID = 0;
        int HungerCooldown;
        int HealthCooldown;
        int SecretMode;
        int MenuOption;
        int AllowedMenuOptions;
        readonly string path = @"C:\ProgramData\EgeSuperMine.net\Raise an EgeSuperMine\save.save";
        readonly string folderpath = @"C:\ProgramData\EgeSuperMine.net\Raise an EgeSuperMine\";
        readonly string esmfolderpath = @"C:\ProgramData\EgeSuperMine.net";
        //readonly public static int[] THE_CODE_YOU_MUST_WATCH_OUT_FOR = new int[666];

        public static string DiscordRPC_Details = "Playing...";
        public static string DiscordRPC_State = "Menu";

        public static bool Thread_ESMClick_isRunning = false;
        public static bool Thread_ESM_Movement_isRunning = true;
        public static bool Thread_ESM_Hunger_isRunning = true;
        public static bool Thread_MoneySystem_isRunning = true;
        public static bool Thread_ESM_Health_isRunning = true;
        public static bool Thread_Autoclicker1_isRunning = true;
        public static bool Thread_HouseSpriting_isRunning = true;
        public static bool Thread_unused1_isRunning = false;

        readonly SoundPlayer MenuBGM = new SoundPlayer(Directory.GetCurrentDirectory() + @"\mus\mus_menu.wav");
        readonly SoundPlayer BGM1 = new SoundPlayer(Directory.GetCurrentDirectory() + @"\mus\mus_game.wav");
        readonly SoundPlayer BGM2 = new SoundPlayer(Directory.GetCurrentDirectory() + @"\mus\mus_overworld.wav");
        /// readonly SoundPlayer BGM3 = new SoundPlayer(Directory.GetCurrentDirectory() + @"\mus\mus_menu.wav");
        readonly SoundPlayer BGM666 = new SoundPlayer();
        //readonly SoundPlayer clickSnd = new SoundPlayer(Properties.Resources.click);
        readonly SoundPlayer ESMDyingSnd = new SoundPlayer(Properties.Resources.ESMDying);
        readonly SoundPlayer ESMDyingApril = new SoundPlayer();
        readonly SoundPlayer OHIO_MUSIC = new SoundPlayer(); // Swag_Like_Ohio
        readonly SoundPlayer HE_MUST_BE_THERE_ESM_PLEASE_BELIEVE_ME = new SoundPlayer(); // HE_MUST_BE_THERE
        readonly SoundPlayer Silence = new SoundPlayer(Properties.Resources.snd_silence);

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.Overworld = new System.Windows.Forms.Panel();
            this.Overworld_Portal = new System.Windows.Forms.PictureBox();
            this.Overworld_HouseDoor = new System.Windows.Forms.PictureBox();
            this.menu = new System.Windows.Forms.Panel();
            this.menu_settings = new System.Windows.Forms.Label();
            this.menu_cheeseburgerstand = new System.Windows.Forms.Label();
            this.menu_cheeseburger = new System.Windows.Forms.PictureBox();
            this.menu_esm2 = new System.Windows.Forms.PictureBox();
            this.menu_modlist = new System.Windows.Forms.Panel();
            this.menu_modlist_multidisplay = new System.Windows.Forms.Label();
            this.menu_mod_fastesm = new System.Windows.Forms.PictureBox();
            this.menu_mods = new System.Windows.Forms.Label();
            this.menu_saveinfo = new System.Windows.Forms.Label();
            this.menu_continue = new System.Windows.Forms.Label();
            this.menu_newgame = new System.Windows.Forms.Label();
            this.menu_insane = new System.Windows.Forms.Label();
            this.menu_text = new System.Windows.Forms.Label();
            this.menu_hard = new System.Windows.Forms.Label();
            this.menu_medium = new System.Windows.Forms.Label();
            this.menu_easy = new System.Windows.Forms.Label();
            this.menu_esm = new System.Windows.Forms.PictureBox();
            this.F3 = new System.Windows.Forms.Label();
            this.menu_easy_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.menu_medium_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.menu_hard_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.exit = new System.Windows.Forms.Label();
            this.minimize = new System.Windows.Forms.Label();
            this.F12_close_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.F12_minimize_tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.credits_text = new System.Windows.Forms.Label();
            this.Updater = new System.Windows.Forms.Timer(this.components);
            this.EgeSuperMine_Text = new System.Windows.Forms.Label();
            this.EgeSuperMine = new System.Windows.Forms.PictureBox();
            this._game = new System.Windows.Forms.Panel();
            this.game_savetext = new System.Windows.Forms.Label();
            this.game_save = new System.Windows.Forms.PictureBox();
            this.game_floor2barrier = new System.Windows.Forms.PictureBox();
            this.game_floor2text = new System.Windows.Forms.Label();
            this.game_floor2 = new System.Windows.Forms.PictureBox();
            this.game_MoneyCount = new System.Windows.Forms.Label();
            this.game_HungerCount = new System.Windows.Forms.Label();
            this.game_HealthCount = new System.Windows.Forms.Label();
            this.game_computer_shading1 = new System.Windows.Forms.PictureBox();
            this.game_computer = new System.Windows.Forms.PictureBox();
            this.game_autohealer_shading1 = new System.Windows.Forms.PictureBox();
            this.game_autohealer = new System.Windows.Forms.PictureBox();
            this.game_autofeeder = new System.Windows.Forms.PictureBox();
            this.game_omor = new System.Windows.Forms.PictureBox();
            this.game_esm = new System.Windows.Forms.PictureBox();
            this.game_gf = new System.Windows.Forms.PictureBox();
            this.game_door = new System.Windows.Forms.PictureBox();
            this.ESM_HE_S_HE______é_ = new System.Windows.Forms.PictureBox();
            this.sndPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.Overworld.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Overworld_Portal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overworld_HouseDoor)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_cheeseburger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_esm2)).BeginInit();
            this.menu_modlist.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_mod_fastesm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_esm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EgeSuperMine)).BeginInit();
            this._game.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.game_save)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_floor2barrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_floor2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_computer_shading1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_computer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autohealer_shading1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autohealer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autofeeder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_omor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_esm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_gf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_door)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESM_HE_S_HE______é_)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sndPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // Overworld
            // 
            this.Overworld.BackColor = System.Drawing.Color.Green;
            this.Overworld.Controls.Add(this.Overworld_Portal);
            this.Overworld.Controls.Add(this.Overworld_HouseDoor);
            this.Overworld.Enabled = false;
            this.Overworld.Location = new System.Drawing.Point(-32767, -32767);
            this.Overworld.Name = "Overworld";
            this.Overworld.Size = new System.Drawing.Size(1365, 765);
            this.Overworld.TabIndex = 2;
            this.Overworld.Visible = false;
            // 
            // Overworld_Portal
            // 
            this.Overworld_Portal.Image = global::Raise_an_EgeSuperMine.Properties.Resources.Overworld_BadPortal;
            this.Overworld_Portal.Location = new System.Drawing.Point(1000, 0);
            this.Overworld_Portal.Name = "Overworld_Portal";
            this.Overworld_Portal.Size = new System.Drawing.Size(50, 100);
            this.Overworld_Portal.TabIndex = 1;
            this.Overworld_Portal.TabStop = false;
            this.Overworld_Portal.Click += new System.EventHandler(this.Overworld_Portal_Click);
            // 
            // Overworld_HouseDoor
            // 
            this.Overworld_HouseDoor.Image = global::Raise_an_EgeSuperMine.Properties.Resources.HouseDoor_2;
            this.Overworld_HouseDoor.Location = new System.Drawing.Point(0, 330);
            this.Overworld_HouseDoor.Name = "Overworld_HouseDoor";
            this.Overworld_HouseDoor.Size = new System.Drawing.Size(50, 100);
            this.Overworld_HouseDoor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Overworld_HouseDoor.TabIndex = 0;
            this.Overworld_HouseDoor.TabStop = false;
            this.Overworld_HouseDoor.Click += new System.EventHandler(this.Overworld_HouseDoor_Click);
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.Black;
            this.menu.Controls.Add(this.menu_settings);
            this.menu.Controls.Add(this.menu_cheeseburgerstand);
            this.menu.Controls.Add(this.menu_cheeseburger);
            this.menu.Controls.Add(this.menu_esm2);
            this.menu.Controls.Add(this.menu_modlist);
            this.menu.Controls.Add(this.menu_mods);
            this.menu.Controls.Add(this.menu_saveinfo);
            this.menu.Controls.Add(this.menu_continue);
            this.menu.Controls.Add(this.menu_newgame);
            this.menu.Controls.Add(this.menu_insane);
            this.menu.Controls.Add(this.menu_text);
            this.menu.Controls.Add(this.menu_hard);
            this.menu.Controls.Add(this.menu_medium);
            this.menu.Controls.Add(this.menu_easy);
            this.menu.Controls.Add(this.menu_esm);
            this.menu.Location = new System.Drawing.Point(-32767, -32767);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1365, 765);
            this.menu.TabIndex = 3;
            // 
            // menu_settings
            // 
            this.menu_settings.AutoSize = true;
            this.menu_settings.BackColor = System.Drawing.Color.Black;
            this.menu_settings.Font = new System.Drawing.Font("8-bit Operator+", 28F);
            this.menu_settings.ForeColor = System.Drawing.Color.DimGray;
            this.menu_settings.Location = new System.Drawing.Point(0, 120);
            this.menu_settings.Margin = new System.Windows.Forms.Padding(0);
            this.menu_settings.Name = "menu_settings";
            this.menu_settings.Size = new System.Drawing.Size(219, 53);
            this.menu_settings.TabIndex = 15;
            this.menu_settings.Text = "Settings";
            // 
            // menu_cheeseburgerstand
            // 
            this.menu_cheeseburgerstand.AutoSize = true;
            this.menu_cheeseburgerstand.BackColor = System.Drawing.Color.Gray;
            this.menu_cheeseburgerstand.Font = new System.Drawing.Font("8-bit Operator+", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menu_cheeseburgerstand.Location = new System.Drawing.Point(-15, 620);
            this.menu_cheeseburgerstand.Name = "menu_cheeseburgerstand";
            this.menu_cheeseburgerstand.Size = new System.Drawing.Size(118, 44);
            this.menu_cheeseburgerstand.TabIndex = 14;
            this.menu_cheeseburgerstand.Text = "         ";
            // 
            // menu_cheeseburger
            // 
            this.menu_cheeseburger.BackColor = System.Drawing.Color.Transparent;
            this.menu_cheeseburger.Image = global::Raise_an_EgeSuperMine.Properties.Resources.shop_cheeseburger;
            this.menu_cheeseburger.Location = new System.Drawing.Point(0, 541);
            this.menu_cheeseburger.Name = "menu_cheeseburger";
            this.menu_cheeseburger.Size = new System.Drawing.Size(100, 100);
            this.menu_cheeseburger.TabIndex = 13;
            this.menu_cheeseburger.TabStop = false;
            // 
            // menu_esm2
            // 
            this.menu_esm2.Image = global::Raise_an_EgeSuperMine.Properties.Resources.ESM_cuteface1_reversed;
            this.menu_esm2.Location = new System.Drawing.Point(245, 665);
            this.menu_esm2.Name = "menu_esm2";
            this.menu_esm2.Size = new System.Drawing.Size(100, 100);
            this.menu_esm2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.menu_esm2.TabIndex = 12;
            this.menu_esm2.TabStop = false;
            // 
            // menu_modlist
            // 
            this.menu_modlist.BackColor = System.Drawing.Color.Silver;
            this.menu_modlist.Controls.Add(this.menu_modlist_multidisplay);
            this.menu_modlist.Controls.Add(this.menu_mod_fastesm);
            this.menu_modlist.Enabled = false;
            this.menu_modlist.Location = new System.Drawing.Point(1160, 500);
            this.menu_modlist.Name = "menu_modlist";
            this.menu_modlist.Size = new System.Drawing.Size(200, 215);
            this.menu_modlist.TabIndex = 11;
            this.menu_modlist.Visible = false;
            // 
            // menu_modlist_multidisplay
            // 
            this.menu_modlist_multidisplay.AutoSize = true;
            this.menu_modlist_multidisplay.Font = new System.Drawing.Font("8-bit Operator+", 16F);
            this.menu_modlist_multidisplay.Location = new System.Drawing.Point(0, 185);
            this.menu_modlist_multidisplay.Name = "menu_modlist_multidisplay";
            this.menu_modlist_multidisplay.Size = new System.Drawing.Size(178, 31);
            this.menu_modlist_multidisplay.TabIndex = 1;
            this.menu_modlist_multidisplay.Text = "Mod Multi: x1";
            // 
            // menu_mod_fastesm
            // 
            this.menu_mod_fastesm.BackColor = System.Drawing.Color.Transparent;
            this.menu_mod_fastesm.Image = global::Raise_an_EgeSuperMine.Properties.Resources.mods_fastesm;
            this.menu_mod_fastesm.Location = new System.Drawing.Point(5, 5);
            this.menu_mod_fastesm.Name = "menu_mod_fastesm";
            this.menu_mod_fastesm.Size = new System.Drawing.Size(50, 50);
            this.menu_mod_fastesm.TabIndex = 0;
            this.menu_mod_fastesm.TabStop = false;
            this.menu_mod_fastesm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Menu_mod_fastesm_MouseClick);
            // 
            // menu_mods
            // 
            this.menu_mods.AutoSize = true;
            this.menu_mods.BackColor = System.Drawing.Color.Gray;
            this.menu_mods.Enabled = false;
            this.menu_mods.Font = new System.Drawing.Font("8-bit Operator+", 24F);
            this.menu_mods.ForeColor = System.Drawing.Color.LightGray;
            this.menu_mods.Location = new System.Drawing.Point(1160, 720);
            this.menu_mods.Margin = new System.Windows.Forms.Padding(0);
            this.menu_mods.Name = "menu_mods";
            this.menu_mods.Size = new System.Drawing.Size(207, 44);
            this.menu_mods.TabIndex = 10;
            this.menu_mods.Text = "Modifiers";
            this.menu_mods.Visible = false;
            this.menu_mods.Click += new System.EventHandler(this.Menu_mods_Click);
            // 
            // menu_saveinfo
            // 
            this.menu_saveinfo.AutoSize = true;
            this.menu_saveinfo.BackColor = System.Drawing.Color.Black;
            this.menu_saveinfo.Enabled = false;
            this.menu_saveinfo.Font = new System.Drawing.Font("8-bit Operator+", 36F);
            this.menu_saveinfo.ForeColor = System.Drawing.Color.Lime;
            this.menu_saveinfo.Location = new System.Drawing.Point(300, 110);
            this.menu_saveinfo.Margin = new System.Windows.Forms.Padding(0);
            this.menu_saveinfo.Name = "menu_saveinfo";
            this.menu_saveinfo.Size = new System.Drawing.Size(843, 335);
            this.menu_saveinfo.TabIndex = 8;
            this.menu_saveinfo.Text = "SAVE\r\nCreated at 22.10.2023 11:11:11\r\nUpdated at 22.10.2023 11:11:11\r\nA Difficult" +
    "y\r\nMods: Mods";
            this.menu_saveinfo.Visible = false;
            // 
            // menu_continue
            // 
            this.menu_continue.AutoSize = true;
            this.menu_continue.BackColor = System.Drawing.Color.Black;
            this.menu_continue.Font = new System.Drawing.Font("8-bit Operator+", 28F);
            this.menu_continue.ForeColor = System.Drawing.Color.Gray;
            this.menu_continue.Location = new System.Drawing.Point(0, 60);
            this.menu_continue.Margin = new System.Windows.Forms.Padding(0);
            this.menu_continue.Name = "menu_continue";
            this.menu_continue.Size = new System.Drawing.Size(222, 53);
            this.menu_continue.TabIndex = 7;
            this.menu_continue.Text = "Continue";
            this.menu_continue.Click += new System.EventHandler(this.Menu_continue_Click);
            // 
            // menu_newgame
            // 
            this.menu_newgame.AutoSize = true;
            this.menu_newgame.BackColor = System.Drawing.Color.Black;
            this.menu_newgame.Font = new System.Drawing.Font("8-bit Operator+", 28F);
            this.menu_newgame.ForeColor = System.Drawing.Color.Gray;
            this.menu_newgame.Location = new System.Drawing.Point(0, 0);
            this.menu_newgame.Margin = new System.Windows.Forms.Padding(0);
            this.menu_newgame.Name = "menu_newgame";
            this.menu_newgame.Size = new System.Drawing.Size(229, 53);
            this.menu_newgame.TabIndex = 6;
            this.menu_newgame.Text = "New Game";
            this.menu_newgame.Click += new System.EventHandler(this.Menu_newgame_Click);
            // 
            // menu_insane
            // 
            this.menu_insane.AutoSize = true;
            this.menu_insane.BackColor = System.Drawing.Color.DarkViolet;
            this.menu_insane.Enabled = false;
            this.menu_insane.Font = new System.Drawing.Font("8-bit Operator+", 64F);
            this.menu_insane.ForeColor = System.Drawing.Color.Indigo;
            this.menu_insane.Location = new System.Drawing.Point(487, 550);
            this.menu_insane.Margin = new System.Windows.Forms.Padding(0);
            this.menu_insane.Name = "menu_insane";
            this.menu_insane.Size = new System.Drawing.Size(395, 119);
            this.menu_insane.TabIndex = 5;
            this.menu_insane.Text = "Insane";
            this.menu_insane.Visible = false;
            this.menu_insane.Click += new System.EventHandler(this.Menu_insane_Click);
            // 
            // menu_text
            // 
            this.menu_text.AutoSize = true;
            this.menu_text.BackColor = System.Drawing.Color.Black;
            this.menu_text.Font = new System.Drawing.Font("8-bit Operator+", 24F);
            this.menu_text.ForeColor = System.Drawing.Color.Gray;
            this.menu_text.Location = new System.Drawing.Point(0, 720);
            this.menu_text.Margin = new System.Windows.Forms.Padding(0);
            this.menu_text.Name = "menu_text";
            this.menu_text.Size = new System.Drawing.Size(191, 44);
            this.menu_text.TabIndex = 4;
            this.menu_text.Text = "BETA 1.2\r\n";
            this.menu_text.Click += new System.EventHandler(this.Menu_text_Click);
            // 
            // menu_hard
            // 
            this.menu_hard.AutoSize = true;
            this.menu_hard.BackColor = System.Drawing.Color.Red;
            this.menu_hard.Font = new System.Drawing.Font("8-bit Operator+", 64F);
            this.menu_hard.ForeColor = System.Drawing.Color.DarkRed;
            this.menu_hard.Location = new System.Drawing.Point(540, 400);
            this.menu_hard.Margin = new System.Windows.Forms.Padding(0);
            this.menu_hard.Name = "menu_hard";
            this.menu_hard.Size = new System.Drawing.Size(290, 119);
            this.menu_hard.TabIndex = 3;
            this.menu_hard.Text = "Hard";
            this.menu_hard_tooltip.SetToolTip(this.menu_hard, "Fast Starvation and Sickness.");
            this.menu_hard.Click += new System.EventHandler(this.Menu_hard_Click);
            // 
            // menu_medium
            // 
            this.menu_medium.AutoSize = true;
            this.menu_medium.BackColor = System.Drawing.Color.Yellow;
            this.menu_medium.Font = new System.Drawing.Font("8-bit Operator+", 64F);
            this.menu_medium.ForeColor = System.Drawing.Color.Orange;
            this.menu_medium.Location = new System.Drawing.Point(475, 250);
            this.menu_medium.Margin = new System.Windows.Forms.Padding(0);
            this.menu_medium.Name = "menu_medium";
            this.menu_medium.Size = new System.Drawing.Size(417, 119);
            this.menu_medium.TabIndex = 2;
            this.menu_medium.Text = "Medium";
            this.menu_medium_tooltip.SetToolTip(this.menu_medium, "Normal Speed Starvation and Sickness.\r\n[Recommended Difficulty]");
            this.menu_medium.Click += new System.EventHandler(this.Menu_medium_Click);
            // 
            // menu_easy
            // 
            this.menu_easy.AutoSize = true;
            this.menu_easy.BackColor = System.Drawing.Color.Lime;
            this.menu_easy.Font = new System.Drawing.Font("8-bit Operator+", 64F);
            this.menu_easy.ForeColor = System.Drawing.Color.Green;
            this.menu_easy.Location = new System.Drawing.Point(540, 100);
            this.menu_easy.Margin = new System.Windows.Forms.Padding(0);
            this.menu_easy.Name = "menu_easy";
            this.menu_easy.Size = new System.Drawing.Size(290, 119);
            this.menu_easy.TabIndex = 1;
            this.menu_easy.Text = "Easy";
            this.menu_easy_tooltip.SetToolTip(this.menu_easy, "Slow Starvation and Sickness.");
            this.menu_easy.Click += new System.EventHandler(this.Menu_easy_Click);
            // 
            // menu_esm
            // 
            this.menu_esm.Image = global::Raise_an_EgeSuperMine.Properties.Resources.ESM_cuteface1;
            this.menu_esm.Location = new System.Drawing.Point(632, 0);
            this.menu_esm.Name = "menu_esm";
            this.menu_esm.Size = new System.Drawing.Size(100, 100);
            this.menu_esm.TabIndex = 0;
            this.menu_esm.TabStop = false;
            this.menu_esm.Click += new System.EventHandler(this.Menu_esm_Click);
            // 
            // F3
            // 
            this.F3.AutoSize = true;
            this.F3.BackColor = System.Drawing.Color.Transparent;
            this.F3.Enabled = false;
            this.F3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.F3.ForeColor = System.Drawing.Color.White;
            this.F3.Location = new System.Drawing.Point(0, 0);
            this.F3.Name = "F3";
            this.F3.Size = new System.Drawing.Size(89, 20);
            this.F3.TabIndex = 15;
            this.F3.Text = ">_> Key.F3";
            this.F3.Visible = false;
            this.F3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.F3_MouseClick);
            // 
            // menu_easy_tooltip
            // 
            this.menu_easy_tooltip.ToolTipTitle = "Easy Difficulty";
            // 
            // menu_medium_tooltip
            // 
            this.menu_medium_tooltip.ToolTipTitle = "Medium Difficulty";
            // 
            // menu_hard_tooltip
            // 
            this.menu_hard_tooltip.BackColor = System.Drawing.Color.Red;
            this.menu_hard_tooltip.ForeColor = System.Drawing.Color.DarkRed;
            this.menu_hard_tooltip.ToolTipTitle = "Hard Difficulty";
            // 
            // exit
            // 
            this.exit.AutoSize = true;
            this.exit.BackColor = System.Drawing.Color.Red;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.exit.Location = new System.Drawing.Point(1315, 0);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(51, 51);
            this.exit.TabIndex = 15;
            this.exit.Text = "X";
            this.F12_close_tooltip.SetToolTip(this.exit, "Closes the Game.");
            this.exit.Visible = false;
            this.exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // minimize
            // 
            this.minimize.AutoSize = true;
            this.minimize.BackColor = System.Drawing.Color.DarkGray;
            this.minimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.minimize.Location = new System.Drawing.Point(1265, 0);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(46, 51);
            this.minimize.TabIndex = 16;
            this.minimize.Text = "⎯";
            this.F12_minimize_tooltip.SetToolTip(this.minimize, "Hides the Game Window.");
            this.minimize.Visible = false;
            this.minimize.Click += new System.EventHandler(this.Minimize_Click);
            // 
            // F12_close_tooltip
            // 
            this.F12_close_tooltip.BackColor = System.Drawing.Color.Red;
            this.F12_close_tooltip.ForeColor = System.Drawing.Color.DarkRed;
            this.F12_close_tooltip.ToolTipTitle = "Close";
            // 
            // F12_minimize_tooltip
            // 
            this.F12_minimize_tooltip.ToolTipTitle = "Minimize";
            // 
            // credits_text
            // 
            this.credits_text.AutoSize = true;
            this.credits_text.BackColor = System.Drawing.Color.Black;
            this.credits_text.Font = new System.Drawing.Font("8-bit Operator+", 86F);
            this.credits_text.ForeColor = System.Drawing.Color.White;
            this.credits_text.Location = new System.Drawing.Point(175, 750);
            this.credits_text.Name = "credits_text";
            this.credits_text.Size = new System.Drawing.Size(1017, 2400);
            this.credits_text.TabIndex = 2;
            this.credits_text.Text = "    Developer\r\nEgeSuperMine\r\n\r\n       Music\r\n     TobyFox\r\n         A.I.\r\n\r\n     " +
    "   Art\r\nEgeSuperMine\r\n      splat\r\n\r\n\r\n\r\n   Thanks for\r\n     Playing!";
            // 
            // Updater
            // 
            this.Updater.Enabled = true;
            this.Updater.Tick += new System.EventHandler(this.Updater_Tick);
            // 
            // EgeSuperMine_Text
            // 
            this.EgeSuperMine_Text.AutoSize = true;
            this.EgeSuperMine_Text.Enabled = false;
            this.EgeSuperMine_Text.Font = new System.Drawing.Font("Arial", 36F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.EgeSuperMine_Text.ForeColor = System.Drawing.Color.White;
            this.EgeSuperMine_Text.Location = new System.Drawing.Point(475, 515);
            this.EgeSuperMine_Text.Name = "EgeSuperMine_Text";
            this.EgeSuperMine_Text.Size = new System.Drawing.Size(410, 55);
            this.EgeSuperMine_Text.TabIndex = 19;
            this.EgeSuperMine_Text.Text = "EGESUPERMINE";
            this.EgeSuperMine_Text.Visible = false;
            // 
            // EgeSuperMine
            // 
            this.EgeSuperMine.Enabled = false;
            this.EgeSuperMine.Image = global::Raise_an_EgeSuperMine.Properties.Resources.EgeSuperMine;
            this.EgeSuperMine.Location = new System.Drawing.Point(602, 282);
            this.EgeSuperMine.Name = "EgeSuperMine";
            this.EgeSuperMine.Size = new System.Drawing.Size(160, 200);
            this.EgeSuperMine.TabIndex = 18;
            this.EgeSuperMine.TabStop = false;
            this.EgeSuperMine.Visible = false;
            // 
            // _game
            // 
            this._game.BackColor = System.Drawing.Color.LightGreen;
            this._game.BackgroundImage = global::Raise_an_EgeSuperMine.Properties.Resources.HomeBG3;
            this._game.Controls.Add(this.game_savetext);
            this._game.Controls.Add(this.game_save);
            this._game.Controls.Add(this.game_floor2barrier);
            this._game.Controls.Add(this.game_floor2text);
            this._game.Controls.Add(this.game_floor2);
            this._game.Controls.Add(this.game_MoneyCount);
            this._game.Controls.Add(this.game_HungerCount);
            this._game.Controls.Add(this.game_HealthCount);
            this._game.Controls.Add(this.game_computer_shading1);
            this._game.Controls.Add(this.game_computer);
            this._game.Controls.Add(this.game_autohealer_shading1);
            this._game.Controls.Add(this.game_autohealer);
            this._game.Controls.Add(this.game_autofeeder);
            this._game.Controls.Add(this.game_omor);
            this._game.Controls.Add(this.game_esm);
            this._game.Controls.Add(this.game_gf);
            this._game.Controls.Add(this.game_door);
            this._game.Enabled = false;
            this._game.Location = new System.Drawing.Point(0, 0);
            this._game.Name = "_game";
            this._game.Size = new System.Drawing.Size(1365, 765);
            this._game.TabIndex = 1;
            this._game.Visible = false;
            this._game.EnabledChanged += new System.EventHandler(this.Game_EnabledChanged);
            // 
            // game_savetext
            // 
            this.game_savetext.AutoSize = true;
            this.game_savetext.Font = new System.Drawing.Font("8-bit Operator+", 16F);
            this.game_savetext.Location = new System.Drawing.Point(1250, 165);
            this.game_savetext.Name = "game_savetext";
            this.game_savetext.Size = new System.Drawing.Size(97, 31);
            this.game_savetext.TabIndex = 15;
            this.game_savetext.Text = "Saved!";
            this.game_savetext.Visible = false;
            // 
            // game_save
            // 
            this.game_save.BackColor = System.Drawing.Color.Transparent;
            this.game_save.Image = global::Raise_an_EgeSuperMine.Properties.Resources.Save;
            this.game_save.Location = new System.Drawing.Point(1250, 200);
            this.game_save.Name = "game_save";
            this.game_save.Size = new System.Drawing.Size(100, 100);
            this.game_save.TabIndex = 14;
            this.game_save.TabStop = false;
            this.game_save.Click += new System.EventHandler(this.Game_save_Click);
            // 
            // game_floor2barrier
            // 
            this.game_floor2barrier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(94)))), ((int)(((byte)(0)))));
            this.game_floor2barrier.Location = new System.Drawing.Point(490, 100);
            this.game_floor2barrier.Name = "game_floor2barrier";
            this.game_floor2barrier.Size = new System.Drawing.Size(400, 25);
            this.game_floor2barrier.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_floor2barrier.TabIndex = 11;
            this.game_floor2barrier.TabStop = false;
            this.game_floor2barrier.Click += new System.EventHandler(this.Game_floor2barrier_Click);
            // 
            // game_floor2text
            // 
            this.game_floor2text.AutoSize = true;
            this.game_floor2text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(94)))), ((int)(((byte)(66)))));
            this.game_floor2text.Font = new System.Drawing.Font("Segoe Print", 32F);
            this.game_floor2text.Location = new System.Drawing.Point(600, 10);
            this.game_floor2text.Name = "game_floor2text";
            this.game_floor2text.Size = new System.Drawing.Size(187, 75);
            this.game_floor2text.TabIndex = 10;
            this.game_floor2text.Text = "Floor 2";
            this.game_floor2text.Click += new System.EventHandler(this.Game_floor2text_Click);
            // 
            // game_floor2
            // 
            this.game_floor2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(94)))), ((int)(((byte)(66)))));
            this.game_floor2.Location = new System.Drawing.Point(490, 0);
            this.game_floor2.Name = "game_floor2";
            this.game_floor2.Size = new System.Drawing.Size(400, 100);
            this.game_floor2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_floor2.TabIndex = 9;
            this.game_floor2.TabStop = false;
            this.game_floor2.Click += new System.EventHandler(this.Game_floor2_Click);
            // 
            // game_MoneyCount
            // 
            this.game_MoneyCount.AutoSize = true;
            this.game_MoneyCount.BackColor = System.Drawing.Color.Transparent;
            this.game_MoneyCount.Font = new System.Drawing.Font("8-bit Operator+", 48F);
            this.game_MoneyCount.ForeColor = System.Drawing.Color.Green;
            this.game_MoneyCount.Location = new System.Drawing.Point(0, 0);
            this.game_MoneyCount.Name = "game_MoneyCount";
            this.game_MoneyCount.Size = new System.Drawing.Size(170, 89);
            this.game_MoneyCount.TabIndex = 1;
            this.game_MoneyCount.Text = "💲0";
            // 
            // game_HungerCount
            // 
            this.game_HungerCount.AutoSize = true;
            this.game_HungerCount.BackColor = System.Drawing.Color.Transparent;
            this.game_HungerCount.Font = new System.Drawing.Font("8-bit Operator+", 48F);
            this.game_HungerCount.ForeColor = System.Drawing.Color.Green;
            this.game_HungerCount.Location = new System.Drawing.Point(0, 180);
            this.game_HungerCount.Name = "game_HungerCount";
            this.game_HungerCount.Size = new System.Drawing.Size(401, 89);
            this.game_HungerCount.TabIndex = 8;
            this.game_HungerCount.Text = "🍏100/100";
            // 
            // game_HealthCount
            // 
            this.game_HealthCount.AutoSize = true;
            this.game_HealthCount.BackColor = System.Drawing.Color.Transparent;
            this.game_HealthCount.Font = new System.Drawing.Font("8-bit Operator+", 48F);
            this.game_HealthCount.ForeColor = System.Drawing.Color.Green;
            this.game_HealthCount.Location = new System.Drawing.Point(0, 90);
            this.game_HealthCount.Name = "game_HealthCount";
            this.game_HealthCount.Size = new System.Drawing.Size(258, 89);
            this.game_HealthCount.TabIndex = 7;
            this.game_HealthCount.Text = "💖250";
            // 
            // game_computer_shading1
            // 
            this.game_computer_shading1.BackColor = System.Drawing.Color.Transparent;
            this.game_computer_shading1.Location = new System.Drawing.Point(1295, 470);
            this.game_computer_shading1.Name = "game_computer_shading1";
            this.game_computer_shading1.Size = new System.Drawing.Size(50, 145);
            this.game_computer_shading1.TabIndex = 6;
            this.game_computer_shading1.TabStop = false;
            // 
            // game_computer
            // 
            this.game_computer.BackColor = System.Drawing.Color.Transparent;
            this.game_computer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.game_computer.Image = global::Raise_an_EgeSuperMine.Properties.Resources.PC_off;
            this.game_computer.Location = new System.Drawing.Point(1220, 565);
            this.game_computer.Name = "game_computer";
            this.game_computer.Size = new System.Drawing.Size(125, 200);
            this.game_computer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_computer.TabIndex = 3;
            this.game_computer.TabStop = false;
            this.game_computer.Click += new System.EventHandler(this.Game_computer_Click);
            // 
            // game_autohealer_shading1
            // 
            this.game_autohealer_shading1.BackColor = System.Drawing.Color.Transparent;
            this.game_autohealer_shading1.Location = new System.Drawing.Point(1215, 410);
            this.game_autohealer_shading1.Name = "game_autohealer_shading1";
            this.game_autohealer_shading1.Size = new System.Drawing.Size(100, 40);
            this.game_autohealer_shading1.TabIndex = 16;
            this.game_autohealer_shading1.TabStop = false;
            // 
            // game_autohealer
            // 
            this.game_autohealer.BackColor = System.Drawing.Color.Transparent;
            this.game_autohealer.Enabled = false;
            this.game_autohealer.Image = global::Raise_an_EgeSuperMine.Properties.Resources.autofeeder_on;
            this.game_autohealer.Location = new System.Drawing.Point(1215, 412);
            this.game_autohealer.Name = "game_autohealer";
            this.game_autohealer.Size = new System.Drawing.Size(100, 100);
            this.game_autohealer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_autohealer.TabIndex = 17;
            this.game_autohealer.TabStop = false;
            this.game_autohealer.Visible = false;
            this.game_autohealer.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Game_autohealer_Click);
            // 
            // game_autofeeder
            // 
            this.game_autofeeder.BackColor = System.Drawing.Color.Transparent;
            this.game_autofeeder.Enabled = false;
            this.game_autofeeder.Image = global::Raise_an_EgeSuperMine.Properties.Resources.autofeeder_on;
            this.game_autofeeder.Location = new System.Drawing.Point(1215, 470);
            this.game_autofeeder.Name = "game_autofeeder";
            this.game_autofeeder.Size = new System.Drawing.Size(100, 100);
            this.game_autofeeder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_autofeeder.TabIndex = 13;
            this.game_autofeeder.TabStop = false;
            this.game_autofeeder.Visible = false;
            this.game_autofeeder.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Game_autofeeder_Click);
            // 
            // game_omor
            // 
            this.game_omor.Image = global::Raise_an_EgeSuperMine.Properties.Resources.omorbius;
            this.game_omor.Location = new System.Drawing.Point(1270, 675);
            this.game_omor.Name = "game_omor";
            this.game_omor.Size = new System.Drawing.Size(45, 75);
            this.game_omor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_omor.TabIndex = 4;
            this.game_omor.TabStop = false;
            this.game_omor.Click += new System.EventHandler(this.Game_omor_Click);
            // 
            // game_esm
            // 
            this.game_esm.BackColor = System.Drawing.Color.Transparent;
            this.game_esm.Image = global::Raise_an_EgeSuperMine.Properties.Resources.ESM_cuteface1;
            this.game_esm.Location = new System.Drawing.Point(5, 665);
            this.game_esm.Name = "game_esm";
            this.game_esm.Size = new System.Drawing.Size(100, 100);
            this.game_esm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.game_esm.TabIndex = 0;
            this.game_esm.TabStop = false;
            this.game_esm.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Game_esm_Click);
            // 
            // game_gf
            // 
            this.game_gf.BackColor = System.Drawing.Color.Transparent;
            this.game_gf.Enabled = false;
            this.game_gf.Image = global::Raise_an_EgeSuperMine.Properties.Resources.GF_cuteface1;
            this.game_gf.Location = new System.Drawing.Point(200, 665);
            this.game_gf.Name = "game_gf";
            this.game_gf.Size = new System.Drawing.Size(100, 100);
            this.game_gf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.game_gf.TabIndex = 12;
            this.game_gf.TabStop = false;
            this.game_gf.Visible = false;
            // 
            // game_door
            // 
            this.game_door.BackColor = System.Drawing.Color.Transparent;
            this.game_door.Image = global::Raise_an_EgeSuperMine.Properties.Resources.HouseDoor;
            this.game_door.Location = new System.Drawing.Point(0, 465);
            this.game_door.Name = "game_door";
            this.game_door.Size = new System.Drawing.Size(150, 300);
            this.game_door.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.game_door.TabIndex = 5;
            this.game_door.TabStop = false;
            this.game_door.Click += new System.EventHandler(this.Game_door_Click);
            // 
            // ESM_HE_S_HE______é_
            // 
            this.ESM_HE_S_HE______é_.BackColor = System.Drawing.Color.Red;
            this.ESM_HE_S_HE______é_.Enabled = false;
            this.ESM_HE_S_HE______é_.Image = global::Raise_an_EgeSuperMine.Properties.Resources.HE_S_HERE_;
            this.ESM_HE_S_HE______é_.Location = new System.Drawing.Point(630, 325);
            this.ESM_HE_S_HE______é_.Name = "ESM_HE_S_HE______é_";
            this.ESM_HE_S_HE______é_.Size = new System.Drawing.Size(100, 100);
            this.ESM_HE_S_HE______é_.TabIndex = 17;
            this.ESM_HE_S_HE______é_.TabStop = false;
            this.ESM_HE_S_HE______é_.Visible = false;
            // 
            // sndPlayer
            // 
            this.sndPlayer.Enabled = true;
            this.sndPlayer.Location = new System.Drawing.Point(10, 25);
            this.sndPlayer.Name = "sndPlayer";
            this.sndPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("sndPlayer.OcxState")));
            this.sndPlayer.Size = new System.Drawing.Size(35, 35);
            this.sndPlayer.TabIndex = 20;
            this.sndPlayer.Visible = false;
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1365, 765);
            this.Controls.Add(this.sndPlayer);
            this.Controls.Add(this.F3);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.menu);
            this.Controls.Add(this._game);
            this.Controls.Add(this.credits_text);
            this.Controls.Add(this.Overworld);
            this.Controls.Add(this.EgeSuperMine);
            this.Controls.Add(this.EgeSuperMine_Text);
            this.Controls.Add(this.ESM_HE_S_HE______é_);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PreviewKeyIsDown);
            this.Overworld.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Overworld_Portal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Overworld_HouseDoor)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_cheeseburger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_esm2)).EndInit();
            this.menu_modlist.ResumeLayout(false);
            this.menu_modlist.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menu_mod_fastesm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menu_esm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EgeSuperMine)).EndInit();
            this._game.ResumeLayout(false);
            this._game.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.game_save)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_floor2barrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_floor2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_computer_shading1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_computer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autohealer_shading1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autohealer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_autofeeder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_omor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_esm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_gf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.game_door)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ESM_HE_S_HE______é_)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sndPlayer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public Game() { InitializeComponent(); }

        static void SetDoubleBuffer(Control ctl, bool DoubleBuffered)
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null,
                ctl, new object[] { DoubleBuffered });
            } catch (Exception) { }
        }

        public void DiscordRPC_Init()
        {
            // DiscordRPC \\
            this.handlers = default;
            DiscordRpc.Initialize("1167897158473748530", ref this.handlers, true, null);
            this.handlers = default;
            DiscordRpc.Initialize("1167897158473748530", ref this.handlers, true, null);
            this.presence.details = $"{DiscordRPC_Details}";
            this.presence.state = $"{DiscordRPC_State}";
            this.presence.largeImageKey = "https://i.pinimg.com/736x/36/bf/7f/36bf7f2d9d76a36a6e35338bea826a02.jpg";
            this.presence.smallImageKey = "";
            this.presence.largeImageText = "EgeSuperMine :3";
            this.presence.smallImageText = "";
            DiscordRpc.UpdatePresence(ref this.presence);
            // DiscordRPC \\
        }

        private async void Game_Load(object sender, EventArgs e)
        {
            try { menu.BackgroundImage = Properties.Resources.MenuBG; } catch (Exception) { MessageBox.Show("Failed to load Menu Background.", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            SetDoubleBuffer(this, true);
            SetDoubleBuffer(EgeSuperMine, true);
            SetDoubleBuffer(_game, true);
            SetDoubleBuffer(menu, true);
            SetDoubleBuffer(game_esm, true);
            SetDoubleBuffer(game_gf, true);
            SetDoubleBuffer(Overworld, true);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();

            if (Screen.PrimaryScreen.Bounds.Width > 1366 && Screen.PrimaryScreen.Bounds.Height > 768)
            {
                FormBorderStyle = FormBorderStyle.FixedSingle;
                CenterToScreen();
            }
            if (Screen.PrimaryScreen.Bounds.Width == 1366 && Screen.PrimaryScreen.Bounds.Height == 768)
            {
                Size = Screen.PrimaryScreen.Bounds.Size;
                FormBorderStyle = FormBorderStyle.None;
                Location = new Point(0, 0);
            }
            if (Screen.PrimaryScreen.Bounds.Width < 1366 || Screen.PrimaryScreen.Bounds.Height < 768)
            {
                MessageBox.Show("Your Screen Resolution is Below 1366x768.", " ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Process.GetCurrentProcess().Kill();
            }

            DiscordRPC_Init();

            await Task.Delay(1000);
            EgeSuperMine.Enabled = true; EgeSuperMine.Visible = true; EgeSuperMine_Text.Enabled = true; EgeSuperMine_Text.Visible = true;
            await Task.Delay(1000);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine_r;
            await Task.Delay(250);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine;
            await Task.Delay(250);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine_r;
            await Task.Delay(250);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine;
            await Task.Delay(250);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine_r;
            await Task.Delay(250);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine;
            await Task.Delay(1000);
            EgeSuperMine.Image = Properties.Resources.EgeSuperMine_r;
            await Task.Delay(500);
            while (EgeSuperMine.Left > -200) { EgeSuperMine.Left -= 2; }
            await Task.Delay(1000);
            EgeSuperMine_Text.Enabled = false; EgeSuperMine_Text.Visible = false;
            await Task.Delay(1000);

            // Hunger = int.MaxValue; MaxHunger = int.MaxValue; Money = int.MaxValue;
            _game.Location = new Point(0, 0);
            menu.Location = new Point(0, 0);
            Overworld.Location = new Point(0, 0);
            game_esm.Location = new Point(5, 665);
            game_gf.Location = new Point(5, 665);
            Text = "Raise an EgeSuperMine";
            F3.Text = null;
            MenuBGM.PlayLooping();
            Hunger = 100;
            Health = 250;
            game_esm.Image = Properties.Resources.ESM_cuteface1;

            if (Screen.PrimaryScreen.Bounds.Width == 1366 && Screen.PrimaryScreen.Bounds.Height == 768)
            {
                Size = Screen.PrimaryScreen.Bounds.Size;
                FormBorderStyle = FormBorderStyle.None;
                _game.Location = new Point(0, 1);
                menu.Location = new Point(0, 1);
                Overworld.Location = new Point(0, 0);
                F12 = true; exit.Visible = true; minimize.Visible = true;
                Location = new Point(0, 0);
            }

            if (!File.Exists(esmfolderpath)) { Directory.CreateDirectory(esmfolderpath); }
            if (!File.Exists(folderpath)) { Directory.CreateDirectory(folderpath); }

            if (File.Exists(path))
            {
                MenuOption = 2;
                menu_continue.ForeColor = Color.Yellow;
                menu_continue.Text = "> Continue";
                menu_easy.Enabled = false; menu_easy.Visible = false;
                menu_medium.Enabled = false; menu_medium.Visible = false;
                menu_hard.Enabled = false; menu_hard.Visible = false;
                menu_insane.Enabled = false; menu_insane.Visible = false;
                menu_esm.Enabled = false; menu_esm.Visible = false;
                menu_saveinfo.Enabled = true; menu_saveinfo.Visible = true; menu_mods.Enabled = false; menu_mods.Visible = false;
                menu_saveinfo.Text = $"SAVE 1\r\nCreated at {Save1_CreatedTime}\r\nUpdated at {Save1_UpdatedTime}\n{GameDifficulty} Difficulty";
            }

            if (!File.Exists(path))
            {
                File.Create(path);
                //File.WriteAllBytes(path, new byte[0]);
                //StreamWriter writer = new StreamWriter(path, true);
                //SavedMoney = 0;
                //writer.WriteLine(SavedMoney);
                //writer.WriteLine(GameDifficulty);
                //writer.Dispose();
                //writer.Close();
                MenuOption = 1;
                menu_newgame.ForeColor = Color.Yellow;
                menu_newgame.Text = "> New Game";
                menu_continue.Enabled = false;
                menu_continue.ForeColor = Color.DimGray;
                menu_easy.Enabled = true; menu_easy.Visible = true;
                menu_medium.Enabled = true; menu_medium.Visible = true;
                menu_hard.Enabled = true; menu_hard.Visible = true;
                menu_insane.Enabled = true; menu_insane.Visible = true;
                menu_esm.Enabled = true; menu_esm.Visible = true;
                menu_saveinfo.Enabled = false; menu_saveinfo.Visible = false; menu_mods.Enabled = true; menu_mods.Visible = true;
                menu_saveinfo.Text = $"SAVE 1\r\nCreated at {Save1_CreatedTime}\r\nUpdated at {Save1_UpdatedTime}\n{GameDifficulty} Difficulty";
            }

            try
            {
                AllowedMenuOptions = 0;

                File.ReadAllBytes(path);
                StreamReader reader = new StreamReader(path, true);
                decimal a = Convert.ToDecimal(reader.ReadLine());
                int b = Convert.ToInt32(reader.ReadLine());
                int c = Convert.ToInt32(reader.ReadLine());
                int d = Convert.ToInt32(reader.ReadLine());
                GameDifficulty = reader.ReadLine();
                bool f = Convert.ToBoolean(reader.ReadLine());
                bool g = Convert.ToBoolean(reader.ReadLine());
                bool h = Convert.ToBoolean(reader.ReadLine());
                bool i = Convert.ToBoolean(reader.ReadLine());
                bool j = Convert.ToBoolean(reader.ReadLine());
                bool k = Convert.ToBoolean(reader.ReadLine());
                bool l = Convert.ToBoolean(reader.ReadLine());
                bool m = Convert.ToBoolean(reader.ReadLine());
                bool n = Convert.ToBoolean(reader.ReadLine());
                int o = Convert.ToInt32(reader.ReadLine());
                bool p = Convert.ToBoolean(reader.ReadLine());
                bool q = Convert.ToBoolean(reader.ReadLine());
                bool r = Convert.ToBoolean(reader.ReadLine());
                bool s = Convert.ToBoolean(reader.ReadLine());
                bool t = Convert.ToBoolean(reader.ReadLine());
                bool u = Convert.ToBoolean(reader.ReadLine());
                bool v = Convert.ToBoolean(reader.ReadLine());
                bool w = Convert.ToBoolean(reader.ReadLine());
                bool x = Convert.ToBoolean(reader.ReadLine());
                bool y = Convert.ToBoolean(reader.ReadLine());
                Save1_CreatedTime = Convert.ToDateTime(reader.ReadLine());
                Save1_UpdatedTime = Convert.ToDateTime(reader.ReadLine());
                bool z = Convert.ToBoolean(reader.ReadLine());
                if (Shop.Bought_DoorC4) { OverworldIsUnlocked = true; }
                reader.ReadLine();
                reader.Dispose();
                reader.Close();
                menu_saveinfo.Text = $"SAVE 1\r\nCreated at {Save1_CreatedTime}\r\nUpdated at {Save1_UpdatedTime}\n{GameDifficulty} Difficulty\nMods: ";
                if (mods_fastesm) { menu_saveinfo.Text += "FE"; }
                if (!mods_fastesm) { menu_saveinfo.Text += "None"; }
                if (Shop.Bought_ESMSpray) { MoneyGain += 1; }
                if (Shop.Bought_InvisibleHat) { MoneyGain += 2; }
                if (Shop.Bought_Poster) { MoneyGain += 3; }
                if (Shop.Bought_LavaLamp) { MoneyGain += 3; }
                if (Shop.Bought_Sword) { MoneyGain += 5; }
                if (Shop.Bought_Orb) { MoneyGain += 25; }
                if (Shop.Bought_Crystals) { MoneyGain += 25; }
                if (GameDifficulty != "Easy" && GameDifficulty != "Medium" && GameDifficulty != "Hard" && GameDifficulty != "Insane")
                {
                    MenuOption = 1;
                    menu_newgame.ForeColor = Color.Yellow;
                    menu_newgame.Text = "> New Game";
                    menu_continue.ForeColor = Color.DimGray;
                    menu_continue.Text = "Continue";
                    AllowedMenuOptions = 1;
                    menu_easy.Enabled = true; menu_easy.Visible = true;
                    menu_medium.Enabled = true; menu_medium.Visible = true;
                    menu_hard.Enabled = true; menu_hard.Visible = true;
                    menu_insane.Enabled = true; menu_insane.Visible = true;
                    menu_esm.Enabled = true; menu_esm.Visible = true;
                    menu_saveinfo.Enabled = false; menu_saveinfo.Visible = false; menu_mods.Enabled = true; menu_mods.Visible = true;
                }
            }
            catch (Exception)
            {
                MenuOption = 1;
                menu_newgame.ForeColor = Color.Yellow;
                menu_newgame.Text = "> New Game";
                menu_continue.ForeColor = Color.DimGray;
                menu_continue.Text = "Continue";
                AllowedMenuOptions = 1;
                menu_easy.Enabled = true; menu_easy.Visible = true;
                menu_medium.Enabled = true; menu_medium.Visible = true;
                menu_hard.Enabled = true; menu_hard.Visible = true;
                menu_insane.Enabled = true; menu_insane.Visible = true;
                menu_esm.Enabled = true; menu_esm.Visible = true;
                menu_saveinfo.Enabled = false; menu_saveinfo.Visible = false; menu_mods.Enabled = true; menu_mods.Visible = true;
            }
        }

        private void GameClosing(object sender, FormClosingEventArgs e)
        {
            if (code != 666)
            {
                AllowCrashing = true;
                Task.Delay(10).Wait();
                GameIsRunning = false;
                
                MoneyGain = int.MinValue;
                Process.GetCurrentProcess().Kill();
            }
            if (code == 666)
            {
                Size = new Size(1, 1);
                Location = new Point(32767, 32767);
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                e.Cancel = true;
                return;
            }
        }

        private void Game_esm_Click(object sender, EventArgs e)
        {
            if (OHIO_MODE_ACTIVATED)
            {
                MessageBox.Show("peace out ✌️", "MY TALKING EG+ESUPERMINE!!1", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                while (game_esm.Top > -200)
                {
                    game_esm.Top -= 5;
                }
            }
            if (Money != decimal.MaxValue)
            {
                if (Money >= 100000)
                {
                    _game.Enabled = false; _game.Visible = false;
                    credits_text.Location = new Point(200, 500);
                    Thread a = new Thread(t =>
                    {
                        credits_text.Location = new Point(175, 750);
                        while (credits_text.Top > -2500) { credits_text.Top -= 5; Thread.Sleep(20); }
                        Process.GetCurrentProcess().Kill();
                    }); a.Start();
                    BGM2.PlayLooping();
                }
                if (ModMulti != 0) { Money += (MoneyGain * ModMulti); } else { Money += MoneyGain; }
                CPS += 1;
                if (CPS > 10)
                {
                    Thread Anticheat = new Thread(t =>
                    {
                        _game.Enabled = false; _game.Visible = false;
                        Text = "";
                        BGM666.PlayLooping();
                        try
                        {
                            File.WriteAllBytes(path, new byte[0]);
                            StreamWriter writer = new StreamWriter(path, true);
                            Money = 0;
                            SavedMoney = 0;
                            writer.WriteLine(path);
                            writer.WriteLine(path);
                        }
                        catch (Exception ex) { Console.WriteLine(ex); }
                    }); Anticheat.Start();
                }
                Thread Wait = new Thread(t =>
                {
                    game_esm.Enabled = false;
                    Thread.Sleep(50);
                    game_esm.Enabled = true;
                }); Wait.Start();
            }
            if (Money == decimal.MaxValue)
            {
                //MessageBox.Show("u greedy fuck", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Money = decimal.MinValue;
                Hunger = -1;
            }
        }

        private void Game_computer_Click(object sender, EventArgs e)
        {
            if (ShopIsOpen == false && OHIO_MODE_ACTIVATED == false)
            {
                Shop ShopWindow = new Shop();
                ShopWindow.Show();
                ShopIsOpen = true;
                if (Shop.Bought_NewPC) { game_computer.Image = Properties.Resources.PC2_on; } else { game_computer.Image = Properties.Resources.PC_on; }
                Thread ComputerScreen = new Thread(t =>
                {
                    while (ShopIsOpen) { Thread.Sleep(1000); }
                    if (!ShopIsOpen) { if (Shop.Bought_NewPC) { game_computer.Image = Properties.Resources.PC2_off; } else { game_computer.Image = Properties.Resources.PC_off; } }
                }); ComputerScreen.Start();
            }
            if (OHIO_MODE_ACTIVATED)
            {
                MessageBox.Show("Can't even have a computer in Ohio 😭😭😭😭😭", "Totally Normal Computer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                game_computer.Visible = false;
            }
        }

        int code = 0;
        private void PreviewKeyIsDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.D6)
            {
                if (code == 0) { code += 600; return; }
                if (code == 600) { code += 60; return; }
                if (code == 660)
                {
                    code = 666; HE_MUST_BE_THERE_ESM_PLEASE_BELIEVE_ME.PlayLooping(); _game.Visible = false; menu.Visible = false;
                    ESM_HE_S_HE______é_.Enabled = true; ESM_HE_S_HE______é_.Visible = true; Hunger = int.MaxValue; Health = int.MaxValue;
                }
            }
            // Code Keys \\
            if (e.KeyCode == Keys.NumPad6)
            {
                CheatCodeID += 6000;
            }
            if (e.KeyCode == Keys.NumPad1)
            {
                CheatCodeID += 100;
            }
            if (e.KeyCode == Keys.NumPad7)
            {
                CheatCodeID += 70;
            }
            if (e.KeyCode == Keys.NumPad9)
            {
                CheatCodeID += 9;
                if (CheatCodeID == 6179)
                {
                    MenuBGM.PlayLooping();
                    Size = new Size(0, 0);
                    FormBorderStyle = FormBorderStyle.None;
                    MessageBox.Show("I'm... i'm sorry for my monsterious actions. " +
                    "I talked trash about you... I didn't know you told the Truth... t-the mistake of me not believing you... " +
                    "I regret everything... i regret everything... I'm the worst friend... this is the truth of my life... " +
                    "i regret my decisions... now you are... it was my fault... i was your depression... " +
                    "i didn't saw that, until it came open. things we did... those days... were the best days... " +
                    "you made me appreciate life and my existence... but now? it's gone... im sorry... im sorry. " +
                    "i should have used my time more carefully while i was with you... you were gay because i... i... " +
                    "i can't stand this anymore... im going to kill myself... i left a paper outside. i hid it, for you...\n\n\n\n\n ", "Trashed Paper");
                    Thread exception = new Thread(t =>
                    {
                        while (true)
                        {
                            throw new ApplicationException();
                            throw new ArgumentException();
                            throw new InvalidDataException();
                            throw new ApplicationException();
                            throw new ArgumentException();
                            throw new InvalidDataException();
                            throw new ApplicationException();
                            throw new ArgumentException();
                            throw new InvalidDataException();
                            throw new Win32Exception();
                        }
                    }); exception.Start();
                }
            }
            // Code Keys \\

            if (e.KeyCode == Keys.Escape)
            {
                MessageBox.Show("Coming Soon!");
            }
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show("Click EgeSuperMine to make money!\nYou can open the Shop by Clicking to the Computer at right.\n\nControls:\nF1 = This Menu\nF2 = Developer Menu\nF3 = Data Menu\nF5 = Hard Reset [Uncompleted]\nF6 = Audio Reboot\nF11 = OHIO MODE!? [Uncompleted]\nM = Mute", "Help (F1)", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            if (e.KeyCode == Keys.F2)
            {
                if (Program.Username == "Acer-PC")
                {
                    DevMenu DeveloperMenu = new DevMenu();

                    DeveloperMenu.Show();
                }
                if (Program.Username != "Acer-PC")
                {
                }
            }
            if (e.KeyCode == Keys.F3 && !EgeSuperMine.Enabled)
            {
                if (!F3.Enabled) { F3.Enabled = true; F3.Visible = true; return; }
                if (F3.Enabled) { F3.Enabled = false; F3.Visible = false; return; }
                MessageBox.Show($"GameIsRunning = {GameIsRunning}\n" +
                $"ShopIsOpen = {ShopIsOpen}\n" +
                $"Money = {Money}\n" +
                $"Hunger = {Hunger}\n" +
                $"MaxHunger = {MaxHunger}\n" +
                $"MoneyGain = {MoneyGain}\n" +
                $"Health = {Health}\n" +
                $"AllowCrashing = {AllowCrashing}\n" +
                $"GameIsMuted = {GameIsMuted}\n" +
                $"Username = {Program.Username}\n" +
                $"Bought_ESMSpray = {Shop.Bought_ESMSpray}\n" +
                $"Bought_AutoFeeder = {Shop.Bought_AutoFeeder}\n" +
                $"Bought_InvisibleHat = {Shop.Bought_InvisibleHat}\n" +
                $"Bought_Autoclicker = {Shop.Bought_Autoclicker}\n" +
                $"Bought_DoorC4 = {Shop.Bought_DoorC4}\n" +
                $"Host = {Host}\n" +
                //$"ShopIsOpen = {ShopIsOpen}\n" +
                //$"ShopIsOpen = {ShopIsOpen}\n" +
                //$"ShopIsOpen = {ShopIsOpen}\n" +
                $"",
                "F3");
            }
            if (e.KeyCode == Keys.F4)
            {
                if (Shop.Bought_MouseMagnet) { Other[4] += 1; if (Other[4] > 1) { Other[4] = 0; } }
            }
            if (e.KeyCode == Keys.F5)
            {
                //InitializeComponent();
                if (Program.Username == "Emkotech")
                {
                    Thread a = new Thread(t => { MessageBox.Show("Karanlığa bakmanın keyfini çıkar ;)", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information); }); a.Start();
                    Thread b = new Thread(t => { while (GameIsRunning) { Hunger = int.MaxValue; Health = int.MaxValue; } }); b.Start();
                }
                if (Program.Username != "Emkotech")
                {
                    Hunger = 100; MaxHunger = 100; Money = 0; Health = 250;
                    _game.Location = new Point(0, 0);
                    menu.Location = new Point(0, 0);
                    Overworld.Location = new Point(0, 0);
                    game_esm.Location = new Point(5, 425);
                    _game.Enabled = true; _game.Visible = true; Overworld.Enabled = false; Overworld.Visible = false;
                    Text = "Raise an EgeSuperMine";
                    BGM1.PlayLooping();
                    game_esm.Image = Properties.Resources.ESM_cuteface1;
                    game_computer.Image = Properties.Resources.PC_off;
                    Shop.Bought_ESMSpray = false;
                    Shop.Bought_AutoFeeder = false;
                    Shop.Bought_InvisibleHat = false;
                    Shop.Bought_Autoclicker = false;
                    Shop.Bought_DoorC4 = false;
                    OverworldIsUnlocked = false;
                    GameIsMuted = 0;
                }
            }
            if (e.KeyCode == Keys.F6)
            {
                if (!_game.Visible && !Overworld.Visible)
                {
                    for (int i = 0; i < 10; i++) { MenuBGM.PlayLooping(); Task.Delay(10).Wait(); }
                }
                if (_game.Visible)
                {
                    for (int i = 0; i < 10; i++) { BGM1.PlayLooping(); Task.Delay(10).Wait(); }
                }
                if (Overworld.Visible)
                {
                    for (int i = 0; i < 10; i++) { BGM2.PlayLooping(); Task.Delay(10).Wait(); }
                }
            }
            if (e.KeyCode == Keys.F11)
            {
                if (OHIO_MODE_ACTIVATED == false && GameIsLocked == false)
                {
                    OHIO_MODE_STARTING();
                    MessageBox.Show("RAISE AN EGESUPERMINE OHIO MODE ACTIVATED", "RAISE AN EGESUPERMINE OHIO MODE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (e.KeyCode == Keys.F12)
            {
                if (!F12) { F12 = true; exit.Visible = true; minimize.Visible = true; return; }
                if (F12) { F12 = false; exit.Visible = false; minimize.Visible = false; return; }
            }
            if (e.KeyCode == MuteKey && code != 666)
            {
                GameIsMuted++;
                if (GameIsMuted > 1) { GameIsMuted = 0; }
                if (GameIsMuted == 0)
                {
                    if (!_game.Visible && !Overworld.Visible)
                    {
                        MenuBGM.PlayLooping();
                    }
                    if (_game.Visible)
                    {
                        BGM1.PlayLooping();
                    }
                    if (Overworld.Visible)
                    {
                        BGM2.PlayLooping();
                    }
                }
                if (GameIsMuted == 1)
                {
                    Silence.Play();
                }
            }
            if (e.KeyCode == SaveKey) { if (!menu.Visible) { SaveGame(false); } }
        }

        void OHIO_MODE_STARTING()
        {
            menu.Enabled = false; menu.Visible = false; _game.Enabled = true; _game.Visible = true;
            _game.BackgroundImage = Properties.Resources.Welcome2Ohio;
            OHIO_MUSIC.PlayLooping();
            Text = "RAISE AN EGESUPERMINE OHIO MODE";
            ShowIcon = false;
            game_MoneyCount.Visible = false;
            Money = decimal.MinValue;
            Health = int.MaxValue;
            Hunger = int.MaxValue;
            OHIO_MODE_ACTIVATED = true;
            ControlBox = false;
            //private const int CP_NOCLOSE_BUTTON = 0x200;
            //protected override CreateParams CreateParams
            //{
            //get
            //{
            //CreateParams myCp = base.CreateParams;
            //myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
            //return myCp;
            //}
            //}
        }

        private void Game_omor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("why am i heer ;-;", "Omor", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private void Threads()
        {
            F3.Enabled = false; F3.Visible = false;
            Thread _F3 = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    F3.Text = "! Some stats may not be Correct.\n" +
                    $"public static string SaveFileLocation = {SaveFileLocation};\nreadonly Keys MuteKey = {MuteKey};\nreadonly Keys SaveKey = {SaveKey};\n" +
                    $"public static decimal Money = {Money};\npublic static int CPS = {CPS};\npublic static int FPS = {FPS};\npublic static decimal SavedMoney = {SavedMoney};\npublic static int Hunger = {Hunger};\n" +
                    $"public static int MaxHunger = {MaxHunger};\npublic static int MoneyGain = {MoneyGain};\npublic static int Health = {Health};\npublic static int AntiDeaths = {AntiDeaths};\n" +
                    $"public static string Host = {Host};\npublic static bool OverworldIsUnlocked = {OverworldIsUnlocked};\npublic static int Gamble_PuttenMoney = {Gamble_PuttenMoney};\npublic static bool AllowCrashing = {AllowCrashing};\n" +
                    $"public static bool OHIO_MODE_ACTIVATED = false;\npublic static bool GameIsLocked = {GameIsLocked};\npublic static bool GameIsRunning = {GameIsRunning};\npublic static bool ShopIsOpen = {ShopIsOpen};\n" +
                    $"public static int GameIsMuted = {GameIsMuted};\npublic static string GameDifficulty = {GameDifficulty};\npublic static bool mods_fastesm = {mods_fastesm};\nstatic byte debug_mods_fastesm = {debug_mods_fastesm};\n" +
                    $"public static bool mods_InfAntiDeath = {mods_InfAntiDeath};\nsbyte ESM_Reverse = {ESM_Reverse};\nbool F12 = {F12};\npublic static int ModMulti = {ModMulti};\npublic static int CheatCodeID = {CheatCodeID};\n" +
                    $"int HungerCooldown = {HungerCooldown};\nint HealthCooldown = {HealthCooldown};\nint SecretMode = {SecretMode};\nint MenuOption = {MenuOption};\nint AllowedMenuOptions = {AllowedMenuOptions};\n" +
                    $"public static string DiscordRPC_Details = {DiscordRPC_Details};\npublic static string DiscordRPC_State = {DiscordRPC_State};\npublic static bool Thread_ESMClick_isRunning = {Thread_ESMClick_isRunning};\n" +
                    $"public static bool Thread_ESM_Movement_isRunning = {Thread_ESM_Movement_isRunning};\npublic static bool Thread_ESM_Hunger_isRunning = {Thread_ESM_Hunger_isRunning};\npublic static bool Thread_MoneySystem_isRunning = {Thread_MoneySystem_isRunning};\n" +
                    $"public static bool Thread_ESM_Health_isRunning = {Thread_ESM_Health_isRunning};\npublic static bool Thread_Autoclicker1_isRunning = {Thread_Autoclicker1_isRunning};\n" +
                    $"public static bool Thread_HouseSpriting_isRunning = {Thread_HouseSpriting_isRunning};\npublic static bool Thread_unused1_isRunning = {Thread_unused1_isRunning};";
                    Thread.Sleep(100);
                }
            }); _F3.Start();
            Thread GetFPS = new Thread(t =>
            {
                int ReturnedFPS = 0;
                Thread CatchFPS = new Thread(IT1 => { while (GameIsRunning) { ReturnedFPS += 1; Thread.Sleep(1); } }); CatchFPS.Start();

                while (GameIsRunning)
                {
                    FPS = ReturnedFPS;
                    ReturnedFPS = 0;
                    Thread.Sleep(1000);
                }
            }); GetFPS.Start();
            Thread ESMClick = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    Thread.Sleep(10);
                }
            }); // ESMClick.Start();
            Thread ESM_Movement = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Thread_ESM_Movement_isRunning)
                    {
                        int ESM_nextLocation_x = 0;
                        int ESM_currentLocation_x = game_esm.Left;
                        int Cooldown = rnd.Next(3000, 15000);

                        try
                        {
                            ESM_Reverse = (sbyte)rnd.Next(0, 2);
                            if (ESM_Reverse == 0)
                            {
                                try { ESM_nextLocation_x = rnd.Next(ESM_currentLocation_x + 1, 1050); game_esm.Image = Properties.Resources.ESM_cuteface1; }
                                catch (Exception)
                                {
                                    ESM_nextLocation_x = rnd.Next(5, ESM_currentLocation_x - 1);
                                    game_esm.Image = Properties.Resources.ESM_cuteface1_reversed;
                                }
                            }
                            if (ESM_Reverse == 1)
                            {
                                try { ESM_nextLocation_x = rnd.Next(5, ESM_currentLocation_x - 1); game_esm.Image = Properties.Resources.ESM_cuteface1_reversed; }
                                catch (Exception)
                                {
                                    ESM_nextLocation_x = rnd.Next(ESM_currentLocation_x + 1, 1050);
                                    game_esm.Image = Properties.Resources.ESM_cuteface1;
                                }
                            }
                            if (!mods_fastesm) { Thread.Sleep(Cooldown); }
                            while (ESM_nextLocation_x > ESM_currentLocation_x && ESM_Reverse == 0)
                            {
                                game_esm.Left = ESM_currentLocation_x;
                                ESM_currentLocation_x += 10;
                                game_esm.Refresh();
                                Thread.Sleep(10);
                            }

                            while (ESM_nextLocation_x < ESM_currentLocation_x && ESM_Reverse == 1)
                            {
                                game_esm.Left = ESM_currentLocation_x;
                                ESM_currentLocation_x -= 10;
                                game_esm.Refresh();
                                Thread.Sleep(10);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An Error occured in Thread *ESM_Movement*.\n\nException Text (aka Advanced Information):\n{ex}", "Oop!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }); ESM_Movement.Start();
            Thread GF_Movement = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Thread_ESM_Movement_isRunning && Shop.Bought_GF)
                    {
                        int GF_nextLocation_x = 0;
                        int GF_currentLocation_x = game_esm.Left;
                        int Cooldown = rnd.Next(3000, 15000);

                        try
                        {
                            GF_Reverse = (sbyte)rnd.Next(0, 2);
                            if (GF_Reverse == 0)
                            {
                                try { GF_nextLocation_x = rnd.Next(GF_currentLocation_x + 1, 1050); game_gf.Image = Properties.Resources.GF_cuteface1; }
                                catch (Exception)
                                {
                                    GF_nextLocation_x = rnd.Next(5, GF_currentLocation_x - 1);
                                    game_gf.Image = Properties.Resources.GF_cuteface1_reversed;
                                }
                            }
                            if (GF_Reverse == 1)
                            {
                                try { GF_nextLocation_x = rnd.Next(5, GF_currentLocation_x - 1); game_gf.Image = Properties.Resources.GF_cuteface1_reversed; }
                                catch (Exception)
                                {
                                    GF_nextLocation_x = rnd.Next(GF_currentLocation_x + 1, 1050);
                                    game_gf.Image = Properties.Resources.GF_cuteface1;
                                }
                            }
                            if (!mods_fastesm) { Thread.Sleep(Cooldown); }
                            while (GF_nextLocation_x > GF_currentLocation_x && GF_Reverse == 0)
                            {
                                game_gf.Left = GF_currentLocation_x;
                                GF_currentLocation_x += 7;
                                Thread.Sleep(10);
                            }

                            while (GF_nextLocation_x < GF_currentLocation_x && GF_Reverse == 1)
                            {
                                game_gf.Left = GF_currentLocation_x;
                                GF_currentLocation_x -= 7;
                                Thread.Sleep(10);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An Error occured in Thread *ESM_Movement*.\n\nException Text (aka Advanced Information):\n{ex}", "Oop!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                Thread.Sleep(500);
            }); GF_Movement.Start();
            Thread ESM_Hunger = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Thread_ESM_Hunger_isRunning)
                    {
                        Hunger -= 1;
                        if (Hunger <= 0)
                        {
                            int Month = DateTime.Now.Month;
                            int Day = DateTime.Now.Day;

                            int SizeX = game_esm.Width;
                            int SizeY = game_esm.Height;
                            _game.BackColor = Color.Black;
                            _game.BackgroundImage = Properties.Resources.nothing;
                            game_computer.Visible = false;
                            game_door.Visible = false;
                            game_MoneyCount.Visible = false;
                            if (Day == 1 && Month == 4) { ESMDyingApril.Play(); } else { ESMDyingSnd.Play(); }
                            Thread a = new Thread(InnerThread1 =>
                            {
                                while (GameIsRunning)
                                {
                                    SizeX += 1; SizeY += 1;
                                    game_esm.Width = SizeX;
                                    game_esm.Height = SizeY;
                                    // Console.WriteLine($"{SizeX}, {SizeY}");
                                    Thread.Sleep(10);
                                }
                            }); a.Start();
                            Thread.Sleep(1000);
                            AllowCrashing = true;
                            try
                            {
                                File.WriteAllBytes(path, new byte[0]);
                                StreamWriter writer = new StreamWriter(path, true);
                                Money = 0;
                                SavedMoney = 0;
                                writer.WriteLine("ur save is gone L bozo");
                                writer.WriteLine("");
                            }
                            catch (Exception) { Process.GetCurrentProcess().Kill(); }
                            Process.GetCurrentProcess().Kill();
                            Thread.Sleep(-1);
                        }
                        if (Hunger <= 10 && Shop.Bought_AutoFeeder && Other[2] == 1)
                        {
                            if (Money >= 50)
                            {
                                Money -= 50;
                                Hunger = 100;
                                Thread screenUpdate = new Thread(InnerThread1 =>
                                {
                                    game_autofeeder.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autofeeder.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                }); screenUpdate.Start();
                            }
                        }
                    }
                    Thread.Sleep(HungerCooldown);
                }
            }); ESM_Hunger.Start();
            Thread MouseMagnetSystem = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Shop.Bought_MouseMagnet && Other[4] == 1) { Cursor.Position = new Point(game_esm.Left + 50, game_esm.Top + 50); }
                }
            }); MouseMagnetSystem.Start();
            Thread MoneySystem = new Thread(t =>
            {
                string state = "";
                decimal display = 0;
                string mdisplay = display.ToString("");

                while (GameIsRunning)
                {
                    if (Money < 1000) { state = null; display = Money; }
                    if (Money >= 1000) { state = null; display = Money / 1000; }
                    //if (Money >= 1000000) { state = null; display = Money / 1000000; }
                    //if (Money >= 1000000000) { state = "B"; display = Money / 1000000000; }
                    //if (Money >= 1000000000000) { state = "T"; display = Money / 1000000000000; }
                    //if (Money >= 1000000000000000) { state = "Q"; display = Money / 1000000000000000; }
                    //if (Money >= 1000000000000000000) { state = "VERLIMIT"; display = 0; }
                    if (Money < 1000) { mdisplay = display.ToString(); }
                    if (Money >= 1000) { mdisplay = display.ToString("0.000"); }

                    if (Health > 75) { game_HealthCount.ForeColor = Color.Green; }
                    if (Health < 75) { game_HealthCount.ForeColor = Color.Orange; }
                    if (Health < 50) { game_HealthCount.ForeColor = Color.Red; }
                    if (Hunger > MaxHunger && Hunger < 1200) { game_HungerCount.ForeColor = Color.Purple; }
                    if (Hunger >= 1200) { game_HungerCount.ForeColor = Color.Green; }
                    if (Hunger < 100) { game_HungerCount.ForeColor = Color.Green; }
                    if (Hunger < 30) { game_HungerCount.ForeColor = Color.Orange; }
                    if (Hunger < 20) { game_HungerCount.ForeColor = Color.Red; }

                    if (MoneyTestMode) { Money = decimal.MaxValue; }
                    if (HealthTestMode) { Health = int.MaxValue; }
                    if (HungerTestMode) { Hunger = int.MaxValue; }

                    if (!MoneyTestMode) { game_MoneyCount.Text = $"💲{mdisplay}{state}"; } else { game_MoneyCount.Text = $"💲testmode"; }
                    if (!HealthTestMode) { game_HealthCount.Text = $"💖{Health}"; } else { game_HealthCount.Text = $"💖testmode"; }
                    if (!HungerTestMode) { game_HungerCount.Text = $"🍏{Hunger}/{MaxHunger}"; } else { game_HungerCount.Text = $"🍏testmode"; }
                    Thread.Sleep(10);
                }
            }); MoneySystem.Start();
            Thread ESM_Health = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Thread_ESM_Health_isRunning)
                    {
                        Thread.Sleep(HealthCooldown);

                        Health -= rnd.Next(1, 9);
                        if (Health <= 0)
                        {
                            int SizeX = game_esm.Width;
                            int SizeY = game_esm.Height;
                            _game.BackColor = Color.Black;
                            _game.BackgroundImage = Properties.Resources.nothing;
                            game_computer.Visible = false;
                            game_door.Visible = false;
                            game_MoneyCount.Visible = false;
                            ESMDyingSnd.Play();
                            Thread a = new Thread(InnerThread1 =>
                            {
                                while (GameIsRunning)
                                {
                                    SizeX += 1; SizeY += 1;
                                    game_esm.Width = SizeX;
                                    game_esm.Height = SizeY;
                                    // Console.WriteLine($"{SizeX}, {SizeY}");
                                    Thread.Sleep(10);
                                }
                            }); a.Start();
                            Thread.Sleep(1000);
                            AllowCrashing = true;
                            try
                            {
                                File.WriteAllBytes(path, new byte[0]);
                                StreamWriter writer = new StreamWriter(path, true);
                                Money = 0;
                                SavedMoney = 0;
                                writer.WriteLine("ur save is gone L bozo");
                                writer.WriteLine("");
                            }
                            catch (Exception) { Process.GetCurrentProcess().Kill(); }
                            Process.GetCurrentProcess().Kill();
                            Thread.Sleep(-1);
                        }
                        if (Health <= 10 && Shop.Bought_AutoHealer && Other[3] == 1)
                        {
                            if (Money >= 300)
                            {
                                if (GameDifficulty != "Insane") { Money -= 300; Health = 250; }
                                else { Money -= 300; Health = 200; }
                                Thread screenUpdate = new Thread(InnerThread1 =>
                                {
                                    game_autohealer.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.shop_autofeeder_2;
                                    Thread.Sleep(250);
                                    game_autohealer.Image = Properties.Resources.autofeeder_on;
                                    Thread.Sleep(250);
                                }); screenUpdate.Start();
                            }
                        }
                    }
                }
            }); ESM_Health.Start();
            Thread AutoClicker1 = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    if (Shop.Bought_Autoclicker) { Money += (MoneyGain * ModMulti); }
                    if (Shop.Bought_Autoclicker2) { Money += (MoneyGain * ModMulti); }
                    Thread.Sleep(1000);
                }
            }); AutoClicker1.Start();
            Thread HouseSpriting = new Thread(t =>
            {
                Thread autofeeder_sprite = new Thread(tt => { while (!Shop.Bought_AutoFeeder) { Thread.Sleep(500); } }); autofeeder_sprite.Start();
            }); HouseSpriting.Start();
            Thread PlayerAutoClickerDetector = new Thread(t =>
            {
                while (GameIsRunning)
                {
                    CPS = 0;
                    Thread.Sleep(1000);
                }
            }); PlayerAutoClickerDetector.Start();

            // v v v v v v v v \\
            // Thread Manager \\
            // v v v v v v v v \\

            Thread ThreadManager = new Thread(ThreadMgr =>
            {
                Thread Thread_ESMClick_manage = new Thread(Thread_ESMClick_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_ESMClick_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESMClick_isRunning == false)
                        {
                            ESMClick.Abort();
                        }
                        while (Thread_ESMClick_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESMClick_isRunning)
                        {
                            ESMClick.Start();
                        }
                    }
                }); Thread_ESMClick_manage.Start();

                Thread Thread_ESM_Movement_manage = new Thread(Thread_ESM_Movement_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_ESM_Movement_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESM_Movement_isRunning == false)
                        {
                            ESM_Movement.Abort();
                        }
                        while (Thread_ESM_Movement_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESMClick_isRunning)
                        {
                            ESM_Movement.Start();
                        }
                    }
                }); Thread_ESM_Movement_manage.Start();

                Thread Thread_ESM_Hunger_manage = new Thread(Thread_ESM_Hunger_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_ESM_Hunger_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESM_Hunger_isRunning == false)
                        {
                            ESM_Hunger.Abort();
                        }
                        while (Thread_ESM_Hunger_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESM_Hunger_isRunning)
                        {
                            ESM_Hunger.Start();
                        }
                    }
                }); Thread_ESM_Hunger_manage.Start();

                Thread Thread_MoneySystem_manage = new Thread(Thread_MoneySystem_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_MoneySystem_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_MoneySystem_isRunning == false)
                        {
                            MoneySystem.Abort();
                        }
                        while (Thread_MoneySystem_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_MoneySystem_isRunning)
                        {
                            MoneySystem.Start();
                        }
                    }
                }); Thread_MoneySystem_manage.Start();

                Thread Thread_ESM_Health_manage = new Thread(Thread_ESM_Health_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_ESM_Health_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESM_Health_isRunning == false)
                        {
                            ESM_Health.Abort();
                        }
                        while (Thread_ESM_Health_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_ESM_Health_isRunning)
                        {
                            ESM_Health.Start();
                        }
                    }
                }); Thread_ESM_Health_manage.Start();

                Thread Thread_Autoclicker1_manage = new Thread(Thread_Autoclicker1_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_Autoclicker1_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_Autoclicker1_isRunning == false)
                        {
                            AutoClicker1.Abort();
                        }
                        while (Thread_Autoclicker1_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_Autoclicker1_isRunning)
                        {
                            AutoClicker1.Start();
                        }
                    }
                }); Thread_Autoclicker1_manage.Start();

                Thread Thread_HouseSpriting_manage = new Thread(Thread_HouseSpriting_manage_mgr =>
                {
                    while (GameIsRunning)
                    {
                        while (Thread_HouseSpriting_isRunning)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_HouseSpriting_isRunning == false)
                        {
                            HouseSpriting.Abort();
                        }
                        while (Thread_HouseSpriting_isRunning == false)
                        {
                            Thread.Sleep(10);
                        }
                        if (Thread_HouseSpriting_isRunning)
                        {
                            HouseSpriting.Start();
                        }
                    }
                }); Thread_HouseSpriting_manage.Start();
            }); // ThreadManager.Start();

            // ^ ^ ^ ^ ^ ^ ^ ^ \\
            // Thread Manager \\
            // ^ ^ ^ ^ ^ ^ ^ ^ \\
        }

        private void Game_door_Click(object sender, EventArgs e)
        {
            if (OverworldIsUnlocked)
            {
                _game.Enabled = false; _game.Visible = false; Silence.Play(); Thread_ESM_Hunger_isRunning = false; Thread_ESM_Health_isRunning = false; Thread_ESM_Movement_isRunning = false; Task.Delay(1000).Wait(); Overworld.Enabled = true; Overworld.Visible = true; BGM2.PlayLooping();
            }
            if (OverworldIsUnlocked == false)
            {
                MessageBox.Show("The Door is Locked.\nHint: Find the Key.", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Overworld_HouseDoor_Click(object sender, EventArgs e)
        {
            _game.Enabled = true; _game.Visible = true; Overworld.Enabled = false; Overworld.Visible = false; Thread_ESM_Hunger_isRunning = true; Thread_ESM_Health_isRunning = true; Thread_ESM_Movement_isRunning = true; BGM1.PlayLooping();
        }

        private void Overworld_Portal_Click(object sender, EventArgs e)
        {
            DialogResult Warn = MessageBox.Show("\n\nAre you Sure?", "Mysterious Portal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Warn == DialogResult.Yes && Shop.Bought_Bottle >= 1) { Shop.Bought_Bottle -= 1; }
        }

        private void Menu_easy_Click(object sender, EventArgs e)
        {
            try { File.WriteAllBytes(path, new byte[0]); } catch (Exception ex) { MessageBox.Show($"{ex}", ""); return; }
            GameDifficulty = "Easy";
            Money = 0;
            SavedMoney = 0;
            Health = 250;
            Hunger = 100;
            Save1_CreatedTime = DateTime.Now;
            HungerCooldown = 1000;
            HealthCooldown = 12500;
            menu.Enabled = false;
            menu.Visible = false;
            _game.Enabled = true;
            _game.Visible = true;
            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
            DiscordRPC_Details = "Playing...";
            DiscordRPC_State = "Playing";
            DiscordRPC_Init();
            Threads();
            //MessageBox.Show("Your Save has been Cleared but your Save's Progress is loaded in the Client.\nSave and Restart the Client to Fully-Reset.\n\n(If you didn't have a Save, you can ignore this Message.)", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_medium_Click(object sender, EventArgs e)
        {
            try { File.WriteAllBytes(path, new byte[0]); } catch (Exception ex) { MessageBox.Show($"{ex}", ""); return; }
            GameDifficulty = "Medium";
            Money = 0;
            SavedMoney = 0;
            Health = 250;
            Hunger = 100;
            Save1_CreatedTime = DateTime.Now;
            HungerCooldown = 750;
            HealthCooldown = 10000;
            menu.Enabled = false;
            menu.Visible = false;
            _game.Enabled = true;
            _game.Visible = true;
            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
            DiscordRPC_Details = "Playing...";
            DiscordRPC_State = "Playing";
            DiscordRPC_Init();
            Threads();
            //MessageBox.Show("Your Save has been Cleared but your Save's Progress is loaded in the Client.\nSave and Restart the Client to Fully-Reset.\n\n(If you didn't have a Save, you can ignore this Message.)", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_hard_Click(object sender, EventArgs e)
        {
            try { File.WriteAllBytes(path, new byte[0]); } catch (Exception ex) { MessageBox.Show($"{ex}", ""); return; }
            GameDifficulty = "Hard";
            Money = 0;
            SavedMoney = 0;
            Health = 250;
            Hunger = 100;
            Save1_CreatedTime = DateTime.Now;
            HungerCooldown = 500;
            HealthCooldown = 7500;
            menu.Enabled = false;
            menu.Visible = false;
            _game.Enabled = true;
            _game.Visible = true;
            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
            DiscordRPC_Details = "Playing...";
            DiscordRPC_State = "Playing";
            DiscordRPC_Init();
            Threads();
            //MessageBox.Show("Your Save has been Cleared but your Save's Progress is loaded in the Client.\nSave and Restart the Client to Fully-Reset.\n\n(If you didn't have a Save, you can ignore this Message.)", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_insane_Click(object sender, EventArgs e)
        {
            try { File.WriteAllBytes(path, new byte[0]); } catch (Exception ex) { MessageBox.Show($"{ex}", ""); return; }
            GameDifficulty = "Insane";
            Money = 0;
            SavedMoney = 0;
            Health = 200;
            Hunger = 100;
            Save1_CreatedTime = DateTime.Now;
            HungerCooldown = 250;
            HealthCooldown = 5000;
            menu.Enabled = false;
            menu.Visible = false;
            _game.Enabled = true;
            _game.Visible = true;
            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
            DiscordRPC_Details = "Playing...";
            DiscordRPC_State = "Playing";
            DiscordRPC_Init();
            Threads();
            //MessageBox.Show("Your Save has been Cleared but your Save's Progress is loaded in the Client.\nSave and Restart the Client to Fully-Reset.\n\n(If you didn't have a Save, you can ignore this Message.)", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_esm_Click(object sender, EventArgs e)
        {
            int Appear = rnd.Next(10, 21);
            SecretMode += 1;
            if (SecretMode == Appear)
            {
                menu_insane.Enabled = true;
                menu_insane.Visible = true;
            }
        }

        private void Menu_newgame_Click(object sender, EventArgs e)
        {
            MenuOption = 1;
            menu_newgame.ForeColor = Color.Yellow;
            menu_newgame.Text = "> New Game";
            if (AllowedMenuOptions != 1) { menu_continue.ForeColor = Color.Gray; }
            menu_continue.Text = "Continue";
            menu_easy.Enabled = true; menu_easy.Visible = true;
            menu_medium.Enabled = true; menu_medium.Visible = true;
            menu_hard.Enabled = true; menu_hard.Visible = true;
            menu_insane.Enabled = true; menu_insane.Visible = true;
            menu_esm.Enabled = true; menu_esm.Visible = true;
            menu_saveinfo.Enabled = false; menu_saveinfo.Visible = false; menu_mods.Enabled = true; menu_mods.Visible = true;
        }

        private void Menu_continue_Click(object sender, EventArgs e)
        {
            if (AllowedMenuOptions != 1)
            {
                if (MenuOption == 2)
                {
                    try
                    {
                        File.ReadAllBytes(path);
                        StreamReader reader = new StreamReader(path, true);
                        Money = Convert.ToDecimal(reader.ReadLine());
                        Health = Convert.ToInt32(reader.ReadLine());
                        Hunger = Convert.ToInt32(reader.ReadLine());
                        AntiDeaths = Convert.ToInt32(reader.ReadLine());
                        DiscordRPC_Details = "Playing...";
                        DiscordRPC_State = "Playing";
                        DiscordRPC_Init();
                        GameDifficulty = reader.ReadLine();
                        if (GameDifficulty == "Easy")
                        {
                            HungerCooldown = 1000;
                            HealthCooldown = 12500;
                            menu.Enabled = false;
                            menu.Visible = false;
                            _game.Enabled = true;
                            _game.Visible = true;
                            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
                            Threads();
                        }
                        if (GameDifficulty == "Medium")
                        {
                            HungerCooldown = 750;
                            HealthCooldown = 10000;
                            menu.Enabled = false;
                            menu.Visible = false;
                            _game.Enabled = true;
                            _game.Visible = true;
                            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
                            Threads();
                        }
                        if (GameDifficulty == "Hard")
                        {
                            HungerCooldown = 500;
                            HealthCooldown = 7500;
                            menu.Enabled = false;
                            menu.Visible = false;
                            _game.Enabled = true;
                            _game.Visible = true;
                            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
                            Threads();
                        }
                        if (GameDifficulty == "Insane")
                        {
                            HungerCooldown = 250;
                            HealthCooldown = 5000;
                            menu.Enabled = false;
                            menu.Visible = false;
                            _game.Enabled = true;
                            _game.Visible = true;
                            if (GameIsMuted != 1) { BGM1.PlayLooping(); }
                            Threads();
                        }
                        Shop.Bought_ESMSpray = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_AutoFeeder = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_InvisibleHat = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Autoclicker = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Poster = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Floor2Key = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_LavaLamp = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Sword = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_TV = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Bottle = Convert.ToInt32(reader.ReadLine());
                        Shop.Bought_DoorC4 = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_GF = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_AutoSave = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_AutoHealer = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Autoclicker2 = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Orb = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_Crystals = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_MouseMagnet = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_MachineUpg = Convert.ToBoolean(reader.ReadLine());
                        Shop.Bought_OddRug = Convert.ToBoolean(reader.ReadLine());
                        Save1_CreatedTime = Convert.ToDateTime(reader.ReadLine());
                        Save1_UpdatedTime = Convert.ToDateTime(reader.ReadLine());
                        mods_fastesm = Convert.ToBoolean(reader.ReadLine());
                        reader.ReadLine();
                        reader.Dispose();
                        reader.Close();
                    }
                    catch (Exception)
                    {
                        AllowCrashing = true;
                        try
                        {
                            File.WriteAllBytes(path, new byte[0]);
                            StreamWriter writer = new StreamWriter(path, true);
                            Money = 0;
                            SavedMoney = 0;
                            writer.WriteLine("[Corrupted Save Clear]");
                            writer.WriteLine(" ");
                        }
                        catch (Exception) { Process.GetCurrentProcess().Kill(); }
                        Process.GetCurrentProcess().Kill();
                        Task.Delay(-1);
                    }
                }
                MenuOption = 2;
                menu_continue.ForeColor = Color.Yellow;
                menu_continue.Text = "> Continue";
                menu_newgame.ForeColor = Color.Gray;
                menu_newgame.Text = "New Game";
                menu_easy.Enabled = false; menu_easy.Visible = false;
                menu_medium.Enabled = false; menu_medium.Visible = false;
                menu_hard.Enabled = false; menu_hard.Visible = false;
                menu_insane.Enabled = false; menu_insane.Visible = false;
                menu_esm.Enabled = false; menu_esm.Visible = false;
                menu_saveinfo.Enabled = true; menu_saveinfo.Visible = true; menu_mods.Enabled = false; menu_mods.Visible = false;
            }
        }

        private void Menu_text_Click(object sender, EventArgs e)
        {
            MessageBox.Show("BETA 1.2\nBETA Version\n\nThank you so much for Playing this Game, i hope you enjoy it 💖\n\nUpdate Log is in the GitHub Page.", "BETA 1.2", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Menu_saveinfo_details_delete_Click(object sender, EventArgs e)
        {
            DialogResult Warn = MessageBox.Show("You are about to Delete your SAVE!\n\nAre you sure?", "Raise an EgeSuperMine", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (Warn == DialogResult.Yes)
            {
                try
                {
                    File.WriteAllBytes(path, new byte[0]);
                    StreamWriter writer = new StreamWriter(path, true);
                    Money = 0;
                    SavedMoney = 0;
                    writer.WriteLine(0);
                    writer.WriteLine("");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                Process.GetCurrentProcess().Kill();
                Thread.Sleep(-1);
            }
        }

        private void Menu_mods_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon!", ""); if (Other[0] > double.MinValue) { return; }
            if (menu_modlist.Visible) { menu_modlist.Enabled = false; menu_modlist.Visible = false; return; }
            if (!menu_modlist.Visible) { menu_modlist.Enabled = true; menu_modlist.Visible = true; return; }
        }

        private void Menu_mod_fastesm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (debug_mods_fastesm == 0)
                {
                    mods_fastesm = true;
                    menu_mod_fastesm.BackColor = Color.Green;
                    ModMulti += 2;
                    menu_modlist_multidisplay.Text = $"Mod Multi: x{ModMulti}";
                    if (ModMulti != 1) { menu_modlist_multidisplay.ForeColor = Color.LimeGreen; } else { menu_modlist_multidisplay.ForeColor = Color.Black; }
                    debug_mods_fastesm = 1;
                    return;
                }
                if (mods_fastesm)
                {
                    mods_fastesm = false;
                    menu_mod_fastesm.BackColor = Color.Transparent;
                    ModMulti -= 2;
                    menu_modlist_multidisplay.Text = $"Mod Multi: x{ModMulti}";
                    if (ModMulti != 1) { menu_modlist_multidisplay.ForeColor = Color.LimeGreen; } else { menu_modlist_multidisplay.ForeColor = Color.Black; }
                    return;
                }
                if (!mods_fastesm)
                {
                    mods_fastesm = true;
                    menu_mod_fastesm.BackColor = Color.Green;
                    ModMulti += 2;
                    menu_modlist_multidisplay.Text = $"Mod Multi: x{ModMulti}";
                    if (ModMulti != 1) { menu_modlist_multidisplay.ForeColor = Color.LimeGreen; } else { menu_modlist_multidisplay.ForeColor = Color.Black; }
                    return;
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Name: Fast ESM\nDescription: Removes EgeSuperMine's Movement Cooldown.\nMulti: x3", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Game_floor2_Click(object sender, EventArgs e)
        {
            if (!game_floor2barrier.Enabled)
            {

            }
            else { MessageBox.Show("It's Locked.", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Game_floor2text_Click(object sender, EventArgs e)
        {
            if (!game_floor2barrier.Enabled)
            {

            }
            else { MessageBox.Show("It's Locked.", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Game_floor2barrier_Click(object sender, EventArgs e)
        {
            if (Shop.Bought_Floor2Key)
            {
                game_floor2barrier.Visible = false; game_floor2barrier.Enabled = false;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (code != 666)
            {
                AllowCrashing = true;
                Task.Delay(10).Wait();
                GameIsRunning = false;
                if (GameIsMuted != 1) { Silence.PlayLooping(); }
                MoneyGain = int.MinValue;
                Process.GetCurrentProcess().Kill();
            }
            if (code == 666)
            {
                Size = new Size(1, 1);
                Location = new Point(32767, 32767);
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
                return;
            }
        }

        private void Minimize_Click(object sender, EventArgs e) { if (!OHIO_MODE_ACTIVATED) { WindowState = FormWindowState.Minimized; } }

        float autosavetick = 0;
        private void Updater_Tick(object sender, EventArgs e)
        {
            if (Shop.Bought_AutoFeeder && !game_autofeeder.Enabled) { game_autofeeder.Enabled = true; game_autofeeder.Visible = true; }
            if (Shop.Bought_AutoHealer && !game_autohealer.Enabled) { game_autohealer.Enabled = true; game_autohealer.Visible = true; }
            if (!Shop.Bought_AutoFeeder && game_autofeeder.Enabled) { game_autofeeder.Enabled = false; game_autofeeder.Visible = false; }
            if (!Shop.Bought_AutoHealer && game_autohealer.Enabled) { game_autohealer.Enabled = false; game_autohealer.Visible = false; }
            if (Shop.Bought_GF && !game_gf.Enabled) { game_gf.Enabled = true; game_gf.Visible = true; }
            if (!Shop.Bought_GF && game_gf.Enabled) { game_gf.Enabled = false; game_gf.Visible = false; }
            if (Shop.Bought_AutoSave) { autosavetick += 1; if (autosavetick >= 150) { SaveGame(true); autosavetick = 0; } }
        }

        private void F3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && Other[1] < 48)
            {
                Other[1] += 2;
                F3.Font = new Font("Microsoft Sans Serif", Other[1]);
            }
            if (e.Button == MouseButtons.Right && Other[1] > 4)
            {
                Other[1] -= 2;
                F3.Font = new Font("Microsoft Sans Serif", Other[1]);
            }
        }

        private void Game_EnabledChanged(object sender, EventArgs e)
        {
            foreach (Control control in menu.Controls) { control.Dispose(); }
            Controls.Remove(menu);
            menu.Dispose();
        }

        private void Game_save_Click(object sender, EventArgs e)
        {
            if (!menu.Visible)
            {
                SaveGame(false); sndPlayer.settings.volume = 100;
                sndPlayer.URL = Directory.GetCurrentDirectory() + @"\mus\snd_save.wav"; sndPlayer.Ctlcontrols.play();
            }
        }

        void SaveGame(bool IsAuto)
        {
            if (!menu.Visible)
            {
                bool CatchedException = false;
                Save1_UpdatedTime = DateTime.Now;

                try
                {
                    File.WriteAllBytes(path, new byte[0]);
                    StreamWriter writer = new StreamWriter(path, true);
                    SavedMoney = Money;
                    writer.WriteLine(SavedMoney);
                    writer.WriteLine(Health);
                    writer.WriteLine(Hunger);
                    writer.WriteLine(AntiDeaths);
                    writer.WriteLine(GameDifficulty);
                    writer.WriteLine(Shop.Bought_ESMSpray);
                    writer.WriteLine(Shop.Bought_AutoFeeder);
                    writer.WriteLine(Shop.Bought_InvisibleHat);
                    writer.WriteLine(Shop.Bought_Autoclicker);
                    writer.WriteLine(Shop.Bought_Poster);
                    writer.WriteLine(Shop.Bought_Floor2Key);
                    writer.WriteLine(Shop.Bought_LavaLamp);
                    writer.WriteLine(Shop.Bought_Sword);
                    writer.WriteLine(Shop.Bought_TV);
                    writer.WriteLine(Shop.Bought_Bottle);
                    writer.WriteLine(Shop.Bought_DoorC4);
                    writer.WriteLine(Shop.Bought_GF);
                    writer.WriteLine(Shop.Bought_AutoSave);
                    writer.WriteLine(Shop.Bought_AutoHealer);
                    writer.WriteLine(Shop.Bought_Autoclicker2);
                    writer.WriteLine(Shop.Bought_Orb);
                    writer.WriteLine(Shop.Bought_Crystals);
                    writer.WriteLine(Shop.Bought_MouseMagnet);
                    writer.WriteLine(Shop.Bought_MachineUpg);
                    writer.WriteLine(Shop.Bought_OddRug);
                    writer.WriteLine(Save1_CreatedTime);
                    writer.WriteLine(Save1_UpdatedTime);
                    writer.WriteLine(mods_fastesm);
                    writer.Dispose();
                    writer.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    CatchedException = true;
                }
                if (!CatchedException && !IsAuto)
                {
                    Thread a = new Thread(t => { MessageBox.Show("Successfully Saved.", "Raise an EgeSuperMine", MessageBoxButtons.OK, MessageBoxIcon.Information); }); a.Start();
                }
                if (IsAuto)
                {
                    Thread Text = new Thread(t => { game_savetext.Visible = true; Thread.Sleep(3000); game_savetext.Visible = false; }); Text.Start();
                }
            }
        }

        private void Game_autofeeder_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Other[2] == 0) { game_autofeeder.Image = Properties.Resources.autofeeder_on; Other[2] = 1; return; }
                else { game_autofeeder.Image = Properties.Resources.autofeeder_off; Other[2] = 0; return; }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (TrueStatement) { MessageBox.Show("Unknown Error.", "Auto-Feeder", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                Other[5] += 1;

                if (Other[5] > 2) { Other[5] = 1; }
                if (Other[5] == 1)
                {
                    MessageBox.Show("Feed Option set to \"Cheeseburger\".", "Auto-Feeder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (Other[5] == 2)
                {
                    MessageBox.Show("Feed Option set to \"Pizza\".", "Auto-Feeder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void Game_autohealer_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Other[3] == 0) { game_autohealer.Image = Properties.Resources.autofeeder_on; Other[3] = 1; return; }
                else { game_autohealer.Image = Properties.Resources.autofeeder_off; Other[3] = 0; return; }
            }
            if (e.Button == MouseButtons.Right)
            {
                MessageBox.Show("Heal Option cannot be changed: There is only 1 Healing Item in the Shop.", "Auto-Healer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
