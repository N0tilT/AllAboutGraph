using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllAboutGraph.MVC.Controller
{
    public class Controller
    {
        private View _view;


        public Controller(View view)
        {
            _view = view;
            view.SetController(this);
        }
    }
}
