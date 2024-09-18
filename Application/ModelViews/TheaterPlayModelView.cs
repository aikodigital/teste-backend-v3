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

        public string Name { get; set; }
        public IList<PlayModelView> Players { get; set; }

        public TheaterPlayModelView()
        {
                
        }


    }
}
