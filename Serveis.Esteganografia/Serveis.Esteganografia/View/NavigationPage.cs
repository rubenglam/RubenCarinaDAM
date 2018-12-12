using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Serveis.Esteganografia.View
{
    public class NavigationPage
    {

        Pila.Pila<Window> _pages;

        public NavigationPage(Window page)
        {

            _pages = new Pila.Pila<Window>();
            _pages.Empila(page);

        }

        public void Push(Window page)
        {

            _pages.Cim.Hide();
            _pages.Empila(page);
            _pages.Cim.Show();

        }

        public void Pop(Window page)
        {

            _pages.PTop().Close();
            _pages.Cim.Show();

        }

        public void PopAll()
        {

            _pages.Desempila();
            while (!_pages.IsBuida)
            {

                _pages.PTop().Close();

            }

        }

    }
}
