using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public sealed class Player : IDisposable
    {
        private readonly LibVLC libVLC;
        private readonly VideoView videoView;
        private readonly MediaPlayer mediaPlayer;
        private readonly float start;

        public Player(float start, LibVLC libVLC)
        {
            this.libVLC = libVLC;

            videoView = new VideoView();
            videoView.Size = new Size(400, 300);
            mediaPlayer = new MediaPlayer(libVLC);
            videoView.MediaPlayer = mediaPlayer;

            this.start = start;
        }

        public Control Control => videoView;


        public void Play(string file)
        {
            using (var media = new Media(libVLC, file, FromType.FromPath))
            {
                mediaPlayer.Media = media;
            }
            if (mediaPlayer.Play())
            {
                mediaPlayer.Position = start;
            }
        }

        public void Dispose()
        {
            mediaPlayer.Dispose();
            videoView.Dispose();
        }
    }
}
