using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheatricalPlayersRefactoringKata;

namespace Application.ModelViews
{
    public class TheaterPlayModelView
    {
        private string _playId { get; set; }
        private PlayModelView _play { get; set; }
        public string Name { get => _playId; set => _playId = value; }
        public PlayModelView Play { get => _play; set => _play = value; }

        public TheaterPlayModelView(string playId, PlayModelView play)
        {
            this._playId = playId;
            this._play = play;
        }
    }
}
