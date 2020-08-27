using System;
using System.Collections.Generic;
using System.Text;
using AgentsProject.Agents;
using AgentsProject.Interfaces;

namespace MAS.MasDB
{
    public class ManageAgents
    {
        public List<IAgent> AllAgents { get; private set; }

        public ManageAgents()
        {
            AllAgents = new List<IAgent>();
            AddAllAgents();
        }

        private void AddAllAgents()
        {
            AllAgents.Add(new MamasAgent());
            AllAgents.Add(new BamasAgent());
            AllAgents.Add(new LaLaLandAgent());
        }
    }
}
