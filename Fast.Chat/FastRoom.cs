using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geocoding.Google;

namespace Fast.Chat
{
    class FastRoom
    {
        public GoogleAddress address { get; set; }

        public FastRoom(GoogleAddress a)
        {
            this.address = a;
        }
    }
}
