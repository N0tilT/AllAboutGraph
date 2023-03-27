using AllAboutGraph.MVC.Controller;
using AllAboutGraph.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllAboutGraph
{
    public static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MainView view = new MainView
            {
                Visible = false
            };
            Controller controller = new Controller(view);
            view.ShowDialog();

        }

    }
}
