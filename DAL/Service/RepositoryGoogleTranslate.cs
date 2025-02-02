using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Cloud.Translation.V2;
namespace DAL.Service
{
    public class RepositoryGoogleTranslate
    {
        private readonly TranslationClient _translation = TranslationClient.Create();
        public string TranslateText(string text,string targetLanguage,string? sourceLanguage = null)
        {
            if (!string.IsNullOrEmpty(text))
            {
                var response = sourceLanguage == null
           ? _translation.TranslateText(text, targetLanguage)  // Автовизначення вхідної мови
           : _translation.TranslateText(text, targetLanguage, sourceLanguage);  // Явне зазначення вхідної мови

                return response.TranslatedText;
            }
            else
                throw new Exception("Text is null or empty");
        }

        public IEnumerable<string> GetListLanguage()
        {
            return (IEnumerable<string>)_translation.ListLanguages();
        }
    }
}
