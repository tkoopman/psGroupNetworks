using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace psGroupNetworks
{
    public class Subnet : IComparable<Subnet>
    {
        public Subnet(String input)
        {
            Input = input;
            Network = IPNetwork.Parse(input);
        }

        public string Input { get; private set; }
        public IPNetwork Network { get; private set; }

        public int CompareTo(Subnet other)
        {
            return Network.CompareTo(other.Network);
        }
    }
}