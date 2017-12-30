using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileCreator
{
    public class TFIDFService
    {
        public void Test(IEnumerable<string> documents)
        {
            RAMDirectory dir = new RAMDirectory();
            StandardAnalyzer analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);
            IndexWriterConfig config = new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer);
            IndexWriter writer = new IndexWriter(dir, config);

            Document doc = new Document();
            doc.Add(new TextField("name", "celine dion", Field.Store.NO));
            writer.AddDocument(doc);

            Document docOther = new Document();
            doc.Add(new TextField("name", "notceline dion", Field.Store.NO));
            writer.AddDocument(docOther);

            writer.Commit();

            IndexSearcher searcher = new IndexSearcher(DirectoryReader.Open(dir));
            Term term = new Term("name", "dion");
            var k = searcher.IndexReader.GetSumDocFreq("name");
        }
    }
}
