using Accord.MachineLearning.Bayes;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Filters;
using Accord.Statistics.Kernels;
using ProfileCreator.Classification;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProfileCreator
{
    public class ClassificationAnswer
    {
        public string Class { get; set; }
        public float Confidence { get; set; }
    }

    public class FeelingsClassificator
    {
        private Dictionary<string, List<Feeling>> feelings;
        private Dictionary<string, float> weights;

        public ClassificationAnswer Decide(IEnumerable<string> words)
        {
            Initialize();

            foreach (string word in words)
            {
                if (TryMatch(word, out Feeling feeling))
                    RegisterMatch(feeling);
            }

            return DetermineFinalAnswer();
        }

        private bool TryMatch(string word, out Feeling feeling)
        {
            feeling = null;

            foreach (string potentialClass in feelings.Keys)
            {
                feeling = feelings[potentialClass].FirstOrDefault(f => f.Value.Contains(word));
                if (feeling != null)
                    return true;
            }

            return false;
        }

        private void Initialize()
        {
            if (feelings == null)
                feelings = FeelingsParser.Parse();

            weights = new Dictionary<string, float>();
            foreach (string probabilityClass in feelings.Keys)
                weights.Add(probabilityClass, 0);
        }

        private void RegisterMatch(Feeling feeling)
        {
            weights[feeling.Class] += feeling.Weight;
        }

        private ClassificationAnswer DetermineFinalAnswer()
        {
            KeyValuePair<string, float> chosen = weights.FirstOrDefault(p => p.Value == weights.Values.Max());
            if (chosen.Value == 0)
                return null;

            return new ClassificationAnswer
            {
                Class = chosen.Key,
                Confidence = chosen.Value / weights.Values.Sum()
            };
        }
    }
}
