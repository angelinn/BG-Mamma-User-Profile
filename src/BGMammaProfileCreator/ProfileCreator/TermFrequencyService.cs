using Lucene.Net.Analysis.Bg;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
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
        public string ProfileUrl { get; set; }
        public List<Frequency> Frequencies = new List<Frequency>();
    }


    public class Frequency
    {
        public string Term { get; set; }
        public int Value { get; set; }

        public Frequency(string text, int frequency)
        {
            Term = text;
            Value = frequency;
        }
    }

    public class TermFrequencyService
    {
        private Directory currentDirectory;

        public void CreateIndex(IEnumerable<ProcessedUser> users)
        {
            if (System.IO.Directory.Exists("Lucene"))
                return;

            System.IO.Directory.CreateDirectory("Lucene");
            currentDirectory = new NIOFSDirectory("Lucene\\Directory");

            BulgarianAnalyzer analyzer = new BulgarianAnalyzer(LuceneVersion.LUCENE_48);
            IndexWriterConfig config = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            IndexWriter writer = new IndexWriter(currentDirectory, config);

            FieldType contentsType = new FieldType
            {
                IsIndexed = true,
                IsStored = true,
                StoreTermVectors = true,
            };

            foreach (ProcessedUser user in users)
            {
                Document doc = new Document();

                doc.Add(new Field("user", user.Username, contentsType));
                doc.Add(new Field("profile_url", user.ProfileUrl, contentsType));
                doc.Add(new Field("contents", String.Join("\n-------\n", user.Comments.Select(c => c.Content)), contentsType));

                writer.AddDocument(doc);
            }
            writer.Commit();
        }

        public IEnumerable<DocumentFrequency> GetTermFrequencies()
        {
            if (currentDirectory == null)
                currentDirectory = new NIOFSDirectory("Lucene\\Directory");

            IndexSearcher searcher = new IndexSearcher(DirectoryReader.Open(currentDirectory));

            for (int i = 0; i < searcher.IndexReader.NumDocs; ++i)
            {
                yield return GetTermFrequencyForSingleDocument(searcher, i);
            }
        }

        public IEnumerable<DocumentFrequency> Search(string query, string field, int topHits = 20)
        {
            if (currentDirectory == null)
                currentDirectory = new NIOFSDirectory(@"D:\Repositories\BG-Mamma-User-Profile\src\BGMammaProfileCreator\ConsoleTest\bin\Debug\Lucene\Directory");

            IndexSearcher searcher = new IndexSearcher(DirectoryReader.Open(currentDirectory));
            QueryParser parser = new QueryParser(LuceneVersion.LUCENE_48, field, new BulgarianAnalyzer(LuceneVersion.LUCENE_48));
            Query q = parser.Parse(query);

            TopDocs docs = searcher.Search(q, topHits);
            foreach (ScoreDoc doc in docs.ScoreDocs)
            {
                Document docu = searcher.Doc(doc.Doc);
                DocumentFrequency freq = GetTermFrequencyForSingleDocument(searcher, doc.Doc);
                freq.User = docu.Get("user");
                freq.ProfileUrl = docu.Get("profile_url");

                yield return freq;
            }
        }

        private DocumentFrequency GetTermFrequencyForSingleDocument(IndexSearcher searcher, int documentID)
        {
            DocumentFrequency documentFrequency = new DocumentFrequency();

            Fields fields = searcher.IndexReader.GetTermVectors(documentID);
            if (fields == null)
                return null;

            foreach (string field in fields)
            {
                Terms terms = fields.GetTerms(field);
                TermsEnum iterator = terms.GetIterator(null);

                while (iterator.Next() != null)
                {
                    Term term = new Term(field, iterator.Term);

                    int frequency = searcher.IndexReader.DocFreq(term);
                    string termText = term.Text();

                    if (field == "contents")
                        documentFrequency.Frequencies.Add(new Frequency(termText, frequency));
                    if (field == "user")
                        documentFrequency.User = termText;
                    if (field == "profile_url")
                        documentFrequency.ProfileUrl = termText;
                }
            }

            return documentFrequency;
        }
    }
}
