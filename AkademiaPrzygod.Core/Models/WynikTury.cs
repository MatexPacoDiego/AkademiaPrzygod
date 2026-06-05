using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkademiaPrzygod.Core.Models
{
    /// <summary>
    /// Przechowuje wynik jednej tury walki.
    /// </summary>
    public class WynikTury
    {
        private string _opis;
        private bool _walkaTrwa;
        private bool _bohaterWygral;
        private int _zdobyteXP;
        private int _zdobyteZloto;

        public string Opis
        {
            get { return _opis; }
            set { _opis = value; }
        }

        public bool WalkaTrwa
        {
            get { return _walkaTrwa; }
            set { _walkaTrwa = value; }
        }

        public bool BohaterWygral
        {
            get { return _bohaterWygral;}
            set { _bohaterWygral = value; }
        }

        public int ZdobyteXP
        {
            get { return _zdobyteXP; }
            set { _zdobyteXP = value; }
        }

        public int ZdobyteZloto
        {
            get { return _zdobyteZloto; }
            set { _zdobyteZloto = value;}
        }
    }
}
