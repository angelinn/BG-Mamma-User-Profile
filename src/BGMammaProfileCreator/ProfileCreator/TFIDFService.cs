using Lucene.Net.Analysis.Bg;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Search.Similarities;
using Lucene.Net.Store;
using Lucene.Net.Util;
using ProfileCreator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileCreator
{
    public class DocumentFrequency
    {
        public string User { get; set; }
        public Dictionary<string, int> Frequencies = new Dictionary<string, int>();
    }

    public class TFIDFService
    {
        private Directory currentDirectory;

        public void CreateIndex(IEnumerable<User> users)
        {
            currentDirectory = new RAMDirectory();
            BulgarianAnalyzer analyzer = new BulgarianAnalyzer(LuceneVersion.LUCENE_48);
            IndexWriterConfig config = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            IndexWriter writer = new IndexWriter(currentDirectory, config);

            foreach (User user in users)
            {
                Document doc = new Document();
                doc.AddStringField("user", user.Username, Field.Store.YES);
                doc.AddStringField("profile_url", user.ProfileUrl, Field.Store.YES);
                doc.AddTextField("contents", String.Join("\n-------\n", user.Comments.Select(c => c.Content)), Field.Store.NO);
                writer.AddDocument(doc);
            }
            writer.Commit();
        }

        public IEnumerable<DocumentFrequency> GetMostUsed(int most = 10)
        {
            IndexSearcher searcher = new IndexSearcher(DirectoryReader.Open(currentDirectory));
            DocumentFrequency documentFrequency = null;

            for (int i = 0; i < searcher.IndexReader.NumDocs; ++i)
            {
                documentFrequency = new DocumentFrequency();

                Fields fields = searcher.IndexReader.GetTermVectors(i);
                foreach (string field in fields)
                {
                    Terms terms = fields.GetTerms(field);
                    TermsEnum iterator = terms.GetIterator(null);

                    while (iterator.Next() != null)
                    {
                        Term term = new Term(field, iterator.Term);

                        int frequency = searcher.IndexReader.DocFreq(term);
                        string termText = term.Text();

                        documentFrequency.Frequencies.Add(termText, frequency);
                    }
                }

                yield return documentFrequency;
            }
        }
    }
}
