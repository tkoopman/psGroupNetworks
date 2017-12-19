using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace psGroupNetworks
{
    [Cmdlet(VerbsData.Group, "Networks")]
    public class GroupNetworks : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string[] Input { get; set; }

        private List<Subnet> Subnets { get; set; } = new List<Subnet>();

        protected override void ProcessRecord()
        {
            foreach (string i in Input)
            {
                Subnets.Add(new Subnet(i));
            }
        }

        protected override void EndProcessing()
        {
            Subnets.Sort();
            Subnet last = null;
            foreach (Subnet s in Subnets)
            {
                if (last == null)
                {
                    WriteObject(s.Input);
                    last = s;
                }
                else
                {
                    if (last.Network == s.Network)
                    {
                        WriteVerbose($"{last.Input} equals {s.Input}. Removing duplicate from list.");
                    }
                    else if (IPNetwork.Overlap(last.Network, s.Network))
                    {
                        WriteVerbose($"{last.Input} overlaps {s.Input}. Removing {s.Input} from list.");
                    }
                    else
                    {
                        WriteObject(s.Input);
                        last = s;
                    }
                }
            }
        }
    }
}