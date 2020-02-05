using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        const int nrOfPlayers = 12;

        readonly Player[] players;
        public Form1()
        {
            InitializeComponent();

            players = new Player[nrOfPlayers];

            LibVLCSharp.Shared.Core.Initialize();

            var libVlc = new LibVLCSharp.Shared.LibVLC(new[] {
                "--verbose=2",
                "--no-audio",
            });

            libVlc.SetLogFile("vlclog.txt");
            for (int i = 0; i < nrOfPlayers; i++)
            {
                players[i] = new Player(((float)i) / nrOfPlayers, libVlc);
            }
            flowLayoutPanel1.Controls.AddRange(players.Select(x => x.Control).ToArray());
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            while (true)
            {
                var playTasks = players.Select(p => Task.Run(() =>
                {
                    p.Play(@"..\..\00020309.MPG");
                }));
                await Task.WhenAll(playTasks);
            }
        }
    }
}
