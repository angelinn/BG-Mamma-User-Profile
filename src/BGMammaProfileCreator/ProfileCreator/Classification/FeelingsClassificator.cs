using ProfileCreator.Classification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileCreator
{
    public class ClassificationAnswer
    {
        public Dictionary<string, float> Weights { get; set; } = new Dictionary<string, float>();
    }

    public class FeelingsClassificator
    {
        private Dictionary<string, List<Feeling>> feelings;
        private Dictionary<string, float> weights;

        public ClassificationAnswer Decide(IEnumerable<Frequency> words)
        {
            Initialize();

            foreach (Frequency word in words)
            {
                if (TryMatch(word.Term, out Feeling feeling))
                    RegisterMatch(feeling, word.Value);
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

        private void RegisterMatch(Feeling feeling, int termFrequency)
        {
            weights[feeling.Class] += feeling.Weight * termFrequency;
        }

        private ClassificationAnswer DetermineFinalAnswer()
        {
            //KeyValuePair<string, float> chosen = weights.FirstOrDefault(p => p.Value == weights.Values.Max());
            // if (chosen.Value == 0)
            // return null;

            return new ClassificationAnswer
            {
                //Class = chosen.Key,
                //Confidence = chosen.Value / weights.Values.Sum()
                Weights = weights.Where(p => p.Value > 0).ToDictionary(t => t.Key, t => t.Value)
            };
        }
    }
}
