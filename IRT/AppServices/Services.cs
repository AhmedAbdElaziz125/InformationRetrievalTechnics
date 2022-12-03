using IRT.Models;

namespace IRT_Project_ASPNetMVC.AppServices
{
    public interface IServices
    {
        public char[] delimiters();
        public String[] stopWords();
        //1
        public List<FileInfo> getDirectoryFiles(string path);
        public List<Term> getDirectoryTerms(List<FileInfo> files);
        public List<string> getFileTokens(FileInfo file);

    }
    public class Services : IServices
    {
        //
        public char[] delimiters()
        {
            return new char[] { ' ', ';', '\u002C', ';' };
        }
        public string[] stopWords()
        {
            String[] stopWords = { "{", "}", ">", "<", "~", "^", ":", ";",
                                   "(", ")", "-", "|", "/", "*", "$", "$",
                                   "%", "#", "@", "!", "+", ",", ".", ".",
                                   ".", "a", "as", "about", "after", "afterwards",
                                   "aint", "all", "already", "also", "although",
                                   "always", "am", "an", "and", "any", "are", "arent",
                                   "as", "at", "be", "because", "been", "before", "beforehand",
                                   "behind", "being", "below", "beside", "besides", "both", "but",
                                   "by", "did", "didnt", "do", "does", "doesnt", "doing", "dont",
                                   "done", "during", "each", "edu", "eg", "either", "else", "elsewhere",
                                   "from", "had", "hadnt", "has", "hasnt", "have", "havent", "having", "he",
                                   "hes", "her", "here", "heres", "hereafter", "hereby", "herein", "hereupon",
                                   "hers", "herself", "hi", "him", "himself", "his", "hither", "how", "if", "immediate",
                                   "inasmuch", "inc", "into", "inward", "is", "isnt", "it", "itd", "itll", "its", "its",
                                   "itself", "just", "last", "lately", "later", "latter", "latterly", "least", "little",
                                   "ltd", "mainly", "many", "maybe", "me", "meanwhile", "merely", "might", "more", "moreover",
                                   "much", "must", "my", "myself", "name", "namely", "nd", "near", "nearly", "necessary", "neither",
                                   "never", "nevertheless", "new", "next", "nine", "no", "nobody", "non", "none", "noone", "nor", "normally",
                                   "not", "nothing", "novel", "now", "nowhere", "obviously", "of", "off", "often", "oh", "ok", "okay", "old",
                                   "on", "once", "one", "ones", "only", "onto", "or", "other", "others", "otherwise", "ought", "our", "ours",
                                   "ourselves", "out", "outside", "over", "overall", "own", "particular", "particularly", "per", "perhaps", "placed",
                                   "please", "plus", "possible", "presumably", "probably", "que", "quite", "qv", "rather", "rd", "re", "really",
                                   "reasonably", "regardless", "regards", "relatively", "respectively", "right", "same", "second", "secondly", "self",
                                   "selves", "sensible", "serious", "seriously", "seven", "several", "she", "since", "so", "some", "somebody", "somehow",
                                   "someone", "something", "sometime", "sometimes", "somewhat", "somewhere", "soon", "sorry", "sub", "such", "sup", "sure",
                                   "ts", "th", "than", "that", "thats", "thats", "the", "their", "theirs", "them", "themselves", "thence", "there",
                                   "theres", "thereafter", "thereby", "therefore", "therein", "theres", "thereupon", "these", "they", "theyd", "theyll",
                                   "theyre", "theyve", "this", "thorough", "thoroughly", "those", "though", "three", "through", "throughout", "thru",
                                   "thus", "together", "too", "toward", "towards", "tries", "truly", "try", "trying", "twice", "two", "un", "under",
                                   "unfortunately", "unless", "unlikely", "until", "unto", "up", "upon", "us", "use", "used", "useful", "uses", "using",
                                   "usually", "value", "various", "very", "via", "viz", "vs", "way", "we", "wed", "well", "weve", "welcome", "well",
                                   "whatever", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether", "yes", "yet", "you", "youd",
                                   "youll", "youre", "youve", "your", "yours", "yourself", "yourselves" };
            return stopWords;
        }
        //1
        public List<FileInfo> getDirectoryFiles(string path)
        {
            List<FileInfo> files = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(path);
            try
            {
                // Determine whether the directory exists.
                if (dir.Exists)
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        files.Add(file);
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally { }
            return files;
        }
        //2
        public List<string> getFileTokens(FileInfo file)
        {
            List<string> fileTokens = new List<string>();
            var reader = file.OpenText();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                foreach (var word in line.Split(delimiters(), StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!stopWords().Contains(word))
                    {
                        fileTokens.Add(word.Trim());
                    }
                }
                fileTokens.Sort();
            }
            return fileTokens;
        }

        public List<Term> getDirectoryTerms(List<FileInfo> files)
        {
            List<Term> terms = new List<Term>();
            List<string> tokens = new List<string>();
            foreach (FileInfo file in files)
            {
                Document document = new Document();
                document.id = "DOC_" + file.Name;
                foreach(var token in getFileTokens(file))
                {
                    if (!tokens.Contains(token))
                    {
                        tokens.Add(token);
                        Term term = new Term();
                        term.id = token;
                        term.postingList.Add(document);
                        term.termFrequency++;
                        terms.Add(term);
                    }
                    else if (tokens.Contains(token))
                    {
                        foreach(var term in terms)
                        {
                            var termdocs = term.postingList;
                            if (term.id == token)
                            {
                                if(!termdocs.Contains(document))
                                {
                                    term.postingList.Add(document);
                                }
                                term.termFrequency++;
                            }
                        }
                    }
                    
                }
            }
            return terms;
        }
    }
}
