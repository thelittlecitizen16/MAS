using System;
using System.Collections.Generic;
using System.Text;
using AgentsProject.Agents;
using AgentsProject.Interfaces;

namespace MAS.NewFolder
{
    public class ManageAgents
    {
        public List<IAgent> allAgents { get; private set; }

        public ManageAgents()
        {
            allAgents = new List<IAgent>();
            AddAllAgents();
        }

        public void RegisterAgent(IAgent agent)
        {
            allAgents.Add(agent);
        }

        private void AddAllAgents()
        {
            allAgents.Add(new MamasAgent());
            allAgents.Add(new BamasAgent());
            allAgents.Add(new LaLaLandAgent());
        }


    }
}
