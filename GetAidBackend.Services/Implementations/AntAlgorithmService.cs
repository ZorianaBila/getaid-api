using AntColony.Algorithm;
using AntColony.Core;
using AntColony.Core.Graphs;

namespace GetAidBackend.Services.Implementations
{
    public class AntAlgorithmService
    {
        public Result GetOptimalRoute()
        {
            var random = new Random();
            var config = new Config();
            var graph = new Graph(3, new int[,] { });

            IAlgorithm antAlgorithm = new AntColonyAlgorithm(graph, config, random);

            return antAlgorithm.Solve();
        }
    }
}