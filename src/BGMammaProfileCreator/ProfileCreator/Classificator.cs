﻿using Accord.MachineLearning.Bayes;
using Accord.Math;
using Accord.Statistics.Distributions.Univariate;
using Accord.Statistics.Filters;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProfileCreator
{
    public class Classificator
    {
        public void Example()
        {
            string[] columnNames = { "Outlook", "Temperature", "Humidity", "Wind", "PlayTennis" };

            string[][] data =
            {
                new string[] { "Sunny", "Hot", "High", "Weak", "No" },
                new string[] { "Sunny", "Hot", "High", "Strong", "No" },
                new string[] { "Overcast", "Hot", "High", "Weak", "Yes" },
                new string[] { "Rain", "Mild", "High", "Weak", "Yes" },
                new string[] { "Rain", "Cool", "Normal", "Weak", "Yes" },
                new string[] { "Rain", "Cool", "Normal", "Strong", "No" },
                new string[] { "Overcast", "Cool", "Normal", "Strong", "Yes" },
                new string[] { "Sunny", "Mild", "High", "Weak", "No" },
                new string[] { "Sunny", "Cool", "Normal", "Weak", "Yes" },
                new string[] {  "Rain", "Mild", "Normal", "Weak", "Yes" },
                new string[] {  "Sunny", "Mild", "Normal", "Strong", "Yes" },
                new string[] {  "Overcast", "Mild", "High", "Strong", "Yes" },
                new string[] {  "Overcast", "Hot", "Normal", "Weak", "Yes" },
                new string[] {  "Rain", "Mild", "High", "Strong", "No" },
            };

            // Create a new codification codebook to
            // convert strings into discrete symbols
            Codification codebook = new Codification(columnNames, data);

            // Extract input and output pairs to train
            int[][] symbols = codebook.Transform(data);
            int[][] inputs = symbols.Get(null, 0, -1); // Gets all rows, from 0 to the last (but not the last)
            int[] outputs = symbols.GetColumn(-1);     // Gets only the last column

            // Create a new Naive Bayes learning
            var learner = new NaiveBayesLearning();

            NaiveBayes nb = learner.Learn(inputs, outputs);

            // Consider we would like to know whether one should play tennis at a
            // sunny, cool, humid and windy day. Let us first encode this instance
            int[] instance = codebook.Transform("Sunny", "Cool", "High", "Strong");

            // Let us obtain the numeric output that represents the answer
            int c = nb.Decide(instance); // answer will be 0

            // Now let us convert the numeric output to an actual "Yes" or "No" answer
            string result = codebook.Revert("PlayTennis", c); // answer will be "No"

            // We can also extract the probabilities for each possible answer
            double[] probs = nb.Probabilities(instance); // { 0.795, 0.205 }
        }
    }
}