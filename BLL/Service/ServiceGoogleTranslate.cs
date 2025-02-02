using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Service;

namespace BLL.Service
{
    public static class ServiceGoogleTranslate
    {
        private static readonly RepositoryGoogleTranslate _googleTranslate;

        static ServiceGoogleTranslate()
        {
            try
            {
                _googleTranslate = new RepositoryGoogleTranslate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка в статичному конструкторі: " + ex.Message);
                throw;
            }
        }

        public static string TranslateText(string text, string targetLanguage, string? sourceLanguage = null)
        {
            return sourceLanguage == null
        ? _googleTranslate.TranslateText(text, targetLanguage)
        : _googleTranslate.TranslateText(text, targetLanguage, sourceLanguage);
        }

        public static IEnumerable<string> GetLanguageToTranslate()
        {
            return _googleTranslate.GetListLanguage();
        }
    }
}
