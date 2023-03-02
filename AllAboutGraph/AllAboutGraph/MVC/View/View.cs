using AllAboutGraph.MVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllAboutGraph
{
    public partial class View : Form
    {
        private Controller _controller;

        public View()
        {
            InitializeComponent();
        }

        internal void SetController(Controller controller)
        {
            _controller = controller;
        }
    }
}
